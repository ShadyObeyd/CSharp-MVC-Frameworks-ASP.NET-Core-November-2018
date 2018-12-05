using Eventures.Models;
using System.Collections.Generic;

namespace Eventures.App.ViewModels.Users
{
    public class AllUsersViewModel
    {
        public IEnumerable<UserViewModel> Admins { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
