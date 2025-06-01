using ChatBotHub.Domain.Common.Specifications;

namespace ChatBotHub.Domain.Attachments.Specifications;

public class AttachmentNameLengthSpecification : ISpecification<Attachment> {
    private const int MaxLength = 50;
    
    private readonly string _name;

    public AttachmentNameLengthSpecification(string name) {
        _name = name;
    }

    public SpecificationResult IsSatisfiedBy(Attachment attachment) {
        if (_name.Length > MaxLength) {
            return SpecificationResult.False($"Name {_name} is not valid for attachment {attachment.Id}");
        }

        return true;
    }
}
