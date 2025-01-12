using WhisperNet.Infrastructure.Services.Interfaces;

namespace WhisperNet.Infrastructure.Services.Repositories;

public class ChatService : IChatService
{
    private readonly ApplicationDbContext _dbContext;

    public ChatService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public string? GetParticipantByChatId(int chatId)
    {
        var result = _dbContext.ChatParticipants.FirstOrDefault(chat => chat.ChatId == chatId);
        var id = result?.UserId;
        
        // ! todo add logic to find user id 
        return id;
    }
}