namespace ChatBotHub.Application.Sessions.Exceptions;

public class SessionNotFoundException : Exception {
    public SessionNotFoundException(Guid id) : base($"Session with id {id} not found") {
    }
}
