using QuanLiNhaSach.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.DTOs
{
    public class BillDTO
    {
        public int ID { get; set; }
        public Nullable<int> IDCus { get; set; }
        public Nullable<int> IDStaff { get; set; }
        public Nullable<System.DateTime> CreateAt { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<decimal> Paid { get; set; }
        public List<BillInfoDTO> BillInfo { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Customer Customer { get; set; }
        public Staff Staff { get; set; }
    }
}
