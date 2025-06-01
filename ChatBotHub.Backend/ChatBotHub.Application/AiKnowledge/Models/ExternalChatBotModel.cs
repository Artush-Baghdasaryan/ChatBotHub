using System.Text.Json.Serialization;

namespace ChatBotHub.Application.AiKnowledge.Models;

public record ExternalChatBotModel {
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("name")]
    public required string  Name { get; set; }

    [JsonPropertyName("description")]
    public required string  Description { get; set; }

    [JsonPropertyName("attachments")]
    public required List<ExternalAttachmentModel> Attachments { get; init; }
}
