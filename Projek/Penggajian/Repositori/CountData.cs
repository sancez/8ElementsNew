using Penggajian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Penggajian.Repositori
{
    public class CountData
    {
        
        public static List<UserA> GetAllUsers(int startIndex = 0, int pageSize = 10)
        {
            using (var dc = new DataEntities())
            {
                var varUser = from objUser in dc.Users
                              orderby objUser.Id descending
                              where objUser.role == "2"
                              select new UserA()
                              {
                                  IdCon = objUser.Id,
                                  username = objUser.username,
                                  password = objUser.password,
                                  role = objUser.role
                              };
                return varUser.ToList();
            }
        }

        public static int GetCountUser()
        {
            using (var dc = new DataEntities())
            {
                var varUser = from objUser in dc.Users
                              orderby objUser.Id descending
                              where objUser.role == "2"
                              select new
                              {
                                  idCon = objUser.Id,
                                  username = objUser.username,
                                  password = objUser.password,
                                  role = objUser.role
                              };
                return varUser.Count();
            }
        }
        }

       
    


}