using MediatR;
using WhisperNet.API.Interfaces;

namespace WhisperNet.API.Endpoints.feature;

public class ChatEndpoints : IEndpoint
{
    
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/chat", () => "Get chat that have user!").WithTags("Chat");
        endpoints.MapDelete("/chat", () => "Delete Chat!").WithTags("Chat");
        
        endpoints.MapPut("/chat/delete/{userId}", () => "Delete user!")
            .WithTags("Chat");
        
        endpoints.MapPut("/chat/update/{roles}/{userId}", () => "Update user!")
            .WithTags("Chat");
        
    }
    
    
    
    
}