using EksperciOnline.Models.Domain;

namespace EksperciOnline.Repositiories
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Usługa>> GetAllAsync();

        Task<Usługa?> GetAsnyc(Guid id);

        Task<Usługa> AddAsync(Usługa usługa);

        Task<Usługa?> UpdateAsync(Usługa usługa);

        Task<Usługa?> DeleteAsync(Guid id);
    }
}
