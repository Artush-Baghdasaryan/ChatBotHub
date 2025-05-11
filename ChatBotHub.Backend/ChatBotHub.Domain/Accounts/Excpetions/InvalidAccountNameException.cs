namespace ChatBotHub.Domain.Accounts.Excpetions;

public class InvalidAccountNameException : Exception {
    public InvalidAccountNameException(string message) : base(message) {
        
    }
}
