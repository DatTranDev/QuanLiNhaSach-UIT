//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLiNhaSach.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class InventoryReport
    {
        public int Id { get; set; }
        public Nullable<int> BookId { get; set; }
        public Nullable<int> FirstIvt { get; set; }
        public Nullable<int> LastIvt { get; set; }
        public Nullable<int> Arise { get; set; }
        public string MonthYear { get; set; }
    
        public virtual Book Book { get; set; }
    }
}
