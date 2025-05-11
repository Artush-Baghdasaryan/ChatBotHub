using ChatBotHub.Application.Accounts.Requests;
using ChatBotHub.WebApi.Models.Accounts;

namespace ChatBotHub.WebApi.Mappers.Accounts;

public static class RegisterRequestMapper {
    public static RegisterRequest Map(RegisterRequestModel model) {
        return new RegisterRequest {
            Name = model.Name,
            LastName = model.LastName,
            Email = model.Email,
            Password = model.Password,
        };
    }
}
