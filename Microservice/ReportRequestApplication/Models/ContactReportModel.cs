using System;
using System.Collections.Generic;
using System.Text;

namespace ReportRequestApplication.Models
{
    public class ContactReportModel
    {
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
