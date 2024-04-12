using QuanLiNhaSach.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.DTOs
{
    public class InventoryReportDTO
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
