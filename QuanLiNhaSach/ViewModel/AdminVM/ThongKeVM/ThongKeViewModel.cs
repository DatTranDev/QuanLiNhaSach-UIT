using QuanLiNhaSach.DTOs;
using QuanLiNhaSach.Model.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace QuanLiNhaSach.ViewModel.AdminVM.ThongKeVM
{
    public partial class ThongKeViewModel:BaseViewModel
    {
        private DateTime _selectedDateFrom;
        public DateTime SelectedDateFrom
        {
            get { return _selectedDateFrom; }
            set { _selectedDateFrom = value; OnPropertyChanged(); }
        }
        private DateTime _selectedDateTo;
        public DateTime SelectedDateTo
        {
            get { return _selectedDateTo; }
            set { _selectedDateTo = value; OnPropertyChanged(); }
        }


        public ICommand LichSuThuTienCM {  get; set; }
        public ICommand LichSuBanCM { get; set; }
        public ICommand DoanhThuCM { get; set; }
        public ICommand SachBanChayCM {  get; set; }
        public ICommand CongNoCM { get; set; }
        public ICommand TonKhoCM { get; set; }


        public ThongKeViewModel()
        {
         
            //xây dựng command LichSuThuTienCM
            LichSuThuTienCM = new RelayCommand<Frame>((p) => { return true; }, async (p) =>
            {
                DanhSachThuTien = new ObservableCollection<PaymentReceiptDTO>(await Task.Run(() => PaymentReceiptService.Ins.GetAllPayment()));
                if (DanhSachThuTien != null)
                {
                    danhSachThuTien = new List<PaymentReceiptDTO>(DanhSachThuTien);
                }
                if (p != null)
                {
                    p.Content = new View.Admin.ThongKe.LichSu.LichSuThuTien.LichSuTable();
                }
                SelectedDateTo = DateTime.Now;
                SelectedDateFrom = DateTime.Now.AddDays(-2);
            });




        }
    }
}
