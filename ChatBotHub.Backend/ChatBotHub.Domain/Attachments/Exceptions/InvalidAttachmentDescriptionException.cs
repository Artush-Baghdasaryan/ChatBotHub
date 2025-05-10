namespace ChatBotHub.Domain.Attachments.Exceptions;

public class InvalidAttachmentDescriptionException : Exception {
    public InvalidAttachmentDescriptionException(string message) : base(message) {
        
    }
}
