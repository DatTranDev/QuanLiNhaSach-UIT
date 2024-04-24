using System;
using QuanLiNhaSach.DTOs;
using QuanLiNhaSach.Model;
using QuanLiNhaSach.Model.Service;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Data.SqlClient;

namespace QuanLiNhaSach.ViewModel.AdminVM.ProductVM
{
    public class ProductViewModel : BaseViewModel
    {
        SqlConnection mysql;
        public static List<BookDTO> prdList;
        private ObservableCollection<BookDTO> _productList;

        public ObservableCollection<BookDTO> ProductList
        {
            get { return _productList; }
            set { _productList = value; OnPropertyChanged(); }
        }

        private BookDTO _selectedItem;

        public BookDTO SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }


        private ObservableCollection<string> _genreList;
        public ObservableCollection<string> GenreList
        {
            get { return _genreList; }
            set { _genreList = value; OnPropertyChanged(); }
        }

        //Add page
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private string _price;
        public string Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(); }
        }

        private string _genre;
        public string Genre
        {
            get { return _genre; }
            set { _genre = value; OnPropertyChanged(); }
        }

        private string _count;
        public string Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged(); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        private string _image;
        public string Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(); }
        }

        //Edit page
        private string _editname;
        public string EditName
        {
            get { return _editname; }
            set { _editname = value; OnPropertyChanged(); }
        }

        private string _editPrice;
        public string EditPrice
        {
            get { return _editPrice; }
            set { _editPrice = value; OnPropertyChanged(); }
        }

        private string _editGenre;
        public string EditGenre
        {
            get { return _editGenre; }
            set { _editGenre = value; OnPropertyChanged(); }
        }

        private string _editCount;
        public string EditCount
        {
            get { return _editCount; }
            set { _editCount = value; OnPropertyChanged(); }
        }

        private string _editDescription;
        public string EditDescription
        {
            get { return _editDescription; }
            set { _editDescription = value; OnPropertyChanged(); }
        }

        private string _editImage;
        public string EditImage
        {
            get { return _editImage; }
            set { _editImage = value; OnPropertyChanged(); }
        }
        private string OriginImage { get; set; }
        public ICommand FirstLoadCM { get; set; }
        public ICommand AddSanPhamCM { get; set; }
        public ProductViewModel()
        {
            //mysql = new SqlConnection(@"Data Source=LAPTOP-M863II0B;Initial Catalog=QuanLiNhaSach;Integrated Security=True;MultipleActiveResultSets=true");
            //mysql.Open();
            FirstLoadCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                ProductList = new ObservableCollection<BookDTO>(await BookService.Ins.GetAllBook());
                if (ProductList != null)
                {
                    prdList = new List<BookDTO>(ProductList);
                }
            });
            AddSanPhamCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Name = null;
                Genre = null;
                Price = null;
                Image = null;
                Count = null;
                Description = null;
                

            });
        }
    }
}
