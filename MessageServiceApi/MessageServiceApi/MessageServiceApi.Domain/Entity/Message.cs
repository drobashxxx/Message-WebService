namespace MessageServiceApi.MessageServiceApi.Domain;

public class Message
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime TimeStamp { get; set; }
}