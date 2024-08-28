using MessageServiceApi.MessageServiceApi.DAL.Interface;
using MessageServiceApi.MessageServiceApi.Domain;
using MessageServiceApi.MessageServiceApi.Service.Interface;

namespace MessageServiceApi.MessageServiceApi.Service.Implementations;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly ILogger<MessageService> _logger;

    public MessageService(IMessageRepository messageRepository, ILogger<MessageService> logger)
    {
        _messageRepository = messageRepository;
        _logger = logger;
    }

    public async Task AddMessageAsync(Message message)
    {
        try
        {
            _logger.LogInformation("Добавление сообщения: {Text} в {Timestamp}", message.Text, message.TimeStamp);
            await _messageRepository.AddMessageAsync(message);
            _logger.LogInformation("Сообщение успешно добавлено.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при добавлении сообщения: {Text}", message.Text);
            throw;
        }
    }

    public async Task<IEnumerable<Message>> GetMessagesAsync(DateTime from, DateTime to)
    {
        try
        {
            _logger.LogInformation("Получение сообщений с {From} до {To}", from, to);
            var messages = await _messageRepository.GetMessageAsync(from, to);
            _logger.LogInformation("Получено {Count} сообщений.", messages.Count());
            return messages;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении сообщений с {From} до {To}", from, to);
            throw;
        }
    }
}