using QuanLiNhaSach.DTOs;
using QuanLiNhaSach.Model.Service;
using QuanLiNhaSach.Model;
using QuanLiNhaSach.Utils;
using QuanLiNhaSach.View.Admin.CustomerManagement;
using QuanLiNhaSach.View.Admin;
using QuanLiNhaSach.View.MessageBox;
using System;
using LiveCharts;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.Entity;
using QuanLiNhaSach.View.Admin.ProductsManager;
using QuanLiNhaSach.View;
using QuanLiNhaSach.View.Staff.Sale;

namespace QuanLiNhaSach.ViewModel.AdminVM
{
    internal class MainAdminViewModel : BaseViewModel
    {
        private SeriesCollection _revenueSeries;
        public SeriesCollection RevenueSeries
        {
            get { return _revenueSeries; }
            set { _revenueSeries = value; OnPropertyChanged(); }
        }

        public static StaffDTO currentStaff;
        private string _currentName;

        public string currentName
        {
            get { return _currentName; }
            set { _currentName = value; OnPropertyChanged(); }
        }

        #region Account Information
        private string editDisplayName;

        public string EditDisplayName
        {
            get { return editDisplayName; }
            set { editDisplayName = value; }
        }

        private string editStartDate;

        public string EditStartDate
        {
            get { return editStartDate; }
            set { editStartDate = value; }
        }

        private string editUserName;

        public string EditUserName
        {
            get { return editUserName; }
            set { editUserName = value; }
        }

        private string editPassWord;

        public string EditPassWord
        {
            get { return editPassWord; }
            set { editPassWord = value; }
        }

        private string editPhoneNumber;

        public string EditPhoneNumber
        {
            get { return editPhoneNumber; }
            set { editPhoneNumber = value; }
        }

        private string editBirthDay;

        public string EditBirthDay
        {
            get { return editBirthDay; }
            set { editBirthDay = value; }
        }

        private string editEmail;
        public string EditEmail
        {
            get { return editEmail; }
            set { editEmail = value; }
        }

        private string editWage;

        public string EditWage
        {
            get { return editWage; }
            set { editWage = value; }
        }

        private string editStatus;

        public string EditStatus
        {
            get { return editStatus; }
            set { editStatus = value; }
        }

        private string editGender;

        public string EditGender
        {
            get { return editGender; }
            set
            {
                editGender = value;
                OnPropertyChanged(nameof(EditGender));
            }
        }

        private string editRole;

        public string EditRole
        {
            get { return editRole; }
            set { editRole = value; }
        }

        public ICommand PasswordChangedInformationCommand { get; set; }
        #endregion
        public ICommand FirstLoadCM { get; set; }
        public ICommand LoadNhanVienPage { get; }
        public ICommand LoadKhachHangPage { get; set; }
        public ICommand LoadSanPhamPage { get; set; }
        public ICommand LoadThongKePage { get; set; } 
        public ICommand LoadHeThongPage { get; set; }
        public ICommand LoadBanHangPage { get; set; }
        public ICommand OpenAccountWindow { get; set; }
        public ICommand EditStaffCommand { get; set; }
        public ICommand OpenHelpPage { get; set; }
        public ICommand LogOutCommand { get; set; }
        public MainAdminViewModel() {
            FirstLoadCM = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new ProductMain(); });
            LoadKhachHangPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new CustomerPage(); });
            LoadSanPhamPage = new RelayCommand<Frame>((p) => { return true; }, (p)=> { p.Content = new ProductMain(); });
            LoadHeThongPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new SystemPage(); });
            LoadBanHangPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new SalePage(); });
            OpenAccountWindow = new RelayCommand<object>(null, (p) =>
            {
                EditBirthDay = currentStaff.BirthDay.ToString();
                EditDisplayName = currentStaff.DisplayName;
                EditEmail = currentStaff.Email;
                EditGender = currentStaff.Gender.Trim();
                EditPassWord = null;
                EditPhoneNumber = currentStaff.PhoneNumber;
                EditRole = currentStaff.Role;
                EditStartDate = currentStaff.StartDate.ToString();
                EditStatus = currentStaff.Status;
                EditUserName = currentStaff.UserName;
                EditWage = ((int)currentStaff.Wage).ToString("N0");
                QuanLiNhaSach.View.Admin.AccountInfomation account = new QuanLiNhaSach.View.Admin.AccountInfomation();
                account.ShowDialog();
            });
            EditStaffCommand = new RelayCommand<Window>(null, async (p) =>
            {
                if (EditDisplayName == null || EditUserName == null || EditPhoneNumber == null || EditEmail == null
                || EditDisplayName == "" || EditUserName == "" || EditPhoneNumber == "" || EditEmail == "")
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Bạn đang nhập thiếu hoặc sai thông tin");
                }

                else
                {
                    string pass;
                    if (EditPassWord == null || EditPassWord == "")
                    {
                        EditPassWord = currentStaff.PassWord;
                        pass = EditPassWord;
                    }
                    else
                        pass = Helper.MD5Hash(EditPassWord);

                    if ((EditPassWord == null || EditPassWord == "") && EditDisplayName == currentStaff.DisplayName && EditEmail == currentStaff.Email && EditUserName == currentStaff.UserName && EditPhoneNumber == currentStaff.PhoneNumber)
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Không có gì mới để chỉnh sửa");
                        p.Close();
                        return;
                    }

                    Staff newStaff = new Staff
                    {
                        ID = currentStaff.ID,
                        DisplayName = this.EditDisplayName,
                        Email = this.EditEmail,
                        Gender = this.EditGender,
                        StartDate = currentStaff.StartDate,
                        Status = this.EditStatus,
                        UserName = this.EditUserName,
                        PassWord = pass,
                        BirthDay = currentStaff.BirthDay,
                        PhoneNumber = this.EditPhoneNumber,
                        Role = this.EditRole,
                        Wage = currentStaff.Wage,
                        IsDeleted = false
                    };
                    (bool success, string messageEdit) = await StaffService.Ins.EditStaff(newStaff);
                    if (success)
                    {
                        using (var context = new QuanLiNhaSachEntities())
                        {
                            Staff staff = await context.Staff.Where(x => x.ID == currentStaff.ID).FirstOrDefaultAsync();
                            StaffDTO curStaff = new StaffDTO
                            {
                                ID = staff.ID,
                                DisplayName = staff.DisplayName,
                                StartDate = staff.StartDate,
                                UserName = staff.UserName,
                                PassWord = staff.PassWord,
                                PhoneNumber = staff.PhoneNumber,
                                BirthDay = staff.BirthDay,
                                Wage = staff.Wage,
                                Status = staff.Status,
                                Email = staff.Email,
                                Gender = staff.Gender,
                                Role = staff.Role,
                                IsDeleted = staff.IsDeleted,
                            };
                            currentStaff = curStaff;
                            currentName = currentStaff.DisplayName;
                        }
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Bạn đã chỉnh sửa thành công");
                        p.Close();
                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, messageEdit);
                    }
                }
            });
            OpenHelpPage = new RelayCommand<object>(null, (p) =>
            {
                try
                {
                    Process.Start("https://web.facebook.com/caphecake");
                }
                catch (Exception)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Có lỗi xảy ra khi mở trang web");
                }
            });
            PasswordChangedInformationCommand = new RelayCommand<PasswordBox>(null, (p) =>
            {
                EditPassWord = p.Password;
            });
        }
    }
}
