using Microsoft.AspNetCore.Http;

namespace eCommerce.Shared.Cores.Sessions;

public class EcommerceSession : IEcommerceSession
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public EcommerceSession(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserId
    {
        get
        {
            var id =_httpContextAccessor.HttpContext?.User.Claims
                .Where(x => x.Type == "Id")
                .Select(x => x.Value)
                .FirstOrDefault();
            if (int.TryParse(id, out var userId))
            {
                return userId;
            }

            return null;
        }
    }


    public string RoleName => _httpContextAccessor.HttpContext.User.Claims
        .Where(x => x.Type == "Role")
        .Select(x => x.Value)
        .FirstOrDefault();
}