using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PANDA.Data;

namespace PANDA.App.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            this.Db = new PandaContext();
        }

        protected PandaContext Db { get; }
    }
}