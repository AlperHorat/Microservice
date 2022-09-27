using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportRequestApplication.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

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


            //doküman oluşturma talebi
            //CreateReportRequest();
            //Console.WriteLine("Doküman oluşturma talebi gönderildi!");
            //Console.ReadKey();

            //doküman talebi okuma ve oluşturup geri dönüş
            GetReportRequestAndGetData();
            //Console.ReadKey();

            //doküman sonucunu alma
            GetDataAndCreateReport();
            Console.ReadKey();
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




                Console.WriteLine($"Received Data : {responseInfo.Content.ReadAsStringAsync().Result }");

                model.ReportUrl = "Testurl";

                WriteToQueue(ReportCreated, model);
            };

            channel.BasicConsume(CreateReport, true, consumerEvent);
        }
        static void GetDataAndCreateReport()
        {
            var consumerEvent2 = new EventingBasicConsumer(channel);

            consumerEvent2.Received += (ch, ea) =>
            {
                Console.WriteLine("Datalar alındı ve doküman oluşturuldu!");
                var modeljson = Encoding.UTF8.GetString(ea.Body.ToArray());
                var model = JsonConvert.DeserializeObject<ReportRequestModel>(modeljson);

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
