using ContactService.Api.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Api.Domain.Entities
{
    public class PersonContactInfo : BaseEntity
    {
        public Guid PersonId { get; set; }
        public ContactType Contacttype { get; set; }
        public string Info { get; set; }
    }

    public enum ContactType
    {
        Phone = 0,
        EMail = 1,
        Location = 2
    }
}
