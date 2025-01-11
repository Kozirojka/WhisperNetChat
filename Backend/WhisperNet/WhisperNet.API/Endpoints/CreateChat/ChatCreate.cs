using MediatR;
using WhisperNet.API.Interfaces;
using WhisperNet.Application.Chat.CreatePrivateChat;

namespace WhisperNet.API.Endpoints.CreateChat;

public class ChatCreate : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/chat", HandleCreateChatRequest).WithTags("Chat");
    }

    private static async Task<IResult> HandleCreateChatRequest(CreatePrivateChatRequest request, IMediator mediator, CancellationToken ct)
    {

        var command = request.MapToCommand();

        var response = await mediator.Send(command, ct);

        return Results.Ok(response);
    }
    
}