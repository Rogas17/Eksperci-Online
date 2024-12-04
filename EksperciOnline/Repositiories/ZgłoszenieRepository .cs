using EksperciOnline.Data;
using EksperciOnline.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EksperciOnline.Repositiories
{
    public class ZgłoszenieRepository : IZgłoszenieRepository
    {
        private readonly EksperciOnlineDbContext eksperciOnlineDbContext;

        public ZgłoszenieRepository(EksperciOnlineDbContext eksperciOnlineDbContext)
        {
            this.eksperciOnlineDbContext = eksperciOnlineDbContext;
        }

        public async Task<IEnumerable<Zgłoszenie>> GetAllAsync()
        {
            return await eksperciOnlineDbContext.Zgłoszenia.Include(z => z.Usługa).ToListAsync();
        }

        public async Task<Zgłoszenie> GetAsync(Guid id)
        {
            return await eksperciOnlineDbContext.Zgłoszenia.Include(z => z.Usługa).FirstOrDefaultAsync(z => z.Id == id);
        }

        public async Task AddAsync(Zgłoszenie zgłoszenie)
        {
            await eksperciOnlineDbContext.Zgłoszenia.AddAsync(zgłoszenie);
            await eksperciOnlineDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Zgłoszenie zgłoszenie)
        {
            eksperciOnlineDbContext.Zgłoszenia.Update(zgłoszenie);
            await eksperciOnlineDbContext.SaveChangesAsync();
        }
    }
}
