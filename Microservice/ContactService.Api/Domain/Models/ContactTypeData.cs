using ContactService.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Api.Domain.Models
{
    public class ContactTypeData
    {
        public ContactType Id { get; set; }
        public string Description { get; set; }

        public static List<ContactTypeData> ContactTypeList = new List<ContactTypeData>();

        static ContactTypeData()
        {
            ContactTypeList.Add(new ContactTypeData { Id = ContactType.Phone, Description="Phone" });
            ContactTypeList.Add(new ContactTypeData { Id = ContactType.EMail, Description = "EMail" });
            ContactTypeList.Add(new ContactTypeData { Id = ContactType.Location, Description = "Location" });
        }
    }
}
