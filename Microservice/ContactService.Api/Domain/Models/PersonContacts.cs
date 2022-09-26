using ContactService.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Api.Domain.Models
{
    public class PersonContacts
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Firm { get; set; }
        public List<PersonContactInfoModel> PersonContactList { get; set; }
    }

    public class PersonContactInfoModel
    {
        public ContactType Contacttype { get; set; }
        public string Contacttypedesc { get; set; }
        public string Info { get; set; }
    }
}
