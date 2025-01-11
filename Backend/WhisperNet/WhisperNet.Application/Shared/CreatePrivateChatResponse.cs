namespace WhisperNet.Application.Shared;

public class CreatePrivateChatResponse
{
    public CreatePrivateChatResponse(string chatId, string message)
    {
        ChatId = chatId;
        Message = message;
    }

    public string ChatId { get; set; }
    public string Message { get; set; }
    
}