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
    
    public partial class GoodReceived
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GoodReceived()
        {
            this.GoodReceivedInfoes = new HashSet<GoodReceivedInfo>();
        }
    
        public int ID { get; set; }
        public Nullable<System.DateTime> CreatAt { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> StaffId { get; set; }
        public Nullable<decimal> Total { get; set; }
    
        public virtual Staff Staff { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoodReceivedInfo> GoodReceivedInfoes { get; set; }
    }
}
