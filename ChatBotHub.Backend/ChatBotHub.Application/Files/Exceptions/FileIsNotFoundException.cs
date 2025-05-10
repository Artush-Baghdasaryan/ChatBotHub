namespace ChatBotHub.Application.Files.Exceptions;

public class FileIsNotFoundException : Exception {
    public FileIsNotFoundException(Guid id) : 
        base($"File with id {id} is not found") {
        
    }
}
