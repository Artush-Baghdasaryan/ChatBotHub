using ChatBotHub.Domain.Common.Models;

namespace ChatBotHub.Domain.Files;

public class File : AuditableEntity {
    public File() {
        GenerateId();
    }

    public required string FileName { get; set; }
    public required byte[] FileData { get; set; }
    public required string FileType { get; set; }
}
