using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EightElements.Showtime.CMS.Web.Models
{
    public class ActionTrackingModel
    {
        public int Id { get; set; }
        public string RefererUrl { get; set; }
        public string RequestUrl { get; set; }
        public int UserId { get; set; }
        public string Remarks { get; set; }
        public int TrackingType { get; set; }
        public string TrafficSource { get; set; }
        public string Campaign { get; set; }
        public string PubId { get; set; }
        public string LandingPage { get; set; }
        public int GeolocationId { get; set; }
        public int TrackingCookieId { get; set; }
    }
}