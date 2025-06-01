using System.Text.Json.Serialization;
using ChatBotHub.Domain.Session;

namespace ChatBotHub.Application.AiKnowledge.Models;

public record ExternalMessageModel {
    [JsonPropertyName("role")]
    public MessageRoleType Role { get; init; }
    
    [JsonPropertyName("content")]
    public required string Content { get; set; }
}
