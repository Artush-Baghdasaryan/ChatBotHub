using System.Text.Json.Serialization;

namespace ChatBotHub.Application.AiKnowledge.Models;

public record ExternalAttachmentModel {
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("description")]
    public required string  Description { get; init; }
}
