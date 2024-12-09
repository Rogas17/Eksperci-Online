using System.ComponentModel.DataAnnotations.Schema;

namespace EksperciOnline.Models.Domain
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string ServiceProviderId { get; set; }
        public ICollection<Message> Messages { get; set; }

        // Właściwości pomocnicze (niezmapowane w bazie danych)
        [NotMapped]
        public string UserName { get; set; }

        [NotMapped]
        public string ServiceProviderName { get; set; }
    }
}
