using Microsoft.AspNetCore.SignalR;
using WhisperNet.Infrastructure.Services.Interfaces;

namespace WhisperNet.API.Hubs;

public class ChatHub : Hub
{
    private readonly ILogger<ChatHub> _logger;
    private readonly IMessageService _messageService;
    private readonly IChatService _chatService;
    
    public ChatHub(ILogger<ChatHub> logger, IMessageService messageService, IChatService chatService)
    {
        _logger = logger;
        _messageService = messageService;
        _chatService = chatService;
    }


    public async Task JoinGroup(int chatId)
    {
        _logger.LogInformation($"Joining group {chatId}");
        
        
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
    }


    public async Task SendPrivateMessage(int chatId, string message)
    {
        var participantUserId = _chatService.GetParticipantsByChatId(chatId);
        
        // ! save message to group
        await Clients.User(participantUserId).SendAsync("ReceiveMessage", chatId, message);
    }
}

