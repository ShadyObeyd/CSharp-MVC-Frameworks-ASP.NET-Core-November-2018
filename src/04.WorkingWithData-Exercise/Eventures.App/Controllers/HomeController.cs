using Microsoft.AspNetCore.Mvc;
namespace Eventures.App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.View("LoggedInIndex");
            }
            else
            {
                return View();
            }
        }
    }
}