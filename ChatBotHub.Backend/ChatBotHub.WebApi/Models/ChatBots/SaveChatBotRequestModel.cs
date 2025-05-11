namespace ChatBotHub.WebApi.Models.ChatBots;

public record SaveChatBotRequestModel {
    public required string Name { get; init; }
    public required string Description { get; init; }
}
