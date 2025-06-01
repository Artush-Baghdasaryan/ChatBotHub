namespace ChatBotHub.WebApi.Models.Knowledge;

public record QueryKnowledgeRequestModel {
    public required string Query { get; init; }
    public Guid? SessionId { get; init; }
}
