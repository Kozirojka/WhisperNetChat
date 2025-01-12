using Microsoft.AspNetCore.SignalR;

namespace WhisperNet.Infrastructure.Services.Interfaces;

public interface IUserIdProvider
{
    public string? GerUserId(HubConnectionContext connection);
}