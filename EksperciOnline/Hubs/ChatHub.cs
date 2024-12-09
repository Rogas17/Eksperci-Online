using EksperciOnline.Data;
using EksperciOnline.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

public class ChatHub : Hub
{
    private readonly EksperciOnlineDbContext eksperciOnlineDbContext;
    private readonly SignInManager<IdentityUser> signInManager;
    private readonly UserManager<IdentityUser> userManager;

    public ChatHub(EksperciOnlineDbContext eksperciOnlineDbContext, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        this.eksperciOnlineDbContext = eksperciOnlineDbContext;
        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    public async Task SendMessage(string chatId, string content)
    {
        var userId = Context.UserIdentifier; // Uzyskanie identyfikatora użytkownika z kontekstu
        var sender = await userManager.FindByIdAsync(userId);

        var message = new Message
        {
            Id = Guid.NewGuid(),
            ChatId = Guid.Parse(chatId),
            SenderId = userId,
            SenderName = sender?.UserName ?? "Anonim",
            Content = content,
            Timestamp = DateTime.UtcNow
        };

        eksperciOnlineDbContext.Messages.Add(message);
        await eksperciOnlineDbContext.SaveChangesAsync();

        await Clients.Group(chatId).SendAsync("ReceiveMessage", message.SenderName, content);
    }


    public override async Task OnConnectedAsync()
    {
        var chatId = Context.GetHttpContext().Request.Query["chatId"];
        if (!string.IsNullOrEmpty(chatId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }
        await base.OnConnectedAsync();
    }
}
