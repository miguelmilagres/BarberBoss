using BarberBoss.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess
{
    internal class BarberBossDbContext : DbContext
    {
        public DbSet<Service> Services { get; set; }

        public BarberBossDbContext(DbContextOptions options) : base(options) { }
    }
}
