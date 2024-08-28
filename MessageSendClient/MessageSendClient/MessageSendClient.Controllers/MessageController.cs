using System.Net.Http.Json;
using MessageSendClient.MessageSendClient.Models.Entity;
using MessageSendClient.MessageSendClient.View;

namespace MessageSendClient.MessageSendClient.Controllers;

public class MessageController
{
    private readonly HttpClient _httpClient;
    private int _messageCounter = 1; 

    public MessageController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            string userInput = ConsoleView.GetMessageFromUser();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                ConsoleView.DisplayError("Сообщение не может быть пустым.");
                continue;
            }

            if (userInput.Length > 128)
            {
                ConsoleView.DisplayError("Сообщение превышает допустимую длину (128 символов).");
                continue;
            }

            var message = new Message
            {
                Id = _messageCounter++,
                Text = userInput
            };

            await SendMessageAsync(message);
        }
    }

    private async Task SendMessageAsync(Message message)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("", message);
            response.EnsureSuccessStatusCode();
            ConsoleView.DisplayMessageSent(message.Id);
        }
        catch (Exception ex)
        {
            ConsoleView.DisplayError($"Не удалось отправить сообщение {message.Id}: {ex.Message}");
        }
    
    }
}