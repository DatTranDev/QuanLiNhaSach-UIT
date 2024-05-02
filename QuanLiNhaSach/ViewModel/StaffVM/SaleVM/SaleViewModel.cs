using QuanLiNhaSach.DTOs;
using QuanLiNhaSach.Model;
using QuanLiNhaSach.Model.Service;
using System.Collections.Generic;
using QuanLiNhaSach.View.Staff.Sale;
using System.Linq;
using System.Windows.Controls;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Data.SqlClient;
using Microsoft.Win32;
using QuanLiNhaSach.Utils;
using QuanLiNhaSach.View.MessageBox;
using System.Windows;
using OfficeOpenXml;
using QuanLiNhaSach.View.Admin.CustomerManagement;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System;

namespace QuanLiNhaSach.ViewModel.StaffVM.SaleVM
{
    internal class SaleViewModel : BaseViewModel
    {

        public static StaffDTO currentStaff;
       
        private bool _prdEnable;
        private int nextBillID = -1;

        public int NextBillID
        {
            get { return nextBillID; }
            set { nextBillID = value; OnPropertyChanged(nameof(nextBillID)); }
        }

        public bool prdEnable
        {
            get { return _prdEnable; }
            set { _prdEnable = value; OnPropertyChanged(); }
        }
        private SystemValue SystemValue;
        //public SystemValue SystemValue
        //{
        //    get { return systemValue; }
        //    set { systemValue = value; }
        //}

        private double profit=0;
        public double Profit
        {
            get { return profit; }
            set { profit = value; OnPropertyChanged(); }
        }
        private bool _endEnable;
        public bool EndEnable
        {
            get { return _endEnable; }
            set { _endEnable = value; OnPropertyChanged(); }
        }

        public static List<BookDTO> prdList;
        private ObservableCollection<BookDTO> _productList;

        public ObservableCollection<BookDTO> ProductList
        {
            get { return _productList; }
            set { _productList = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> combogenrelist;
        public ObservableCollection<string> ComboList
        {
            get { return combogenrelist; }
            set { combogenrelist = value; OnPropertyChanged(); }
        }
        private BookDTO _selectedItem;

        public BookDTO SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }
        private string _genreBox;
        public string GenreBox
        {
            get { return _genreBox; }
            set { _genreBox = value; OnPropertyChanged(); UpdateCb(); }
        }


        public static List<BillInfoDTO> billInfoList;
        public static List<BillDTO> billList;

        private ObservableCollection<BillDTO> _billlist;
        public ObservableCollection<BillDTO> BillList
        {
            get { return _billlist; }
            set { _billlist = value; OnPropertyChanged(); }
        }
        private ObservableCollection<BillInfoDTO> _billInfoList;

        public ObservableCollection<BillInfoDTO> BillInfoList
        {
            get { return _billInfoList; }
            set { _billInfoList = value; OnPropertyChanged(); }
        }
        private BillDTO _selectedbill;
        public BillDTO SelectedBill
        {
            get { return _selectedbill; }
            set { _selectedbill = value; OnPropertyChanged(); }
        }
        private BookDTO _selectedPrdItem;
        public BookDTO SelectedPrdItem
        {
            get { return _selectedPrdItem; }
            set { _selectedPrdItem = value; OnPropertyChanged(); }
        }
        private BillInfoDTO _selectedBillInfo;
        public BillInfoDTO SelectedBillInfo
        {
            get { return _selectedBillInfo; }
            set { _selectedBillInfo = value; OnPropertyChanged(); }
        }
        private string _tableName;
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; OnPropertyChanged(); }
        }
        private Customer _cusOfBill;
        public Customer CusOfBill
        {
            get { return _cusOfBill; }
            set { _cusOfBill = value; OnPropertyChanged(); }
        }
        private string _cusInfo;
        public string CusInfo
        {
            get { return _cusInfo; }
            set { _cusInfo = value; OnPropertyChanged(); }
        }
        private decimal _debt;

        public decimal Debt
        {
            get { return _debt; }
            set { _debt = value; OnPropertyChanged(); }
        }

        private decimal _paidBillValue;

        public decimal PaidBillValue
        {
            get { return _paidBillValue; }
            set { _paidBillValue = value; OnPropertyChanged(); UpdateDebt(); }
        }
        private decimal _totalBillValue;

        public decimal TotalBillValue
        {
            get { return _totalBillValue; }
            set { _totalBillValue = value; OnPropertyChanged(); }
        }
        private SolidColorBrush _brush;
        public SolidColorBrush Brush
        {
            get { return _brush; }
            set { _brush = value; OnPropertyChanged(); }
        }

        private string _payContent;
        public string PayContent
        {
            get { return _payContent; }
            set { _payContent = value; OnPropertyChanged(); }
        }
        private bool _payEnabled;
        public bool PayEnabled
        {
            get { return _payEnabled; }
            set { _payEnabled = value; OnPropertyChanged(); }
        }

