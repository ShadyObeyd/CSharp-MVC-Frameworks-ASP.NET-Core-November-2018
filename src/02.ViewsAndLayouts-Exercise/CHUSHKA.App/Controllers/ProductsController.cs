using System;
using System.Linq;
using CHUSHKA.App.ViewModels.Products;
using CHUSKA.Models;
using CHUSKA.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CHUSHKA.App.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly UserManager<ChushkaUser> userManager;

        public ProductsController(UserManager<ChushkaUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(ProductCreateViewModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Type = Enum.Parse<ProductType>(model.Type)
            };

            this.Db.Products.Add(product);
            this.Db.SaveChanges();

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var product = this.Db.Products.Where(p => p.Id == id).Select(p => new ProductDetailsViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Type = p.Type
            })
            .FirstOrDefault();

            if (product == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return View(product);
        }

        [Authorize]
        public IActionResult Order(int id)
        {
            var product = this.Db.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return this.RedirectToAction("Details", "Products");
            }

            string userId = this.userManager.GetUserId(HttpContext.User);

            var user = this.Db.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return this.RedirectToAction("Details", "Products");
            }

            var order = new Order
            {
                Product = product,
                Client = user,
                OrderedOn = DateTime.Now
            };

            this.Db.Orders.Add(order);
            this.Db.SaveChanges();

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = this.Db.Products.FirstOrDefault(p => p.Id == id);

            return this.View(product);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(Product productInput)
        {
            var product = this.Db.Products.FirstOrDefault(p => p.Id == productInput.Id);

            product.Name = productInput.Name;
            product.Description = productInput.Description;
            product.Price = productInput.Price;
            product.Type = productInput.Type;

            this.Db.Products.Update(product);
            this.Db.SaveChanges();

            return this.RedirectToAction("Details", "Products");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = this.Db.Products.FirstOrDefault(p => p.Id == id);

            return this.View(product);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            this.Db.Products.Remove(product);
            this.Db.SaveChanges();

            return this.RedirectToAction("Index", "Home");
        }
    }
}