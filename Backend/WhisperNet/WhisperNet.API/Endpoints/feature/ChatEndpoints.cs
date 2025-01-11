using WhisperNet.API.Interfaces;

namespace WhisperNet.API.Endpoints.feature;

public class ChatEndpoints : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/chat", () => "Create Chat!");
        endpoints.MapGet("/chat", () => "Get chat that have user!");
        endpoints.MapDelete("/chat", () => "Delete Chat!");
        
        endpoints.MapPut("/chat/delete/{userId}", () => "Delete user!").RequireAuthorization("Admin");
        endpoints.MapPut("/chat/update/{roles}/{userId}", () => "Update user!").RequireAuthorization("Admin");
    }
}