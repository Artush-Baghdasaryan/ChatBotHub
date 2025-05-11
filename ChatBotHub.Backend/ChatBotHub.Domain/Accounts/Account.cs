using ChatBotHub.Domain.Accounts.Excpetions;
using ChatBotHub.Domain.Accounts.Specifications;
using ChatBotHub.Domain.Common.Models;

namespace ChatBotHub.Domain.Accounts;

public class Account : AuditableEntity {
    public Account() {
        GenerateId();
    }

    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string HashPassword { get; set; }

    public void SetName(string name) {
        if (Name == name) {
            return;
        }
        
        ValidateName(name);
        Name = name;
    }

    public void SetLastName(string lastName) {
        if (LastName == lastName) {
            return;
        }
        
        ValidateLastName(lastName);
        LastName = lastName;
    }

    public void SetEmail(string email) {
        if (Email == email) {
            return;
        }
        
        ValidateEmail(email);
        Email = email;
    }

    public void SetHashPassword(string hashPassword) {
        HashPassword = hashPassword;
    }

    public static void ValidateName(string name) {
        var firstNameSpec = AccountNameFormatValidity.IsSatisfiedBy(name);
        if (!firstNameSpec.Value) {
            throw new InvalidAccountNameException(firstNameSpec.Message);
        }
    }

    public static void ValidateLastName(string lastName) {
        var lastNameSpec = AccountNameFormatValidity.IsSatisfiedBy(lastName);
        if (!lastNameSpec.Value) {
            throw new InvalidAccountNameException(lastNameSpec.Message);
        }
    }

    public static void ValidateEmail(string email) {
        var emailSpec = AccountEmailFormatValidity.IsSatisfiedBy(email);
        if (!emailSpec.Value) {
            throw new InvalidAccountEmailException(emailSpec.Message);
        }
    }
}
