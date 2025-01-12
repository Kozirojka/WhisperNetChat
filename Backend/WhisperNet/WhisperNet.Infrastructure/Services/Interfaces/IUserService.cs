using System.Security.Claims;

namespace WhisperNet.Infrastructure.Services.Interfaces;

public interface IUserService
{
    string? GetUserId(ClaimsPrincipal? user);
}
