using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ChatBotHub.WebApi.Controllers;

[ApiController]
public class BaseController : ControllerBase {
    protected Guid GetAccountId() {
        var claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(claimValue) || !Guid.TryParse(claimValue, out var id)) {
            throw new UnauthorizedAccessException();
        }

        return id;
    }    
}
