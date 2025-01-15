using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WhisperNet.API.Endpoints.LoginRegister;
using WhisperNet.API.Extensions;
using WhisperNet.API.Hubs;
using WhisperNet.Application.Chat.CreatePrivateChat;
using WhisperNet.Domain.Configurations;
using WhisperNet.Domain.Entities;
using WhisperNet.Infrastructure;
using WhisperNet.Infrastructure.Services.Chat;
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


builder.Services.AddMongoDbServiceExtension(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);



builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateChatCommandHandler).Assembly);
});
    
builder.Services.AddSignalR(o =>
{
    o.EnableDetailedErrors = true;
});

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IChatService, ChatService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5174") 
            .AllowAnyMethod()                  
            .AllowAnyHeader()                 
            .AllowCredentials();               
    });
});
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
        return;
    }
    await next();
});


app.UseCors("AllowSpecificOrigins");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapLoginEndpoints();
app.RegisterAllEndpoints();

app.MapHub<ChatHub>("/chathub");

app.Run();


