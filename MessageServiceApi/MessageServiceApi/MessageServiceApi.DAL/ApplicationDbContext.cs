using Microsoft.Data.Sqlite;

namespace MessageServiceApi.DAL;

public class ApplicationDbContext
{
    private readonly string _connectionString;

    public ApplicationDbContext(string connectionString = "Data Source=messages.db")
    {
        _connectionString = connectionString;
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        string dbPath = Path.GetFullPath(_connectionString.Replace("Data Source=", ""));
        
        if (!File.Exists(dbPath))
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                CreateTables(connection);
            }
        }
    }

    private void CreateTables(SqliteConnection connection)
    {
        string createMessageTable = @"CREATE TABLE IF NOT EXISTS Messages (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Text TEXT NOT NULL,
                    Timestamp DATETIME NOT NULL
                );";

        using (var command = new SqliteCommand(createMessageTable, connection))
        {
            command.ExecuteNonQuery();
        }
    }

    public SqliteConnection GetConnection()
    {
        var connection = new SqliteConnection(_connectionString);
        connection.Open();
        return connection;
    }
}