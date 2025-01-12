namespace WhisperNet.Infrastructure.Services.Interfaces;

public interface IChatService
{
    public string GetParticipantsByChatId(int chatId);
}