namespace EightElements.Showtime.CMS.Web.Models
{
    public class PromotionModel
    {
        public int Id { set; get; }
        public int EventId { set; get; }
        public string Rules { set; get; }
        public string StartDate { set; get; }
        public string StartTime { set; get; }
        public string EndDate { set; get; }
        public string EndTime { set; get; }
    }
}