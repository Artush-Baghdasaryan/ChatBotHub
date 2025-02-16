using System.Text.Json.Serialization;

namespace ChatBotHub.WebApi.Application.Models.Dto;

public record AttachmentModel {
    [JsonPropertyName("file")]
    public required FileModelModel File { get; init; }
    
    [JsonPropertyName("description")]
    public required string Description { get; init; }
}