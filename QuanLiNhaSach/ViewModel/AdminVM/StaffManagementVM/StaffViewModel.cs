using QuanLiNhaSach.DTOs;
using QuanLiNhaSach.Model.Service;
using QuanLiNhaSach.Model;
using QuanLiNhaSach.Utils;
using QuanLiNhaSach.View.Admin.StaffManagement;
using QuanLiNhaSach.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Windows.Media;
using System.Drawing;
using System.Data.Common;

namespace QuanLiNhaSach.ViewModel.AdminVM.StaffManagementVM
{
    public class StaffViewModel : BaseViewModel
    {

        private List<StaffDTO> staffList; //Instance of database
        private ObservableCollection<StaffDTO> staffObservation; //ListView source
        public ObservableCollection<StaffDTO> StaffObservation
        {
            get { return staffObservation; }
            set
            {
                staffObservation = value; OnPropertyChanged(nameof(StaffObservation));
                OnPropertyChanged(nameof(Count));
            }
        }
        private ObservableCollection<StaffDTO>ListImportStaff; //ListView source
        public ObservableCollection<StaffDTO> listImportStaff
        {
            get { return ListImportStaff; }
            set { ListImportStaff = value; OnPropertyChanged(); }
        }
        public int Count => StaffObservation?.Count ?? 0;

        public ICommand FirstLoadStaffPage { get; }

        //Add Staff
        private string displayName;

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string passWord;

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }

        private string confirmPassWord;

        public string ConfirmPassWord
        {
            get { return confirmPassWord; }
            set { confirmPassWord = value; }
        }

        private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        private DateTime birthDay;
        public DateTime BirthDay
        {
            get { return birthDay; }
            set { birthDay = value; }
        }

        private string wage;

