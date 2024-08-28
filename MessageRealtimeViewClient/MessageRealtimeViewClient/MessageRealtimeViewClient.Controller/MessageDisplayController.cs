using System.Net.Http.Json;
using MessageRealtimeViewClient.MessageRealtimeViewClient.Models.Entity;

namespace MessageRealtimeViewClient.MessageRealtimeViewClient.Controller;

public class MessageDisplayController
{
    private readonly HttpClient _httpClient;

    public MessageDisplayController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Message>> GetMessagesAsync(DateTime from, DateTime to)
    {
        try
        {
            var response = await _httpClient.GetAsync($"?from={from:O}&to={to:O}");
            response.EnsureSuccessStatusCode();
            var messages = await response.Content.ReadFromJsonAsync<IEnumerable<Message>>();
            return messages ?? new List<Message>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при получении сообщений: {ex.Message}");
            return new List<Message>();
        }
    }
}