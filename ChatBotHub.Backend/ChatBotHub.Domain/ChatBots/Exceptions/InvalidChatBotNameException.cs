namespace ChatBotHub.Domain.ChatBots.Exceptions;

public class InvalidChatBotNameException : Exception{
    public InvalidChatBotNameException() : base("Invalid chat bot name") {
        
    }

    public InvalidChatBotNameException(string message) : base(message) {
        
    }
}
