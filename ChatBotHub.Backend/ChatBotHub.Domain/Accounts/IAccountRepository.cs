using ChatBotHub.Domain.Common.Repositories;

namespace ChatBotHub.Domain.Accounts;

public interface IAccountRepository : IRepository<Account> {
    Task<Account?> GetByEmailAsync(string email);
}
