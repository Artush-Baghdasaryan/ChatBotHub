using System.Text.Json.Serialization;
using ChatBotHub.Domain.Session;

namespace ChatBotHub.Application.AiKnowledge.Models.Requests;

public record ExternalQueryKnowledgeRequest {

    [JsonPropertyName("chat_bot")]
    public required ExternalChatBotModel ChatBot { get; init; }

    [JsonPropertyName("query")]
    public required string Query { get; init; }
    
    [JsonPropertyName("chat_history")]
    public required List<ExternalMessageModel> ChatHistory { get; init; }
}
