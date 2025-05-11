using ChatBotHub.Application.Accounts.Results;
using ChatBotHub.WebApi.Models.Accounts;

namespace ChatBotHub.WebApi.Mappers.Accounts;

public static class LoginResultMapper {
    public static LoginResultModel Map(LoginResult result) {
        return new LoginResultModel {
            Token = result.Token
        };
    }
} 