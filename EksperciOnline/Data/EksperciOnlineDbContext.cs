using EksperciOnline.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EksperciOnline.Data
{
    public class EksperciOnlineDbContext : DbContext
    {
        public EksperciOnlineDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Usługa> Usługi { get; set; }
        public DbSet<Kategoria> Kategorie { get; set; }
    }
}
