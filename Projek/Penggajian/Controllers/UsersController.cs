using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Penggajian.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
        /* public ActionResult Home()
         {
             return View("../Home/Index");
         }*/

        public ActionResult Home()
        {
            return View();
        }

    }
}