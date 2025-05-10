using ChatBotHub.Application.Accounts.Requests;
using ChatBotHub.Domain.Accounts.Commands;

namespace ChatBotHub.Application.Accounts.Mappers;

public static class SaveAccountCommandMapper {
    public static SaveAccountCommand Map(SaveAccountRequest request) {
        return new() {
            Name = request.Name,
            LastName = request.LastName,
            Email = request.Email,
            HashPassword = request.HashPassword
        };
    } 
}
