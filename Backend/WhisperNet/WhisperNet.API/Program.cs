using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using WhisperNet.API.Endpoints.LoginRegister;
using WhisperNet.API.Extensions;
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


BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

var mongoDbConnectionString = builder.Configuration.GetConnectionString("MongoDb");
var mongoClientSettings = MongoClientSettings.FromConnectionString(mongoDbConnectionString);

builder.Services.AddSingleton<IMongoClient>(new MongoClient(mongoClientSettings));

ConventionRegistry.Register("camelCase", new ConventionPack {
    new CamelCaseElementNameConvention()
}, _ => true);

ConventionRegistry.Register("EnumStringConvention", new ConventionPack
{
    new EnumRepresentationConvention(BsonType.String)
}, _ => true);  




if (!string.IsNullOrEmpty(mongoDbConnectionString))
{
    Console.WriteLine("The mongoDbConnectionString is empty");
}




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


