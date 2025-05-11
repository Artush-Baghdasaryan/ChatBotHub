using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using ChatBotHub.Domain.Common.Specifications;

namespace ChatBotHub.Domain.Accounts.Specifications;

public static class AccountEmailFormatValidity {
    private const int MaxLength = 254;
    private static readonly Regex EmailRegex = new(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled);

    public static SpecificationResult IsSatisfiedBy(string email) {

        if (string.IsNullOrWhiteSpace(email)) {
            return SpecificationResult.False("Email cannot be empty");
        }

        if (email.Length > MaxLength) {
            return SpecificationResult.False($"Email cannot be longer than {MaxLength} characters");
        }

        if (!EmailRegex.IsMatch(email)) {
            return SpecificationResult.False("Invalid email address");
        }

        return true;
    }
}
