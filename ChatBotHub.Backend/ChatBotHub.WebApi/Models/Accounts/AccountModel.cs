using ChatBotHub.WebApi.Models.Common;

namespace ChatBotHub.WebApi.Models.Accounts;

public record AccountModel : AuditableEntityModel {
    public required string Name { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
}
