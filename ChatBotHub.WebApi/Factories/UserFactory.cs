using ChatBotHub.WebApi.Dto;
using ChatBotHub.WebApi.Infrastructure.Domain;

namespace ChatBotHub.WebApi.Factories;

public static class UserFactory {
    public static UserDto CreateUserDto(User user) {
        return new UserDto {
            Username = user.Username,
            Email = user.Email
        };
    }
}