namespace Application.Models.Requests;

public record AttachmentCreateModel {
    public required Guid FileId { get; init; }
    public required string Description { get; init; }
}