namespace ChatBotHub.Application.Accounts.Exceptions;

public class AccountNotFoundException : Exception {
    public AccountNotFoundException(Guid id) :
        base($"Account with id {id} is not found") {
        
    }

    public AccountNotFoundException(string email) :
        base($"Account with email {email} is not found") {
        
    }
}
