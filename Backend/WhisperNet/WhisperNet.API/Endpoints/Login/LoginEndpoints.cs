using Microsoft.AspNetCore.Identity;
using WhisperNet.Domain.Entities;
using WhisperNet.Infrastructure.Services.Interfaces;
using LoginRequest = WhisperNet.Domain.LoginRequest;

namespace WhisperNet.API.Endpoints.Login;

public static class LoginEndpoints
{
    public static void MapLoginEndpoints(this WebApplication app)
    {
        app.MapPost("/login", HandleLogin).AllowAnonymous();
        app.MapPost("/register", HandleRegister).AllowAnonymous();
    }

    private static async Task<IResult> HandleRegister(RegisterRequest model,
        IJwtTokenService service, 
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager)
    {


        if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
        {
            return Results.BadRequest("Problem with password or email.");
        }

        var newRegisterUser = new ApplicationUser()
        {
            Email = model.Email,
            UserName = model.Email,
            FirstName = model.FirstName, 
            LastName = model.LastName
        };
        
        var doesUserExist = userManager.FindByEmailAsync(model.Email).Result;
        
        if (doesUserExist != null)
        {
            return Results.Problem("User not found.");
        }
        
        
        var result = userManager.CreateAsync(newRegisterUser, model.Password).Result;

        if (!result.Succeeded)
        {
            return Results.BadRequest(result.Errors);
        }
        
        await userManager.AddToRoleAsync(newRegisterUser, "User");
        
        
        var tokenString = service.GenerateToken(model.Email, "User");
        
        //Тут проблема була у тому, що я
        //створював лямбда функцію і хотів її передати
        return Results.Ok(new
        {
             token = tokenString,
             name = "User successfully was added to db and registered",
        });
    }
    
    private static IResult HandleLogin(LoginRequest request, IJwtTokenService _service)
    {
        var tokenString = _service.GenerateToken(request.Email, "User");
        
        return Results.Ok(tokenString);
    }
}