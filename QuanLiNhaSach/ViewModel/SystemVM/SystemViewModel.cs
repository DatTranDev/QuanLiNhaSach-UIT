using QuanLiNhaSach.DTOs;
using QuanLiNhaSach.Model.Service;
using QuanLiNhaSach.Model;
using QuanLiNhaSach.View.MessageBox;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLiNhaSach.ViewModel.SystemVM.Validation;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using QuanLiNhaSach.View.Admin.System_;

namespace QuanLiNhaSach.ViewModel.SystemVM
{
    public class SystemViewModel : BaseViewModel
    {
        private string minReceived;
        public string MinReceived
        {
            get { return minReceived; }
            set { minReceived = value; OnPropertyChanged(nameof(minReceived)); }
        }
        private string maxInventory;
        public string MaxInventory
        {
            get { return maxInventory; }
            set { maxInventory = value; OnPropertyChanged(nameof(maxInventory)); }
        }
        private string maxDebts;
        public string MaxDebts
        {
            get { return maxDebts; }
            set { maxDebts = value; OnPropertyChanged(nameof(maxDebts)); }
        }
        private string minSaleInventory;
        public string MinSaleInventory
        {
            get { return minSaleInventory; }
            set { minSaleInventory = value; OnPropertyChanged(nameof(minSaleInventory)); }
        }
        private string profit;
        public string Profit
        {
            get { return profit; }
            set { profit = value; OnPropertyChanged(nameof(profit)); }
        }
        private bool debtsPolicy = false;
        public bool DebtsPolicy
        {
            get { return debtsPolicy; }
            set { debtsPolicy = value; OnPropertyChanged(nameof(debtsPolicy)); }
        }
        //private string iD;
        //public string ID
        //{
        //    get { return iD; }
        //    set { iD = value; }
        //}
        private string displayName;
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; OnPropertyChanged(nameof(displayName)); }
        }
        private int id = 0;
        private string quantity;
        public string Quantity
        {
            get { return quantity; }
            set { quantity = value; OnPropertyChanged(nameof(quantity)); }
        }
        private GenreBookDTO _selectedItem;
        public GenreBookDTO SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }
        private string editDisplayName;
        public string EditDisplayName
        {
            get { return editDisplayName; }
            set { editDisplayName = value; OnPropertyChanged(); }
        }
        private string editQuantity;
        public string EditQuantity
        {
            get { return editQuantity; }
            set { editQuantity = value; OnPropertyChanged(); }
        }
        private string confirmDisplayName;
        public string ConfirmDisplayName
        {
            get { return confirmDisplayName; }
            set { confirmDisplayName = value; OnPropertyChanged(); }
        }
        private string confirmQuantity;
        public string ConfirmQuantity
        {
            get { return confirmQuantity; }

            set { confirmQuantity = value; OnPropertyChanged(); }
        }
        public SystemValue SV_ins { get; set; }
        private List<string> genreBookList; //Instance of database
        public List<string> GenreBookList
        {
            get { return genreBookList; }
            set { genreBookList = value; OnPropertyChanged(); }
        }
        private ObservableCollection<GenreBookDTO> genreBookObservation; //ListView source
        public ObservableCollection<GenreBookDTO> GenreBookObservation
        {
            get { return genreBookObservation; }
            set
            {
                genreBookObservation = value; OnPropertyChanged();
                OnPropertyChanged(nameof(Count));
            }
        }
        public int Count => GenreBookObservation?.Count ?? 0;
        public GenreBookDTO currentGenreBook;
        private SystemValue data;

        public ICommand FirstLoadCMD { get; set; }
        public ICommand OpenAddWindowCMD { get; set; }
        public ICommand CloseAddWindowCMD { get; set; }
        public ICommand OpenEditWidowCMD { get; set; }
        public ICommand CloseEditWindowCMD { get; set; }
        public ICommand AddDanhMucCMD { get; set; }
        public ICommand RemoveDanhMucCMD { get; set; }
        public ICommand EditDanhMucCMD { get; set; }
        public ICommand SavingCMD { get; set; }
        public bool Validate()
        {
            try
            {
                string error_text = "";
                if (!ValidationUtil.IntValidationRule(MinReceived))
                {
                    error_text += "Invalid MinRecevied\n";
                }
                if (!ValidationUtil.IntValidationRule(MinSaleInventory))
                {
                    error_text += "Invalid MinSaleInventory\n";
                }
                if (!ValidationUtil.IntValidationRule(MaxInventory))
                {
                    error_text += "Invalid MaxInventory\n";

                }
                if (!ValidationUtil.DoubleValidationRule(MaxDebts))
                {
                    error_text += "Invalid MaxDebts\n";
                }
                if (!ValidationUtil.DoubleValidationRule(Profit))
                {
                    error_text += "Invalid Profit\n";
                }
                if (error_text != "")
                {
                    throw new Exception(error_text);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public SystemViewModel()
        {
            FirstLoadCMD = new RelayCommand<object>(null, async (p) =>
            {
                genreBookList = new List<string>(await GenreService.Ins.GetAllGenreBook());

                // Create the observation collection of DTOs
                GenreBookObservation = new ObservableCollection<GenreBookDTO>();

                var bookList = new List<BookDTO>(await BookService.Ins.GetAllBook());

                SystemValue systemValue = await SystemValueService.Ins.GetData();
                MinReceived = systemValue.MinReceived.ToString();
                MaxDebts = ((decimal)systemValue.MaxDebts).ToString("N0");
                MaxInventory = systemValue.MaxInventory.ToString();
                MinSaleInventory = systemValue.MinSaleInventory.ToString();
                DebtsPolicy = (bool)systemValue.DebtsPolicy;
                Profit = systemValue.Profit.ToString();

                for (int i = 0; i < genreBookList.Count; i++)
                {
                    // Create a new DTO instance
                    (int _, GenreBook currentGenre) = await GenreService.Ins.FindGenrePrD(genreBookList[i]);

                    var genreDTO = new GenreBookDTO
                    {
                        STT = i + 1,
                        ID = (int)currentGenre.ID,
                        DisplayName = currentGenre.DisplayName,
                        Quantity = bookList.Count(book => book.IDGenre == currentGenre.ID), // Initial Quantity (you can set this to any default value)
                    };

                    // Add the DTO to the observation collection
                    GenreBookObservation.Add(genreDTO);
                }

            });
            AddDanhMucCMD = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                try
                {
                    if (ConfirmDisplayName == null || ConfirmDisplayName == "")
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Bạn đang nhập thiếu hoặc sai thông tin");
                    }
                    genreBookList = new List<string>(await GenreService.Ins.GetAllGenreBook());

                    var bookList = new List<BookDTO>(await BookService.Ins.GetAllBook());
                    GenreBook newGenre = new GenreBook
                    {
                        DisplayName = this.ConfirmDisplayName,
                        IsDeleted = false
                    };

                    (bool IsAdded, string messageAdd) = await GenreService.Ins.AddNewGenre(newGenre);
                    if (IsAdded)
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Bạn đã thêm thành công danh mục");
                        (int newID, var _) = await GenreService.Ins.FindGenrePrD(newGenre.DisplayName);
                        genreBookList = new List<string>(await GenreService.Ins.GetAllGenreBook());
                        var newGenreDTO = new GenreBookDTO
                        {
                            STT = genreBookList.Count() + 1,
                            ID = newID,
                            DisplayName = newGenre.DisplayName,
                            Quantity = bookList.Count(book => book.IDGenre == newID),
                            IsDeleted = newGenre.IsDeleted
                        };
                        genreBookObservation.Add(newGenreDTO);
                        p.Close();
                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, messageAdd);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            });

            EditDanhMucCMD = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                if (EditDisplayName == null || EditDisplayName == "")
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Bạn đang nhập thiếu hoặc sai thông tin");
                }
                else
                {
                    if (EditDisplayName == SelectedItem.DisplayName)
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Không có gì mới để chỉnh sửa");
                        p.Close();
                        return;
                    }
                    else
                    {
                        GenreBook newGenre = new GenreBook
                        {
                            ID = SelectedItem.ID,
                            DisplayName = EditDisplayName,
                            IsDeleted = false
                        };

                        (bool success, string messageEdit) = await GenreService.Ins.EditGenre(newGenre);
                        if (success)
                        {
                            //genreBookObservation == "need to be modified with the info of newGenre"
                            MessageBoxCustom.Show(MessageBoxCustom.Success, "Bạn đã chỉnh sửa thành công");
                            genreBookList = new List<string>(await GenreService.Ins.GetAllGenreBook());
                            // Create the observation collection of DTOs
                            GenreBookObservation = new ObservableCollection<GenreBookDTO>();

                            var bookList = new List<BookDTO>(await BookService.Ins.GetAllBook());

                            for (int i = 0; i < genreBookList.Count; i++)
                            {
                                // Create a new DTO instance
                                (int _, GenreBook currentGenre) = await GenreService.Ins.FindGenrePrD(genreBookList[i]);

                                var genreDTO = new GenreBookDTO
                                {
                                    STT = i + 1,
                                    ID = (int)currentGenre.ID,
                                    DisplayName = currentGenre.DisplayName,
                                    Quantity = bookList.Count(book => book.IDGenre == currentGenre.ID), // Initial Quantity (you can set this to any default value)
                                };

                                // Add the DTO to the observation collection
                                GenreBookObservation.Add(genreDTO);
                            }
                            p.Close();
                        }
                        else
                        {
                            MessageBoxCustom.Show(MessageBoxCustom.Error, messageEdit);
                        }
                    }
                }
            });

            RemoveDanhMucCMD = new RelayCommand<GenreBookDTO>((p) => { return true; }, async (p) =>
            {
                Warning wd = new Warning("Bạn có muốn xóa danh mục này?");
                wd.ShowDialog();

                try
                {
                    if (wd.DialogResult == true)
                    {
                        genreBookList.Remove(SelectedItem.DisplayName);
                        if (SelectedItem.Quantity > 0)
                        {
                            throw new Exception("Không thể xóa vì tồn tại sản phẩm thuộc danh mục");
                        }
                        var newGenre = new GenreBook
                        {
                            ID = SelectedItem.ID,
                            DisplayName = SelectedItem.DisplayName,
                            IsDeleted = true
                        };
                        (bool success, string messageEdit) = await GenreService.Ins.DeleteGenre(newGenre);
                        if (success)
                        {
                            MessageBoxCustom.Show(MessageBoxCustom.Success, "Đã xóa thành công!");
                            genreBookList = new List<string>(await GenreService.Ins.GetAllGenreBook());
                            // Create the observation collection of DTOs
                            GenreBookObservation = new ObservableCollection<GenreBookDTO>();

                            var bookList = new List<BookDTO>(await BookService.Ins.GetAllBook());

                            for (int i = 0; i < genreBookList.Count; i++)
                            {
                                // Create a new DTO instance
                                (int _, GenreBook currentGenre) = await GenreService.Ins.FindGenrePrD(genreBookList[i]);

                                var genreDTO = new GenreBookDTO
                                {
                                    STT = i + 1,
                                    ID = (int)currentGenre.ID,
                                    DisplayName = currentGenre.DisplayName,
                                    Quantity = bookList.Count(book => book.IDGenre == currentGenre.ID), // Initial Quantity (you can set this to any default value)
                                };

                                // Add the DTO to the observation collection
                                GenreBookObservation.Add(genreDTO);
                            }

                        }
                        else
                        {
                            MessageBoxCustom.Show(MessageBoxCustom.Error, messageEdit);
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, ex.Message);
                }
            });

            OpenAddWindowCMD = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                DisplayName = null;
                Quantity = null;
                AddDanhMuc addDanhMuc = new AddDanhMuc();
                addDanhMuc.ShowDialog();
            });

            CloseAddWindowCMD = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            OpenEditWidowCMD = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                EditDisplayName = SelectedItem.DisplayName;
                EditQuantity = SelectedItem.Quantity.ToString();
                EditDanhMuc editDanhMuc = new EditDanhMuc();
                editDanhMuc.ShowDialog();
            });
            CloseEditWindowCMD = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });



            SavingCMD = new RelayCommand<Window>(null, async (p) =>
            {
                SV_ins = new SystemValue
                {
                    MinReceived = int.Parse(MinReceived),
                    MaxInventory = int.Parse(MaxInventory),
                    MaxDebts = decimal.Parse(MaxDebts),
                    MinSaleInventory = int.Parse(MinSaleInventory),
                    Profit = double.Parse(Profit),
                    DebtsPolicy = DebtsPolicy,
                    ID = 0
                };
                SystemValueService.Ins.EditData(SV_ins, 0);
                MessageBoxCustom.Show(MessageBoxCustom.Success, "Đã lưu thành công!");
            });

        }
    }
}
