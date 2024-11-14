using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;

namespace BarberBoss.Infrastructure.DataAccess.Repositories
{
    internal class ServiceRepository : IServicesRepository
    {
        private readonly BarberBossDbContext _dbContext;

        public ServiceRepository(BarberBossDbContext dbContext)
        {
            _dbContext = dbContext;    
        }
        public void Add(Service service)
        {
            _dbContext.Services.Add(service);

            _dbContext.SaveChanges();
        }
    }
}
