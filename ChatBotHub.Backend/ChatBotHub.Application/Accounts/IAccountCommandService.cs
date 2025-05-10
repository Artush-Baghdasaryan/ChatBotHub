using ChatBotHub.Application.Accounts.Requests;
using ChatBotHub.Domain.Accounts;

namespace ChatBotHub.Application.Accounts;

public interface IAccountCommandService {
    Task<Account> CreateAsync(SaveAccountRequest request);
    Task<Account> UpdateAsync(Guid id, SaveAccountRequest request);
    Task DeleteAsync(Guid id);
}
