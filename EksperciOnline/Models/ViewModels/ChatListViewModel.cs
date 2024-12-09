using EksperciOnline.Models.Domain;

namespace EksperciOnline.Models.ViewModels
{
    public class ChatListViewModel
    {
        public Guid ActiveChatId { get; set; }
        public List<Chat> Chats { get; set; }
    }
}
