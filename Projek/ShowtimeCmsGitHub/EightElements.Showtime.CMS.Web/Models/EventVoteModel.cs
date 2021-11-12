using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EightElements.Showtime.CMS.Web.Models
{
    public class EventVoteModel
    {
        public int Id { set; get; }
        public string CreateDate { set; get; }
        public int UserID { set; get; }
        public int EventParticipantID { set; get; }
        public string Reference { set; get; }
        public int status { set; get; }
        public string StatusMessage { set; get; }
        public int Amount { set; get; }
        public int EventID { set; get;}
        public int TotalVote { set; get; }
        public int PromotionID { set; get; }
   
    }
}