using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;

namespace BarberBoss.Infrastructure.DataAccess.Repositories
{
    internal class ServiceRepository : IServicesRepository
    {
        public void Add(Service service)
        {
            var dbContext = new BarberBossDbContext();

            dbContext.Services.Add(service);

            dbContext.SaveChanges();
        }
    }
}
