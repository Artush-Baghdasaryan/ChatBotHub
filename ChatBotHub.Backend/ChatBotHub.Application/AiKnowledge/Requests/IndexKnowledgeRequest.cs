namespace ChatBotHub.Application.AiKnowledge.Requests;

public record IndexKnowledgeRequest {
    public required Guid AttachmentId { get; init; }
    public required string FileName { get; init; }
    public required byte[] FileData { get; init; }
    public required string FileType { get; init; }
    public required string Description { get; init; }
}
