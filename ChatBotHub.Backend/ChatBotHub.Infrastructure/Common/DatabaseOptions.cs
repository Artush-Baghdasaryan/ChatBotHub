namespace ChatBotHub.Infrastructure.Common;

public record DatabaseOptions {
    public required string ConnectionString { get; init; }
    public required string DatabaseName { get; init; }
}
