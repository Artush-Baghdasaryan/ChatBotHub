namespace ChatBotHub.WebApi.Models.Sessions;

public record MessageModel {
    public required MessageRoleTypeModel Role { get; init; }
    public required string Content { get; init; }
    public DateTime DateTime { get; init; }
}
