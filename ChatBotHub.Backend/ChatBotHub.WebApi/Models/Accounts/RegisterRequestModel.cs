namespace ChatBotHub.WebApi.Models.Accounts;

public record RegisterRequestModel {
    public required string Name { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}
