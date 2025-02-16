using ChatBotHub.WebApi.Infrastructure.Domain;
using ChatBotHub.WebApi.Infrastructure.Repositories;

namespace ChatBotHub.WebApi.Application.Services.Implementations;

public class UserService : BaseService<User>, IUserService {
    public UserService(UserRepository repository) : base(repository) {
    }

    public async Task<User> GetByEmailAsync(string email) {
        return await Repository.FindOneAsync(e => e.Email == email);
    }
}
