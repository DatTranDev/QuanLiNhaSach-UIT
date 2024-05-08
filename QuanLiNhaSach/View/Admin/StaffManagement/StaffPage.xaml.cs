using QuanLiNhaSach.ViewModel.AdminVM.StaffManagementVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLiNhaSach.View.Admin.StaffManagement
{
    /// <summary>
    /// Interaction logic for StaffPage.xaml
    /// </summary>
    public partial class StaffPage : Page
    {
        public StaffPage()
        {
            InitializeComponent();
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as StaffViewModel).OpenEditStaffCommand.Execute(new object());
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as StaffViewModel).DeleteStaffCommand.Execute(new object());
        }
    }
}
