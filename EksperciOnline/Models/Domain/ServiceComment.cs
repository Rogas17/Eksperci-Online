namespace EksperciOnline.Models.Domain
{
    public class ServiceComment
    {
        public Guid Id { get; set; }
        public int Grade { get; set; }
        public string Description { get; set; }
        public Guid ServiceId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
