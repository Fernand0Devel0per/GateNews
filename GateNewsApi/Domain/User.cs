using Microsoft.AspNetCore.Identity;

namespace GateNewsApi.Domain
{

    public class User : IdentityUser<Guid>
    {
        public Author Author { get; set; }
    }
}
