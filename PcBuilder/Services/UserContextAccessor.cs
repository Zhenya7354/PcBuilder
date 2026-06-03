using PcBuilder.Models;
using PcBuilder.Services.Interfaces;
using System.Security.Claims;

namespace PcBuilder.Services;

public class UserContextAccessor(IHttpContextAccessor httpContextAccessor) : IUserContextAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    public UserContextResponse GetUserContext()
    {
        var httpContext = _httpContextAccessor.HttpContext ??
            throw new UnauthorizedAccessException();
        var userResponse = new UserContextResponse
        {
            UserId = httpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
            throw new UnauthorizedAccessException(),
            Email = httpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ??
            throw new UnauthorizedAccessException()
        };

        return userResponse;
    }
}
