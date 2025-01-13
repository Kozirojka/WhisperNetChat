using Microsoft.AspNetCore.SignalR;
using WhisperNet.Domain.DocumentEntities;
using WhisperNet.Infrastructure.Services.Chat;
using WhisperNet.Infrastructure.Services.Interfaces;

namespace WhisperNet.API.Hubs;

//Todo: ? Потрібно завтар стоврити клієнт на React та протесутвати додаток


public class ChatHub(
    ILogger<ChatHub> logger,
    IMessageService messageService,
    IChatService chatService,
    IUserService userService)
    : Hub
{
    public override async Task OnConnectedAsync()
    {        
        await Clients.All.SendAsync("ReceiveMessage", $"Connected to {Context.ConnectionId}");
    }
    
    // ? КОРИСТУВАЧ НАДСИЛА СЮДИ
    public async Task SendPrivateMessage(int chatId, string message)
    {
        var participantUserId = chatService.GetParticipantByChatId(chatId);
        var senderId = userService.GetUserId(Context.User);
        
            
        if (participantUserId == null)
        {
            logger.LogInformation("There is no such user");
            
        }

        var messageObject = new Message()
        {
            CreateAt = DateTime.UtcNow,
            SenderId = senderId,
            Text = message
        };
        
        await messageService.SendMessageToMongoDb(messageObject);
        
        // ! save message to group
        await Clients.User(participantUserId).SendAsync("ReceiveMessage", chatId, message);
    }
    
    
}

