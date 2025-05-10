namespace ChatBotHub.Domain.ChatBots.Exceptions;

public class InvalidAttachmentException : Exception{
    public InvalidAttachmentException(string message) :
        base(message) {
        
    }
}
