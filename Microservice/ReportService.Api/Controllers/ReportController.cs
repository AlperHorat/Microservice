using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportService.Api.Domain.Entities;
using ReportService.Api.Domain.Models;
using ReportService.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            this._reportService = reportService;
        }
        [HttpPost("addreportrequest")]
        public ActionResult AddReportRequest()
        {
            Report entity = new Report();
            entity.Id = Guid.NewGuid();
            entity.ReportDate = DateTime.Now;
            entity.ReportStatus = ReportType.Hazırlanıyor;

            var result = _reportService.AddReportRequest(entity);
            if (result)
                return Ok("Rapor talebi başarılı bir şekilde eklendi. Eklenen Id= " + entity.Id);
            else
                return BadRequest("Rapor talebi eklenirken bir hata oluştu.");
        }
        [HttpPost("completereportrequest")]
        public ActionResult CompleteReportRequest(Guid id, string url)
        {
            var entity = _reportService.GetReportById(id);
            entity.ReportStatus = ReportType.Tamamlandı;
            entity.ReportUrl = url;

            var result = _reportService.UpdateReportRequest(entity);
            if (result)
                return Ok("Rapor talebi başarılı bir şekilde tamamlandı. Id= " + entity.Id);
            else
                return BadRequest("Rapor talebi tamamlanırken bir hata oluştu.");
        }
        [HttpGet("getreportlist")]
        public List<ReportModel> GetPersons()
        {
            return _reportService.GetReports();
        }
        [HttpGet("getreportbyid")]
        public Report GetPersons(Guid id)
        {
            return _reportService.GetReportById(id);
        }
    }
}
