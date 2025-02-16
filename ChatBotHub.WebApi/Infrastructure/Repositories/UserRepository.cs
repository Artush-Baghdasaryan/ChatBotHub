using ChatBotHub.WebApi.Infrastructure.Context;
using ChatBotHub.WebApi.Infrastructure.Domain;

namespace ChatBotHub.WebApi.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User> {
    public UserRepository(DataContext context) : base(context, "users") {
    }
}
