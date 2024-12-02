using EksperciOnline.Data;
using EksperciOnline.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EksperciOnline.Repositiories
{
    public class FavoriteServiceRepository : IFavoriteServiceRepository
    {
        private readonly EksperciOnlineDbContext eksperciOnlineDbContext;

        public FavoriteServiceRepository(EksperciOnlineDbContext eksperciOnlineDbContext)
        {
            this.eksperciOnlineDbContext = eksperciOnlineDbContext;
        }

        public async Task<FavoriteService> AddAsync(FavoriteService favoriteService)
        {
            await eksperciOnlineDbContext.FavoriteServices.AddAsync(favoriteService);
            await eksperciOnlineDbContext.SaveChangesAsync();
            return favoriteService;
        }

        public async Task<bool> RemoveAsync(Guid userId, Guid serviceId)
        {
            var favorite = await eksperciOnlineDbContext.FavoriteServices
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ServiceId == serviceId);

            if (favorite != null)
            {
                eksperciOnlineDbContext.FavoriteServices.Remove(favorite);
                await eksperciOnlineDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<FavoriteService>> GetByUserIdAsync(Guid userId)
        {
            return await eksperciOnlineDbContext.FavoriteServices
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }
    }
}
