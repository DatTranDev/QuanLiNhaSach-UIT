using QuanLiNhaSach.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.DTOs
{
    public class DebtReportDTO
    {
        public int Id { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<decimal> FirstDebt { get; set; }
        public Nullable<decimal> LastDebt { get; set; }
        public Nullable<decimal> Arise { get; set; }
        public string MonthYear { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
