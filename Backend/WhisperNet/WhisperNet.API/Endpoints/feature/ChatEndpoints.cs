using WhisperNet.API.Interfaces;

namespace WhisperNet.API.Endpoints.feature;

public class ChatEndpoints : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/chat", () => "Create Chat!").WithTags("Chat");
        endpoints.MapGet("/chat", () => "Get chat that have user!").WithTags("Chat");
        endpoints.MapDelete("/chat", () => "Delete Chat!").WithTags("Chat");
        
        endpoints.MapPut("/chat/delete/{userId}", () => "Delete user!").RequireAuthorization("Admin").WithTags("Chat");
        endpoints.MapPut("/chat/update/{roles}/{userId}", () => "Update user!").RequireAuthorization("Admin").WithTags("Chat");
    }
}