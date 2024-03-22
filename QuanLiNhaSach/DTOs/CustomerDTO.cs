using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.DTOs
{
    public class CustomerDTO : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Spend { get; set; }
        public Nullable<decimal> _debts;
        public Nullable<decimal> Debts 
        {
            get { return _debts; } 
            set {  _debts = value; OnPropertyChanged(nameof(Debts)); } 
        }
        public Nullable<bool> IsDeleted { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
