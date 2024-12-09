using EksperciOnline.Data;
using EksperciOnline.Models.Domain;
using EksperciOnline.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EksperciOnline.Controllers
{
    [Authorize]
    public class MessegeController : Controller
    {
        private readonly EksperciOnlineDbContext eksperciOnlineDbContext;
        private readonly UserManager<IdentityUser> userManager;

        public MessegeController(EksperciOnlineDbContext eksperciOnlineDbContext, UserManager<IdentityUser> userManager)
        {
            this.eksperciOnlineDbContext = eksperciOnlineDbContext;
            this.userManager = userManager;
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> StartChat(string serviceProviderId)
        {
            var userId = userManager.GetUserId(User);

            // Znajdź istniejący czat lub utwórz nowy
            var chat = await eksperciOnlineDbContext.Chats
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ServiceProviderId == serviceProviderId);

            if (chat == null)
            {
                chat = new Chat
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    ServiceProviderId = serviceProviderId,
                    Messages = new List<Message>()
                };
                eksperciOnlineDbContext.Chats.Add(chat);
            }

            await eksperciOnlineDbContext.SaveChangesAsync();

            return RedirectToAction("List", new { chatId = chat.Id });
        }



        [HttpGet]
        public async Task<IActionResult> List(Guid? chatId)
        {
            var userId = userManager.GetUserId(User);
            var chats = await eksperciOnlineDbContext.Chats
                .Include(c => c.Messages)
                .Where(c => c.UserId == userId || c.ServiceProviderId == userId)
                .ToListAsync();

            var activeChatId = chatId ?? chats.FirstOrDefault()?.Id ?? Guid.Empty;

            // Pobieranie nazw użytkowników dla czatów i wiadomości
            foreach (var chat in chats)
            {
                // Pobieranie nazw użytkowników dla właścicieli czatu
                var serviceProvider = await userManager.FindByIdAsync(chat.ServiceProviderId);
                var user = await userManager.FindByIdAsync(chat.UserId);

                chat.ServiceProviderName = serviceProvider?.UserName ?? "Anonim";
                chat.UserName = user?.UserName ?? "Anonim";

                // Pobieranie nazw użytkowników dla wiadomości
                foreach (var message in chat.Messages)
                {
                    var sender = await userManager.FindByIdAsync(message.SenderId);
                    message.SenderName = sender?.UserName ?? "Anonim";
                }
            }

            var model = new ChatListViewModel
            {
                ActiveChatId = activeChatId,
                Chats = chats
            };

            return View(model);
        }
    }

}
