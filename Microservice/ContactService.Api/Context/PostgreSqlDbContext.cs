using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Api.Context
{
    public class PostgreSqlDbContext : DbContext
    {
        public PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(PostgreSqlDbContext).Assembly);
        }
    }
}
