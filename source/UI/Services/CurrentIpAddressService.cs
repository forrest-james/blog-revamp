using Microsoft.AspNetCore.Http;

namespace UI.Services
{
    public class CurrentIpAddressService
    {
        public CurrentIpAddressService(IHttpContextAccessor httpContextAccessor) => IpAddress = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress.ToString();
        
        public string IpAddress { get; set; }
    }
}