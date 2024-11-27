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

        public async Task<int> CountAsync()
        {
            return await eksperciOnlineDbContext.Kategorie.CountAsync();
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

        public async Task<IEnumerable<Kategoria>> GetAllAsync(string? searchQuery, string? sortBy, string? sortDirection, int pageNumber = 1, int pageSize = 100)
        {
            var query = eksperciOnlineDbContext.Kategorie.AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(searchQuery) == false) 
            { 
                query = query.Where(x => x.NazwaKategorii.Contains(searchQuery));
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

                if (string.Equals(sortBy, "NazwaKategorii", StringComparison.OrdinalIgnoreCase)) 
                {
                    query = isDesc ? query.OrderByDescending(x => x.NazwaKategorii) : query.OrderBy(x => x.NazwaKategorii);
                }
                
            }

            // Pagination
            // Skip 0 Take 5 -> Page 1 of 5 results
            // Skip 5 Take next 5 -> Page 1 of 5 results
            var skipResults = (pageNumber - 1) * pageSize;
            query = query.Skip(skipResults).Take(pageSize);

            return await query.ToListAsync();

            // return await eksperciOnlineDbContext.Kategorie.ToListAsync();
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
