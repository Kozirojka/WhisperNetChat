using MediatR;
using WhisperNet.Application.Shared;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using WhisperNet.Domain.Entities;
using WhisperNet.Domain.Enums;
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
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
             var newChatRoom = new ChatRoom()
            {
                IsPrivate = true,
                IsShared = false,
                Created = DateTime.UtcNow
            };

            await _dbContext.ChatRooms.AddAsync(newChatRoom, cancellationToken);

            var findRecipient = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.Recipient, cancellationToken);
            var findSender = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.Sender, cancellationToken);

            if (findRecipient == null || findSender == null)
            {
                await transaction.RollbackAsync(cancellationToken);
                
                return Error.NotFound("UserNotFound", "One or both users were not found.");
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            var newChatParticipants = new[]
            {
                ChatParticipants.CreateChatParticipant(newChatRoom, findRecipient, ChatRoomRoles.User),
                ChatParticipants.CreateChatParticipant(newChatRoom, findSender, ChatRoomRoles.User)
            };

            _dbContext.ChatParticipants.AddRange(newChatParticipants);

            await _dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            var responseBack = new CreatePrivateChatResponse(
                chatId: newChatRoom.Id.ToString(),
                message: "Successfully saved!"
            );

             return responseBack;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Error.Failure("ChatCreationFailed", $"An error occurred: {ex.Message}");
        }
    }
}