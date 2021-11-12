using System.Web.Mvc;
using EightElements.Showtime.CMS.Data;


namespace EightElements.Showtime.CMS.Web.Controllers
{
    public class PortalController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PageText()
        {
                       
            return View();
        }
        
        public ActionResult Banner()
        {
            return View();
        }
        public ActionResult Carousel()
        {
            return View();
        }
        public ActionResult Category()
        {
            return View();
        }
        public ActionResult Vote()
        {
            return View();
        }
        public ActionResult Join()
        {
            return View();
        }
        public ActionResult Promotion(int id = 0)
        {
            var data = Repositories.GetPromotionById(id);
            ViewData["dataPromotion"] = data;
            return View();
        }
    }
}