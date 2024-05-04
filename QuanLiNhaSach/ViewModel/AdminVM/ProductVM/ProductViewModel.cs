using System;
using QuanLiNhaSach.DTOs;
using QuanLiNhaSach.Model;
using QuanLiNhaSach.Model.Service;
using System.Collections.Generic;
using QuanLiNhaSach.View.Admin.ProductsManager;
using System.Linq;
using System.Windows.Controls;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.Globalization;
using QuanLiNhaSach.Utils;
using QuanLiNhaSach.View.MessageBox;
using System.Windows;
using System.Diagnostics;
using OfficeOpenXml;


namespace QuanLiNhaSach.ViewModel.AdminVM.ProductVM
{
    public class ProductViewModel : BaseViewModel
    {
        //test
        public ObservableCollection<StaffDTO> listStaff;
        public ObservableCollection<StaffDTO> ListStaff
        {
            get { return listStaff; }
            set { listStaff = value; OnPropertyChanged(); }
        }
        public Staff Staff;
        public static StaffDTO currentStaff;
        //
        private SystemValue systemValue;
        public SystemValue SystemValue
        {
            get { return systemValue; }
            set { systemValue = value; OnPropertyChanged(); }
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

        private ImportItem bookImport;

        public ImportItem BookImport
        {
            get { return bookImport; }
            set { bookImport = value; OnPropertyChanged(); }
        }



        private ObservableCollection<ImportItem> listImport;
        public ObservableCollection<ImportItem> ListImport
        {
            get { return listImport; }
            set { listImport = value; OnPropertyChanged(); }
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

        private string _inventory;
        public string Inventory
        {
            get { return _inventory; }
            set { _inventory = value; OnPropertyChanged(); }
        }

        private string _author;
        public string Author
        {
            get { return _author; }
            set { _author = value; OnPropertyChanged(); }
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

        private string _editAuthor;
        public string EditAuthor
        {
            get { return _editAuthor; }
            set { _editAuthor = value; OnPropertyChanged(); }
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

        private string _editInventory;
        public string EditInventory
        {
            get { return _editInventory; }
            set { _editInventory = value; OnPropertyChanged(); }
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

        private string _genreBox;
        public string GenreBox
        {
            get { return _genreBox; }
            set { _genreBox = value; OnPropertyChanged(); UpdateCb(); }
        }


        private GoodReceivedInfoDTO receivedInfo;
        public GoodReceivedInfoDTO ReceivedInfo
        {
            get { return receivedInfo; }
            set { receivedInfo = value; OnPropertyChanged(); }
        }

        private GoodReceivedDTO goodReceived;
        public GoodReceivedDTO GoodReceived
        {
            get { return goodReceived; }
            set { goodReceived = value; OnPropertyChanged(); }
        }

        private string dateImport;
        public string DateImport
        {
            get { return dateImport; }
            set { dateImport = value; OnPropertyChanged(); }
        }
        private string OriginImage { get; set; }
        public ICommand FirstLoadCM { get; set; }
        public ICommand AddSanPhamCM { get; set; }
        public ICommand AddSanPhamListCM { get; set; }
        public ICommand ProductFilter { get; set; }
        public ICommand AllPrDFilter { get; set; }
        public ICommand CloseWdCM { get; set; }
        public ICommand OpenEditWdCM { get; set; }
        public ICommand EditSanPhamListCM { get; set; }
        public ICommand DeleteSanPhamListCM { get; set; }
        public ICommand UploadImageCM { get; set; }
        public ICommand EditImageCM { get; set; }
        public ICommand OpenImportGoods { get; set; }
        public ICommand ConfirmImport { get; set; }
        public ICommand CloseWd { get; set; }
        public ICommand Search { get; set; }

        private string FormalPrice(string s)
        {
            string valuePrice = "";
            valuePrice += s.Substring(0, s.Length - 5);
            return valuePrice;
        }
        private void closingWd(Window p)
        {
            EditName = null;
            EditGenre = null;
            EditPrice = null;
            EditInventory = null;
            EditImage = null;
            EditDescription = null;
            OriginImage = null;
            Name = null;
            Genre = null;
            Price = null;
            Image = null;
            Inventory = null;
            Description = null;
            SelectedItem = null;
            p.Close();
        }

        async void UpdateCb()
        {
            ProductList = new ObservableCollection<BookDTO>(await BookService.Ins.GetAllBook());
            prdList = new List<BookDTO>(ProductList);
            if (GenreBox != "Tất cả thể loại")
            {
                ProductList = new ObservableCollection<BookDTO>(prdList.FindAll(x => (x.GenreName == GenreBox)));
            }
        }
        public ProductViewModel()
        {
            FirstLoadCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                //test
                (Staff e,bool g) = await StaffService.Ins.FindStaff(5);
                Staff = e;
                MessageBox.Show("" + Staff.ID);
                //
                SystemValue = new SystemValue();
                SystemValue = await SystemValueService.Ins.GetData();
                ProductList = new ObservableCollection<BookDTO>(await BookService.Ins.GetAllBook());
                if (ProductList != null)
                {
                    prdList = new List<BookDTO>(ProductList);
                }
                GenreList = new ObservableCollection<string>(await GenreService.Ins.GetAllGenreBook());
                ComboList = new ObservableCollection<string>(await GenreService.Ins.GetAllGenreBook());
                ComboList.Insert(0, "Tất cả thể loại");
                GenreBox = "Tất cả thể loại";
            });
            AddSanPhamCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Name = null;
                Genre = null;
                Price = null;
                Image = null;
                Author = null;
                Inventory = null;
                Description = null;
                AddProducts add= new AddProducts();
                add.ShowDialog();
            });
            AddSanPhamListCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {

                if (this.Name == null || this.Genre == null || this.Image == null)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Không nhập đủ dữ liệu!");
                }
                else
                {
                    int id;
                    GenreBook genrePrD = new GenreBook();
                    (id, genrePrD) = await GenreService.Ins.FindGenrePrD(Genre);
                    if (this.Description == null) this.Description = "";
                    Book newPrd;
                    try
                    {
                        newPrd = new Book
                        {
                            DisplayName = this.Name,
                            Price = decimal.Parse(this.Price),
                            Description = this.Description,
                            IDGenre = id,
                            Inventory = int.Parse(this.Inventory),
                            Author = this.Author,
                            Image = await CloudService.Ins.UploadImage(this.Image),
                            IsDeleted = false,

                        };
                    }
                    catch {
                        newPrd = null;
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Chưa nhập đúng dữ liệu");
                    };

                   if(newPrd != null) {
                        (bool IsAdded, string messageAdd) = await BookService.Ins.AddNewPrD(newPrd);
                        if (IsAdded)
                        {
                            p.Close();
                            ProductList = new ObservableCollection<BookDTO>(await BookService.Ins.GetAllBook());
                            prdList = new List<BookDTO>(ProductList);
                            MessageBoxCustom.Show(MessageBoxCustom.Success, "Thêm thành công");
                            //AddedSuccessfully addedSuccessfully = new AddedSuccessfully();
                            //addedSuccessfully.Show();
                        }
                        else
                        {
                            MessageBoxCustom.Show(MessageBoxCustom.Error, messageAdd);
                        }
                    }
                    
                }

            });

            UploadImageCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.png;*.jpeg;*.gif|All Files|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    Image = openFileDialog.FileName;
                    if (Image != null)
                    {
                        // Image was uploaded successfully.                        
                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Tải ảnh lên thất bại!");
                    }
                }

            });

            EditImageCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.png;*.jpeg;*.webp;*.gif|All Files|*.*";
                if (openFileDialog.ShowDialog() == true)
                {

                    EditImage = openFileDialog.FileName;
                    if (EditImage != null)
                    {
                        // Image was uploaded successfully.                        
                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Tải ảnh lên thất bại!");
                    }
                }
            });

            CloseWdCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                EditName = null;
                EditGenre = null;
                EditPrice = null;
                EditInventory = null;
                EditImage = null;
                EditDescription = null;
                OriginImage = null;
                Name = null;
                Genre = null;
                Price = null;
                Image = null;
                Inventory = null;
                Description = null;
                p.Close();
            });
            OpenImportGoods = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                
                ListImport = new ObservableCollection<ImportItem>();
                Import_products imp= new Import_products();
                imp.ShowDialog();

            });
            ConfirmImport = new RelayCommand<Window>(p => { return true; }, async (p) =>
            {
                //MessageBox.Show("" + ListImport.Count());
                DateTime date;
                if (DateTime.TryParseExact(DateImport, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    GoodReceived = new GoodReceivedDTO
                    {
                        CreateAt = date,
                        Staff = this.Staff,
                        StaffId = this.Staff.ID,
                        GoodReceivedInfo = new List<GoodReceivedInfoDTO>()
                    };
                    bool flag = true;
                    for (int i = 0; i < ListImport.Count; i++)
                    {
                        if (ListImport[i].DisplayName == null || ListImport[i].GenreName == null || ListImport[i].Author == null)
                        {
                            if (ListImport[i].DisplayName == null && ListImport[i].GenreName == null && ListImport[i].Author == null)
                            {
                                continue;
                            }
                            else
                            {
                                flag = false;
                                Error wd3 = new Error("Nhập thiếu thông tin tại STT: " + ListImport[i].STT);
                                wd3.ShowDialog();
                                break;
                            }

                        }
                        (bool a, Book b) = await BookService.Ins.findIdBook(ListImport[i].DisplayName, ListImport[i].GenreName, ListImport[i].Author);
                        if (!a)
                        {
                            flag = false;
                            Error wd = new Error("Thông tin nhập sai tại STT: " + ListImport[i].STT);
                            wd.ShowDialog();
                            break;
                        }
                        else
                        {
                            if (b.Inventory > SystemValue.MaxInventory)
                            {
                                flag = false;
                                Error wd1 = new Error("STT " + ListImport[i].STT + " ở kho vẫn còn hàng");
                                wd1.ShowDialog();
                                break;
                            }
                            if (ListImport[i].Count < SystemValue.MinReceived)
                            {
                                flag = false;
                                Error wd2 = new Error("STT " + ListImport[i].STT + " có số lượng nhập không đủ");
                                wd2.ShowDialog();
                                break;
                            }
                        }
                    }
                    if (flag)
                    {
                        for (int i = 0; i < ListImport.Count; i++)
                        {
                            if (ListImport[i].DisplayName == null && ListImport[i].GenreName == null && ListImport[i].Author == null)
                            {
                                continue;
                            }
                            ImportItem item = ListImport[i];

                            (bool a, Book b) = await BookService.Ins.findIdBook(ListImport[i].DisplayName, ListImport[i].GenreName, ListImport[i].Author);
                            if (a)
                            {
                                if (b.Price < ListImport[i].Price)
                                {
                                    b.Price = ListImport[i].Price;
                                }
                                b.Inventory += ListImport[i].Count;
                                Book newPrD = new Book
                                {
                                    ID = b.ID,
                                    DisplayName = b.DisplayName,
                                    IDGenre = b.IDGenre,
                                    Price = (decimal)b.Price,
                                    Inventory = b.Inventory,
                                    Author = b.Author,
                                    Image = b.Image,
                                    Description = b.Description,
                                    IsDeleted = false,
                                };
                                (bool c, string s) = await BookService.Ins.EditPrD(newPrD, b.ID);
                                if (!c)
                                {
                                    flag = false;
                                    break;
                                }
                                else
                                {
                                    receivedInfo = new GoodReceivedInfoDTO
                                    {
                                        IDBook = b.ID,
                                        Quantity = item.Count,
                                        BookAuthor = item.Author,
                                        BookName = item.DisplayName,
                                        BookGenre = item.GenreName,
                                        BookPrice = item.Price,
                                        IsDeleted = false,
                                    };
                                    GoodReceived.GoodReceivedInfo.Add(receivedInfo);
                                }
                            }
                        }
                    }
                    if (flag)
                    {
                        (bool d, string str) = await GoodReceivedService.Ins.AddNewGoodReceived(GoodReceived);
                        if (d)
                        {
                            Success wd4 = new Success("Nhập hàng thành công");
                            wd4.ShowDialog();
                        }

                    }
                } 
                else
                {
                    Error wd5 = new Error("Nhập sai ngày tháng năm");
                    wd5.ShowDialog();
                }    
                
            });
            CloseWd = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            
            

            OpenEditWdCM = new RelayCommand<BookDTO>((p) => { return true; }, (p) => {
                SelectedItem = p;
                BookDTO a = new BookDTO();
                a = SelectedItem;

                EditName = SelectedItem.DisplayName;
                EditGenre = SelectedItem.GenreName;
                EditPrice = FormalPrice(SelectedItem.Price.ToString());
                EditInventory = SelectedItem.Inventory.ToString();
                EditAuthor = SelectedItem.Author.ToString();
                EditImage = SelectedItem.Image;
                EditDescription = SelectedItem.Description;
                OriginImage = SelectedItem.Image;
                EditProduct wd = new EditProduct();
                wd.ShowDialog();
            });
            EditSanPhamListCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                if (SelectedItem == null)
                    MessageBox.Show("SelectedItem null");
                else
                {
                    if (this.EditName == null || this.EditGenre == null || this.EditImage == null|| this.EditAuthor==null)
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Không nhập đủ dữ liệu!");
                    }
                    else
                    {
                        if (this.EditDescription == null) this.EditDescription = "";
                        int id;

                        GenreBook genrePrD = new GenreBook();
                        (id, genrePrD) = await GenreService.Ins.FindGenrePrD(EditGenre);
                        if (OriginImage != EditImage)
                        {
                            await CloudService.Ins.DeleteImage(OriginImage);
                            EditImage = await CloudService.Ins.UploadImage(EditImage);
                        }

                        Book newPrD = new Book
                        {
                            ID = SelectedItem.ID,
                            DisplayName = EditName,
                            IDGenre = id,
                            Price = decimal.Parse(EditPrice),
                            Inventory = int.Parse(EditInventory),
                            Author = EditAuthor,
                            Image = EditImage,
                            Description = EditDescription,
                            IsDeleted = false,
                        };
                        (bool success, string messageEdit) = await BookService.Ins.EditPrD(newPrD, SelectedItem.ID);
                        if (success)
                        {
                            ProductList = new ObservableCollection<BookDTO>(await BookService.Ins.GetAllBook());
                            prdList = new List<BookDTO>(ProductList);
                            MessageBoxCustom.Show(MessageBoxCustom.Success, "Sửa thành công");
                            closingWd(p);
                        }
                        else
                        {
                            MessageBoxCustom.Show(MessageBoxCustom.Error, messageEdit);
                        }
                    }
                }

            });
            DeleteSanPhamListCM = new RelayCommand<BookDTO>((p) => { return true; }, async (p) =>
            {
                SelectedItem = p;
                BookDTO a = new BookDTO();
                a = SelectedItem;

                Warning wd = new Warning();
                wd.ShowDialog();
                if (wd.DialogResult == true)
                {
                    string deleteImg = SelectedItem.Image;
                    await CloudService.Ins.DeleteImage(deleteImg);
                    (bool sucess, string messageDelete) = await BookService.Ins.DeletePrD(SelectedItem.ID);
                    if (sucess)
                    {
                        ProductList.Remove(SelectedItem);
                        prdList = new List<BookDTO>(ProductList);
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Bạn đã xóa thành công");
                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, messageDelete);
                    }
                }
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

        }

    }
}
