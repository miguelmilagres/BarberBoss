using BarberBoss.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess
{
    internal class BarberBossDbContext : DbContext
    {
        public DbSet<Service> Services { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost;Database=barberbossdb;Uid=root;Pwd=@Password123;";
            
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 39));

            optionsBuilder.UseMySql(connectionString, serverVersion);
        }
    }
}
