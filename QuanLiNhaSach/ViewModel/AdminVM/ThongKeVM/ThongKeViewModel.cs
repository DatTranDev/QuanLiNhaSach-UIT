using QuanLiNhaSach.DTOs;
using QuanLiNhaSach.Model.Service;
using QuanLiNhaSach.View.Admin.ThongKe.LichSu.LichSuBan;
using QuanLiNhaSach.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public ICommand InfoBillCM { get; set; }
        public ICommand CloseWdCM {  get; set; }
        public ICommand DeleteBillCM { get; set; }

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


            //lịch sử bán
            LichSuBanCM = new RelayCommand<Frame>((p) => { return true; }, async (p) =>
            {
                DanhSachHoaDon = new ObservableCollection<BillDTO>(await Task.Run(() => BillService.Ins.GetAllBill()));
                if (DanhSachHoaDon != null)
                {
                    danhSachHoaDon = new List<BillDTO>(DanhSachHoaDon);
                }
                if (p != null)
                {
                    p.Content = new View.Admin.ThongKe.LichSu.LichSuBan.LichSuTable();
                }
                SelectedDateTo = DateTime.Now;
                SelectedDateFrom = DateTime.Now.AddDays(-2);
            });


            //chi tiết hóa đơn 
            InfoBillCM = new RelayCommand<BillDTO>((p) => { return true; }, (p) =>
            {
                if (SelectedItem != null)
                {
                    BillDTO a = SelectedItem;
                    CusName = SelectedItem.Customer.DisplayName;
                    StaffName = SelectedItem.Staff.DisplayName;
                    BillDate = SelectedItem.CreateAt.ToString();
                    TotalPrice = SelectedItem.TotalPrice ?? 0;
                    Paid= SelectedItem.Paid ?? 0;
                    TienConLai = TotalPrice - Paid;
                    BookList = new ObservableCollection<BillInfoDTO>(SelectedItem.BillInfo);
                    ChiTietHoaDon wd = new ChiTietHoaDon();
                    wd.ShowDialog();
                }
            });


            //close window
            CloseWdCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });



            //xóa 1 hóa đơn
            DeleteBillCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                DeleteMessage wd = new DeleteMessage();
                wd.ShowDialog();
                if (wd.DialogResult == true)
                {
                    (bool sucess, string messageDelete) = await BillService.Ins.DeleteBill(SelectedItem);
                    if (sucess)
                    {
                        DanhSachHoaDon.Remove(SelectedItem);
                        danhSachHoaDon = new List<BillDTO>(DanhSachHoaDon);
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Xóa thành công");
                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, messageDelete);
                    }
                }
            });
        }
    }
}
