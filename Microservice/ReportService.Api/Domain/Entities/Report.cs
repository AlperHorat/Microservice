using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Api.Domain.Entities
{
    public class Report
    {
        public Guid Id { get; set; }
        public DateTime ReportDate { get; set; }
        public ReportType? ReportStatus { get; set; }
        public string ReportUrl { get; set; }
    }
    public enum ReportType
    {
        Hazırlanıyor = 0,
        Tamamlandı = 1,
    }
}
