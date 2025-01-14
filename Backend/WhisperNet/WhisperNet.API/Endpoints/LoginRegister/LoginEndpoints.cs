using Microsoft.AspNetCore.Identity;
using WhisperNet.Domain.Entities;
using WhisperNet.Infrastructure.Dtos;
using WhisperNet.Infrastructure.Dtos.RegisterHandlerDto;
using WhisperNet.Infrastructure.Services.Interfaces;

namespace WhisperNet.API.Endpoints.LoginRegister;

public static class LoginEndpoints
{
    public static void MapLoginEndpoints(this WebApplication app)
    {
        app.MapPost("/login", HandleLogin).AllowAnonymous();
        app.MapPost("/register", HandleRegister).AllowAnonymous();
    }

    private static async Task<IResult> HandleRegister(RegisterRequestDto model,
        IJwtTokenService service, 
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager)
    {


        if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
        {
            return Results.BadRequest("Problem with password or email.");
        }

        var newRegisterUser = model.ToApplicationUser();
        
        var doesUserExist = await userManager.FindByEmailAsync(model.Email);
        
        if (doesUserExist != null)
        {
            return Results.Problem("User not found.");
        }
        
        
        var result = await userManager.CreateAsync(newRegisterUser, model.Password);

        if (!result.Succeeded)
        {
            return Results.BadRequest(result.Errors);
        }
        
        await userManager.AddToRoleAsync(newRegisterUser, "User");
        
        
        var tokenString = service.GenerateToken(model.Email, "User");
        
        return Results.Ok(new
        {
             token = tokenString,
             name = "User successfully was added to db and registered",
        });
    }   
    
    private static async Task<IResult> HandleLogin(LoginRequest request, IJwtTokenService _service, UserManager<ApplicationUser> userManager)
    {
        var result = userManager.FindByEmailAsync(request.Email).Result;

        if (result == null)
        {
            return Results.NotFound("User not found. Please create a new user");
        }
        var roles = await userManager.GetRolesAsync(result);
        
        if (roles == null || !roles.Any())
        {
            return Results.BadRequest("User does not have any roles assigned");
        }
        
        var role = roles.First();
        
        var tokenString = _service.GenerateToken(request.Email, role);
        
        return Results.Ok(new
        {
            token = tokenString,
            role = role
        });
    }
}