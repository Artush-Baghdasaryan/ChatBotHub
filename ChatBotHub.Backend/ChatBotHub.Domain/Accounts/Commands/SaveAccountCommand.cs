namespace ChatBotHub.Domain.Accounts.Commands;

public class SaveAccountCommand {
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string HashPassword { get; set; }
}
