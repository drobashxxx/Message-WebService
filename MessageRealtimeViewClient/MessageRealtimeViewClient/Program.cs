using MessageRealtimeViewClient.MessageRealtimeViewClient.Controller;

var handler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true 
};

using (var httpClient = new HttpClient(handler))
{
    httpClient.BaseAddress = new Uri("https://localhost:7221/api/Message");
    var messageDisplayController = new MessageDisplayController(httpClient);

    DateTime lastCheckedTime = DateTime.UtcNow;

    Console.WriteLine("Приложение для отображения сообщений в реальном времени запущено.");

    while (true)
    {
        var messages = await messageDisplayController.GetMessagesAsync(lastCheckedTime.AddSeconds(-1), DateTime.UtcNow);

        foreach (var message in messages)
        {
            Console.WriteLine($"[ID: {message.Id}] {message.TimeStamp}: {message.Text}");
        }

        lastCheckedTime = DateTime.UtcNow;
        
        await Task.Delay(2500);
    }
}
