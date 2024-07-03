using EksperciOnline.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EksperciOnline.Data
{
    public class EksperciOnlineDbContext : DbContext
    {
        public EksperciOnlineDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Service> Services { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
