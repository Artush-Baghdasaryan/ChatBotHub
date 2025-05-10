using ChatBotHub.Domain.Accounts.Commands;

namespace ChatBotHub.Domain.Accounts;

public static class AccountFactory {
    public static Account Create(SaveAccountCommand command) {
        Account.ValidateName(command.Name);
        Account.ValidateLastName(command.LastName);
        Account.ValidateEmail(command.Email);

        return new() {
            Name = command.Name,
            LastName = command.LastName,
            Email = command.Email,
            HashPassword = command.HashPassword
        };
    }
}
