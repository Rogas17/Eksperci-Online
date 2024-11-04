using EksperciOnline.Models.Domain;

namespace EksperciOnline.Repositiories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Kategoria>> GetAllAsync();

        Task<Kategoria?> GetAsync(Guid id);

        Task<Kategoria> AddAsync(Kategoria kategoria);

        Task<Kategoria?> UpdateAsync(Kategoria kategoria);

        Task<Kategoria?> DeleteAsync(Guid id);
    }
}
