namespace ChatBotHub.Application.Accounts.Results;

public record LoginResult {
    public required string Token { get; init; }
}
