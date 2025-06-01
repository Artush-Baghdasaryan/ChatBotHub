namespace ChatBotHub.Application.AiKnowledge.Requests;

public record QueryKnowledgeRequest {
    public required string Query { get; init; }
}
