using EksperciOnline.Data;
using EksperciOnline.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EksperciOnline.Repositiories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EksperciOnlineDbContext eksperciOnlineDbContext;

        public CategoryRepository(EksperciOnlineDbContext eksperciOnlineDbContext)
        {
            this.eksperciOnlineDbContext = eksperciOnlineDbContext;
        }
        public async Task<Kategoria> AddAsync(Kategoria kategoria)
        {
            await eksperciOnlineDbContext.Kategorie.AddAsync(kategoria);
            await eksperciOnlineDbContext.SaveChangesAsync();
            return kategoria;
        }

        public async Task<Kategoria?> DeleteAsync(Guid id)
        {
            var existingCategory = await eksperciOnlineDbContext.Kategorie.FindAsync(id);

            if (existingCategory != null) 
            { 
                eksperciOnlineDbContext.Kategorie.Remove(existingCategory);
                await eksperciOnlineDbContext.SaveChangesAsync();
                return existingCategory;
            }

            return null;
        }

        public async Task<IEnumerable<Kategoria>> GetAllAsync()
        {
            return await eksperciOnlineDbContext.Kategorie.ToListAsync();
        }

        public Task<Kategoria?> GetAsync(Guid id)
        {
            return eksperciOnlineDbContext.Kategorie.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Kategoria?> UpdateAsync(Kategoria kategoria)
        {
            var existingCategory = await eksperciOnlineDbContext.Kategorie.FindAsync(kategoria.Id);

            if (existingCategory != null)
            {
                existingCategory.NazwaKategorii = kategoria.NazwaKategorii;
                existingCategory.UrlZdjęcia = kategoria.UrlZdjęcia;

                await eksperciOnlineDbContext.SaveChangesAsync();

                return existingCategory;
            }

            return null;
        }
    }
}
