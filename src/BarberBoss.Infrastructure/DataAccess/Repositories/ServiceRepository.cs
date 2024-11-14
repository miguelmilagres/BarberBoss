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
        public async Task Add(Service service)
        {
            await _dbContext.Services.AddAsync(service);
        }
    }
}
