using MediatR;
using WhisperNet.API.Interfaces;
using WhisperNet.Application.Chat.SharedChatFeature;
using WhisperNet.Infrastructure.Services.Interfaces;

namespace WhisperNet.API.Endpoints.SharedChatFeature.GetAllChat;

public class GetAllChat : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/chats", GetAllChatHandler).WithTags("Chats");
    }

    private static async Task<IResult> GetAllChatHandler(HttpContext context, IMediator _mediator, IUserService _userService)
    {

        string? UserId = _userService.GetUserId(context.User);

        // if (UserId == null)
        // {
        //     return Results.NotFound("There is no such user");
        // }
        
        var query = new GetAllChatCommand(UserId);
        var result = _mediator.Send(query).Result;

        if (result.IsError)
        {
            return Results.NotFound(result);
        }
        
        return Results.Ok(result.Value);
    }
}