using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using ReportService.Api.Domain.Entities;
using ReportService.Api.Domain.Models;
using ReportService.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        private static IConnection connection;
        private readonly static string CreateReport = "create_report_queue";
        private readonly static string ReportCreated = "report_created_queue";
        private readonly static string ReportCreatedExChange = "report_create_exchange";
        static IModel _channel;
        static IModel channel => _channel ?? (_channel = GetChannel());


        public ReportController(IReportService reportService)
        {
            this._reportService = reportService;

            if (connection == null)
                connection = GetConnection();
            else if (!connection.IsOpen)
                connection = GetConnection();

            channel.ExchangeDeclare(ReportCreatedExChange, "direct");

            channel.QueueDeclare(CreateReport, false, false, false);
            channel.QueueBind(CreateReport, ReportCreatedExChange, CreateReport);

            channel.QueueDeclare(ReportCreated, false, false, false);
            channel.QueueBind(ReportCreated, ReportCreatedExChange, ReportCreated);
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

        private static IModel GetChannel()
        {
            return connection.CreateModel();
        }
        private static void WriteToQueue(string queuename, Report model)
        {
            var modelArr = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            channel.BasicPublish(ReportCreatedExChange, queuename, null, modelArr);
        }
        [HttpPost("addreportrequest")]
        public async Task<ActionResult> AddReportRequest()
        {
            Report entity = new Report();
            entity.Id = Guid.NewGuid();
            entity.ReportDate = DateTime.Now;
            entity.ReportStatus = null; //ilk talep olduğu için değer atanmadı

            var result = _reportService.AddReportRequest(entity);
            if (result)
            {
                WriteToQueue(CreateReport, entity);
                return await Task.FromResult(Ok("Rapor talebi başarılı bir şekilde eklendi. Eklenen Id= " + entity.Id));
            }
            else
                return await Task.FromResult(BadRequest("Rapor talebi eklenirken bir hata oluştu."));
            //await Task.Delay(2000);
        }
        [HttpPost("updaterequest")]
        public ActionResult UpdateReportRequest([FromBody] ReportRequestModel requestentity)
        {
            var entity = _reportService.GetReportById(requestentity.Id);
            if(entity == null)
            {
                return BadRequest("Güncellenmek istenilen rapor kaydı sistemde mevcut değil.");
            }
            entity.ReportStatus = requestentity.ReportStatus;
            entity.ReportUrl = requestentity.ReportUrl;

            var result = _reportService.UpdateReportRequest(entity);
            if (result)
                return Ok("Rapor talebi başarılı bir şekilde güncellendi. Id= " + entity.Id);
            else
                return BadRequest("Rapor talebi tamamlanırken bir hata oluştu.");
        }
        [HttpGet("getreportlist")]
        public List<ReportModel> GetReports()
        {
            return _reportService.GetReports();
        }
        [HttpGet("getreportbyid")]
        public Report GetReportById(Guid id)
        {
            return _reportService.GetReportById(id);
        }
    }
}
