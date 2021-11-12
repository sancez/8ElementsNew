using EightElements.Showtime.CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace EightElements.Showtime.CMS.Web.Models
{
    public class EventParticipantPagingModel
    {
        public List<ContentEventParticipant> Contents { get; set; }
        public int PageCount { get; set; }
    }


}