using System.Security.Claims;
using WhisperNet.Infrastructure.Services.Interfaces;

namespace WhisperNet.Infrastructure.Services.Repositories;

public class UserService : IUserService
{
    public string? GetUserId(ClaimsPrincipal? user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}