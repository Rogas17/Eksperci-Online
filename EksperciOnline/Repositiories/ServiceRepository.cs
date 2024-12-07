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

        public async Task<int> CountAsync()
        {
            return await eksperciOnlineDbContext.Usługi.CountAsync();
        }

        public async Task<Usługa?> DeleteAsync(Guid id)
        {
            var existingService = await eksperciOnlineDbContext.Usługi.FindAsync(id);

            if (existingService != null)
            {
                eksperciOnlineDbContext.Usługi.Remove(existingService);
                await eksperciOnlineDbContext.SaveChangesAsync();
                return existingService;
            }

            return null;
        }

        public async Task<IEnumerable<Usługa>> GetAllAsync(string? searchQuery, string? searchLocalQuery, string? sortBy, string? sortDirection, int pageNumber = 1, int pageSize = 100)
        {
            var query = eksperciOnlineDbContext.Usługi.AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                query = query.Where(x => x.Tytuł.Contains(searchQuery));
            }

            if (string.IsNullOrWhiteSpace(searchLocalQuery) == false)
            {
                query = query.Where(x => x.Lokalizacja.Contains(searchLocalQuery));
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

                if (string.Equals(sortBy, "Tytuł", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(x => x.Tytuł) : query.OrderBy(x => x.Tytuł);
                }
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

                if (string.Equals(sortBy, "DataPublikacji", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(x => x.DataPulikacji) : query.OrderBy(x => x.DataPulikacji);
                }
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

                if (string.Equals(sortBy, "Cena", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(x => x.CenaOd) : query.OrderBy(x => x.CenaOd);
                }
            }

            // Pagination
            // Skip 0 Take 5 -> Page 1 of 5 results
            // Skip 5 Take next 5 -> Page 1 of 5 results
            var skipResults = (pageNumber - 1) * pageSize;
            query = query.Skip(skipResults).Take(pageSize);


            return await query.Include(x=>x.Kategoria).ToListAsync();

            // return await eksperciOnlineDbContext.Usługi.Include(x => x.Kategoria).ToListAsync();
        }

        public async Task<Usługa?> GetAsync(Guid id)
        {
            return await eksperciOnlineDbContext.Usługi.Include(x => x.Kategoria).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ServiceComment>> GetCommentsByServiceIdAsync(Guid serviceId)
        {
            return await eksperciOnlineDbContext.ServiceComment
                .Where(c => c.ServiceId == serviceId)
                .ToListAsync();
        }

        public async Task<int> GetTotalComments(Guid serviceId)
        {
            return await eksperciOnlineDbContext.ServiceComment
                .CountAsync(c => c.ServiceId == serviceId);
        }

        public async Task<Usługa?> UpdateAsync(Usługa usługa)
        {
            var existingCategory = await eksperciOnlineDbContext.Usługi.Include(x => x.Kategoria).FirstOrDefaultAsync(x => x.Id == usługa.Id);

            if (existingCategory != null)
            {
                existingCategory.Id = usługa.Id;
                existingCategory.Tytuł = usługa.Tytuł;
                existingCategory.Lokalizacja = usługa.Lokalizacja;
                existingCategory.NrTelefonu = usługa.NrTelefonu;
                existingCategory.CenaOd = usługa.CenaOd;
                existingCategory.CenaDo = usługa.CenaDo;
                existingCategory.Opis = usługa.Opis;
                existingCategory.KrótkiOpis = usługa.KrótkiOpis;
                existingCategory.Widoczność = usługa.Widoczność;
                existingCategory.UrlZdjęcia = usługa.UrlZdjęcia;
                existingCategory.UrlBaneru = usługa.UrlBaneru;
                existingCategory.DataPulikacji = usługa.DataPulikacji;
                existingCategory.AutorId = usługa.AutorId;
                existingCategory.Kategoria = usługa.Kategoria;

                await eksperciOnlineDbContext.SaveChangesAsync();
                return existingCategory;
            }
            return null;
        }
    }
}
