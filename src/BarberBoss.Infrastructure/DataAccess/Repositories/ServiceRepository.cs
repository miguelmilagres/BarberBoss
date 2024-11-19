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

        public async Task<List<Service>> FilterByMonth(DateOnly date)
        {
            var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;
            var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
            var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);

            return await _dbContext
                .Services
                .AsNoTracking()
                .Where(service => service.Date >= startDate && service.Date <= endDate)
                .OrderBy(service => service.Date)
                .ThenBy(service => service.Title)
                .ToListAsync();
        }
    }
}
