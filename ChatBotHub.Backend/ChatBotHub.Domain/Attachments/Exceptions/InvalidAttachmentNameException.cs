namespace ChatBotHub.Domain.Attachments.Exceptions;

public class InvalidAttachmentNameException : Exception {
    public InvalidAttachmentNameException(string message) : base(message) {
        
    }
}
