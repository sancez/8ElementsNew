using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightElements.Showtime.CMS.Data.Models
{
    public class UpdateContentDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }
        public int? CategoryId { get; set; }
        public int? ClassificationId { get; set; }
        public int? AgeRatingId { get; set; }
        public bool IsCP { get; set; }
        public List<string> Tags { get; set; }
        public List<ContentItem> ContentItems { get; set; }

        public DateTime? LastUpdatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public string LastUpdatedByName { get; set; }
        //New Object
        public string CategoryName { get; set; }
        public string ClassificationName { get; set; }
        public string AgeRatingName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
