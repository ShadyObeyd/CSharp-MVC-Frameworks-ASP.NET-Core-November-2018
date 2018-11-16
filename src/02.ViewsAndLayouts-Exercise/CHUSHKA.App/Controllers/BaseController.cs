using CHUSKA.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CHUSHKA.App.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            this.Db = new ChuskaDbContext(new DbContextOptions<ChuskaDbContext>());
        }

        protected ChuskaDbContext Db { get; }
    }
}