namespace ChatBotHub.WebApi.Models.Sessions;

public record SessionModel {
    public required Guid Id { get; init; }
    public required Guid BotId { get; init; }
    public required List<MessageModel> Messages { get; init; }
}
