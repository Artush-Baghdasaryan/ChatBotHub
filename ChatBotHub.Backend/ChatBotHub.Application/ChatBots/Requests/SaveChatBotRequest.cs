using ChatBotHub.Application.Attachments.Requests;

namespace ChatBotHub.Application.ChatBots.Requests;

public record SaveChatBotRequest {
    public required string Name { get; init; }
    public required string Description { get; init; }
}
