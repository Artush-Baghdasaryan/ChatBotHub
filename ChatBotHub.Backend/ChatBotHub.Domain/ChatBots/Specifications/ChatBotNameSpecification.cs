using ChatBotHub.Domain.Common.Specifications;

namespace ChatBotHub.Domain.ChatBots.Specifications;

public class ChatBotNameSpecification : ISpecification<ChatBot> {
    private const int MaxLength = 32;

    private readonly string _name;

    public ChatBotNameSpecification(string name) {
        _name = name;
    }

    public SpecificationResult IsSatisfiedBy(ChatBot bot) {
        if (_name.Length > MaxLength) {
            return SpecificationResult.False($"Name is not valid for bot with Id {bot.Id}");
        }

        return true;
    }
}
