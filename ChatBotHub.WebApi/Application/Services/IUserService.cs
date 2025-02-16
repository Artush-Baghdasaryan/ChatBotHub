using ChatBotHub.WebApi.Infrastructure.Domain;

namespace ChatBotHub.WebApi.Application.Services;

public interface IUserService : IBaseService<User> {
    Task<User> GetByEmailAsync(string email);
}