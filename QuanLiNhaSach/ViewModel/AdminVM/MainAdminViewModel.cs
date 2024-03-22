using QuanLiNhaSach.View.Admin.CustomerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiNhaSach.ViewModel.AdminVM
{
    internal class MainAdminViewModel : BaseViewModel
    {
        public ICommand FirstLoadCM { get; set; }
        public ICommand LoadNhanVienPage { get; }
        public ICommand LoadKhachHangPage { get; set; }
        public ICommand LoadSanPhamPage { get; set; }
        public ICommand LoadThongKePage { get; set; } 
        public ICommand LoadHeThongPage { get; set; }
        public ICommand OpenAccountWindow { get; set; }
        public ICommand EditStaffCommand { get; set; }
        public ICommand OpenHelpPage { get; set; }
        public ICommand LogOutCommand { get; set; }
        public MainAdminViewModel() {
            LoadKhachHangPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new CustomerPage(); });
        }
    }
}
