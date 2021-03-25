using EmployeeHomework.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EmployeeHomework
{
    public class EmployeeDatabaseContext : DbContext
    {
        public DbSet<EmployeeEntity> EmployeeTable { get; set; }

        public EmployeeDatabaseContext()
        {
        }

        public EmployeeDatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }
        }
    }
}