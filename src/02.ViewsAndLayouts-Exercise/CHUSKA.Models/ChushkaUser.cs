using Microsoft.AspNetCore.Identity;

namespace CHUSKA.Models
{
    public class ChushkaUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
