using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WhisperNet.Domain.Entities;
using WhisperNet.Infrastructure;

namespace WhisperNet.Application.Chat.SharedChatFeature;


public sealed record GetAllChatCommand(string UserId) : IRequest<ErrorOr<List<ChatRoom>>>;

public class GetAllChat : IRequestHandler<GetAllChatCommand, ErrorOr<List<ChatRoom>>>
{
    
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    public GetAllChat(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }


    public async Task<ErrorOr<List<ChatRoom>>> Handle(GetAllChatCommand request,
        CancellationToken cancellationToken)
    {
        // this command exist
        // for fetch from the db all
        // related to user chats 
        
        var chatList = await _dbContext
            .ChatParticipants
            .AsNoTracking()
            .Where(cp => cp.UserId == request.UserId)
            .Select(cp => cp.ChatRoom)
            .ToListAsync(cancellationToken);
        
        return chatList;
    }
}