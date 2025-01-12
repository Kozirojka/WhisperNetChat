using WhisperNet.Application.Chat.CreatePrivateChat;

namespace WhisperNet.API.Endpoints.ChatPrivate.CreatePrivateChat;

internal static class CreateShipmentMappingExtensions
{
    public static CreatePrivateChatCommand MapToCommand(this CreatePrivateChatRequest request)
        => new(request.Recipient,
            request.Sender);
}