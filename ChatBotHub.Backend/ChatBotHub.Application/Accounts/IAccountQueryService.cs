using ChatBotHub.Domain.Accounts;

namespace ChatBotHub.Application.Accounts;

public interface IAccountQueryService {
    Task<Account> RequireByIdAsync(Guid id);
    Task<Account?> GetByEmailAsync(string email);
}
