using Microsoft.AspNetCore.Identity;

namespace GateNewsApi.Domain
{

    public class User : IdentityUser
    {
        public Author Author { get; set; }
    }
}
