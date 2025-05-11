namespace ChatBotHub.WebApi.Models.Common;

public record AuditModel {
    public DateTime CreatedOn { get; init; }
    public DateTime ModifiedOn { get; init; }
}
