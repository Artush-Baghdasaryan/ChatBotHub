namespace Infrastructure.Settings;

public record RagServiceSettings {
    public required string BaseUrl { get; init; }
    public required string ApiKey { get; init; }
}