namespace ChatBotHub.WebApi.Models.Common;

public record AuditableEntityModel : EntityModel {
    public AuditModel Audit { get; init; } = new();
}
