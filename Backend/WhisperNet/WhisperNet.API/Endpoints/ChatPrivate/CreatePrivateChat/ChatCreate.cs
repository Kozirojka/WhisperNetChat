using MediatR;
using WhisperNet.API.Interfaces;
using WhisperNet.Application.Chat.CreatePrivateChat;

namespace WhisperNet.API.Endpoints.ChatPrivate.CreatePrivateChat;

public class ChatCreate : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/chat", HandleCreateChatRequest).WithTags("Chat").RequireAuthorization();
    }
    
    private static async Task<IResult> HandleCreateChatRequest(CreatePrivateChatRequest request, IMediator mediator, CancellationToken ct)
    {
        var command = request.MapToCommand();
        
        var response = await mediator.Send(command, ct);
        
        if (response.IsError)
        {
            return Results.Conflict(response.Errors);
        }
        
        return Results.Ok(response.Value);
    }
}   