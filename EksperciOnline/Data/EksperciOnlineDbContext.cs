using EksperciOnline.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EksperciOnline.Data
{
    public class EksperciOnlineDbContext : DbContext
    {
        public EksperciOnlineDbContext(DbContextOptions<EksperciOnlineDbContext> options) : base(options)
        {
        }

        public DbSet<Usługa> Usługi { get; set; }

        public DbSet<Kategoria> Kategorie { get; set; }

        public DbSet<ServiceComment> ServiceComment { get; set; }

        public DbSet<FavoriteService> FavoriteServices { get; set; }

        public DbSet<Zgłoszenie> Zgłoszenia { get; set; }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<Message> Messages { get; set; }

    }
}
