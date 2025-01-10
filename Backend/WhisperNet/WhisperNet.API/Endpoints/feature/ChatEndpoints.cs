namespace WhisperNet.API.Endpoints.feature;

public static class ChatEndpoints
{
    public static void MapChatEndpoints(this WebApplication app)
    {
        app.MapPost("/chat", () => "Create Chat!");
        app.MapGet("/chat", () => "Get chat that have user!");
        app.MapDelete("/chat", () => "Delete Chat!");
        
        app.MapPut("/chat/delete/{userId}", () => "Delete user!").RequireAuthorization("Admin");
        app.MapPut("/chat/update/{roles}/{userId}", () => "Update user!").RequireAuthorization("Admin");
    }
}