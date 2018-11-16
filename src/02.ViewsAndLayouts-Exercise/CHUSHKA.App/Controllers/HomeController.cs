using System.Linq;
using CHUSHKA.App.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
namespace CHUSHKA.App.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var products = this.Db.Products.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    Name = p.Name,
                    Price = p.Price
                })
                .ToList();

                return this.View("LoggedInIndex", products);
            }
            else
            {
                return View();
            }
        }
    }
}