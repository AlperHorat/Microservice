using ClosedXML.Excel;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportRequestApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace ReportRequestApplication
{
    class Program
    {
        private static IConnection connection;
        private readonly static string CreateReport = "create_report_queue";
        private readonly static string ReportCreated = "report_created_queue";
        private readonly static string ReportCreatedExChange = "report_create_exchange";
        static IModel _channel;
        static IModel channel => _channel ?? (_channel = GetChannel());
        static void Main(string[] args)
        {
            if (connection == null)
                connection = GetConnection();
            else if (!connection.IsOpen)
                connection = GetConnection();

            channel.ExchangeDeclare(ReportCreatedExChange, "direct");

            channel.QueueDeclare(CreateReport, false, false, false);
            channel.QueueBind(CreateReport, ReportCreatedExChange, CreateReport);

            channel.QueueDeclare(ReportCreated, false, false, false);
            channel.QueueBind(ReportCreated, ReportCreatedExChange, ReportCreated);


            //doküman talebi okuma ve oluşturup geri dönüş
            GetReportRequestAndGetData();
            //Console.ReadKey();

            //doküman sonucunu alma ve tamamlandı durumuna getirme
            GetDataAndCompleteReport();
            Console.ReadKey();
        }
        private static string ExportExcel<T>(List<T> exportData, string id)
        {
            XLWorkbook workbook = new XLWorkbook();
            DataTable table = new DataTable() { TableName = "New Worksheet" };
            DataSet ds = new DataSet();

            List<string> _headers = new List<string>();
            List<string> _type = new List<string>();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

            foreach (PropertyDescriptor prop in properties)
            {
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                _type.Add(type.Name);
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ??
                                  prop.PropertyType);
                string name = Regex.Replace(prop.Name, "([A-Z])", " $1").Trim(); //space separated 
                                                                                 //name by caps for header
                _headers.Add(name);
            }

            foreach (T item in exportData)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }

            //Convert datatable to dataset and add it to the workbook as worksheet
            ds.Tables.Add(table);
            workbook.Worksheets.Add(ds);

            //save
            //string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string desktopPath = Environment.CurrentDirectory + "\\ReportExcel";
            string savePath = Path.Combine(desktopPath, "rapor" + id + ".xlsx");
            workbook.SaveAs(savePath, false);

            return savePath;
        }
        private static void WriteToQueue(string queuename, ReportRequestModel model)
        {
            var modelArr = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            channel.BasicPublish(ReportCreatedExChange, queuename, null, modelArr);
        }
        private static IModel GetChannel()
        {
            return connection.CreateModel();
        }
        static void CreateReportRequest()
        {
            var model = new ReportRequestModel()
            {
                Id = Guid.NewGuid(),
                ReportDate = DateTime.Now,
                ReportStatus = null,
                ReportUrl = null
            };

            WriteToQueue(CreateReport, model);
        }
        static void GetReportRequestAndGetData()
        {
            var consumerEvent = new EventingBasicConsumer(channel);

            consumerEvent.Received += (ch, ea) =>
            {
                Console.WriteLine("Rapor Talebi alındı ve Doküman dataları contact servisten alındı!");
                var modeljson = Encoding.UTF8.GetString(ea.Body.ToArray());
                var model = JsonConvert.DeserializeObject<ReportRequestModel>(modeljson);

                //contactservice veri çekme alanı

                HttpClientHandler handler = new HttpClientHandler();
                HttpClient client = new HttpClient(handler);

                //var json = JsonConvert.SerializeObject(model);
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://localhost:44308/api/contact/GetContactReport/"),

                    Content = new StringContent(modeljson, Encoding.UTF8, "application/json")
                };
                var response = client.SendAsync(request).ConfigureAwait(false);

                var responseInfo = response.GetAwaiter().GetResult();
                List<ContactReportModel> serviceresult = JsonConvert.DeserializeObject<List<ContactReportModel>>(responseInfo.Content.ReadAsStringAsync().Result);




                //Console.WriteLine($"Received Data : {responseInfo.Content.ReadAsStringAsync().Result }");
                
                model.ReportUrl = ExportExcel<ContactReportModel>(serviceresult, model.Id.ToString());

                Console.WriteLine("Alınan datalar ile excel raporu oluşturularak excel url bilgisi gönderildi.!");
                WriteToQueue(ReportCreated, model);
            };

            channel.BasicConsume(CreateReport, true, consumerEvent);
        }
        static void GetDataAndCompleteReport()
        {
            var consumerEvent2 = new EventingBasicConsumer(channel);

            consumerEvent2.Received += (ch, ea) =>
            {
                var modeljson = Encoding.UTF8.GetString(ea.Body.ToArray());
                var model = JsonConvert.DeserializeObject<ReportRequestModel>(modeljson);
                model.ReportStatus = ReportType.Tamamlandı;
                //gelen veri raporun url si ve durumu tamamlandı olarak report tablosunda güncelleniyor.
                HttpClientHandler handler = new HttpClientHandler();
                HttpClient client = new HttpClient(handler);

                var json = JsonConvert.SerializeObject(model);
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:44326/api/report/updaterequest/"),

                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
                var response = client.SendAsync(request).ConfigureAwait(false);

                var responseInfo = response.GetAwaiter().GetResult();
                if (responseInfo.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("Rapor url bilgisi alındı ve rapor tablosundaki veriler güncellendi!");
                }
                //Console.WriteLine($"Received Data : { modeljson}");
            };

            channel.BasicConsume(ReportCreated, true, consumerEvent2);

        }
        private static IConnection GetConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            return connectionFactory.CreateConnection();
        }
    }
}
