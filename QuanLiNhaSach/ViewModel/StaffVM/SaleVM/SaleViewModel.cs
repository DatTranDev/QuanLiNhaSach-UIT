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

namespace QuanLiNhaSach.ViewModel.StaffVM.SaleVM
{
    internal class SaleViewModel : BaseViewModel
    {
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
        private decimal _totalBillValue;

        public decimal TotalBillValue
        {
            get { return _totalBillValue; }
            set { _totalBillValue = value; OnPropertyChanged(); }
        }

        public ICommand FirstLoadCM { get; set; }
        public ICommand Search { get; set; }
        public ICommand SearchCusCM { get; set; }
        public ICommand AddCustomerCM { get; set; }
        async void UpdateCb()
        {
            ProductList = new ObservableCollection<BookDTO>(await BookService.Ins.GetAllBook());
            prdList = new List<BookDTO>(ProductList);
            if (GenreBox != "Tất cả thể loại")
            {
                ProductList = new ObservableCollection<BookDTO>(prdList.FindAll(x => (x.GenreName == GenreBox)));
            }
        }
        public SaleViewModel() 
        {
            FirstLoadCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                ProductList = new ObservableCollection<BookDTO>(await BookService.Ins.GetAllBook());
                if (ProductList != null)
                {
                    prdList = new List<BookDTO>(ProductList);
                }
                ComboList = new ObservableCollection<string>(await GenreService.Ins.GetAllGenreBook());
                ComboList.Insert(0, "Tất cả thể loại");
                GenreBox = "Tất cả thể loại";
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
            AddCustomerCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddCustomerWindow wd = new AddCustomerWindow();
                wd.ShowDialog();
            });
        }

    }
}
