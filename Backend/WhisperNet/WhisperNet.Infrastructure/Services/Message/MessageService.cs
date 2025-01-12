using ErrorOr;
using WhisperNet.Domain.DocumentEntities;
using WhisperNet.Infrastructure.DbContexts.MongoDbs;
using WhisperNet.Infrastructure.Services.Interfaces;

namespace WhisperNet.Infrastructure.Services.Repositories;

public class MessageService : IMessageService
{
    private readonly MongoDbContext _context;

    public MessageService(MongoDbContext context)
    {
        _context = context;
    }

    
    //todo: here is big problem
    public async Task<ErrorOr<bool>> SendMessageToMongoDb(Message message)
    {
       await _context.Messages.InsertOneAsync(message);
       return true;
    }

}