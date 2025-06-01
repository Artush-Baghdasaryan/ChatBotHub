using System.Text.Json.Serialization;

namespace ChatBotHub.Application.AiKnowledge.Models.Requests;

public record ExternalQueryKnowledgeRequest {

    [JsonPropertyName("chat_bot")]
    public required ExternalChatBotModel ChatBot { get; init; }

    [JsonPropertyName("query")]
    public required string Query { get; init; }
    
    [JsonPropertyName("session_id")]
    public Guid? SessionId { get; init; }
}
