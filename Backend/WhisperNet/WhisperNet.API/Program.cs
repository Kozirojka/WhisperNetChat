using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WhisperNet.API.Endpoints.feature;
using WhisperNet.API.Endpoints.LoginRegister;
using WhisperNet.API.Extensions;
using WhisperNet.Application.Chat.CreatePrivateChat;
using WhisperNet.Domain;
using WhisperNet.Domain.Configurations;
using WhisperNet.Domain.Entities;
using WhisperNet.Infrastructure;
using WhisperNet.Infrastructure.DbContexts.MongoDbs;
using WhisperNet.Infrastructure.Services.Interfaces;
using WhisperNet.Infrastructure.Services.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));

builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var mongoDbConnectionString = builder.Configuration.GetConnectionString("MongoDb");

if (!string.IsNullOrEmpty(mongoDbConnectionString))
{
    Console.WriteLine("The mongoDbConnectionString is empty");
}

builder.Services.AddDbContext<EfCoreMongoDbContext>(x => x
    .EnableSensitiveDataLogging().UseMongoDB(mongoDbConnectionString, "messages"));


builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateChatCommandHandler).Assembly);
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


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapLoginEndpoints();
app.RegisterAllEndpoints();


app.Run();


