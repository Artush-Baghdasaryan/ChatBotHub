namespace ChatBotHub.Domain.ChatBots.Commands;

public record SaveChatBotCommand {
    public required string Name { get; init; }
    public required string Description { get; init; }
}
