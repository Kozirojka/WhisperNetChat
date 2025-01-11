using MediatR;
using WhisperNet.Application.Shared;
using ErrorOr;
using WhisperNet.Domain.Entities;
using WhisperNet.Infrastructure;


namespace WhisperNet.Application.Chat.CreatePrivateChat;



public sealed record CreatePrivateChatRequest(
    string Recipient,
    string Sender);

public sealed record CreatePrivateChatCommand(
    string Recipient,
    string Sender)
    : IRequest<ErrorOr<CreatePrivateChatResponse>>;

public sealed class CreateChatCommandHandler :
    IRequestHandler<CreatePrivateChatCommand,
        ErrorOr<CreatePrivateChatResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateChatCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<CreatePrivateChatResponse>> Handle(CreatePrivateChatCommand request, CancellationToken cancellationToken)
    {
        var newChatRoom = new ChatRoom()
        {
            IsPrivate = true,
            IsShared = false,
            Created = DateTime.UtcNow
        };

        await _dbContext.ChatRooms.AddAsync(newChatRoom, cancellationToken);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        if (result > 0)        {
            var responseBack = new CreatePrivateChatResponse(
                chatId: newChatRoom.Id.ToString(),
                message: "Successfully saved!"
            );

            return responseBack; 
        }

        return Error.Failure("ChatCreationFailed", "Nothing was saved!");
    }
}