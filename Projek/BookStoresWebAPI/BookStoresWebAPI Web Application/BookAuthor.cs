//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookStoresWebAPI_Web_Application
{
    using System;
    using System.Collections.Generic;
    
    public partial class BookAuthor
    {
        public int author_id { get; set; }
        public int book_id { get; set; }
        public Nullable<byte> author_order { get; set; }
        public Nullable<int> royality_percentage { get; set; }
    
        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
    }
}
