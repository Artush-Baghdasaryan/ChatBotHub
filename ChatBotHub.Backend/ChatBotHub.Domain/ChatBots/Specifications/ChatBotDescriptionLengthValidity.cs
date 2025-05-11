using ChatBotHub.Domain.Common.Specifications;

namespace ChatBotHub.Domain.ChatBots.Specifications;

public class ChatBotDescriptionLengthValidity : ISpecification<ChatBot> {
    private const int MaxLength = 1000;

    private readonly string _description;

    public ChatBotDescriptionLengthValidity(string description) {
        _description = description;
    }
    
    public SpecificationResult IsSatisfiedBy(ChatBot entity) {
        if (_description.Length > MaxLength) {
            return SpecificationResult.False($"Description is invalid for bot with id {entity.Id}");
        }

        return true;
    }
}
