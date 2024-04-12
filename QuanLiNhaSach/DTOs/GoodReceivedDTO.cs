using QuanLiNhaSach.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.DTOs
{
    public class GoodReceivedDTO
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> CreateAt { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public int StaffId { get; set; }
        public Staff Staff { get; set; }
        public List<GoodReceivedInfoDTO> GoodReceivedInfo { get; set; }
    }
}
