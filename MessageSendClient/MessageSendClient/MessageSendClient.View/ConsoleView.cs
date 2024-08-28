namespace MessageSendClient.MessageSendClient.View;

public static class ConsoleView 
{
    public static string GetMessageFromUser()
    {
        Console.Write("Введите сообщение (до 128 символов): ");
        return Console.ReadLine();
    }

    public static void DisplayMessageSent(int messageId)
    {
        Console.WriteLine($"Сообщение {messageId} успешно отправлено.");
    }

    public static void DisplayError(string errorMessage)
    {
        Console.WriteLine($"Ошибка: {errorMessage}");
    }
}