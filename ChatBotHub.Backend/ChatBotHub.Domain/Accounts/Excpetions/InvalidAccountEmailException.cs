namespace ChatBotHub.Domain.Accounts.Excpetions;

public class InvalidAccountEmailException : Exception {
    public InvalidAccountEmailException(string message) : base(message) {
        
    }
}
