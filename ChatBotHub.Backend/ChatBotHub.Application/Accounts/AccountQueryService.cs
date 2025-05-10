using ChatBotHub.Application.Accounts.Exceptions;
using ChatBotHub.Domain.Accounts;

namespace ChatBotHub.Application.Accounts;

public class AccountQueryService : IAccountQueryService {
    private readonly IAccountRepository _repository;

    public AccountQueryService(IAccountRepository repository) {
        _repository = repository;
    }

    public async Task<Account> RequireByIdAsync(Guid id) {
        return await _repository.GetByIdAsync(id) ??
            throw new AccountNotFoundException(id);
    }

    public Task<Account?> GetByEmailAsync(string email) {
        return _repository.GetByEmailAsync(email);
    }
}
