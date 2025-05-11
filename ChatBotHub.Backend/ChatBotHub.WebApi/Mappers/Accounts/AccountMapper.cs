using ChatBotHub.Domain.Accounts;
using ChatBotHub.WebApi.Mappers.Common;
using ChatBotHub.WebApi.Models.Accounts;

namespace ChatBotHub.WebApi.Mappers.Accounts;

public static class AccountMapper {
    public static AccountModel Map(Account account) {
        return new AccountModel {
            Id = account.Id,
            Name = account.Name,
            LastName = account.LastName,
            Email = account.Email,
            Audit = AuditMapper.Map(account.Audit),
        };
    }
}