        private SolidColorBrush _endBackGround;
        public SolidColorBrush EndBackGround
        {
            get { return _endBackGround; }
            set { _endBackGround = value; OnPropertyChanged(); }
        }

        public ICommand FirstLoadCM { get; set; }
        public ICommand Search { get; set; }
        public ICommand SearchCusCM { get; set; }
        public ICommand AddCustomerCM { get; set; }
        public ICommand DeleteBillInfoCM { get; set; }
        public ICommand SubBillInfoCM { get; set; }
        public ICommand PlusBillInfoCM { get; set; }
        public ICommand ChangeCountCM { get; set; }
        public ICommand SelectPrd { get; set; }
        public ICommand PayBill {  get; set; }
        public ICommand EndBill { get; set; }
        async void UpdateCb()
        {
            ProductList = new ObservableCollection<BookDTO>(await BookService.Ins.GetAllBook());
            prdList = new List<BookDTO>(ProductList);
            if (GenreBox != "Tất cả thể loại")
            {
                ProductList = new ObservableCollection<BookDTO>(prdList.FindAll(x => (x.GenreName == GenreBox)));
            }
        }
        void resetData()
        {
            CusInfo = null;
            CusOfBill = null;
            BillInfoList = null;
            TotalBillValue = 0;
            Debt = 0;
            PaidBillValue = 0;
            PayEnabled = false;
        }
        void UpdateDebt()
        {
            if (PaidBillValue > TotalBillValue)
                PaidBillValue = TotalBillValue;
            Debt = TotalBillValue - PaidBillValue;
        }
        public SaleViewModel() 
        {
            FirstLoadCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                SystemValue= new SystemValue();
                SystemValue = await SystemValueService.Ins.GetData();
                if (SystemValue != null)
                Profit=(double)SystemValue.Profit;
                MessageBox.Show("" + Profit);
                ProductList = new ObservableCollection<BookDTO>(await BookService.Ins.GetAllBook());
                if (ProductList != null)
                {
                    prdList = new List<BookDTO>(ProductList);
                }
                ComboList = new ObservableCollection<string>(await GenreService.Ins.GetAllGenreBook());
                ComboList.Insert(0, "Tất cả thể loại");
                GenreBox = "Tất cả thể loại";
                SelectedBill = new BillDTO();
                BillInfoList = new ObservableCollection<BillInfoDTO>();
                billInfoList = new List<BillInfoDTO>(BillInfoList);
                TotalBillValue = 0;
            });
            Search = new RelayCommand<TextBox>((p) => { return true; }, async (p) =>
            {
                if (p.Text == "")
                {
                    ProductList = new ObservableCollection<BookDTO>(await BookService.Ins.GetAllBook());
                }
                else
                {
                    ProductList = new ObservableCollection<BookDTO>(await BookService.Ins.GetAllBook());
                    prdList = new List<BookDTO>(ProductList);
                    ProductList = new ObservableCollection<BookDTO>(prdList.FindAll(x => x.DisplayName.ToLower().Contains(p.Text.ToLower())));
                }
            });
            SearchCusCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                (Customer a, bool success, string messageSearch) = await CustomerService.Ins.findCusbyPhone(CusInfo);
                if (a != null)
                {
                    CusOfBill = a;
                }
                else
                {
                    (Customer b, bool success1, string messageSearch1) = await CustomerService.Ins.findCusbyEmail(CusInfo);
                    if (b != null)
                    {
                        CusOfBill = b;
                    }
                    else
                    {
                        //MessageBoxCustom.Show(MessageBoxCustom.Error, messageSearch);
                    }
                }
            });
            SelectPrd = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedPrdItem != null)
                {

                    Book a = new Book
                    {
                        ID = SelectedPrdItem.ID,
                        DisplayName = SelectedPrdItem.DisplayName,
                        Price = SelectedPrdItem.Price,
                        IDGenre = SelectedPrdItem.IDGenre,
                        Inventory = SelectedPrdItem.Inventory,
                        Author = SelectedPrdItem.Author,    
                        Description = SelectedPrdItem.Description,
                        Image = SelectedPrdItem.Image,
                        IsDeleted = SelectedPrdItem.IsDeleted,
                    };

                    BillInfoDTO billInfo = new BillInfoDTO
                    {

                        IDBook = SelectedPrdItem.ID,
                        IsDeleted = SelectedPrdItem.IsDeleted,
                        PriceItem = SelectedPrdItem.Price,
                        Quantity = 1,
                        Book = a
                    };
                    var billIF = BillInfoList.Where(x => x.IDBook == a.ID).FirstOrDefault();
                    if (billIF == null)
                    {
                        BillInfoList.Add(billInfo);
                        TotalBillValue = TotalBillValue + billInfo.PriceItem ?? 0;
                    }
                    else
                    {
                        billIF.Quantity++;
                    }
                    Debt = TotalBillValue;
                    Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F0BD70"));
                    PayContent = "Thanh toán";
                    PayEnabled = true;

                }
                else
                {
                    MessageBox.Show("Selected Item null");
                }

            });
            //AddCustomerCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            //{
            //    AddCustomerWindow wd = new AddCustomerWindow();
            //    wd.ShowDialog();
            //});

            DeleteBillInfoCM = new RelayCommand<BillInfoDTO>((p) => { return true; }, (p) => {
                SelectedBillInfo = p;
                Warning wd = new Warning("Bạn có muốn xóa hóa đơn này");
                wd.ShowDialog();
                if (wd.DialogResult == true)
                {
                    TotalBillValue = TotalBillValue - SelectedBillInfo.PriceItem ?? 0;
                    BillInfoList.Remove(SelectedBillInfo);
                }
            });
            SubBillInfoCM = new RelayCommand<BillInfoDTO>((p) => { return true; }, (p) => {
                SelectedBillInfo = p;
                if (SelectedBillInfo.Quantity > 0)
                    SelectedBillInfo.Quantity--;
            });
            PlusBillInfoCM = new RelayCommand<BillInfoDTO>((p) => { return true; }, (p) => {
                SelectedBillInfo = p;
                SelectedBillInfo.Quantity++;
            });
            ChangeCountCM = new RelayCommand<BillInfoDTO>((p) => { return true; }, (p) =>
            {
                SelectedBillInfo = p;
                TotalBillValue = TotalBillValue - SelectedBillInfo.PriceItem ?? 0;
                SelectedBillInfo.PriceItem = SelectedBillInfo.Quantity * SelectedBillInfo.Book.Price;
                TotalBillValue = TotalBillValue + SelectedBillInfo.PriceItem ?? 0;
            });

            PayBill = new RelayCommand<Frame>((p) => { return true; }, async (p) =>
            {
                Warning wd = new Warning("Xác nhận thanh toán hóa đơn?");
                wd.ShowDialog();
                if (wd.DialogResult == true)
                {
                        PayEnabled = false;
                        PayContent = "Đang xử lý";

                        (Staff a, bool success1) = await StaffService.Ins.FindStaff(currentStaff.ID);
                        billInfoList = new List<BillInfoDTO>(BillInfoList);
                        SelectedBill.BillInfo = billInfoList;
                        SelectedBill.Customer = CusOfBill;
                        if (CusOfBill != null)
                        {
                            SelectedBill.IDCus = CusOfBill.ID;
                        }
                        else
                        {
                            SelectedBill.IDCus = 1;
                            //SelectedBill.Customer = null;
                        }
                        SelectedBill.IDStaff = currentStaff.ID;
                        SelectedBill.IsDeleted = false;
                        SelectedBill.CreateAt = DateTime.Now;
                        SelectedBill.Staff = a;
                        SelectedBill.TotalPrice = TotalBillValue;
                        SelectedBill.Paid = PaidBillValue;

                        //if (NextBillID == -1)
                        //{
                        //    List<BillDTO> allBill = await BillService.Ins.GetAllBill();
                        //    if (allBill.Count > 0)
                        //    {
                        //        nextBillID = allBill[0].ID + 1;
                        //    }
                        //    else
                        //        NextBillID = 1;
                        //}
                        //else
                        //    NextBillID++;
                        // Lâu
                        (bool isAdded, string messageAdd) = await BillService.Ins.AddNewBill(SelectedBill);
                        if (isAdded)
                        {
                            //billList.Add(SelectedBill);
                            Brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFD8B4"));
                            EndEnable = true;
                            EndBackGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F0BD70"));
                            (bool suc, string mEdit) = await CustomerService.Ins.updateSpend(TotalBillValue, CusOfBill.ID);
                            if (!suc) MessageBoxCustom.Show(MessageBoxCustom.Error, "Chỉnh sửa chi tiêu khách hàng thất bại!");
                        (bool success, string message) = await CustomerService.Ins.updateDebts(Debt, CusOfBill.ID);
                        if (!suc) MessageBoxCustom.Show(MessageBoxCustom.Error, "Chỉnh sửa nợ khách hàng thất bại!");
                           PayContent = "Đã thanh toán";
                            MessageBoxCustom.Show(MessageBoxCustom.Success, "Thành công");
                            new InvoicePrint().ShowDialog();
                        }
                        else
                        {
                            MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                            PayContent = "";
                        }
                        
                        //prdEnable = false;
                }
            });


            EndBill = new RelayCommand<Button>((p) =>{ return true;},  (p) =>
                {
                            resetData();
                            SelectedBill = null;
                            SelectedBill = new BillDTO();
                            CusOfBill = null;
                            CusInfo = null;
                            CusOfBill = new Customer();
                            BillInfoList = null;
                            BillInfoList = new ObservableCollection<BillInfoDTO>();
                            Brush = null;
                            PayEnabled = false;
                            PayContent = "";
                            EndEnable = false;
                            EndBackGround = null;
                            TotalBillValue = 0;
                            SelectedBill = null;
                    
                });
        }

    }
}
