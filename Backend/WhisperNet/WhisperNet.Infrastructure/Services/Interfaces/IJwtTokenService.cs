namespace WhisperNet.Infrastructure.Services.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(string email, string role);
}
