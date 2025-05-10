namespace ChatBotHub.Application.Accounts.Requests;

public class SaveAccountRequest {
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string HashPassword { get; set; }
}
