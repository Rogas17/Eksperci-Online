using System.ComponentModel.DataAnnotations.Schema;

namespace EksperciOnline.Models.Domain
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public string SenderId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public Chat Chat { get; set; }
        public string SenderName { get; set; }
    }
}
