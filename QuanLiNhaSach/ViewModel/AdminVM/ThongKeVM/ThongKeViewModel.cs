﻿using LiveCharts.Wpf;
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
using QuanLiNhaSach.View.Admin.ThongKe;
using System.Threading;

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
        public ICommand DateChange { get; set; }



        bool checkLanDau = false;
        #endregion
        public ThongKeViewModel()
        {

            #region set dữ liệu cho lần đầu mở page thống kê
            IsBorderForDatePickerVisible = true;
            setListThang();
            SelectedThang = "Tháng "+ DateTime.Now.Month;
            TextBoxYear = DateTime.Now.Year.ToString();
            CaseNav = 0;
            #endregion


            #region Lịch sử thu tiền
            //xây dựng command LichSuThuTienCM
            LichSuThuTienCM = new RelayCommand<Frame>((p) => { return true; }, async (p) =>
            {

                if (p == null) return;

                if (!checkThaoTac && checkLanDau)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error,"Thao tác quá nhanh!");
                    return;
                }
                checkThaoTac = false;

                DanhSachThuTien = new ObservableCollection<PaymentReceiptDTO>(await Task.Run(() => PaymentReceiptService.Ins.GetAllPayment()));


                if (DanhSachThuTien != null)
                {
                    danhSachThuTien = new List<PaymentReceiptDTO>(DanhSachThuTien);
                }
                p.Content = new View.Admin.ThongKe.LichSu.LichSuThuTien.LichSuTable();

                if (!checkLanDau)
                {
                    SelectedDateTo = DateTime.Now;
                    SelectedDateFrom = DateTime.Now.AddDays(-2);
                }


                //không cho thao tác quá nhanh 
                checkThaoTac = true;
                CaseNav = 0;
                checkLanDau = true;
                IsBorderForDatePickerVisible = true;

            });
            #endregion



            #region Lịch sử bán

            //lịch sử bán
            LichSuBanCM = new RelayCommand<Frame>((p) => { return true; }, async (p) =>
            {
                
                if (p == null) return;


                if (!checkThaoTac)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Thao tác quá nhanh!");
                    return;
                }
                checkThaoTac = false;

                DanhSachHoaDon = new ObservableCollection<BillDTO>(await Task.Run(() => BillService.Ins.GetAllBill()));
                if (DanhSachHoaDon != null)
                {
                    danhSachHoaDon = new List<BillDTO>(DanhSachHoaDon);
                }
                p.Content = new View.Admin.ThongKe.LichSu.LichSuBan.LichSuTable();

                checkThaoTac = true;
                CaseNav = 1;
                IsBorderForDatePickerVisible = true;

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
                if (SelectedItem == null) return;

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

                if (p == null) return;


                if (!checkThaoTac)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Thao tác quá nhanh!");
                    return;
                }
                checkThaoTac = false;


                SumBillTotalPaid = 0;
                p.Content = new DoanhThuTable();
                List<int> revenueValues = new List<int>();
                List<DateTime> dates = new List<DateTime>();
                DateTime currentDate = SelectedDateFrom;
                DateTime UpDate = SelectedDateTo.AddDays(1);
                while (currentDate <= UpDate)
                {
                    int revenue = await BillService.Ins.getBillByDate(currentDate);
                    revenueValues.Add(revenue);
                    SumBillTotalPaid += revenue;
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

                checkThaoTac = true;
                CaseNav = 2;
                IsBorderForDatePickerVisible = true;

            });
            #endregion

            #region Sách ưa thích
            FavorCM = new RelayCommand<Frame>((p) => { return true; }, async (p) =>
            {
                if (p == null) return;
                if (!checkThaoTac)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Thao tác quá nhanh!");
                    return;
                }
                checkThaoTac = false;
                p.Content = new SachBanChayTable();
                FavorList = new ObservableCollection<BookDTO>( await Task.Run(() => ThongKeService.Ins.GetTop10SalerBetween(SelectedDateFrom, SelectedDateTo)));

                checkThaoTac = true;
                CaseNav = 3;
                IsBorderForDatePickerVisible = true;

            });
            #endregion

            #region công nợ
            CongNoCM = new RelayCommand<Frame>((p) => { return true; }, async (p) =>
            {
                if (p == null) return;
                if (!checkThaoTac)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Thao tác quá nhanh!");
                    return;
                }
                checkThaoTac = false;

                p.Content = new CongNoTable();

                SelectedThang = "Tháng " + DateTime.Now.Month;
                TextBoxYear =DateTime.Now.Year.ToString();
                month = formatMonth(SelectedThang);
                year = TextBoxYear;

                DebtList = new ObservableCollection<DebtReportDTO>(await Task.Run(() => ReportService.Ins.GetDebtReportByMonth(formatMonthYear(month,year))));
                if (DebtList != null)
                {
                    debtList = new List<DebtReportDTO>(DebtList);
                }
                checkThaoTac = true;
                CaseNav = 4;
                IsBorderForDatePickerVisible = false;
            });
            #endregion

            #region tồn kho
            TonKhoCM = new RelayCommand<Frame>((p) => { return true; }, async (p) =>
            {
                if (p == null) return;
                if (!checkThaoTac)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Thao tác quá nhanh!");
                    return;
                }
                checkThaoTac = false;

                p.Content = new TonKhoTable();

                SelectedThang = "Tháng " + DateTime.Now.Month;
                TextBoxYear = DateTime.Now.Year.ToString();
                month = formatMonth(SelectedThang);
                year = TextBoxYear;

                InventoryList = new ObservableCollection<InventoryReportDTO>(await Task.Run(() => ReportService.Ins.GetInventoryReportByMonth(formatMonthYear(month, year))));
                if (InventoryList != null)
                {
                    inventoryList = new List<InventoryReportDTO>(InventoryList);
                }


                checkThaoTac = true;
                CaseNav = 5;
                IsBorderForDatePickerVisible = false;
            });
            #endregion


            #region select date from,to
            DateChange = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (p == null) return;

                if (CaseNav == -1) return;

                //lịch sử thu tiền
                if (CaseNav == 0)
                {
                    if (danhSachThuTien != null)
                    {
                        DanhSachThuTien = new ObservableCollection<PaymentReceiptDTO>(danhSachThuTien.FindAll(x => x.CreateAt >= SelectedDateFrom && x.CreateAt <= SelectedDateTo));
                    }
                    return;
                }


                //lịch sử bán
                if (CaseNav == 1)
                {
                    if (danhSachHoaDon != null)
                    {
                        DanhSachHoaDon = new ObservableCollection<BillDTO>(danhSachHoaDon.FindAll(x => x.CreateAt >= SelectedDateFrom && x.CreateAt <= SelectedDateTo));
                    }
                    return;
                }

                //doanh thu
                if (CaseNav == 2)
                {
                    List<int> revenueValues = new List<int>();
                    List<DateTime> dates = new List<DateTime>();


                    for (DateTime currentDate = SelectedDateFrom; currentDate <= SelectedDateTo; currentDate = currentDate.AddDays(1))
                    {
                        int revenue = await BillService.Ins.getBillByDate(currentDate);
                        revenueValues.Add(revenue);
                        dates.Add(currentDate);
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

                    return;
                }

                //sách ưa thích

                if (CaseNav == 3)
                {
                    FavorList = new ObservableCollection<BookDTO>(await Task.Run(() => ThongKeService.Ins.GetTop10SalerBetween(SelectedDateFrom, SelectedDateTo)));
                }
            });
            #endregion
        }

        bool checkThaoTac = false;





        #region bổ trợ cho lựa chọn thời gian của báo cáo công nợ,tồn kho
        private bool _isBorderForDatePickerVisible;
        public bool IsBorderForDatePickerVisible
        {
            get { return _isBorderForDatePickerVisible; }
            set
            {
                _isBorderForDatePickerVisible = value;
                OnPropertyChanged(nameof(IsBorderForDatePickerVisible));
                IsBorderForTonKhoCongNoVisible = !IsBorderForDatePickerVisible;
            }
        }

        private bool _isBorderForTonKhoCongNoVisible;
        public bool IsBorderForTonKhoCongNoVisible
        {
            get { return _isBorderForTonKhoCongNoVisible; }
            set
            {
                _isBorderForTonKhoCongNoVisible = value;
                OnPropertyChanged(nameof(IsBorderForTonKhoCongNoVisible));
            }
        }

        private string month = null;
        private string year = DateTime.Now.Year.ToString();
        private ObservableCollection<string> listThang;
        public ObservableCollection<string> ListThang
        {
            get { return listThang; }
            set
            {
                if (listThang != value)
                {
                    listThang = value;
                    OnPropertyChanged(nameof(ListThang));
                }
            }
        }

        private string _selectedThang;
        public string SelectedThang
        {
            get { return _selectedThang; }
            set
            {
                _selectedThang = value;
                OnPropertyChanged(nameof(SelectedThang));
                month = formatMonth(SelectedThang);

                if (CaseNav == 4)
                {
                    if (debtList != null)
                    {
                        DebtList = new ObservableCollection<DebtReportDTO>(debtList.FindAll(d => d.MonthYear == formatMonthYear(month, year)));
                    }
                }

                if (CaseNav == 5)
                {
                    if (inventoryList != null)
                    {
                        InventoryList = new ObservableCollection<InventoryReportDTO>(inventoryList.FindAll(d => d.MonthYear == formatMonthYear(month, year)));
                    }
                }
            }
        }


        private string textBoxYear;
        public string TextBoxYear
        {
            get { return textBoxYear; }
            set
            {
             

                if (value != textBoxYear)
                {
                    textBoxYear = value;
                    if (IsYear(textBoxYear))
                    {
                        ValidateTextBoxYear = "";
                        year = TextBoxYear;
                        if (CaseNav == 4)
                        {
                            if (debtList != null)
                            {
                                DebtList = new ObservableCollection<DebtReportDTO>(debtList.FindAll(d => d.MonthYear == formatMonthYear(month, year)));
                            }
                        }

                        if (CaseNav == 5)
                        {
                            if (inventoryList != null)
                            {
                                InventoryList = new ObservableCollection<InventoryReportDTO>(inventoryList.FindAll(d => d.MonthYear == formatMonthYear(month, year)));
                            }
                        }
                    }
                    else
                    {
                        ValidateTextBoxYear = "Không hợp lệ!";
                    }
                }
                OnPropertyChanged(nameof(TextBoxYear));

            }
        }

        private string validateTextBoxYear;
        public string ValidateTextBoxYear
        {

            get { return validateTextBoxYear; }
            set
            {
                validateTextBoxYear= value;
                OnPropertyChanged(nameof(ValidateTextBoxYear));
            }
        }
        #endregion

    }
}
