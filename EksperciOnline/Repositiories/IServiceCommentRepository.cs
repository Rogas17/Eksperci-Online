using EksperciOnline.Models.Domain;

namespace EksperciOnline.Repositiories
{
    public interface IServiceCommentRepository
    {
        Task<ServiceComment> AddAsync(ServiceComment serviceComment);

        Task<IEnumerable<ServiceComment>> GetCommentsByServiceIdAsync(Guid serviceId);
        Task<int> GetTotalComments(Guid serviceId);
    }
}
