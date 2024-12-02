namespace EksperciOnline.Models.Domain
{
    public class FavoriteService
    {
        public Guid Id { get; set; }
        public Guid ServiceId { get; set; }
        public Guid UserId { get; set; }
    }
}
