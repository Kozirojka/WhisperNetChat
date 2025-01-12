using MongoDB.Driver;
using WhisperNet.Domain.DocumentEntities;

namespace WhisperNet.Infrastructure.DbContexts.MongoDbs;

public class MongoDbContext(IMongoClient mongoClient)
{
    private readonly IMongoDatabase _database = mongoClient.GetDatabase("ChatApp");
    
    public IMongoCollection<Message> Messages => _database.GetCollection<Message>("Messages");
}