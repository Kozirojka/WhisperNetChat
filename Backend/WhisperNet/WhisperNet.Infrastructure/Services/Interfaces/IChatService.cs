namespace WhisperNet.Infrastructure.Services.Interfaces;

public interface IChatService
{
    public string? GetParticipantByChatId(int chatId);
}