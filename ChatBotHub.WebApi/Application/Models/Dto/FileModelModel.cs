using System.Text.Json.Serialization;

namespace ChatBotHub.WebApi.Application.Models.Dto;

public record FileModelModel {
    [JsonPropertyName("file_name")]
    public required string FileName { get; init; }
    
    [JsonPropertyName("file_data")]
    public required byte[] FileData { get; init; }
    
    [JsonPropertyName("file_type")]
    public required string FileType { get; init; }
    
    [JsonPropertyName("upload_date")]
    public required DateTime UploadDate { get; init; }
}