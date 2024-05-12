using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.ViewModel.AdminVM.ProductVM
{
    public class ImportItem
    {
        public int STT { get; set; }    
        public string DisplayName { get; set; }
        public decimal Price { get; set; }
        public string GenreName { get; set; }
        public string Author { get; set; }
        public int Count { get; set; }
        public int PublishYear { get; set; }
        public string Publisher {  get; set; }
    }
}
