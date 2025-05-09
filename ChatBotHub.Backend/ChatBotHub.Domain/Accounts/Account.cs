using ChatBotHub.Domain.Common.Models;

namespace ChatBotHub.Domain.Accounts;

public class Account : AuditableEntity {
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string HashPassword { get; set; }
}
