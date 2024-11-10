﻿using EksperciOnline.Data;
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

        public async Task<IEnumerable<Usługa>> GetAllAsync()
        {
            return await eksperciOnlineDbContext.Usługi.Include(x => x.Kategoria).ToListAsync();
        }

        public async Task<Usługa?> GetAsync(Guid id)
        {
            return await eksperciOnlineDbContext.Usługi.Include(x => x.Kategoria).FirstOrDefaultAsync(x => x.Id == id);
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
                existingCategory.Autor = usługa.Autor;
                existingCategory.Kategoria = usługa.Kategoria;

                await eksperciOnlineDbContext.SaveChangesAsync();
                return existingCategory;
            }
            return null;
        }
    }
}