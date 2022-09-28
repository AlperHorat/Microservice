using System;
using System.Collections.Generic;
using System.Text;

namespace ReportRequestApplication.Models
{
    public class ReportRequestModel
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
