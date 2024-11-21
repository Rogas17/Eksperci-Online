using EksperciOnline.Data;
using EksperciOnline.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EksperciOnline.Repositiories
{
    public class ServiceCommentRepository : IServiceCommentRepository
    {
        private readonly EksperciOnlineDbContext eksperciOnlineDbContext;

        public ServiceCommentRepository(EksperciOnlineDbContext eksperciOnlineDbContext)
        {
            this.eksperciOnlineDbContext = eksperciOnlineDbContext;
        }

        public async Task<ServiceComment> AddAsync(ServiceComment serviceComment)
        {
            await eksperciOnlineDbContext.ServiceComment.AddAsync(serviceComment);
            await eksperciOnlineDbContext.SaveChangesAsync();
            return serviceComment;
        }

        public async Task<IEnumerable<ServiceComment>> GetCommentsByServiceIdAsync(Guid serviceId)
        {
            return await eksperciOnlineDbContext.ServiceComment.Where(x => x.ServiceId == serviceId)
                .ToListAsync();
        }

        public async Task<int> GetTotalComments(Guid serviceId)
        {
            return await eksperciOnlineDbContext.ServiceComment
                .CountAsync(x => x.ServiceId == serviceId);
        }
    }
}
