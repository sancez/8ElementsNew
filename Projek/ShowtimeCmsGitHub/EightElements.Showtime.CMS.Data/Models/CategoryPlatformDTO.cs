using EightElements.Showtime.CMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EightElements.Showtime.CMS.Data.Models
{
    public class CategoryPlatformDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ContentPlatform> Platforms { get; set; }
    }
}
