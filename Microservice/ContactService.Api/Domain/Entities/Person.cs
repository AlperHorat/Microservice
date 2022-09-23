using ContactService.Api.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Api.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Firm { get; set; }
    }
}
