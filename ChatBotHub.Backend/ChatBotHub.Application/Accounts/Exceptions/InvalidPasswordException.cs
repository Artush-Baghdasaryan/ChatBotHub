namespace ChatBotHub.Application.Accounts.Exceptions;

public class InvalidPasswordException : Exception {
    public InvalidPasswordException() : base("Invalid password") {
        
    }
}
