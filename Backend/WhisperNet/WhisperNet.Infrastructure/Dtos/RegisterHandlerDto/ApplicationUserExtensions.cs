using WhisperNet.Domain.Entities;

namespace WhisperNet.Infrastructure.Dtos.RegisterHandlerDto;

public static class ApplicationUserExtensions
{
    public static ApplicationUser ToApplicationUser(this RegisterRequestDto model)
    {
        return new ApplicationUser
        {
            Email = model.Email,
            UserName = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };
    }
}