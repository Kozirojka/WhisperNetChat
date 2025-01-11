using MediatR;
using WhisperNet.Application.Shared;
using ErrorOr;


namespace WhisperNet.Application.Chat.CreatePrivateChat;



public sealed record CreatePrivateChatRequest(
    string recipient,
    string sender);

public sealed record CreatePrivateChatCommand(
    string recipient,
    string sender)
    : IRequest<CreatePrivateChatResponse>;

public sealed class CreateChatCommandHandler :
    IRequestHandler<CreatePrivateChatCommand,
       CreatePrivateChatResponse>
{
    public Task<CreatePrivateChatResponse> Handle(CreatePrivateChatCommand request, CancellationToken cancellationToken)
    {
        var response = new CreatePrivateChatResponse(
            chatId: Guid.NewGuid().ToString(), 
            message: "Chat successfully created!"
        );
        
        
        
        return Task.FromResult(response);
    }
}
