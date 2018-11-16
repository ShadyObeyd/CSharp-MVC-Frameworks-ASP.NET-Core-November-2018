using FDMC.App.ViewModels.Cats;
using FDMC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FDMC.App.Controllers
{
    public class CatsController : BaseController
    {
        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(CatInputModel model)
        {
            var cat = new Cat
            {
                Name = model.Name,
                Age = model.Age,
                Breed = model.Breed,
                ImageUrl = model.ImageUrl
            };

            this.Db.Cats.Add(cat);
            this.Db.SaveChanges();

            return this.Redirect("/");
        }

        [HttpGet]
        public IActionResult Cat(int id)
        {
            var cat = this.Db.Cats.Where(c => c.Id == id).Select(c => new CatViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Age = c.Age,
                Breed = c.Breed,
                ImageUrl = c.ImageUrl
            }).FirstOrDefault();

            return this.View(cat);
        }
    }
}