using MessageSendClient.MessageSendClient.Controllers;

var handler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
};

using (var httpClient = new HttpClient(handler))
{
    httpClient.BaseAddress = new Uri("https://localhost:7221/api/Message");
    var messageController = new MessageController(httpClient);
    await messageController.RunAsync();
}
