using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using EightElements.Showtime.CMS.Data;
using Newtonsoft.Json;

namespace EightElements.Showtime.CMS.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Content()
        {
            return View();
        }

        public ActionResult Showcase()
        {
            return View();
        }

        public static long ToUnixEpochDate(DateTime date) =>
            new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds();

        public static string CreateAccessToken(string path, string secret)
        {
            var now = DateTime.Now;
            int timestamp = (int) ToUnixEpochDate(now);
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

        public ActionResult Detail(int contentId)
        {
            var data = Repositories.GetContentParticipant(contentId);
            List<ContentItem> listContentDetail = new List<ContentItem>();
            foreach (var content in data)
            {
                if (string.IsNullOrEmpty(content.ContentPath))
                {
                    listContentDetail.Add(content);

                }
                else {
                    var directory = "images/";
                    if (content.ContentTypeId == 5)
                    {
                        directory = "videos/";
                    }
                    var secretKey = System.Configuration.ConfigurationManager.AppSettings["SecretKey"] ?? "showtime";
                    var path = content.ContentPath.Substring(0, 1);
                    var token = CreateAccessToken((path == "/" ? "" : "/") + directory + content.ContentPath, secretKey);
                    content.ContentPath = directory +  content.ContentPath + "?verify=" + token;
                    listContentDetail.Add(content);
                }
            }

            ViewData["listCategories"] = Repositories.GetEventCategories();
            ViewData["epId"] = Repositories.GetEventParticipantWhereContentId(contentId);

            ViewData["listContent"] = listContentDetail;
            return View();
        }
       
        public ActionResult ShowcaseDetail(int contentId)
        {
            var data = Repositories.GetContentParticipant(contentId);
            List<ContentItem> listContentDetail = new List<ContentItem>();
            foreach (var content in data)
            {
                if (string.IsNullOrEmpty(content.ContentPath))
                {
                    listContentDetail.Add(content);

                }
                else
                {
                    var directory = "images/";
                    if (content.ContentTypeId == 5) {
                        directory = "videos/";
                    }
                    var secretKey = System.Configuration.ConfigurationManager.AppSettings["SecretKey"] ?? "showtime";
                    var path = content.ContentPath.Substring(0, 1);
                    var token = CreateAccessToken((path == "/" ? "" : "/") + directory + content.ContentPath, secretKey);
                    content.ContentPath = directory + content.ContentPath + "?verify=" + token;
                    listContentDetail.Add(content);
                }
            }

            ViewData["listContent"] = listContentDetail;
            return View();
        }

        public ActionResult ShowcaseDetail2()
        {
            return View();
        }
        //......ShowcaseDetail2 balikin view
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}