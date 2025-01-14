using Microsoft.EntityFrameworkCore;
using MyInsurance.Models.Domain;

namespace MyInsurance.Data
{
    public class InsuranceDbContext : DbContext
    {
        public InsuranceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Insurance> Insurances { get; set; }
    }
}
