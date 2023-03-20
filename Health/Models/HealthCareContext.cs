using System;
using Microsoft.EntityFrameworkCore;

namespace Health.Models
{
    public class HealthCareContext : DbContext
    {
        public HealthCareContext(DbContextOptions<HealthCareContext> options)
            : base(options)
        {
        }

        public DbSet<HealthRecord> HealthRecords { get; set; } = null!;
    }
}