        public string Wage
        {
            get { return wage; }
            set { wage = value; }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string gender;

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        private string role;

        public string Role
        {
            get { return role; }
            set { role = value; }
        }

        private StaffDTO selectedItem;

        public StaffDTO SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; }
        }

        // Edit Staff
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
                if (GenderList.Contains(value))
                {
                    editGender = value;
                    OnPropertyChanged(nameof(EditGender));
                }
            }
        }

        private string editRole;

        public string EditRole
        {
            get { return editRole; }
            set { editRole = value; }
        }

        private ObservableCollection<string> _genderList;

        public ObservableCollection<string> GenderList
        {
            get { return _genderList; }
            set
            {
                _genderList = value;
                OnPropertyChanged(nameof(GenderList));
            }
        }

        private ObservableCollection<string> _statusList;

        public ObservableCollection<string> StatusList
        {
            get { return _statusList; }
            set
            {
                _statusList = value;
                OnPropertyChanged(nameof(StatusList));
            }
        }

        private ObservableCollection<string> _roleList;

        public ObservableCollection<string> RoleList
        {
            get { return _roleList; }
            set { _roleList = value; }
        }


        public ICommand OpenAddWindowCommand { get; }
        public ICommand CloseAddWindowCommand { get; }
        public ICommand SearchStaff { get; }
        public ICommand AddStaffCommand { get; }
        public ICommand ImportStaffCommand {  get; }
        public ICommand ExportStaffCommand { get; }
        public ICommand PasswordChangedCommand { get; }
        public ICommand ConfirmPasswordChangedCommand { get; }
        public ICommand DeleteStaffCommand { get; }
        public ICommand EditStaffCommand { get; }
        public ICommand OpenEditStaffCommand { get; }
        public ICommand CloseEditStaffCommand { get; }
        public ICommand EditPasswordChangedCommand { get; }

        public StaffViewModel()
        {
            StartDate = DateTime.Now;
            BirthDay = DateTime.Now;
            GenderList = new ObservableCollection<string> { "Nam", "Nữ" };
            StatusList = new ObservableCollection<string> { "Đang làm", "Xin nghỉ" };
            RoleList = new ObservableCollection<string> { "Quản lí", "Nhân viên" };
            FirstLoadStaffPage = new RelayCommand<object>(null, async (p) =>
            {
                StaffObservation = new ObservableCollection<StaffDTO>(await StaffService.Ins.GetAllStaff());
                if (StaffObservation.Count > 0)
                    staffList = new List<StaffDTO>(StaffObservation);
            });

            SearchStaff = new RelayCommand<TextBox>(null, (p) =>
            {
                if (p.Text != null)
                {
                    if (staffList != null)
                        StaffObservation = new ObservableCollection<StaffDTO>(staffList.FindAll(x => x.DisplayName.ToLower().Contains(p.Text.ToLower())));
                }
            });

            AddStaffCommand = new RelayCommand<Window>(null, async (p) =>
            {
                int iWage = 0;
                if (DisplayName == null || UserName == null || PassWord == null
                || PhoneNumber == null || Wage == null || Status == null || Email == null
                || Gender == null || Role == null || !int.TryParse(Wage.Replace(".", ""), out iWage)
                || DisplayName == "" || UserName == "" || PassWord == ""
                || PhoneNumber == "" || Wage == "" || Status == "" || Email == ""
                || Gender == "" || Role == "")
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Bạn đang nhập thiếu hoặc sai thông tin");
                }


                else
                {
                    if (PassWord != ConfirmPassWord)
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Xác nhận mật khẩu không đúng");
                        return;
                    }
                    if (DateTime.Compare(BirthDay, new DateTime(1900, 1, 1)) < 0 || DateTime.Compare(BirthDay, DateTime.Now) > 0)
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Ngày sinh không hợp lệ");
                        return;
                    }

                    else if (DateTime.Compare(StartDate, new DateTime(1900, 1, 1)) < 0 || DateTime.Compare(StartDate, DateTime.Now) > 0)
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Ngày bắt đầu không hợp lệ");
                        return;
                    }

                    else if (StartDate.Year - (BirthDay).Year < 16)
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Đảm bảo nhân viên vào làm trên 16 tuổi");
                        return;
                    }
                    string pass = Helper.MD5Hash(this.PassWord);

                    Staff newStaff = new Staff
                    {
                        DisplayName = this.DisplayName,
                        StartDate = this.StartDate,
                        UserName = this.UserName,
                        PassWord = pass,
                        PhoneNumber = this.PhoneNumber,
                        BirthDay = this.BirthDay,
                        Wage = iWage,
                        Status = this.Status,
                        Email = this.Email,
                        Gender = this.Gender,
                        Role = this.Role,
                        IsDeleted = false
                    };
                    (bool IsAdded, string messageAdd) = await StaffService.Ins.AddNewStaff(newStaff);
                    if (IsAdded)
                    {
                        StaffObservation = new ObservableCollection<StaffDTO>(await StaffService.Ins.GetAllStaff());
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Bạn đã thêm thành công nhân viên");
                        p.Close();
                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, messageAdd);
                    }
                }

            });
            ImportStaffCommand = new RelayCommand<Page>((p) => { return true; }, async (p) =>
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                if (openFileDialog.ShowDialog() == true)
                {
                    string filePath = openFileDialog.FileName;
                    try
                    {
                        using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Lấy sheet đầu tiên
                            int rowCount = worksheet.Dimension.Rows;
                            int colCount = worksheet.Dimension.Columns;
                            // Duyệt qua từng dòng để đọc dữ liệu
                            listImportStaff = new ObservableCollection<StaffDTO>();
                            for (int row = 5; row <= rowCount; row++) // Bắt đầu từ hàng 5 để bỏ qua header
                            {
                                try
                                {
                                    StaffDTO staff = new StaffDTO();
                                    staff.DisplayName = worksheet.Cells[row, 2].Value?.ToString();
                                    staff.Email = worksheet.Cells[row, 3].Value?.ToString();
                                    staff.PhoneNumber = worksheet.Cells[row, 4].Value?.ToString();
                                    staff.Role = worksheet.Cells[row, 5].Value?.ToString();
                                    staff.Wage = Convert.ToDecimal(worksheet.Cells[row, 6].Value);
                                    staff.Gender = worksheet.Cells[row, 7].Value?.ToString();
                                    string Date = worksheet.Cells[row, 8].Value?.ToString();
                                    DateTime dateBirthDay;
                                    String[] DateFormat = { "dd/MM/yyyy", "d/M/yyyy", "d/MM/yyyy", "dd/M/yyyy", "dd/MM/yyyy h:mm:ss tt", "d/M/yyyy h:mm:ss tt",
                                    "d/MM/yyyy h:mm:ss tt", "dd/M/yyyy h:mm:ss tt","M/d/yyyy","M/dd/yyyy","MM/d/yyyy","MM/dd/yyyy","M/d/yyyy h:mm:ss tt"
                                    ,"M/dd/yyyy h:mm:ss tt","MM/d/yyyy h:mm:ss tt","MM/dd/yyyy h:mm:ss tt"};
                                    if (DateTime.TryParseExact(Date, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateBirthDay))
                                    {
                                        staff.BirthDay = dateBirthDay;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Invalid Birthday format");
                                        return;
                                    }
                                    string DateStart = worksheet.Cells[row, 9].Value?.ToString();
                                    DateTime dateStart;
                                    if (DateTime.TryParseExact(DateStart, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateStart))
                                    {
                                        staff.StartDate = dateStart;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Invalid Startdate format");
                                        return;
                                    }
                                    staff.Status = worksheet.Cells[row, 10].Value?.ToString();
                                    staff.UserName = worksheet.Cells[row, 11].Value?.ToString();
                                    staff.PassWord = worksheet.Cells[row, 12].Value?.ToString();
                                    listImportStaff.Add(staff);
                                }
                                catch
                                {
                                    Error wd5 = new Error("Dữ liệu trong File tải lên không đúng");
                                    wd5.ShowDialog();
                                    listImportStaff = new ObservableCollection<StaffDTO>();
                                    return;
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi import dữ liệu từ tệp Excel: " + ex.Message);
                    }
                    try
                    {
                        string EmailError = "Email đã tồn tại";
                        string PhoneNumberError = "Số điện thoại đã tồn tại";
                        string UserNameError = "Tài khoản đã tồn tại";
                        for (int i = 0; i < listImportStaff.Count; i++)
                        {
                            //gan du lieu cho viewmodel bang null de dam bao k check sai
                            DisplayName = null;
                            StartDate = DateTime.Now;
                            UserName = null;
                            PassWord = null;
                            PhoneNumber = null;
                            BirthDay = DateTime.Now;
                            Wage = null;
                            Status = null;
                            Email = null;
                            Gender = null;
                            Role = null;
                            ConfirmPassWord = null;
                            //gan du lieu tung phan tu vao de thao tac
                            DisplayName = listImportStaff[i].DisplayName;
                            StartDate = (DateTime)listImportStaff[i].StartDate;
                            UserName = listImportStaff[i].UserName;
                            PassWord = listImportStaff[i].PassWord;
                            PhoneNumber = listImportStaff[i].PhoneNumber;
                            try
                            {
                                if (listImportStaff[i].BirthDay is DateTime date)
                                {
                                    BirthDay = date;
                                }
                                else
                                {
                                    MessageBox.Show("Invalid date format or type. Please check the input data.");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("An unexpected error occurred: " + ex.Message);
                            }
                            Wage = listImportStaff[i].Wage.ToString();
                            Status = listImportStaff[i].Status;
                            Email = listImportStaff[i].Email;
                            Gender = listImportStaff[i].Gender;
                            Role = listImportStaff[i].Role;
                            ConfirmPassWord = listImportStaff[i].PassWord;

                            //check ngoai le
                            int iWage = 0;
                            if (DisplayName == null || UserName == null || PassWord == null
                            || PhoneNumber == null || Wage == null || Status == null || Email == null
                            || Gender == null || Role == null || !int.TryParse(Wage.Replace(".", ""), out iWage)
                            || DisplayName == "" || UserName == "" || PassWord == ""
                            || PhoneNumber == "" || Wage == "" || Status == "" || Email == ""
                            || Gender == "" || Role == "")
                            {
                                MessageBoxCustom.Show(MessageBoxCustom.Error, "Bạn đang nhập thiếu hoặc sai thông tin của nhân viên thứ " + (i + 1) + " trong file Excel");
                                listImportStaff = new ObservableCollection<StaffDTO>();
                                return;
                            }


                            else
                            {
                                if (PassWord != ConfirmPassWord)
                                {
                                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Xác nhận mật khẩu không đúng của nhân viên thứ " + (i + 1) + " trong file Excel");
                                    listImportStaff = new ObservableCollection<StaffDTO>();
                                    return;
                                }
                                if (DateTime.Compare(BirthDay, new DateTime(1900, 1, 1)) < 0 || DateTime.Compare(BirthDay, DateTime.Now) > 0)
                                {
                                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Ngày sinh không hợp lệ của nhân viên thứ " + (i + 1) + " trong file Excel");
                                    listImportStaff = new ObservableCollection<StaffDTO>();
                                    return;
                                }

                                else if (DateTime.Compare(StartDate, new DateTime(1900, 1, 1)) < 0 || DateTime.Compare(StartDate, DateTime.Now) > 0)
                                {
                                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Ngày bắt đầu không hợp lệ của nhân viên thứ " + i + " trong file Excel");
                                    listImportStaff = new ObservableCollection<StaffDTO>();
                                    return;
                                }

                                else if (StartDate.Year - (BirthDay).Year < 16)
                                {
                                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Đảm bảo nhân viên thứ " + (i + 1) + " vào làm trên 16 tuổi");
                                    listImportStaff = new ObservableCollection<StaffDTO>();
                                    return;
                                }
                                string pass = Helper.MD5Hash(this.PassWord);

                                Staff newStaff = new Staff
                                {
                                    DisplayName = this.DisplayName,
                                    StartDate = this.StartDate,
                                    UserName = this.UserName,
                                    PassWord = pass,
                                    PhoneNumber = this.PhoneNumber,
                                    BirthDay = this.BirthDay,
                                    Wage = iWage,
                                    Status = this.Status,
                                    Email = this.Email,
                                    Gender = this.Gender,
                                    Role = this.Role,
                                    IsDeleted = false
                                };
                                (bool IsAdded, string messageAdd) = await StaffService.Ins.AddNewStaff(newStaff);
                                if (!IsAdded && messageAdd.Equals(EmailError))
                                {
                                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Nhân viên thứ " + (i + 1) + " trong file excel có " + messageAdd);
                                    return;
                                }
                                else if (!IsAdded && messageAdd.Equals(PhoneNumberError))
                                {
                                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Nhân viên thứ " + (i + 1) + " trong file excel có " + messageAdd);
                                    return;
                                }
                                else if (!IsAdded && messageAdd.Equals(UserNameError))
                                {
                                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Nhân viên thứ " + (i + 1) + " trong file excel có " + messageAdd);
                                    return;
                                }
                            }
                        }
                        StaffObservation = new ObservableCollection<StaffDTO>(await StaffService.Ins.GetAllStaff());
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Bạn đã thêm danh sách thành công nhân viên từ Excel");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi import dữ liệu từ tệp Excel: " + ex.Message);
                    }

                }
            });
            ExportStaffCommand = new RelayCommand<Page>((p) => { return true; }, async (p) =>
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
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Staff");

                        // Merge hàng từ hàng 1,2 từ cột A -> L
                        var mergedCells = worksheet.Cells["A1:L2"];
                        mergedCells.Merge = true;
                        //set text 
                        mergedCells.Value = "Danh sách nhân viên";
                        mergedCells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        mergedCells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        // Thiết lập cỡ chữ và in đậm
                        mergedCells.Style.Font.Size = 24;
                        mergedCells.Style.Font.Bold = true;
                        mergedCells.Style.Font.Color.SetColor(System.Drawing.Color.Red);

                        //thiet lap mau nen
                        mergedCells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        mergedCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

                        //ghi tieu de cho cot
                        string[] columnHeaders = { "ID","Họ tên", "Email", "Số điện thoại", "Chức vụ", "Lương", "Giới tính", "Ngày sinh", "Ngày bắt đầu", 
                            "Trạng thái", "Tài khoản", "Mật khẩu" };
                        for (int i = 0; i < columnHeaders.Length; i++)
                        {
                            worksheet.Cells[3, i + 1].Value = columnHeaders[i];
                            worksheet.Cells[3, i + 1].Style.Font.Size = 14; // Thiết lập cỡ chữ 14
                            worksheet.Cells[3, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[3, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSkyBlue);
                        }


                        // Ghi dữ liệu từ danh sách vào worksheet
                        for (int i = 0; i < StaffObservation.Count; i++)
                        {
                            var employee = StaffObservation[i];
                            worksheet.Cells[i + 4, 1].Value = employee.ID;
                            worksheet.Cells[i + 4, 2].Value = employee.DisplayName;
                            worksheet.Cells[i + 4, 3].Value = employee.Email;
                            worksheet.Cells[i + 4, 4].Value = employee.PhoneNumber;
                            worksheet.Cells[i + 4, 5].Value = employee.Role;
                            worksheet.Cells[i + 4, 6].Value = employee.Wage;
                            worksheet.Cells[i + 4, 7].Value = employee.Gender;
                            DateTime? birthDate = employee.BirthDay;
                            if (birthDate.HasValue)
                            {
                                worksheet.Cells[i + 4, 8].Value = birthDate.Value.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                MessageBoxCustom.Show(MessageBoxCustom.Error, "Nhân viên có mã "+employee.ID+" không có ngày sinh");
                                return;
                            }
                            DateTime? startDate = employee.StartDate;
                            if (startDate.HasValue)
                            {
                                worksheet.Cells[i + 4, 9].Value = startDate.Value.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                MessageBoxCustom.Show(MessageBoxCustom.Error, "Nhân viên có mã " + employee.ID + " không có ngày bắt đầu");
                                return;
                            }
                            worksheet.Cells[i + 4, 10].Value = employee.Status;
                            worksheet.Cells[i + 4, 11].Value = employee.UserName;
                            worksheet.Cells[i + 4, 12].Value = employee.PassWord;

                            //can trai cho all noi dung cua cot 
                            for (int j = 1; j <= 12; j++)
                            {
                                worksheet.Cells[i + 4, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            }
                        }

                        // Định dạng bảng

                        worksheet.Cells.AutoFitColumns();

                        DateTime date = DateTime.Now;
                        string filePath = Path.Combine(selectedFolderPath, "ListStaff.xlsx");
                        package.SaveAs(new FileInfo(filePath));
                        Success wd = new Success($"Tệp Excel đã được lưu vào: {filePath}");
                        wd.ShowDialog();
                    }

                }
            });
            OpenAddWindowCommand = new RelayCommand<Page>((mainStaffWindow) => { return true; }, (mainStaffWindow) =>
            {
                DisplayName = null;
                StartDate = DateTime.Now;
                UserName = null;
                PassWord = null;
                PhoneNumber = null;
                BirthDay = DateTime.Now;
                Wage = null;
                Status = null;
                Email = null;
                Gender = null;
                Role = null;
                ConfirmPassWord = null;
                AddStaff addStaffWindow = new AddStaff();
                addStaffWindow.ShowDialog();
            });

            CloseAddWindowCommand = new RelayCommand<Window>((mainStaffWindow) => { return true; }, (mainStaffWindow) =>
            {
                mainStaffWindow.Close();
            });

            PasswordChangedCommand = new RelayCommand<PasswordBox>(null, (p) =>
            {
                passWord = p.Password;
            });

            EditPasswordChangedCommand = new RelayCommand<PasswordBox>(null, (p) =>
            {
                EditPassWord = p.Password;
            });

            ConfirmPasswordChangedCommand = new RelayCommand<PasswordBox>(null, (p) =>
            {
                confirmPassWord = p.Password;
            });

            OpenEditStaffCommand = new RelayCommand<object>(null, (p) =>
            {
                EditBirthDay = SelectedItem.BirthDay.ToString();
                EditDisplayName = SelectedItem.DisplayName;
                EditEmail = SelectedItem.Email;
                EditGender = SelectedItem.Gender.Trim();
                EditPassWord = null;
                EditPhoneNumber = SelectedItem.PhoneNumber;
                EditRole = SelectedItem.Role;
                EditStartDate = SelectedItem.StartDate.ToString();
                EditStatus = SelectedItem.Status;
                EditUserName = SelectedItem.UserName;
                EditWage = ((int)SelectedItem.Wage).ToString();
                ModifyStaff modifyStaff = new ModifyStaff();
                modifyStaff.ShowDialog();
            });

            EditStaffCommand = new RelayCommand<Window>(null, async (p) =>
            {
                int iWage;
                if (EditDisplayName == null || EditStartDate == null || EditUserName == null || EditPhoneNumber == null
                || EditBirthDay == null || EditWage == null || EditStatus == null || EditEmail == null || EditGender == null
                || EditRole == null || !int.TryParse(EditWage.Replace(".", ""), out iWage)
                || EditDisplayName == "" || EditUserName == "" || EditPhoneNumber == ""
                || EditWage == "" || EditStatus == "" || EditEmail == "" || EditGender == ""
                || EditRole == "")
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Bạn đang nhập thiếu hoặc sai thông tin");
                }

                else
                {
                    DateTime tempBirthDay;
                    DateTime.TryParseExact(EditBirthDay, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out tempBirthDay);

                    DateTime tempStartDate;
                    DateTime.TryParseExact(EditStartDate, "dd/MM/yyyy", new CultureInfo("vi-VN"), DateTimeStyles.None, out tempStartDate);

                    if ((EditPassWord == null || EditPassWord == "") && EditDisplayName == SelectedItem.DisplayName && EditEmail == SelectedItem.Email
                        && EditGender == SelectedItem.Gender.Trim() && tempStartDate == SelectedItem.StartDate && EditStatus == SelectedItem.Status
                        && EditUserName == SelectedItem.UserName && tempBirthDay == SelectedItem.BirthDay && EditPhoneNumber == SelectedItem.PhoneNumber
                        && EditRole == SelectedItem.Role && iWage == SelectedItem.Wage)
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Không có gì mới để chỉnh sửa");
                        p.Close();
                        return;
                    }

                    if (DateTime.Compare(tempBirthDay, new DateTime(1900, 1, 1)) < 0 || DateTime.Compare(tempBirthDay, DateTime.Now) > 0)
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Ngày sinh không hợp lệ");

                    else if (DateTime.Compare(tempStartDate, new DateTime(1900, 1, 1)) < 0 || DateTime.Compare(tempStartDate, DateTime.Now) > 0)
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Ngày bắt đầu không hợp lệ");

                    else if (tempStartDate.Year - tempBirthDay.Year < 16)
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Đảm bảo nhân viên vào làm trên 16 tuổi");

                    else
                    {
                        string pass;
                        if (EditPassWord == null || EditPassWord == "")
                        {
                            EditPassWord = SelectedItem.PassWord;
                            pass = EditPassWord;
                        }
                        else
                            pass = Helper.MD5Hash(EditPassWord);
                        Staff newStaff = new Staff
                        {
                            ID = SelectedItem.ID,
                            DisplayName = this.EditDisplayName,
                            Email = this.EditEmail,
                            Gender = this.EditGender,
                            StartDate = tempStartDate,
                            Status = this.EditStatus,
                            UserName = this.EditUserName,
                            PassWord = pass,
                            BirthDay = tempBirthDay,
                            PhoneNumber = this.EditPhoneNumber,
                            Role = this.EditRole,
                            Wage = iWage,
                            IsDeleted = false
                        };
                        (bool success, string messageEdit) = await StaffService.Ins.EditStaff(newStaff);
                        if (success)
                        {
                            StaffObservation = new ObservableCollection<StaffDTO>(await StaffService.Ins.GetAllStaff());
                            MessageBoxCustom.Show(MessageBoxCustom.Success, "Bạn đã chỉnh sửa thành công");
                            p.Close();
                        }
                        else
                        {
                            MessageBoxCustom.Show(MessageBoxCustom.Error, messageEdit);
                        }
                    }

                }


            });

            CloseEditStaffCommand = new RelayCommand<Window>(null, (p) =>
            {
                p.Close();
            });

            DeleteStaffCommand = new RelayCommand<object>(null, async (p) =>
            {
                DeleteMessage deleteMessage = new DeleteMessage("Bạn có chắc chắn muốn xóa không?");
                deleteMessage.ShowDialog();
                if (deleteMessage.DialogResult == true)
                {
                    (bool IsDeleted, string messageDelete) = await StaffService.Ins.DeleteStaff(selectedItem.ID);
                    if (IsDeleted)
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Bạn đã xóa thành công nhân viên");
                        StaffObservation = new ObservableCollection<StaffDTO>(await StaffService.Ins.GetAllStaff());
                    } 
                    else
                        MessageBoxCustom.Show(MessageBoxCustom.Error, messageDelete);
                }
            });
        }
    }
}
