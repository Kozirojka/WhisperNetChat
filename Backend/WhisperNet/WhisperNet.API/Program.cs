using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WhisperNet.API.Endpoints.Login;
using WhisperNet.API.Extensions;
using WhisperNet.Domain;
using WhisperNet.Domain.Entities;
using WhisperNet.Infrastructure;
using WhisperNet.Infrastructure.Services.Interfaces;
using WhisperNet.Infrastructure.Services.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));

builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});



builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     try
//     {
//         var context = services.GetRequiredService<ApplicationDbContext>();
//         context.Database.Migrate();
//     }
//     catch (Exception ex)
//     { 
//         var logger = services.GetRequiredService<ILogger<Program>>();
//         logger.LogError(ex, "An error occurred during migration.");
//     }
// }

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapLoginEndpoints();


app.Run();


