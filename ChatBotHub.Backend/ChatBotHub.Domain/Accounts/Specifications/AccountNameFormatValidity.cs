using ChatBotHub.Domain.Common.Specifications;
using System.Text.RegularExpressions;

namespace ChatBotHub.Domain.Accounts.Specifications;

public class AccountNameFormatValidity {
    private const int MaxLength = 50;
    private static readonly Regex NameRegex = new(@"^[a-zA-Z\-]+$", RegexOptions.Compiled);
    
    public static SpecificationResult IsSatisfiedBy(string name) {
        if (string.IsNullOrWhiteSpace(name)) {
            return SpecificationResult.False("Name cannot be empty");
        }

        if (name.Length > MaxLength) {
            return SpecificationResult.False($"Name cannot be longer than {MaxLength} characters");
        }

        if (!NameRegex.IsMatch(name)) {
            return SpecificationResult.False("Name can only contain letters and hyphens");
        }

        return true;
    }
}
