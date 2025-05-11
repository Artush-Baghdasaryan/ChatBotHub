namespace ChatBotHub.Domain.Session;

public record Message {
    public required MessageRoleType Role { get; init; }
    public required string Content { get; init; }
    public DateTime CreatedAt { get; init; }

    public static Message Create(MessageRoleType role, string content) {
        return new Message {
            Role = role,
            Content = content,
            CreatedAt = DateTime.UtcNow
        };
    }
}
