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
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!((e.Key >= Key.D0 && e.Key <= Key.D9) ||  // Số từ 0 đến 9
            (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||  // Số từ bàn phím số
            e.Key == Key.Delete ||  // Phím xóa
            e.Key == Key.Back ||  // Phím backspace
            (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.A)))
            {
                e.Handled = true; // Ngăn chặn ký tự nếu không phải số từ bàn phím
            }
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(tb5.Text))
                {
                    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("vi-VN");
                    string textBefore = tb5.Text.ToString();
                    string textAfter = "";
                    for (int i = 0; i < textBefore.Length; i++)
                    {
                        if (textBefore[i] != '.')
                        {
                            textAfter += textBefore[i];
                        }
                    }
                    int value = Int32.Parse(textAfter);
                    string formattedValue = value.ToString("#,##0", culture);
                    tb5.Text = formattedValue;
                    tb5.Select(tb5.Text.Length, 0);
                }
            }
            catch (Exception)
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Không hợp lệ");
            }
        }

        private void tb5_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
