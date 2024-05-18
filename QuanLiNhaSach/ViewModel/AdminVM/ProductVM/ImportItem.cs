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
        public ImportItem(String input = "")
        {
            // Split the input string by new lines and handle the format
            var parts = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length >= 5)
            {
                var firstLineParts = parts[0].Split('-');
                //STT = int.Parse(firstLineParts[0]);
                DisplayName = firstLineParts[1].Trim();

                GenreName = parts[1].Substring("Danh mục: ".Length).Trim();

                Author = parts[2].Substring("Tác giả: ".Length).Trim();

                PublishYear = int.Parse(parts[3].Substring("Năm xuất bản: ".Length).Trim());

                Publisher = parts[4].Substring("NXB: ".Length).Trim();

                Price = decimal.Parse(parts[5].Substring("Giá nhập: ".Length).Trim().Replace(",", ""));
            }
        }
    }
}
