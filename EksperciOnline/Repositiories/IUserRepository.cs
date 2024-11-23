using Microsoft.AspNetCore.Identity;

namespace EksperciOnline.Repositiories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>>GetAll();
    }
}
