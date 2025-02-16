namespace Infrastructure.Settings;

public record DatabaseSettings {
    public required string ConnectionString { get; init; }
    public required string DatabaseName { get; init; }
}
