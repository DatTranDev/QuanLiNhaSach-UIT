using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.DTOs
{
    public class SystemValueDTO
    {
        public Nullable<int> MinReceived { get; set; }
        public Nullable<int> MaxInventory { get; set; }
        public Nullable<decimal> MaxDebts { get; set; }
        public Nullable<int> MinSaleInventory { get; set; }
        public Nullable<double> Profit { get; set; }
        public Nullable<bool> DebtsPolicy { get; set; }
        public int ID { get; set; }
    }
}
