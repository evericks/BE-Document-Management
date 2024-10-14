namespace Application.Settings;

public class RedisSettings
{
    public required bool Enabled { get; set; }
    public required string ConnectionString { get; set; }
}