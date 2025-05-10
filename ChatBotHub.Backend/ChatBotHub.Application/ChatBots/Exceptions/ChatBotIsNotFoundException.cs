namespace ChatBotHub.Application.ChatBots.Exceptions;

public class ChatBotIsNotFoundException : Exception {
    public ChatBotIsNotFoundException(Guid id) : 
        base($"Chat bot with id {id} is not found") {
        
    }
}
