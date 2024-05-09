using LiveCharts.Wpf;
using LiveCharts;
using QuanLiNhaSach.DTOs;
using QuanLiNhaSach.Model.Service;
using QuanLiNhaSach.View.Admin.ThongKe.DoanhThu;
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
using QuanLiNhaSach.View.Admin.ThongKe.SachBanChay;
using QuanLiNhaSach.View.Admin.ThongKe.CongNo;
using QuanLiNhaSach.View.Admin.ThongKe.TonKho;

namespace QuanLiNhaSach.ViewModel.AdminVM.ThongKeVM
{
    public partial class ThongKeViewModel : BaseViewModel
    {

        #region chọn ngày
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
        #endregion



        #region các Icommand 
        public ICommand LichSuThuTienCM { get; set; }
        public ICommand LichSuBanCM { get; set; }
        public ICommand DoanhThuCM { get; set; }
        public ICommand SachBanChayCM { get; set; }
        public ICommand CongNoCM { get; set; }
        public ICommand TonKhoCM { get; set; }
        public ICommand InfoBillCM { get; set; }
        public ICommand CloseWdCM { get; set; }
        public ICommand DeleteBillCM { get; set; }
        public ICommand RevenueCM { get; set; }
        public ICommand FavorCM { get; set; }

        #endregion

        public ThongKeViewModel()
        {

            #region Lịch sử thu tiền
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
            #endregion

            #region Lịch sử bán

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
            #endregion

            #region Chi tiết hóa đơn

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
                    Paid = SelectedItem.Paid ?? 0;
                    TienConLai = TotalPrice - Paid;
                    BookList = new ObservableCollection<BillInfoDTO>(SelectedItem.BillInfo);
                    ChiTietHoaDon wd = new ChiTietHoaDon();
                    wd.ShowDialog();
                }
            });
            #endregion

            #region close window

            //close window
            CloseWdCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (p != null)
                {
                    p.Close();
                }
            });
            #endregion

            #region xóa hóa đơn
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
            #endregion

            #region DoanhThu
            RevenueCM = new RelayCommand<Frame>((p) => { return true; }, async (p) =>
            {
                SumBillTotal = 0;
                if (p != null)
                {
                    p.Content = new DoanhThuTable();
                    List<int> revenueValues = new List<int>();
                    List<DateTime> dates = new List<DateTime>();
                    DateTime currentDate = SelectedDateFrom;
                    DateTime UpDate = SelectedDateTo.AddDays(1);
                    while (currentDate <= UpDate)
                    {
                        int revenue = await BillService.Ins.getBillByDate(currentDate);
                        revenueValues.Add(revenue);
                        SumBillTotal += revenue;
                        dates.Add(currentDate);
                        currentDate = currentDate.AddDays(1);
                    }

                    string[] dateStrings = dates.Select(date => date.ToString("dd/MM/yyyy")).ToArray();
                    RevenueSeries = new SeriesCollection
                    {
                    new LineSeries
                    {
                        Title = "Doanh thu",
                        Values = new ChartValues<int>(revenueValues),
                    }
                    };
                    Labels = dateStrings;
                    YFormatter = value =>
                    {
                        return value.ToString("N");

                    };
                }
            });
            #endregion

            #region Sách ưa thích
            FavorCM = new RelayCommand<Frame>((p) => { return true; }, async (p) =>
            {
                if (p != null)
                {
                    p.Content = new SachBanChayTable();
                    FavorList = await Task.Run(() => ThongKeService.Ins.GetTop10SalerBetween(SelectedDateFrom, SelectedDateTo));
                }
            });
            #endregion

            #region công nợ
            CongNoCM = new RelayCommand<Frame>((p) => { return true; }, async (p) =>
            {
                if (p != null)
                {
                    p.Content = new CongNoTable();
                    DebtList = await Task.Run(() => ReportService.Ins.GetDebtReportByMonth(SelectedDateFrom.ToString("MM-yyyy")));
                }
            });
            #endregion

            #region tồn kho
            TonKhoCM = new RelayCommand<Frame>((p) => { return true; }, async (p) =>
            {
                if (p != null)
                {
                    p.Content = new TonKhoTable();
                    InventoryList = await Task.Run(() => ReportService.Ins.GetInventoryReportByMonth(SelectedDateFrom.ToString("MM-yyyy")));
                }
            });
            #endregion
        }
    } 
}
