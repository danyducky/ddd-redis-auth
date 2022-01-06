using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Identity;

public class UserIdentity : IUserIdentity
{
    private readonly IHttpContextAccessor _accessor;

    public UserIdentity(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public string GetUserIdentity()
    {
        return _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public string GetUserEmail()
    {
        return _accessor.HttpContext.User.Identity.Name;
    }

    public IEnumerable<string> GetUserRoles()
    {
        return _accessor.HttpContext.User.FindAll(ClaimTypes.Role).Select(x => x.Value);
    }
}