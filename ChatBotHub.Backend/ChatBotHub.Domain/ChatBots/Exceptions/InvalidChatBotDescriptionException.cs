namespace ChatBotHub.Domain.ChatBots.Exceptions;

public class InvalidChatBotDescriptionException : Exception{
    public InvalidChatBotDescriptionException() : 
        base("Invalid chat bot description") {
        
    }

    public InvalidChatBotDescriptionException(string message) : base(message) {
        
    }
}
