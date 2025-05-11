using ChatBotHub.Application.Accounts.Exceptions;
using ChatBotHub.Application.Accounts.Mappers;
using ChatBotHub.Application.Accounts.Requests;
using ChatBotHub.Domain.Accounts;

namespace ChatBotHub.Application.Accounts;

public class AccountCommandService : IAccountCommandService {
    private readonly IAccountRepository _repository;
    private readonly IAccountQueryService _queryService;

    public AccountCommandService(
        IAccountRepository repository,
        IAccountQueryService queryService
    ) {
        _repository = repository;
        _queryService = queryService;
    }

    public async Task<Account> CreateAsync(SaveAccountRequest request) {
        var command = SaveAccountCommandMapper.Map(request);
        var account = AccountFactory.Create(command);
        await _repository.InsertAsync(account);

        return account;
    }

    public async Task<Account> UpdateAsync(Guid id, SaveAccountRequest request) {
        var account = await _queryService.RequireByIdAsync(id);
        
        account.SetName(request.Name);
        account.SetLastName(request.LastName);
        account.SetEmail(request.Email);
        account.SetHashPassword(request.HashPassword);

        await _repository.UpdateAsync(account);

        return account;

    }

    public async Task DeleteAsync(Guid id) {
        if (!await _repository.ExistsAsync(id)) {
            throw new AccountNotFoundException(id);
        }

        await _repository.DeleteAsync(id);
    }
}
