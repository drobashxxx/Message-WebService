using MessageServiceApi.MessageServiceApi.Domain;

namespace MessageServiceApi.MessageServiceApi.DAL.Interface;

public interface IMessageRepository
{
    Task AddMessageAsync(Message message);
    Task<IEnumerable<Message>> GetMessageAsync(DateTime from, DateTime to); 
}