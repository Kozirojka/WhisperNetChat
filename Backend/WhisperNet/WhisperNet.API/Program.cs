using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WhisperNet.API.Endpoints.LoginRegister;
using WhisperNet.API.Extensions;
using WhisperNet.API.Hubs;
using WhisperNet.Application.Chat.CreatePrivateChat;
using WhisperNet.Domain.Configurations;
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


builder.Services.AddMongoDbServiceExtension(builder.Configuration);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateChatCommandHandler).Assembly);
});

builder.Services.AddSignalR();

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMessageService, MessageService>();

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


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapLoginEndpoints();
app.RegisterAllEndpoints();

app.MapHub<ChatHub>("/chathub");

app.Run();


