using QuanLiNhaSach.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.DTOs
{
    public class GoodReceivedInfoDTO : INotifyPropertyChanged
    {
        public int IDGoodReceived {  get; set; }
        public int IDBook {  get; set; }
        private Nullable<int> _quantity;
        public Nullable<int> Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }
        public string BookAuthor { get; set; }
        public string BookName { get; set; }
        public string BookGenre { get; set; }
        public Nullable<decimal> BookPrice { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public GoodReceived GoodReceived { get; set; }
        public Book Book { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
