namespace ChatBotHub.Application.AiKnowledge.Models.Requests;

public record QueryKnowledgeRequest {
    public required string Query { get; init; }
    public Guid? SessionId { get; init; }
}
