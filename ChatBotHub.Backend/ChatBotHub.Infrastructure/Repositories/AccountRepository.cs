using ChatBotHub.Domain.Accounts;
using ChatBotHub.Infrastructure.Common;
using ChatBotHub.Infrastructure.MongoDb;

namespace ChatBotHub.Infrastructure.Repositories;

public class AccountRepository : DataRepository<Account>, IAccountRepository {
    public AccountRepository(MongoDbDataContext context) : base(context, "accounts") {
    }
}
