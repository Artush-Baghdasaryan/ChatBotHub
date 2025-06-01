namespace ChatBotHub.Domain.Session;

public record Message {
    public required MessageRoleType Role { get; init; }
    public required string Content { get; init; }
    public DateTime DateTime { get; private init; }

    public static Message Create(MessageRoleType role, string content) {
        return new Message {
            Role = role,
            Content = content,
            DateTime = DateTime.UtcNow
        };
    }
}
