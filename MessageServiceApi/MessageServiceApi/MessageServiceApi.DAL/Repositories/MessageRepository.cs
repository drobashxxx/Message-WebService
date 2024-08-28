using MessageServiceApi.DAL;
using MessageServiceApi.MessageServiceApi.DAL.Interface;
using MessageServiceApi.MessageServiceApi.Domain;
using Microsoft.Data.Sqlite;

namespace MessageServiceApi.MessageServiceApi.DAL;

public class MessageRepository : IMessageRepository
{
    private readonly ApplicationDbContext _context;

    public MessageRepository()
    {
        _context = new ApplicationDbContext();
    }
    
    public async Task AddMessageAsync(Message message)
    {
        using (var connection = _context.GetConnection())
        {
            var command = new SqliteCommand("INSERT INTO Messages (Text, Timestamp) VALUES (@text, @timestamp)", connection);
            command.Parameters.AddWithValue("@text", message.Text);
            command.Parameters.AddWithValue("@timestamp", message.TimeStamp);
            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task<IEnumerable<Message>> GetMessageAsync(DateTime from, DateTime to)
    {
        var messages = new List<Message>();

        using (var connection = _context.GetConnection())
        {
            var command = new SqliteCommand("SELECT * FROM Messages WHERE Timestamp BETWEEN @from AND @to ORDER BY Timestamp", connection);
            command.Parameters.AddWithValue("@from", from);
            command.Parameters.AddWithValue("@to", to);

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    messages.Add(new Message
                    {
                        Id = reader.GetInt32(0),
                        Text = reader.GetString(1),
                        TimeStamp = reader.GetDateTime(2)
                    });
                }
            }
        }

        return messages;
    }
}