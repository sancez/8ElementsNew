using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Penggajian.Models
{
    public class GetAllUserPaginationModel
    {
        public List<UserA> users { get; set; }
        public int pageCount { get; set; }
    }

}