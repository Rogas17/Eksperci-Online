using EksperciOnline.Models.Domain;

namespace EksperciOnline.Repositiories
{
    public interface IFavoriteServiceRepository
    {
        Task<FavoriteService> AddAsync(FavoriteService favoriteService);
        Task<bool> RemoveAsync(Guid userId, Guid serviceId);
        Task<IEnumerable<FavoriteService>> GetByUserIdAsync(Guid userId);
    }
}
