using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLiNhaSach.Model;

namespace QuanLiNhaSach.DTOs
{
    public class PaymentReceiptDTO
    {
        public int ID { get; set; }
        public Nullable<int> IDCus { get; set; }
        public string CusAddress { get; set; }
        public string CusName { get; set; }
        public string CusNumber { get; set; }
        public string CusEmail { get; set; }
        public Nullable<System.DateTime> CreatAt { get; set; }
        public Nullable<decimal> AmountReceived { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Customer Customer; 
    }
}
