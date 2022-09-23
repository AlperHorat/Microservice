using ContactService.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Api.EntityConfiguration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(200);
            builder.Property(c => c.Surname).HasMaxLength(200);
            builder.Property(c => c.Firm).HasMaxLength(200);
        }
    }
}
