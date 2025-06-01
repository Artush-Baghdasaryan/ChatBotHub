using System.Text.Json.Serialization;

namespace ChatBotHub.Application.AiKnowledge.Models.Requests;

public record IndexKnowledgeRequest {
    [JsonPropertyName("attachment_id")]
    public required Guid AttachmentId { get; init; }
    
    [JsonPropertyName("file_name")]
    public required string FileName { get; init; }
    
    [JsonPropertyName("file_data")]
    public required byte[] FileData { get; init; }
    
    [JsonPropertyName("file_type")]
    public required string FileType { get; init; }
    
    [JsonPropertyName("description")]
    public required string Description { get; init; }
}
