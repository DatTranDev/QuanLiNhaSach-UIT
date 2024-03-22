using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.DTOs
{
    public class StaffDTO
    {
        public int ID { get; set; }
        public string DisplayName { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<System.DateTime> BirthDay { get; set; }
        public Nullable<decimal> Wage { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
