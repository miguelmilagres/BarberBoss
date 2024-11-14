using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess.Repositories
{
    internal class ServiceRepository : IServicesReadOnlyRepository, IServicesWriteOnlyRepository, IServicesUpdateOnlyRepository
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

        public async Task<bool> Delete(long id)
        {
            var result = await _dbContext.Services.FirstOrDefaultAsync(s => s.Id == id);

            if (result is null)
                return false;

            _dbContext.Services.Remove(result);
            return true;
        }

        public async Task<List<Service>> GetAll()
        {
            return await _dbContext.Services.AsNoTracking().ToListAsync();
        }

        async Task<Service?> IServicesReadOnlyRepository.GetById(long id)
        {
            return await _dbContext.Services.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        
        async Task<Service?> IServicesUpdateOnlyRepository.GetById(long id)
        {
            return await _dbContext.Services.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Service service)
        {
            _dbContext.Services.Update(service);
        }
    }
}
