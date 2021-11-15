using EightElements.Showtime.CMS.Data;
using EightElements.Showtime.CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                return Json(new { Success = true, Message = "Success", Data = data }, JsonRequestBehavior.AllowGet);
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
                return Json(new { Success = true, Message = "Success", Data = data });
            }
            catch(Exception e)
            {
                return Json(new { Success = false, Message = $"Failed\n{e.Message}\n{e.StackTrace}"});
            }
            
        }
    }
}