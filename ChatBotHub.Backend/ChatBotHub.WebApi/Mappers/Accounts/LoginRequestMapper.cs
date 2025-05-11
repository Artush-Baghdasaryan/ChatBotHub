using ChatBotHub.Application.Accounts.Requests;
using ChatBotHub.WebApi.Models.Accounts;

namespace ChatBotHub.WebApi.Mappers.Accounts;

public static class LoginRequestMapper {
    public static LoginRequest Map(LoginRequestModel model) {
        return new LoginRequest {
            Email = model.Email,
            Password = model.Password,
        };
    }
}
