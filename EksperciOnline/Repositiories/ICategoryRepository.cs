using EksperciOnline.Models.Domain;

namespace EksperciOnline.Repositiories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Kategoria>> GetAllAsync(
            string? searchQuery = null,
            string? sortBy = null,
            string? sortDirection = null,
            int pageNumber = 1,
            int pageSize = 100);

        Task<Kategoria?> GetAsync(Guid id);

        Task<Kategoria> AddAsync(Kategoria kategoria);

        Task<Kategoria?> UpdateAsync(Kategoria kategoria);

        Task<Kategoria?> DeleteAsync(Guid id);

        Task<int> CountAsync();
    }
}
