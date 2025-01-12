using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using WhisperNet.Infrastructure.DbContexts.MongoDbs;

namespace WhisperNet.API.Extensions;

public static class MongoDbServiceExtension
{
    public static IServiceCollection AddMongoDbServiceExtension(this IServiceCollection services, IConfiguration configuration)
    {

        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        var mongoDbConfiguration = configuration.GetSection("MongoDb");
        var connectionString = mongoDbConfiguration["DefaultConnection"];
        var mongoClientSettings = MongoClientSettings.FromConnectionString(connectionString);

        services.AddSingleton<IMongoClient>(new MongoClient(mongoClientSettings));

        ConventionRegistry.Register("camelCase", new ConventionPack {
            new CamelCaseElementNameConvention()
        }, _ => true);

        ConventionRegistry.Register("EnumStringConvention", new ConventionPack
        {
            new EnumRepresentationConvention(BsonType.String)
        }, _ => true);
        
        services.AddSingleton<MongoDbContext>();

        return services;
    }
}