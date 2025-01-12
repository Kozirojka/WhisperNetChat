using ErrorOr;
using WhisperNet.Domain.DocumentEntities;

namespace WhisperNet.Infrastructure.Services.Interfaces;

public interface IMessageService
{
   public Task<ErrorOr<bool>> SendMessageToMongoDb(Message message); 
}