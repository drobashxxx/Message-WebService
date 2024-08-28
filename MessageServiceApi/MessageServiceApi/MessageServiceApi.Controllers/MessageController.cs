using MessageServiceApi.MessageServiceApi.Domain;
using MessageServiceApi.MessageServiceApi.Service.Implementations;
using MessageServiceApi.MessageServiceApi.Service.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MessageServiceApi.MessageServiceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;
    private readonly ILogger<MessageController> _logger;

    public MessageController(IMessageService messageService, ILogger<MessageController> logger)
    {
        _messageService = messageService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] Message message)
    {
        if (message == null)
        {
            _logger.LogWarning("Получено пустое сообщение.");
            return BadRequest("Сообщение не может быть пустым.");
        }
        
        message.TimeStamp = DateTime.UtcNow; 
        await _messageService.AddMessageAsync(message);
        return Ok("Сообщение успешно отправлено.");
    }

    [HttpGet]
    public async Task<IActionResult> GetMessages([FromQuery] DateTime from, [FromQuery] DateTime to)
    {
        var messages = await _messageService.GetMessagesAsync(from, to);
        return Ok(messages);
    }
}