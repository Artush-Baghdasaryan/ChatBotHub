using ChatBotHub.Domain.Session;

namespace ChatBotHub.Application.AiKnowledge.Requests;

public record QueryRequest {
    public required string Query { get; init; }
    public List<Message>? Context { get; init; }
}
