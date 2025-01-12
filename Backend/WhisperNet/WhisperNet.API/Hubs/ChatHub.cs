using Microsoft.AspNetCore.SignalR;
using WhisperNet.Infrastructure.Services.Interfaces;

namespace WhisperNet.API.Hubs;

public class ChatHub : Hub
{
    private readonly ILogger<ChatHub> _logger;
    private readonly IMessageService _messageService;

    public ChatHub(ILogger<ChatHub> logger, IMessageService messageService)
    {
        _logger = logger;
        _messageService = messageService;
    }


    public async Task JoinGroup(int chatId)
    {
        _logger.LogInformation($"Joining group {chatId}");
        
        
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
    }
    
}

