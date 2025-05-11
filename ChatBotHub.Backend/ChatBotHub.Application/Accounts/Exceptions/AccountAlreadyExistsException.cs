namespace ChatBotHub.Application.Accounts.Exceptions;

public class AccountAlreadyExistsException : Exception {
    public AccountAlreadyExistsException(string email) : base($"Account with email {email} already exists") {
        
    }
}
