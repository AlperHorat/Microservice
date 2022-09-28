using ReportService.Api.Domain.Entities;
using ReportService.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Api.Services
{
    public interface IReportService
    {
        bool AddReportRequest(Report entity);
        bool UpdateReportRequest(Report entity);
        Report GetReportById(Guid id);
        List<ReportModel> GetReports();
    }
}
