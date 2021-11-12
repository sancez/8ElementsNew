using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using EightElements.Showtime.CMS.Data;
using EightElements.Showtime.CMS.Web.Models;
using Newtonsoft.Json;

namespace EightElements.Showtime.CMS.Web.Controllers
{
    public class RestController : Controller
    {
        // GET
        public ActionResult GetAllEvent()
        {
            var data = Repositories.GetAllEvent();
            return Content(JsonConvert.SerializeObject(data));
        }

        public static string UploadFile(string path, string upload_path)
        {
            string ip = "192.168.110.16"; // please enter your ip/ftp url here
            string username = "8eKaloz"; //Enter your FTP user name
            string password = "8eKaloz"; //Enter your FTP password name

            string fullpath = "ftp://" + ip + "/" + upload_path; //with ip/url create a ftp path where you will upload

            FileInfo fileInf = new FileInfo(path);

            FtpWebRequest reqFTP;
            // Create FtpWebRequest object from the Uri provided
            reqFTP = (FtpWebRequest) FtpWebRequest.Create(fullpath);
            // Provide the WebPermission Credentials
            reqFTP.Credentials = new NetworkCredential(username, password);
            reqFTP.Proxy = null; //Set Proxy to be null
            // By default KeepAlive is true, where the control connection is not closed
            // after a command is executed.
            reqFTP.KeepAlive = false;
            // Specify the command to be executed.
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            // reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
            // Specify the data transfer type.
            reqFTP.UseBinary = true;
            // Notify the server about the size of the uploaded file
            reqFTP.ContentLength = fileInf.Length;
            //SSL
            reqFTP.EnableSsl = false;
            // The buffer size is set to 2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;

            // Stream to which the file to be upload is written
            try
            {
                Stream strm = reqFTP.GetRequestStream();
                // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
                FileStream fs = fileInf.OpenRead();
                try
                {
                    do
                    {
                        contentLen = fs.Read(buff, 0, buffLength);
                        strm.Write(buff, 0, contentLen);
                    } while (contentLen != 0);

                    fs.Close();
                    strm.Close();
                }
                catch (WebException ex)
                {
                    String status = ((FtpWebResponse) ex.Response).StatusDescription;
                    return ex.ToString();
                }
            }
            catch (WebException ex)
            {
                String status = ((FtpWebResponse) ex.Response).StatusDescription;
                return ex.ToString();
            }

            //here it will delete the file from local application temp location after uploading to ftp server.
            if ((System.IO.File.Exists(path)))
            {
                System.IO.File.Delete(path);
            }

            return "1";
        }

        public List<string> SaveFile(HttpPostedFileBase[] files)
        {
            try
            {
                List<string> path = new List<string>();
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Content/uploaded/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        path.Add(InputFileName);
                    }
                }

                return path;
            }
            catch (Exception e)
            {
                return new List<string>();
            }
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult AddorUpdatePortalAssets(HttpPostedFileBase[] files,int eventId,string key,string eventDetailId)
        {
            var localPath = SaveFile(files);
            string[] detailId = eventDetailId.Split(',');
            int id = 0;
            foreach (string path in localPath)
            {
                var serverPath = "/" + eventId + "/assets/" + path;
                UploadFile(Path.Combine(Server.MapPath("~/Content/uploaded/") + path), "Showtime/CDN" + serverPath);
                EventDetail eventDetail = new EventDetail();
                eventDetail.Id = int.Parse( string.IsNullOrEmpty(detailId[id]) ?"0" : detailId[id]);
                eventDetail.EventId = eventId;
                eventDetail.Key = key;
                eventDetail.ContentPath = serverPath;
                Repositories.AddEventDetail(eventDetail);
                id++;
            }

            return Content("ok");
        }
        
