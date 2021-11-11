using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace Penggajian.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(User userModel)
        {
            using (DataEntities db = new DataEntities())
            {
                var logger = db.Users.Where(x => x.username == userModel.username && x.password == userModel.password).FirstOrDefault();
                if (logger == null)
                {
                    Session["errorMessage"] = "Wrong Username Or Password";
                    return View("index");
                }
                else 
                {
                    Session["userID"] = userModel.Id;
                    Session["userName"] = userModel.username;
                    return RedirectToAction("index", "Home");
                }

            }
            return View();//File Not Created
        }
        public ActionResult Logout()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("index","Login");
        }

    }

}