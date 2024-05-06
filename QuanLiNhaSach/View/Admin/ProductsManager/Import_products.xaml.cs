using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLiNhaSach.View.Admin.ProductsManager
{
    /// <summary>
    /// Interaction logic for Import_products.xaml
    /// </summary>
    public partial class Import_products : Window
    {
        public Import_products()
        {
            InitializeComponent();
        }
        public class BookViewModel : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            private ObservableCollection<Book> _books;

            public ObservableCollection<Book> Books
            {
                get { return _books; }
                set
                {
                    _books = value;
                    OnPropertyChanged();
                }
            }

            public BookViewModel()
            {
                Books = new ObservableCollection<Book>();
            }

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public class Book
        {
            public int STT { get; set; }
            public string BookName { get; set; }
            public string Category { get; set; }
            public string Author { get; set; }
            public int Quantity { get; set; }
            public double UnitPrice { get; set; }
            public string ImageLink { get; set; }
        }

    }
}
