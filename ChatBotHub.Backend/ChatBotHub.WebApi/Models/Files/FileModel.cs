using ChatBotHub.WebApi.Models.Common;

namespace ChatBotHub.WebApi.Models.Files;

public record FileModel : AuditableEntityModel {
    public required string FileName { get; init; }
    public required string FileType { get; init; }
}
