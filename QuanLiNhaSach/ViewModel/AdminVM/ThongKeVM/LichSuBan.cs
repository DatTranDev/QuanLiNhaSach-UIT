using QuanLiNhaSach.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.ViewModel.AdminVM.ThongKeVM
{
    public partial class ThongKeViewModel
    {
        private  List<BillDTO> danhSachHoaDon;
        private ObservableCollection<BillDTO> _danhSachHoaDon;
        public ObservableCollection<BillDTO> DanhSachHoaDon
        {
            get { return _danhSachHoaDon; }
            set { _danhSachHoaDon = value; OnPropertyChanged(nameof(DanhSachHoaDon)); }
        }


        //phục vụ xem chi tiết hóa đơn
        private BillDTO _selectedItem;
        public BillDTO SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }


        private ObservableCollection<BillInfoDTO> _bookList;

        public ObservableCollection<BillInfoDTO> BookList
        {
            get { return _bookList; }
            set { _bookList = value; OnPropertyChanged(nameof(BookList)); }
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
        private decimal _totalPrice;
        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; OnPropertyChanged(); }
        }

        private decimal _paid;
        public decimal Paid
        {
            get { return _paid; }
            set { _paid = value; OnPropertyChanged(); }
        }

        private decimal _tienConLai;
        public decimal TienConLai
        {
            get { return _tienConLai; }
            set { _tienConLai = value; OnPropertyChanged(); }
        }

        private string _soLuong;
        public string SoLuong
        {
            get { return _soLuong; }
            set { _soLuong = value; OnPropertyChanged(); }
        }


    }
}
