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
using QuanLiNhaSach.DTOs;
using QuanLiNhaSach.View.MessageBox;
using QuanLiNhaSach.ViewModel.SystemVM;

namespace QuanLiNhaSach.View.Admin.System_
{
    /// <summary>
    /// Interaction logic for SystemPage.xaml
    /// </summary>
    public partial class SystemPage : Page
    {
        public SystemPage()
        {
            InitializeComponent();
        }

        //private void Edit_Click(object sender, RoutedEventArgs e)
        //{
        //    (DataContext as SystemViewModel).OpenEditWidowCMD.Execute(new object());
        //}

        //private void Delete_Click(object sender, RoutedEventArgs e)
        //{
        //    (DataContext as SystemViewModel).RemoveDanhMucCMD.Execute(new object());
        //}

        private void GenreList_MouseMove(object sender, MouseEventArgs e) { }
        //{
        //    // Get the ListViewItem under the mouse cursor
        //    ListViewItem listViewItem = GetListViewItemAtMousePosition(e.GetPosition(GenreList));

        //    if (listViewItem != null && listViewItem.GetType() == typeof(GenreBookDTO))
        //    {
        //        // Update the SelectedItem to the DataContext of the ListViewItem
        //        GenreBookDTO selectedGenre = listViewItem.DataContext as GenreBookDTO;
        //        if (selectedGenre != null)
        //        {
        //            MessageBoxCustom.Show(MessageBoxCustom.Success, "Cập nhật được");
        //            GenreList.SelectedItem = selectedGenre;
        //        }
        //        else
        //        {
        //            MessageBoxCustom.Show(MessageBoxCustom.Error, "Không được");
        //        }
        //    }
        //}

        //private ListViewItem GetListViewItemAtMousePosition(Point mousePosition)
        //{
        //    // Find the ListViewItem under the mouse position
        //    HitTestResult hitTestResult = VisualTreeHelper.HitTest(GenreList, mousePosition);
        //    DependencyObject hitItem = hitTestResult?.VisualHit;

        //    while (hitItem != null && !(hitItem is ListViewItem))
        //    {
        //        hitItem = VisualTreeHelper.GetParent(hitItem);
        //    }

        //    return hitItem as ListViewItem;
        //}
    }
}
