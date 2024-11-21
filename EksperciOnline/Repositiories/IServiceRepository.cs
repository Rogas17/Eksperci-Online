using EksperciOnline.Models.Domain;

namespace EksperciOnline.Repositiories
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Usługa>> GetAllAsync();

        Task<Usługa?> GetAsync(Guid id);

        Task<List<ServiceComment>> GetCommentsByServiceIdAsync(Guid serviceId);

        Task<int> GetTotalComments(Guid serviceId);

        Task<Usługa> AddAsync(Usługa usługa);

        Task<Usługa?> UpdateAsync(Usługa usługa);

        Task<Usługa?> DeleteAsync(Guid id);
    }
}
