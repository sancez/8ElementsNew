using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EightElements.Showtime.CMS.Data;

namespace EightElements.Showtime.CMS.Web.Helpers
{
    public class SessionHandler
    {
        private readonly static string _userInfo = "UserInfo";

        public static User UserInfo
        {
            get
            {
                User userInfo = new User();
                if (HttpContext.Current.Session[_userInfo] == null)
                {
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        //var formsIdentity = HttpContext.Current.User.Identity as FormsIdentity;
                        //this.username.Text = formsIdentity.Ticket.UserData;
                        //get from database
                        //DataAccess.Login();
                    }
                }
                else
                {
                    userInfo = HttpContext.Current.Session[_userInfo] as User;
                }

                return userInfo;
            }
            set { HttpContext.Current.Session[_userInfo] = value; }
        }
    }
}