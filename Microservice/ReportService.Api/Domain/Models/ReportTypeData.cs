using ReportService.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Api.Domain.Models
{
    public class ReportTypeData
    {
        public ReportType Id { get; set; }
        public string Description { get; set; }

        public static List<ReportTypeData> ReportTypeDataList = new List<ReportTypeData>();

        static ReportTypeData()
        {
            ReportTypeDataList.Add(new ReportTypeData { Id = ReportType.Hazırlanıyor, Description = "Hazırlanıyor" });
            ReportTypeDataList.Add(new ReportTypeData { Id = ReportType.Tamamlandı, Description = "Tamamlandı" });
        }
    }
}
