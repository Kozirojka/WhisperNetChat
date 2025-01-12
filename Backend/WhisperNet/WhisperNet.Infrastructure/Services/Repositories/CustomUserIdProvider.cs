using Microsoft.AspNetCore.SignalR;
using IUserIdProvider = WhisperNet.Infrastructure.Services.Interfaces.IUserIdProvider;

namespace WhisperNet.Infrastructure.Services.Repositories;

public class CustomUserIdProvider : IUserIdProvider
{
    public string? GerUserId(HubConnectionContext connection)
    {
        return connection.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
    }
}