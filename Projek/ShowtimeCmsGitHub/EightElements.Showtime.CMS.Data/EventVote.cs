//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EightElements.Showtime.CMS.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class EventVote
    {
        public int Id { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int UserId { get; set; }
        public int EventParticipantId { get; set; }
        public string Reference { get; set; }
        public int Status { get; set; }
        public string StatusMessage { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public int EventId { get; set; }
        public Nullable<int> TotalVote { get; set; }
        public Nullable<int> PromotionId { get; set; }
    }
}
