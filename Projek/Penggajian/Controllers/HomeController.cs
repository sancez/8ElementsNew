using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;
using Penggajian.Models;
using Penggajian.Repositori;

namespace Penggajian.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        private DataEntities DataEntities;//Data is the name of Database and you add name Entities
        public HomeController()
        {
            DataEntities = new DataEntities();
        }


        public ActionResult GetAllUser()

        {

            var varUser = (from objUser in DataEntities.Users orderby objUser.Id descending where objUser.role == "2"
                           select new
                           {
                               idCon = objUser.Id,
                               username = objUser.username,
                               password = objUser.password,
                               role = objUser.role
                           }
                ).ToList();

            return Json(new { Success = true, data = varUser }, JsonRequestBehavior.AllowGet);
        }
        /*public string PostingUsingRequest()
        {
            string cekIdvar = Request["cekId"];
            return cekIdvar;
        }*/
        public ActionResult FilterRole(string rolevar)
        {
            var varUser = (from objUser in DataEntities.Users
                           orderby objUser.Id descending
                           where objUser.role == rolevar || objUser.username.Contains("test like SQL") 
                           select new
                           {
                               idCon = objUser.Id,
                               username = objUser.username,
                               password = objUser.password,
                               role = objUser.role
                           }
                ).ToList();

            return Json(new { Success = true, data = varUser }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult search(string searchVal)
        {
            var varUser = (from objUser in DataEntities.Users
                           orderby objUser.Id descending
                           where objUser.username == searchVal || objUser.Id.ToString() == searchVal || objUser.password == searchVal || objUser.username.Contains(searchVal) // This is SQL "Like"
                           select new
                           {
                               idCon = objUser.Id,
                               username = objUser.username,
                               password = objUser.password,
                               role = objUser.role
                           }
                ).ToList();

            return Json(new { Success = true, data = varUser }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult addCon(User objUserModel)//Program Code Same With update Data you can use this add data program for update data
        {
            string Message = "Data Successfully Updated";
            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState
                                 where item.Value.Errors.Any()
                                 select item.Value.Errors[0].ErrorMessage).ToList();

                return Json(new { success = false, Message = "Some Problem in validation", errorList });
            }
            User objVar = DataEntities.Users.SingleOrDefault(x => x.Id == objUserModel.Id) ?? new User();
            objVar.Id = objUserModel.Id;
            objVar.username = objUserModel.username;
            objVar.password = objUserModel.password;
            objVar.role = objUserModel.role;

            if (objUserModel.Id == 0)
            {
                Message = "Data SuccessFully Added..";
                DataEntities.Users.Add(objVar);
            }

            DataEntities.SaveChanges();
            return Json(new { Success = true, Message = Message }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(int Id)//ID Must Be Same With DataBase
        {
            User objUser =
            DataEntities.Users.Single(x => x.Id == Id);
            DataEntities.Users.Remove(objUser);
            DataEntities.SaveChanges();
            return Json(new { Success = true, Message = "Data Successfully Deleted" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllUserPagination(int startIndex = 0, int pageSize = 10)
        {
            var data = CountData.GetAllUsers(startIndex, pageSize);
            var pageCount = CountData.GetCountUser();
            GetAllUserPaginationModel GetAllUserPaginationModel = new GetAllUserPaginationModel() {
                users = data,
                pageCount = pageCount
            };
            /*return Json(new { Success = true, data = varUser }, JsonRequestBehavior.AllowGet);*/
            return Content(JsonConvert.SerializeObject(GetAllUserPaginationModel));
        }
        public ActionResult EditData(int id)
        {
            return Json(DataEntities.Users.SingleOrDefault(x=>x.Id == id),JsonRequestBehavior.AllowGet);
        }


    }// => End Controller
}