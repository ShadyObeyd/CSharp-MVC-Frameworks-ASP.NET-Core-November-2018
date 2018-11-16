using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FDMC.App.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            var cats = this.Db.Cats.ToList();

            return this.View(cats);
        }
    }
}
