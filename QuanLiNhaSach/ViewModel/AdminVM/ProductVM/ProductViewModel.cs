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
using System.IO;

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
        public StaffDTO Staff;
        public static StaffDTO currentStaff;
        //
        private string filePath;
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; OnPropertyChanged(); }
        }
        private SystemValue systemValue;
        public SystemValue SystemValue
        {
            get { return systemValue; }
            set { systemValue = value; OnPropertyChanged(); }
        }

        private List<string> _importComboListString;
        public List<string> ImportComboListString
        {
            get { return _importComboListString; }
            set {  _importComboListString = value; OnPropertyChanged(); }
        }
        private String _selectedImportBook;
        public String SelectedImportBook
        {
            get { return _selectedImportBook; }
            set { _selectedImportBook = value; OnPropertyChanged(); }
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
        private ObservableCollection<BookDTO> addList;
        public ObservableCollection<BookDTO> ListAdd
        {
            get { return addList; }
            set { addList = value; OnPropertyChanged(); }
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
        public ICommand OpenExcel { get; set; }
        public ICommand ExportExcel { get; set; }
        public ICommand AddListProduct { get; set; }
        public ICommand OpenAddList { get; set; }   
        public ICommand AddNewGenre { get; set; }
        public ICommand OpenAddNewGenre { get; set; }
        public ICommand AddBookToImportTable { get; set; }
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
        private string FormatAuthor(string input)
        {
            string[] names = input.Split(',').Select(name => name.Trim()).ToArray();
            Array.Sort(names, new CompareWithVietnameseSigns());
            string sortedNames = string.Join(", ", names);
            return sortedNames;
        }    
        public ProductViewModel()
        {
            FirstLoadCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                Staff  = MainAdminViewModel.currentStaff;
                //
                ProductList = new ObservableCollection<BookDTO>(await BookService.Ins.GetAllBook());
                if (ProductList != null)
                {
                    prdList = new List<BookDTO>(ProductList);
                }
                SystemValue = new SystemValue();
                SystemValue = await SystemValueService.Ins.GetData();
                GenreList = new ObservableCollection<string>(await GenreService.Ins.GetAllGenreBook());
                ComboList = GenreList;
                ComboList.Insert(0, "Tất cả thể loại");
                GenreBox = "Tất cả thể loại";
            });
            AddNewGenre = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                //Mỗi cái một dòng
                if(Genre!=null)
                {
                    string[] lines = Genre.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    int error = 0;
                    foreach (string line in lines)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            GenreBook ab;
                            try
                            {
                                ab = new GenreBook
                                {
                                    DisplayName = line,
                                };
                            }
                            catch
                            {
                                ab = null;
                                error++;
                                continue;
                            }
                            if (ab != null)
                            {
                                (bool b, string c) = await GenreService.Ins.AddNewGenre(ab);
                                if (!b)
                                {
                                    error++;
                                }
                            }
                        } 
                        else { error++; }
                    }
                    Success wd2 = new Success("Đã thêm thành công " + (lines.Length - error) + " danh mục mới");
                    wd2.ShowDialog();
                    GenreList = new ObservableCollection<string>(await GenreService.Ins.GetAllGenreBook());
                    ComboList = new ObservableCollection<string>(await GenreService.Ins.GetAllGenreBook());
                    ComboList.Insert(0, "Tất cả thể loại");
                    GenreBox = "Tất cả thể loại";
                    Genre = null;
                }
            });
            OpenAddNewGenre = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddGenre wd = new AddGenre();
                wd.ShowDialog();

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

                if (this.Name == null || this.Genre == null || this.Image == null || this.Price==null)
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

            OpenAddList = new RelayCommand<Window>((p) => { return true; },  (p) =>
            {
                p.Close();
                ListAdd = new ObservableCollection<BookDTO>();
                AddListProduct wd6 =new AddListProduct(); 
                wd6.ShowDialog();
               
            });

            AddListProduct = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                bool flag = true;
                for(int i=0; i<ListAdd.Count; i++) 
                { 
                    BookDTO item = (BookDTO)ListAdd[i];
                    if(item.Description==null)
                    {
                        item.Description = "";
                    }
                    if(item.DisplayName==null || item.GenreName==null ||item.Image==null || item.Price==null|| item.Inventory==null) 
                    {
                        Error wd= new Error("Bạn nhập thiếu dữ liệu tại dòng thứ " +(i+1));
                        wd.ShowDialog();
                        flag = false;
                        break;
                    }
                    int id;
                    GenreBook genrePrD = new GenreBook();
                    (id, genrePrD) = await GenreService.Ins.FindGenrePrD(item.GenreName);
                    if (id == -1)
                    {
                        Error wd = new Error("Bạn nhập sai dữ liệu tại dòng thứ " + (i + 1));
                        wd.ShowDialog();
                        flag = false;
                        break;
                    }
                    (bool a, Book b) = await BookService.Ins.findIdBook(item.DisplayName, item.GenreName, item.Author, item.Publisher, (int)item.PublishYear);
                    if(a)
                    {
                        Error wd = new Error("Sản phẩm đã tại dòng thứ " + (i + 1) +" đã tồn tại");
                        wd.ShowDialog();
                        flag = false;
                        break;
                    }    
                }
                
                if (flag)
                {
                    for (int i = 0; i < ListAdd.Count; i++)
                    {
                        BookDTO item = (BookDTO)ListAdd[i];
                        int id;
                        GenreBook genrePrD = new GenreBook();
                        (id, genrePrD) = await GenreService.Ins.FindGenrePrD(item.GenreName);
                        Book newPrd;
                        try
                        {
                            newPrd = new Book
                            {
                                DisplayName = item.DisplayName,
                                Price = (decimal)item.Price,
                                Description = item.Description,
                                IDGenre = id,
                                Inventory = (int)item.Inventory,
                                Author = item.Author,
                                Image =item.Image,
                                IsDeleted = false,
                            };
                        }
                        catch
                        {
                            newPrd = null;
                            MessageBoxCustom.Show(MessageBoxCustom.Error, "Chưa nhập đúng dữ liệu");
                            flag = false;
                            break;
                        };
                        if (newPrd != null)
                        {
                            (bool IsAdded, string messageAdd) = await BookService.Ins.AddNewPrD(newPrd);
                            if (!IsAdded)
                            {
                                MessageBoxCustom.Show(MessageBoxCustom.Error, messageAdd);
                                flag = false;
                                break;
                            }
                        }
                    }
                }
                if(flag)
                {
                    Success wd1 = new Success("Thêm sản phẩm thành công");
                    wd1.ShowDialog();
                    ProductList = new ObservableCollection<BookDTO>(await BookService.Ins.GetAllBook());
                    if (ProductList != null)
                    {
                        prdList = new List<BookDTO>(ProductList);
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

            AddBookToImportTable = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ImportItem addValue = new ImportItem(SelectedImportBook);
                addValue.STT = ListImport.Count()+1;
                ListImport.Add(addValue);
            });
            OpenExcel = new RelayCommand<string>((p) => { return true; },(p)=>
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == true)
                {
                    FilePath = openFileDialog.FileName;

                    Success wd = new Success("Đã chọn tệp: " + FilePath);
                    if (p == "ImportGoods")
                    {
                        using (var package = new ExcelPackage(new FileInfo(FilePath)))
                        {
                            try
                            {
                                var worksheet = package.Workbook.Worksheets[0];

                                // Lấy số hàng và số cột của worksheet
                                int rowCount = worksheet.Dimension.Rows;
                                int colCount = worksheet.Dimension.Columns;
                                string input = worksheet.Cells[2, 4].Value.ToString();
                                string[] parts = input.Split(':'); // Tách chuỗi theo dấu ':'
                                if (parts.Length >= 2) // Kiểm tra xem có phần tử thứ 2 hay không
                                {
                                    string dateString = parts[1].Trim(); // Lấy phần tử thứ 2 và loại bỏ dấu cách ở đầu và cuối
                                    DateImport = dateString;
                                }
                                // Duyệt qua từng hàng để đọc dữ liệu
                                for (int row = 5; row <= rowCount; row++) // Bắt đầu từ hàng thứ 2, vì hàng đầu tiên thường là tiêu đề
                                {
                                    // Tạo một đối tượng ExcelData để lưu trữ dữ liệu từ hàng
                                        BookImport = new ImportItem();
                                        // Gán dữ liệu từ các cột vào các thuộc tính của đối tượng ExcelData
                                        BookImport.STT = ListImport.Count()+1;
                                        BookImport.DisplayName = worksheet.Cells[row, 2].Value?.ToString();
                                        BookImport.GenreName = worksheet.Cells[row, 3].Value?.ToString();
                                        BookImport.Author = worksheet.Cells[row, 4].Value?.ToString();
                                        BookImport.Publisher = worksheet.Cells[row, 5].Value?.ToString();
                                        BookImport.PublishYear = Convert.ToInt32(worksheet.Cells[row, 6].Value);
                                        BookImport.Count = Convert.ToInt32(worksheet.Cells[row, 7].Value);
                                        BookImport.Price = Convert.ToDecimal(worksheet.Cells[row, 8].Value);
                                        // Thêm đối tượng ExcelData vào danh sách
                                        ListImport.Add(BookImport);
                                }
                            }
                            catch
                            {
                                Error wd5 = new Error("File tải lên không đúng");
                                wd5.ShowDialog();
                                ListImport = new ObservableCollection<ImportItem>();
                                
                            }
                            // Chọn worksheet đầu tiên
                            
                        }
                    }
                    if (p == "ListNewProduct")
                    {
                        using (var package = new ExcelPackage(new FileInfo(FilePath)))
                        {
                            try
                            {
                                // Chọn worksheet đầu tiên
                                var worksheet = package.Workbook.Worksheets[0];

                                // Lấy số hàng và số cột của worksheet
                                int rowCount = worksheet.Dimension.Rows;
                                int colCount = worksheet.Dimension.Columns;

                                // Duyệt qua từng hàng để đọc dữ liệu
                                for (int row = 4; row <= rowCount; row++) // Bắt đầu từ hàng thứ 2, vì hàng đầu tiên thường là tiêu đề
                                {
                                    // Tạo một đối tượng ExcelData để lưu trữ dữ liệu từ hàng
                                        BookDTO bookDTO = new BookDTO();
                                        // Gán dữ liệu từ các cột vào các thuộc tính của đối tượng ExcelData
                                        //bookDTO = Convert.ToInt32(worksheet.Cells[row, 1].Value);
                                        bookDTO.DisplayName = worksheet.Cells[row, 2].Value?.ToString();
                                        bookDTO.GenreName = worksheet.Cells[row, 3].Value?.ToString();
                                        bookDTO.Author = worksheet.Cells[row, 4].Value?.ToString();
                                        bookDTO.Inventory = Convert.ToInt32(worksheet.Cells[row, 5].Value);
                                        bookDTO.Price = Convert.ToDecimal(worksheet.Cells[row, 6].Value);
                                        bookDTO.Description = worksheet.Cells[row, 7].Value?.ToString();
                                        bookDTO.Image = worksheet.Cells[row, 8].Value?.ToString();
                                        // Thêm đối tượng ExcelData vào danh sách
                                        ListAdd.Add(bookDTO);
                                }
                            }
                            catch
                            {
                                Error wd5 = new Error("File tải lên không đúng");
                                wd5.ShowDialog();
                                ListAdd = new ObservableCollection<BookDTO>();
                            }
                         }
                           
                    }

                }
                
            });
            OpenImportGoods = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                
                ListImport = new ObservableCollection<ImportItem>();
                if(prdList.Count > 0)
                {
                    ImportComboListString = prdList.Select(prd => prd.ToString()).ToList();
                    SelectedImportBook = ImportComboListString.FirstOrDefault();
                }
                DateImport = null;
                Import_products imp= new Import_products();
                imp.ShowDialog();

            });
            ConfirmImport = new RelayCommand<Window>(p => { return true; }, async (p) =>
            {
            //MessageBox.Show("" + ListImport.Count());
            (Staff findedStaff, bool isSuc) = await StaffService.Ins.FindStaff(Staff.ID);
            DateTime date;
            if (DateTime.TryParseExact(DateImport, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                    if(date==null)
                        date = DateTime.Now;
                    GoodReceived = new GoodReceivedDTO
                    {
                        CreateAt = date,
                        Staff = findedStaff,
                        StaffId = this.Staff.ID,
                        Total=0,
                        GoodReceivedInfo = new List<GoodReceivedInfoDTO>()
                    };
                    bool flag = true;
                    for (int i = 0; i < ListImport.Count; i++)
                    {
                        ImportItem item = ListImport[i];
                        if (ListImport[i].DisplayName == null && ListImport[i].GenreName == null && ListImport[i].Author == null)
                        {
                            continue;
                        }
                        if (ListImport[i].DisplayName == null || ListImport[i].GenreName == null || ListImport[i].Author == null || ListImport[i].PublishYear==null || ListImport[i].Publisher==null)
                        {
                                flag = false;
                                Error wd3 = new Error("Nhập thiếu thông tin tại STT: " + ListImport[i].STT);
                                wd3.ShowDialog();
                                break;
                        }
                        if (ListImport[i].Count < SystemValue.MinReceived)
                        {
                            flag = false;
                            Error wd2 = new Error("STT " + ListImport[i].STT + " có số lượng nhập không đủ");
                            wd2.ShowDialog();
                            break;
                        }
                        int id;
                        GenreBook genrePrD = new GenreBook();
                        (id, genrePrD) = await GenreService.Ins.FindGenrePrD(item.GenreName);
                        if (id == -1)
                        {
                            GenreBook newGenre = new GenreBook
                            {
                                DisplayName = item.GenreName,
                            };
                            (bool a1, string b1)= await GenreService.Ins.AddNewGenre(newGenre);
                            if(!a1 && b1==null)
                            {
                                flag = false;
                                Error wd3 = new Error("Xảy ra lỗi tại STT1: " + ListImport[i].STT);
                                wd3.ShowDialog();
                                break;
                            } 

                        }
                        (bool a, Book b) = await BookService.Ins.findIdBook(ListImport[i].DisplayName, ListImport[i].GenreName, FormatAuthor(ListImport[i].Author), ListImport[i].Publisher, ListImport[i].PublishYear);
                        if (!a)
                        {

                            int id2;
                            GenreBook genre1= new GenreBook();
                            (id2, genre1) = await GenreService.Ins.FindGenrePrD(item.GenreName);
                            if (id2 == -1)
                            {
                                Error wd = new Error("Xảy ra lỗi tại STT2: " + item.STT);
                                wd.ShowDialog();
                                flag = false;
                                break;
                            }
                            else
                            {
                                Book newBook = new Book
                                {
                                    DisplayName = item.DisplayName,
                                    IDGenre = id2,
                                    Image= "/QuanLiNhaSach;component/Resources/Image/Book_Default.png",
                                    Author = FormatAuthor(item.Author),
                                    Publisher = item.Publisher,
                                    PublishYear = item.PublishYear,
                                    Price = item.Price,
                                    Description="",
                                    Inventory=0,
                                    IsDeleted= false,
                                    
                                };
                                (bool a2, string b2) = await BookService.Ins.AddNewPrD(newBook);
                                if(!a2)
                                {
                                    Error wd = new Error("Xảy ra lỗi tại STT3: " + item.STT);
                                    wd.ShowDialog();
                                    flag = false;
                                    break;
                                }
                            }    
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
                            if(b.Price!=item.Price)
                            {
                                flag = false;
                                Error wd3 = new Error("STT " + ListImport[i].STT + " có giá nhập sai");
                                wd3.ShowDialog();
                                break;
                            }    
                        }
                    }
                    //Bắt đầu thêm
                    if (flag)
                    {
                        for (int i = 0; i < ListImport.Count; i++)
                        {
                            if (ListImport[i].DisplayName == null && ListImport[i].GenreName == null && ListImport[i].Author == null)
                            {
                                continue;
                            }
                            ImportItem item = ListImport[i];

                            (bool a, Book b) = await BookService.Ins.findIdBook(ListImport[i].DisplayName, ListImport[i].GenreName,FormatAuthor(ListImport[i].Author), ListImport[i].Publisher, ListImport[i].PublishYear);
                            if (a)
                            {
                                    receivedInfo = new GoodReceivedInfoDTO
                                    {
                                        IDBook = b.ID,
                                        Quantity = item.Count,
                                        BookAuthor = item.Author,
                                        BookName = item.DisplayName,
                                        BookGenre = item.GenreName,
                                        BookPrice = item.Price,
                                        TotalPriceItem=item.Price*item.Count,
                                        IsDeleted = false,
                                    };
                                    GoodReceived.GoodReceivedInfo.Add(receivedInfo);
                                    GoodReceived.Total+=receivedInfo.TotalPriceItem;
                            }
                        }
                    }
                    if (flag)
                    {
                        (bool d, string str) = await GoodReceivedService.Ins.AddNewGoodReceived(GoodReceived);
                        if (d)
                        {
                            Success wd4 = new Success("Nhập hàng thành công");
                            ProductList = new ObservableCollection<BookDTO>(await BookService.Ins.GetAllBook());
                            if (ProductList != null)
                            {
                                prdList = new List<BookDTO>(ProductList);
                            }
                            GenreList = new ObservableCollection<string>(await GenreService.Ins.GetAllGenreBook());
                            p.Close();
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

            ExportExcel= new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();

                // Hiển thị hộp thoại chọn thư mục và lấy kết quả
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    string selectedFolderPath = dialog.SelectedPath;
                    // tạo excel
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        // Tạo một worksheet mới
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Books");
                        worksheet.Cells["D1"].Value = "Danh sách sách";
                        var cell = worksheet.Cells["D1"];

                        // Đặt cỡ chữ và in đậm
                        cell.Style.Font.Size = 20;
                        cell.Style.Font.Bold = true;
                        // Ghi tiêu đề cột
                        worksheet.Cells["A3"].Value = "STT";
                        worksheet.Cells["B3"].Value = "Tên sách";
                        worksheet.Cells["C3"].Value = "Thể loại";
                        worksheet.Cells["D3"].Value = "Tác giả";
                        worksheet.Cells["E3"].Value = "Nhà xuất bản";
                        worksheet.Cells["F3"].Value = "Năm xuất bản";
                        worksheet.Cells["G3"].Value = "Số lượng";

                        // Ghi dữ liệu từ danh sách vào worksheet
                        for (int i = 0; i < ProductList.Count; i++)
                        {
                            var book = ProductList[i];
                            worksheet.Cells[i + 4, 1].Value = i+1;
                            worksheet.Cells[i + 4, 2].Value = book.DisplayName;
                            worksheet.Cells[i + 4, 3].Value = book.GenreName;
                            worksheet.Cells[i + 4, 4].Value = book.Author;
                            worksheet.Cells[i + 4, 5].Value = book.Publisher;
                            worksheet.Cells[i + 4, 6].Value = book.PublishYear;
                            worksheet.Cells[i + 4, 7].Value = book.Inventory;
                        }
                        var table = worksheet.Tables.Add(new ExcelAddressBase(3, 1,ProductList.Count + 3, 7), "Book");

                        // Định dạng bảng
                       
                        worksheet.Cells.AutoFitColumns();
                       
                        DateTime date = DateTime.Now;
                        string Date= date.TimeOfDay.Hours.ToString()+"h"+ date.TimeOfDay.Minutes.ToString()+" " + date.Day +"_" + date.Month +"_" + date.Year;
                        string filePath = Path.Combine(selectedFolderPath, "Danh sách sách "+ Date+".xlsx");
                        package.SaveAs(new FileInfo(filePath));
                        Success wd = new Success($"Tệp Excel đã được lưu vào: {filePath}");
                        wd.ShowDialog();
                    }

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

                        Book newPrD = new Book
                        {
                            ID = SelectedItem.ID,
                            DisplayName = EditName,
                            IDGenre = id,
                            Price = decimal.Parse(EditPrice),
                            Inventory = int.Parse(EditInventory),
                            Publisher = SelectedItem.Publisher,
                            PublishYear = SelectedItem.PublishYear,
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
                    UpdateCb();
                }
                else
                {
                    ProductList = new ObservableCollection<BookDTO>(await BookService.Ins.GetAllBook());
                    prdList = new List<BookDTO>(ProductList);
                    if(GenreBox != "Tất cả thể loại")
                    {
                        ProductList = new ObservableCollection<BookDTO>(prdList.FindAll(x => x.DisplayName.ToLower().Contains(p.Text.ToLower()) && x.GenreName == GenreBox));
                    }
                    else
                    {
                        ProductList = new ObservableCollection<BookDTO>(prdList.FindAll(x => x.DisplayName.ToLower().Contains(p.Text.ToLower())));
                    }
                }    
                   
            });

        }

    }
}
