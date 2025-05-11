using ChatBotHub.Domain.Common.Specifications;

namespace ChatBotHub.Domain.Attachments.Specifications;

public class AttachmentDescriptionSpecification : ISpecification<Attachment> {
    private const int MaxLength = 1000;

    private readonly string _description;

    public AttachmentDescriptionSpecification(string description) {
        _description = description;
    }

    public SpecificationResult IsSatisfiedBy(Attachment entity) {
        if (_description.Length > MaxLength) {
            return SpecificationResult.False($"Description is not valid for attachment with id {entity.Id}");
        }

        return true;
    }
}
