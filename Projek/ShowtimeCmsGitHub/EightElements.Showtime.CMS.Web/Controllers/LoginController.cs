using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using System.Net;
using System.Web.Http;
using EightElements.Showtime.CMS.Data;
using EightElements.Showtime.CMS.Web;
using Newtonsoft.Json;


namespace EightElements.Showtime.CMS.Web.Controllers
{
    public class LoginController : Controller
    {

        private ShowtimeEntities objShowtimeEntities;
        public LoginController()
        {
            objShowtimeEntities = new ShowtimeEntities();
        }


        // GET: Login
        public ActionResult Index()
        {
            Session["errorMessage"] = null;
            return View();
        }

       
        [System.Web.Mvc.HttpPost]
 
        public ActionResult Authorize(UserAdmin userModel)
        {
            using (ShowtimeEntities db = new ShowtimeEntities())
            {
                /*Dont forget to start your vpn*/
                Repositories.Authorize(userModel);
                var userDetails = db.UserAdmins.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                Session["errorMessage"] = null;
                if (userDetails == null)
                {
                    /*userModel.LoginErrormessage = "Wrong Username or Password.";*/
                    Session["errorMessage"] = "Wrong Username or Password.";
                    
                    //ViewData["Message"] = "Wrong Username or Password.";
                    return View("index");

                }
                else
                {
                    Session["userID"] = userDetails.ID;
                    Session["userName"] = userDetails.UserName;
                    return RedirectToAction("index", "Home");
                }
            }
            return View();
        }

        
        
        public ActionResult LogOut()
        {
            if (Session["userID"] != null)
            {
                int userId = (int)Session["userID"];
                Session.Abandon();
                return RedirectToAction("index", "Login");
            }
            
            return RedirectToAction("index", "Login");
        }


        public JsonResult AddUpdateuserAdmin(UserAdmin objUserAdmin)
        {
            string Message = "Data SuccessFully Added";
            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState
                                 where item.Value.Errors.Any()
                                 select item.Value.Errors[0].ErrorMessage).ToList();

                return Json(new { success = false, Message = "Some Problem in validation", errorList });
            }
            if(objUserAdmin != null) Repositories.AddUserAdmins(objUserAdmin);
            return Json(new { Success = true, Message = Message }, JsonRequestBehavior.AllowGet);
       
        }

    }
}