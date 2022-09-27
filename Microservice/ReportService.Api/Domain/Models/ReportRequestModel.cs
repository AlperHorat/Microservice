using ReportService.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Api.Domain.Models
{
    public class ReportRequestModel
    {
        public Guid Id { get; set; }
        public DateTime ReportDate { get; set; }
        public ReportType? ReportStatus { get; set; }
        public string ReportUrl { get; set; }
    }
}
