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
    
    public partial class Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill()
        {
            this.BillInfoes = new HashSet<BillInfo>();
        }
    
        public int ID { get; set; }
        public Nullable<int> IDCus { get; set; }
        public Nullable<int> IDStaff { get; set; }
        public Nullable<System.DateTime> CreateAt { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<decimal> Paid { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Staff Staff { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillInfo> BillInfoes { get; set; }
    }
}
