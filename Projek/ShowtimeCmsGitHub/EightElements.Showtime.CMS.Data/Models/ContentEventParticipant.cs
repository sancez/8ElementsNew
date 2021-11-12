using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightElements.Showtime.CMS.Data.Models
{
    public class ContentEventParticipant
    {
        public EventParticipant EventParticipant { get; set; }
        public Content Content { get; set; }
        public UserDetail User { get; set; }
        public PageText PageTexts { get; internal set; }
    }
}
