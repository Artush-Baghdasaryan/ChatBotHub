using System.Diagnostics.CodeAnalysis;
using ChatBotHub.Domain.Session;
using ChatBotHub.WebApi.Models.Sessions;

namespace ChatBotHub.WebApi.Mappers.Sessions;

public static class SessionMapper {
    [return: NotNullIfNotNull("session")]
    public static SessionModel? Map(Session? session) {
        if (session is null) return null;

        return new SessionModel {
            Id = session.Id,
            BotId = session.BotId,
            Messages = session.Messages.Select(MessageMapper.Map).ToList()
        };
    }
}
