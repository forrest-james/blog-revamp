using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace UI.Services
{
    public class CurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor) => UserId = int.TryParse(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : 0;
        public int UserId { get; set; }
    }
}
