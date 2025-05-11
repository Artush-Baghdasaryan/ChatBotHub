namespace ChatBotHub.WebApi.Models.Accounts;

public record LoginResultModel {
    public required string Token { get; init; }
} 