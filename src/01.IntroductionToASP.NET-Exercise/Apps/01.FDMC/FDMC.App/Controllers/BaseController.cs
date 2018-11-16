using FDMC.Data;
using Microsoft.AspNetCore.Mvc;

namespace FDMC.App.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            this.Db = new FdmcContext();
        }

        public FdmcContext Db { get; set; }
    }
}
