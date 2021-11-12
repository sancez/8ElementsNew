using EightElements.Showtime.CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EightElements.Showtime.CMS.Web.Models
{
    public class ShowcasePagingModel
    {
        public List<ShowcaseModel> Showcases { get; set; }
        public int PageCount { get; set; }
    }
}