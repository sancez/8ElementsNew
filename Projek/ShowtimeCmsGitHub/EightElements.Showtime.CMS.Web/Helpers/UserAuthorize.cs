using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;

 namespace EightElements.Showtime.CMS.Web.Helpers
{
    public class UserAuthorize : AuthorizeAttribute
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //check cookie name
            //if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated == false)
            //{

            //}
            //Check permission
            //if (SessionHandler.UserInfo != null)
            //{
            //    if (SessionHandler.UserInfo.Username != null)
            //    {
            //        //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            //        //{
            //        //    controller = "Home",
            //        //    action = "Index"
            //        //}));
            //    }
            //    else {
            //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            //        {
            //            controller = "Login",
            //            action = "Index"
            //        }));
            //    }
            //}
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(UserAuthorize));
    }
}