        [System.Web.Mvc.HttpPost]
        public ActionResult AddPromotion([FromBody]PromotionModel value)
        {
            var startDate = DateTime.Parse(value.StartDate + " " + value.StartTime);
            var endDate = DateTime.Parse(value.EndDate + " " + value.EndTime);
            var check = Repositories.GetPromotionByTimeRange(startDate, endDate,value.Id);
            if (check != null)
            {
                return Content("There's an event on that time");
            }
            Promotion promotion = new Promotion()
            {
                Id = value.Id,
                EventId = value.EventId,
                StartDate = startDate,
                EndDate = endDate,
                Rules = value.Rules,
                Pause = false,
                Disabled = false,
                CreateDate = DateTime.Now
            };
            Repositories.AddOrUpdatePromotion(promotion);
            return Content("ok");
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult AddEventVote([FromBody] EventVoteModel value)
        {
            EventVote EvVote = new EventVote()
            {
                Id = value.Id,
                CreateDate= DateTime.Now,
                UserId = value.UserID,
                EventParticipantId = value.EventParticipantID,
                Reference = value.Reference,
                Status = value.status,
                StatusMessage = value.StatusMessage,
                Amount = value.Amount,
                EventId = value.EventID,
                TotalVote = value.TotalVote,
                PromotionId = value.PromotionID 

            };
            Repositories.AddEventVote(EvVote);
            return Content("ok");
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult AddActionTracking([FromBody] ActionTrackingModel value)
        {
            ActionTracking BB = new ActionTracking()
            {
             
                RefererUrl = value.RefererUrl,
                RequestUrl = value.RequestUrl,
                UserId = value.UserId,
                CreateDate = DateTime.Now,
                Remarks = value.Remarks,
                TrackingType = value.TrackingType,
                TrafficSource = value.TrafficSource,
                Campaign = value.Campaign,
                PubId = value.PubId,
                LandingPage = value.LandingPage,
                GeolocationId = value.GeolocationId,
                TrackingCookieId = value.TrackingCookieId
            };
            Repositories.AddActionTracking(BB);
            return Content("ok");
        }


        public ActionResult PauseOrEndPromotion(int id, bool pause, bool disable)
        {
            Promotion promotion = new Promotion()
            {
                Id = id,
                Pause = pause,
                Disabled = disable,
            };
            Repositories.AddOrUpdatePromotion(promotion);
            return Content("ok");
        }
        
        public ActionResult GetAllPromotion(int eventId)
        {
            var data = Repositories.GetAllPromotions(eventId);
            return Content(JsonConvert.SerializeObject(data),"application/json");
        }

        public ActionResult GetAllEventParticipant(int eventId,int eventCategory = 0, int startIndex = 0, int pageSize = 10, int ContentStatus = -1)// add int startIndex = 0, int pageSize = 10
        {
           
            var data = Repositories.GetAllEventParticipant(eventId, eventCategory, startIndex, pageSize, ContentStatus);//add  startIndex, pageSize
            //return Content(JsonConvert.SerializeObject(data));
            var pageCount = Repositories.GetCountEventParticipant();
            EventParticipantPagingModel GetAllEventParticipantPagingModel = new EventParticipantPagingModel()
            {
                Contents = data,
                PageCount = pageCount
            };
            return Content(JsonConvert.SerializeObject(GetAllEventParticipantPagingModel));

        }
        //........
        public ActionResult GetAllShowcase(int startIndex = 0, int pageSize = 10,int ContentStatus = -191)
        {
            var data = Repositories.GetAllShowcase(startIndex, pageSize, ContentStatus);
            var pageCount = Repositories.GetCountShowcase();
            ShowcasePagingModel showcasePagingModel = new ShowcasePagingModel()
            {
                Showcases = data,
                PageCount = pageCount
            };
            return Content(JsonConvert.SerializeObject(showcasePagingModel));
        }

        public ActionResult Test(int startIndex = 0, int pageSize = 10)
        {
            var data_test = Repositories.Test(startIndex, pageSize);
            return Content(JsonConvert.SerializeObject(data_test));
        }

        public ActionResult GetAllCategories()
        {
            var data = Repositories.GetEventCategories();
            return Content(JsonConvert.SerializeObject(data),"application/json");
        }

        public ActionResult UpdateEventParticipant(int id, int categoryId)
        {
            Repositories.UpdateEventParticipant(id, categoryId);
            return Content("ok");
        }

        public ActionResult GetEventDetail(int eventId,string key)
        {
            var data = Repositories.GetEventDetailByKey(key,eventId);
            return Content(JsonConvert.SerializeObject(data));
        }

        public ActionResult GetAllEventParticipantBySearchName(int eventId, string search, int eventCategory = 0)
        {
            var data = Repositories.GetAllEventParticipantBySearchName(eventId, search, eventCategory);
            return Content(JsonConvert.SerializeObject(data));
        }

        public ActionResult GetAllEventDetailByEventId(int eventId, int startIndex = 0, int pageSize = 10)
        {
            // var data = Repositories.GetAllEventDetailByEventId(eventId);
            //return Content(JsonConvert.SerializeObject(data));
            try
            {
                var json = JsonConvert.SerializeObject(Repositories.GetAllEventDetailByEventId(eventId, startIndex, pageSize));
                return Content(json);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
        
        public ActionResult Report()
        {
            try
            {
                var json = JsonConvert.SerializeObject(Repositories.GetReport());
                return Content(json);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult ApproveOrRejectContentDetail(int contentDetailId, int status)
        {
            Repositories.ApproveOrRejectContentDetail(contentDetailId, status);
            return Content("ok");
        }
        public ActionResult ApproveOrRejectContent(int contentId, int status)
        {
            var data = Repositories.GetContentParticipantApproved(contentId);
            if (data.Count < 2)
            {
                return Content("You need approve at least banner & thumbnail contents");
            }
            Repositories.ApproveOrRejectContent(contentId, status);
            return Content("ok");
        }
        
        
        public ActionResult AddOrUpdatePageText([FromBody] PageTextModel body)//Add And Update
        {
            PageText pageText = new PageText();
            var logger = Session["userName"];
            pageText.Id = body.id;
            pageText.Key = body.key;
            pageText.Text = body.text;
            pageText.LastUpdateDate = DateTime.Now;
            pageText.LastUpdateBy = (string)logger;
            pageText.LanguageCode = body.lang;
            if (body.id < 1)
            {
                pageText.CreateDate = DateTime.Now;
                pageText.CreateBy = "";            
            }
            Repositories.AddOrUpdatePageTexts(pageText,(string)logger);            
            return Content("ok");
        }

        public ActionResult AddOrUpdateActionTracking([FromBody] ActionTrackingModel vl)//Add And Update => 10/21/2010 9:00 AM
        {
            ActionTracking vr = new ActionTracking();
            vr.Id = vl.Id;
            vr.RefererUrl = vl.RefererUrl;
            vr.RequestUrl = vl.RequestUrl;
            vr.UserId = vl.UserId;
            vr.Remarks = vl.Remarks;
            vr.TrackingType = vl.TrackingType;
            vr.TrafficSource = vl.TrafficSource;
            vr.Campaign = vl.Campaign;
            vr.PubId = vl.PubId;
            vr.LandingPage = vl.LandingPage;
            vr.GeolocationId = vl.GeolocationId;
            vr.TrackingCookieId = vl.TrackingCookieId;
            vr.CreateDate = DateTime.Now;

            Repositories.AddOrUpdateActionTracking(vr);
            return Content("ok");
        }

        public ActionResult GetAllPageText(string lang, int startIndex = 0, int pageSize = 10)
        {
           // try
           // {
              //  var json = JsonConvert.SerializeObject(Repositories.GetAllPageText(lang, startIndex, pageSize));
             //   return Content(json);
            //}
            //catch (Exception e)
            //{
            //    return Content(e.Message);
           // }

          
           var data = Repositories.GetAllPageText(lang, startIndex, pageSize);//add  startIndex, pageSize
            //return Content(JsonConvert.SerializeObject(data));
           var pageCount = Repositories.GetCountPageText(lang);
           PageTextPagingModel PageTextPagingModel = new PageTextPagingModel()
            {
                 PageTexts = data,
                 PageCount = pageCount
           };
           return Content(JsonConvert.SerializeObject(PageTextPagingModel));

        }
    }
}