using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.DTOs
{
    public class BookDTO : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string DisplayName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> IDGenre { get; set; }
        public string GenreName { get; set; }
        public Nullable<int> PublishYear { get; set; }
        public String Publisher {  get; set; }
        private Nullable<int> _inventory;
        public Nullable<int> Inventory
        {
            get { return _inventory; }
            set { _inventory = value; OnPropertyChanged(nameof(Inventory)); }
        }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string toString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"{ID}-{DisplayName}-");
            result.AppendLine($"Danh mục: {GenreName}");
            result.AppendLine($"Tác giả: {Author}");
            result.AppendLine($"Năm xuất bản: {PublishYear ?? 0}");
            result.AppendLine($"NXB: {Publisher}");
            result.AppendLine($"Giá nhập: {((decimal)Price).ToString("N0")}");
            return result.ToString();
        }
    }
}
