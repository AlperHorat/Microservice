using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Api.Domain.Abstracts
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
