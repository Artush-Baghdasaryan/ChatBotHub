using ChatBotHub.Domain.Common.Models;

namespace ChatBotHub.Domain.Session;

public class Session : AuditableEntity {
    public Session(Guid botId) {
        GenerateId();
        BotId = botId;
    }

    public Guid BotId { get; private set; }
    public List<Message> Messages { get; private set; } = [];

    public void AddMessage(Message message) {
        Messages.Add(message);
    }
}
