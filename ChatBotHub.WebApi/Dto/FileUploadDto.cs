namespace ChatBotHub.WebApi.Dto;

public class FileUploadDto {
    public required IFormFile File { get; set; }
    public required string Description { get; set; }
}