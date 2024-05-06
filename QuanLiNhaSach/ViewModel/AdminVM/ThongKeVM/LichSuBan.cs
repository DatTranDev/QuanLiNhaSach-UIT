using QuanLiNhaSach.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.ViewModel.AdminVM.ThongKeVM
{
    public partial class ThongKeViewModel : BaseViewModel
    {
        private static List<BillDTO> billList;
        private ObservableCollection<BillDTO> _billList;
        public ObservableCollection<BillDTO> BillList
        {
            get { return _billList; }
            set { _billList = value; OnPropertyChanged(); }
        }


        private ObservableCollection<BillInfoDTO> _bookList;

        public ObservableCollection<BillInfoDTO> BookList
        {
            get { return _bookList; }
            set { _bookList = value; OnPropertyChanged(); }
        }


        private BillDTO _selectedItem;
        public BillDTO SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }


        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }
        private string _cusName;
        public string CusName
        {
            get { return _cusName; }
            set { _cusName = value; OnPropertyChanged(); }
        }
        private string _staffName;
        public string StaffName
        {
            get { return _staffName; }
            set { _staffName = value; OnPropertyChanged(); }
        }
        private string _billDate;
        public string BillDate
        {
            get { return _billDate; }
            set { _billDate = value; OnPropertyChanged(); }
        }
        private decimal _billValue;
        public decimal BillValue
        {
            get { return _billValue; }
            set { _billValue = value; OnPropertyChanged(); }
        }
        private string _soLuong;
        public string SoLuong
        {
            get { return _soLuong; }
            set { _soLuong = value; OnPropertyChanged(); }
        }


    }
}
