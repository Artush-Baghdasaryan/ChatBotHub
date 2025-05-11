
using ChatBotHub.Domain.Session;

namespace ChatBotHub.Application.Sessions.Requests;

public record AddMessageRequest(string Content, MessageRoleType Role);
