using EightElements.Showtime.CMS.Data;
using EightElements.Showtime.CMS.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace EightElements.Showtime.CMS.Web.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content
        public ActionResult Index()
        {
            return View();
        }
        public static long ToUnixEpochDate(DateTime date) =>
            new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds();
        public static string CreateAccessToken(string path, string secret)
        {
            var now = DateTime.Now;
            int timestamp = (int)ToUnixEpochDate(now);
            string message = string.Format("{0}{1}", path, timestamp);
            var encoding = new UTF8Encoding();
            byte[] secretBytes = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmac = new HMACSHA256(secretBytes))
            {
                byte[] hash = hmac.ComputeHash(messageBytes);
                string token = HttpUtility.UrlEncode(Convert.ToBase64String(hash));
                return string.Format("{0}-{1}", timestamp, token);
            }
        }

        private ShowtimeEntities ShowtimeEntities;
        public ContentController()
        {
            ShowtimeEntities = new ShowtimeEntities();
        }
        public ActionResult ContentAgeRating(string keyword = "")
        {
           var data = Repositories.GetContentAgeRatings(keyword);
            return Json(new { Success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddContentAgeRating(string name)
        {            
            string Message = "Data Successfully Added";
            Repositories.AddContentAgeRating(name);
            return Json(new {Success = true,Message = Message },JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateContentAgeRating(ContentAgeRating objData)
        {
            string Message = "Data Successfully updated";
            Repositories.UpdateContentAgeRating(objData);            
            return Json(new{ Success = true,Message = Message },JsonRequestBehavior.AllowGet);
            
        }
        public ActionResult DeleteContentAgeRating(int Id)
        {
            var Message = "Data Successfully Deleted"; 
            Repositories.DeleteContentAgeRating(Id);
            return Json(new{Success = true,Message = Message },JsonRequestBehavior.AllowGet);
        }
        public ActionResult ContentCategory(string keyword = "")
        {
            var data = Repositories.GetContentCategory(keyword);
            return Json(new {Success = true,data = data},JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddContentCategory(CategoryPlatformDTO objData)
        {
            var Message = "Data Successfully Added";
            Repositories.AddContentCategory(objData);
            return Json(new { Success = true,Message = Message},JsonRequestBehavior.AllowGet);
        }
  
        public ActionResult UpdateContentCategory(CategoryPlatformDTO objData)
        {
            var Message = "Data Success Updated";
            Repositories.UpdateContentCategory(objData);
            return Json(new { Success = true,Message = Message},JsonRequestBehavior.AllowGet);
        }
  
        public ActionResult DeleteContentCategory(ContentCategory objData)
        {
            var Message = "Data Success Deleted";
            var data = Repositories.DeleteContentCategory(objData);
            if (data == null) Message = "id not found";
            return Json(new { Success = true,Message=Message},JsonRequestBehavior.AllowGet);
        }

        public ActionResult ContentClassification(string keyword = "")
        {
            var data = Repositories.GetContentClassification(keyword);
            return Json(new { Success = true,data = data},JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddContentClassification(string name)
        {
            var Message = "Data Successfully Added";
            Repositories.AddContentClassification(name);
            return Json(new { Success = true,data = Message},JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateContentClassification(ContentClassification objData)
        {
            var Message = "Data Successfully Updated";
            Repositories.UpdateContentClassification(objData);
            return Json(new { Success = true,Message = Message},JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteContentClassification(int Id)
        {
            var Message = "Data Success Deleted";
            Repositories.DeleteContentClassification(Id);
            return Json(new { Success = true,Message = Message},JsonRequestBehavior.AllowGet);
        }
        public ActionResult ContentPlatform(string keyword = "")
        {
            var data = Repositories.GetContentPlatform(keyword);
            return Json(new { Success = true,data = data},JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddContentPlatform(string name)
        {
            var Message = "Data Successfully Added";
            Repositories.AddContentPlatform(name);
            return Json(new { Success = true,Message = Message},JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateContentPlatform(ContentPlatform objData)
        {
            var Message = "Data Successfully Updated";
            Repositories.UpdateContentPlatform(objData);
            return Json(new { Success = true,Message = Message},JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteContentPlatform(int Id)
        {
            var Message = "Data Successfully Deleted";
            Repositories.DeleteContentPlatform(Id);
            return Json(new{Success = true,Message = Message },JsonRequestBehavior.AllowGet);
        }
        public ActionResult ContentTag(string keyword="")
        {
            var data = Repositories.GetContentTag(keyword);
            return Json(new { Success = true,data = data},JsonRequestBehavior.AllowGet);
        }
        public ActionResult ContentCategoryPlatform(string keyword = "")
        {
            var data = Repositories.GetContentCategoryPlatform(keyword);
            return Json(new { Success = true, data = data }, JsonRequestBehavior.AllowGet);
        }
    
        //Get And Check UserId And EventId    
        public ActionResult CekContentItems(int userId = 0,int eventId = 0)
        {
            var Message = Repositories.CekContentItems(userId,eventId);
            return Json(new { Success = true,Message = Message},JsonRequestBehavior.AllowGet);
        }

        /*public ActionResult ContentStatus(int eventId, int eventCategory = 0, int startIndex = 0, int pageSize = 10, int ContentStatus = 0)
        {
            var Message = Repositories.ContentStatus(eventId,eventCategory,startIndex,pageSize,ContentStatus);
            return Json(new { Success = true,Message = Message},JsonRequestBehavior.AllowGet);
        }*/
        //......get id for view
        public ActionResult GetContentDetail(int contentId)
        {
            try
            {
                
                var data = Repositories.GetContentDetail(contentId);
                //me-start
                
                var directory = "images/";

                var items = data.ContentItems;
                var z = items.Count();
                for (var i = 0; i < z; i++)
                {
                    

                    if (data.ContentItems[i].ContentTypeId < 5)
                    {
                        //directory = "videos/";
                       directory = "images/";
                    }
                    else {
                        //directory = "images/";
                        directory = "videos/";
                    }
                    var secretKey = System.Configuration.ConfigurationManager.AppSettings["SecretKey"] ?? "showtime";
                    var lnkUrl = data.ContentItems[i];
                    if (lnkUrl == null)
                    {
                        lnkUrl = null; 
                    }
                    if (data.ContentItems[i].ContentPath != null) {
                        var path = data.ContentItems[i].ContentPath.Substring(0, 1);
                        var token = CreateAccessToken((path == "/" ? "" : "/") + directory + data.ContentItems[i].ContentPath, secretKey);
                        data.ContentItems[i].ContentPath = directory + data.ContentItems[i].ContentPath + "?verify=" + token;
                    }
                    
                    //listContentDetail.Add(content);
                }
                //me-end
                return Content(JsonConvert.SerializeObject(new { Success = true, Message = "Success", Data = data }), "application/json");
            }
            catch (Exception e)
            {
                return Json(new { Success = false, Message = $"Failed\n{e.Message}\n{e.StackTrace}" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UpdateContentDetail(UpdateContentDTO dto)
        {
            try
            {
                var userId = (int) (Session["userID"]==null ? 24 : Session["userID"]);
                var data = Repositories.UpdateContentDetail(dto, userId);
                return Content(JsonConvert.SerializeObject(new { Success = true, Message = "Success", Data = data }), "application/json");
            }
            catch(Exception e)
            {
                return Json(new { Success = false, Message = $"Failed\n{e.Message}\n{e.StackTrace}"});
            }
            
        }
    }
}