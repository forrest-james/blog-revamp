using Microsoft.AspNetCore.Identity;

namespace Domain.Persistence.Identity
{
    public class BlogUser
        : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}