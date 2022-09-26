using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Api.Domain.Models
{
    public class ReportModel
    {
        public Guid Id { get; set; }
        public DateTime ReportDate { get; set; }
        public string ReportUrl { get; set; }
        public string ReportStatusDesc { get; set; }
    }
}
