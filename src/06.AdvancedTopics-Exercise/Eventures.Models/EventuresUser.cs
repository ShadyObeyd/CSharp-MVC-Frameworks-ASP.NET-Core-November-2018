using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Eventures.Models
{
    public class EventuresUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UCN { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
