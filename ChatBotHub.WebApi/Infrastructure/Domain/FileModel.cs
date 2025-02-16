namespace ChatBotHub.WebApi.Infrastructure.Domain;

public class FileModel : BaseModel {
    public required string FileName { get; set; }
    public required byte[] FileData { get; set; }
    public required string FileType { get; set; }
    public required DateTime UploadDate { get; set; }
}