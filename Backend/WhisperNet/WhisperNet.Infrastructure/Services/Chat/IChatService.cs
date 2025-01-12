namespace WhisperNet.Infrastructure.Services.Chat;

public interface IChatService
{
    public string? GetParticipantByChatId(int chatId);
}