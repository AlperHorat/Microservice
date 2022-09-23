using ContactService.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Api.EntityConfiguration
{
    public class PersonContactInfoConfiguration : IEntityTypeConfiguration<PersonContactInfo>
    {
        public void Configure(EntityTypeBuilder<PersonContactInfo> builder)
        {
            builder.Property(c => c.Contacttype).HasMaxLength(1);
            builder.Property(c => c.Info).HasMaxLength(100);
        }
    }
}
