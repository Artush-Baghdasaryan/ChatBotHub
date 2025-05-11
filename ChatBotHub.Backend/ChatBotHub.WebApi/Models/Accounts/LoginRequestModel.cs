namespace ChatBotHub.WebApi.Models.Accounts;

public record LoginRequestModel {
    public LoginRequestModel(string email, string password) {
        Email = email;
        Password = password;
    }

    public required string Email { get; init; }
    public required string Password { get; init; }
}
