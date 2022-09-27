using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Api.Domain.Models
{
    public class ContactReportModel
    {
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
