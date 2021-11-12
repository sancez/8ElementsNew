using EightElements.Showtime.CMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EightElements.Showtime.CMS.Web.Models
{
    public class PageTextPagingModel
    {
        public List<PageText> PageTexts { get; set; }
        public int PageCount { get; set; }

    }
}
