using System.Security.Claims;

namespace api.Extensions;

public static  class ClaimsExtensions
{
    public static string GetUserId(this ClaimsPrincipal user)
    {
        return user.Claims
            .SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
            .Value;
    }
}