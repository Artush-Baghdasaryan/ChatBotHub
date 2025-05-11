
namespace ChatBotHub.Application.Attachments.Exceptions;

public class AttachmentIsNotFoundException : Exception {
    public AttachmentIsNotFoundException(Guid id) : 
        base($"Attachment with id {id} is not found") {
        
    }
}
