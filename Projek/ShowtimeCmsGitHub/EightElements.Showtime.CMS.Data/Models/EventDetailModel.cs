using System;

namespace EightElements.Showtime.CMS.Data.Models
{
    public class EventDetailModel
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Key { get; set; }
        public string ContentSource { get; set; }
        public string ContentPath { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public int Count { get; set; }
    }
}