using EksperciOnline.Data;
using EksperciOnline.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EksperciOnline.Repositiories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly EksperciOnlineDbContext eksperciOnlineDbContext;

        public ServiceRepository(EksperciOnlineDbContext eksperciOnlineDbContext)
        {
            this.eksperciOnlineDbContext = eksperciOnlineDbContext;
        }

        public async Task<Usługa> AddAsync(Usługa usługa)
        {
            await eksperciOnlineDbContext.AddAsync(usługa);
            await eksperciOnlineDbContext.SaveChangesAsync();
            return usługa;
        }

        public Task<Usługa?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Usługa>> GetAllAsync()
        {
            return await eksperciOnlineDbContext.Usługi.Include(x => x.Kategoria).ToListAsync();
        }

        public Task<Usługa?> GetAsnyc(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Usługa?> UpdateAsync(Usługa usługa)
        {
            throw new NotImplementedException();
        }
    }
}
