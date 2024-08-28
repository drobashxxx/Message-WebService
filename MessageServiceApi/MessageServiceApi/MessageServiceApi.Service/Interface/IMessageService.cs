using MessageServiceApi.MessageServiceApi.Domain;

namespace MessageServiceApi.MessageServiceApi.Service.Interface;

public interface IMessageService
{
    Task AddMessageAsync(Message message);
    Task<IEnumerable<Message>> GetMessagesAsync(DateTime from, DateTime to);
}