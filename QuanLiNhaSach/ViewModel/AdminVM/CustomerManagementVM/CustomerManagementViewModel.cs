using QuanLiNhaSach.DTOs;
using QuanLiNhaSach.Model;
using QuanLiNhaSach.Model.Service;
using QuanLiNhaSach.View.Admin.CustomerManagement;
using QuanLiNhaSach.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiNhaSach.ViewModel.AdminVM.CustomerManagementVM
{
    public class CustomerManagementViewModel : BaseViewModel
    {
        public static List<CustomerDTO> cusList;
        private ObservableCollection<CustomerDTO> _customerList;

        public ObservableCollection<CustomerDTO> CustomerList
        {
            get { return _customerList; }
            set { _customerList = value; OnPropertyChanged(); }
        }
        private CustomerDTO _selectedItem;

        public CustomerDTO SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }
        //Add page
        private string _editname;

        public string EditName
        {
            get { return _editname; }
            set { _editname = value; }
        }
        private string _editemail;

        public string EditEmail
        {
            get { return _editemail; }
            set { _editemail = value; }
        }
        private string _editphoneNumber;

        public string EditPhoneNumber
        {
            get { return _editphoneNumber; }
            set { _editphoneNumber = value; }
        }
        private string _editspend;

        public string EditSpend
        {
            get { return _editspend; }
            set { _editspend = value; }
        }
        private string _editdescription;

        public string EditDescription
        {
            get { return _editdescription; }
            set { _editdescription = value; }
        }
        private string _editAddress;

        public string EditAddress
        {
            get { return _editAddress; }
            set { _editAddress = value; }
        }
        private string _editDebts;

        public string EditDebts
        {
            get { return _editDebts; }
            set { _editDebts = value; }
        }
        //Edit page
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }
        private string _spend;

        public string Spend
        {
            get { return _spend; }
            set { _spend = value; }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private string _Address;

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        private string _Debts;

        public string Debts
        {
            get { return _Debts; }
            set { _Debts = value; }
        }

        public ICommand FirstLoadCM { get; set; }
        public ICommand SearchCustomerCM { get; set; }
        public ICommand AddCusWdCM { get; set; }
        public ICommand AddCusListCM { get; set; }
        public ICommand CloseWdCM { get; set; }
        public ICommand OpenEditWdCM { get; set; }
        public ICommand EditCusListCM { get; set; }
        public ICommand DeleteCusListCM { get; set; }
        public ICommand DebtCusCM { get; set; }
        public CustomerManagementViewModel()
        {
            FirstLoadCM = new RelayCommand<Page>((p) => { return true; }, async (p) =>
            {
                CustomerList = new ObservableCollection<CustomerDTO>(await Task.Run(() => CustomerService.Ins.GetAllCus()));
                if (CustomerList != null)
                    cusList = new List<CustomerDTO>(CustomerList);
            });

            SearchCustomerCM = new RelayCommand<TextBox>((p) => { return true; }, async (p) =>
            {
                if (p.Text == "")
                {
                    CustomerList = new ObservableCollection<CustomerDTO>(await CustomerService.Ins.GetAllCus());
                }
                else
                {
                    CustomerList = new ObservableCollection<CustomerDTO>(cusList.FindAll(x => x.DisplayName.ToLower().Contains(p.Text.ToLower())));
                }

            });
            AddCusWdCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddCustomerWindow wd = new AddCustomerWindow();
                wd.ShowDialog();
            });
            AddCusListCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                if (this.Name == null || this.PhoneNumber == null || this.Email == null)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Không nhập đủ dữ liệu");
                }
                else
                {
                    if (this.Description == null) this.Description = "";
                    if (this.Spend == null || this.Spend == "") this.Spend = "0";
                    Customer newCus = new Customer
                    {
                        Description = this.Description,
                        DisplayName = this.Name,
                        PhoneNumber = this.PhoneNumber,
                        Email = this.Email,
                        Address = this.Address,
                        IsDeleted = false,

                    };
                    (bool IsAdded, string messageAdd) = await CustomerService.Ins.AddNewCus(newCus);
                    if (IsAdded)
                    {
                        p.Close();
                        resetData();
                        CustomerList = new ObservableCollection<CustomerDTO>(await CustomerService.Ins.GetAllCus());
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Đã thêm khách hàng thành công");
                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi khi thêm khách hàng");
                    }
                }
            });
            OpenEditWdCM = new RelayCommand<object>((p) => { return true; }, (p) => {
                EditName = SelectedItem.DisplayName;
                EditEmail = SelectedItem.Email;
                EditDescription = SelectedItem.Description;
                EditPhoneNumber = SelectedItem.PhoneNumber;
                EditSpend = SelectedItem.Spend.ToString();
                EditAddress = SelectedItem.Address;
                EditDebts = SelectedItem.Debts.ToString();
                EditCustomerWindow wd = new EditCustomerWindow();
                wd.ShowDialog();
            });
            EditCusListCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {

                if (this.EditName == null || this.EditPhoneNumber == null || this.EditEmail == null)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Không nhập đủ dữ liệu");
                }
                else
                {
                    if (this.EditDescription == null) this.EditDescription = "";

                    Customer newCus = new Customer
                    {
                        ID = SelectedItem.ID,
                        Description = EditDescription,
                        PhoneNumber = EditPhoneNumber,
                        Email = EditEmail,
                        DisplayName = EditName,
                        Address = EditAddress,
                        IsDeleted = false,
                    };
                    (bool success, string messageEdit) = await CustomerService.Ins.EditCusList(newCus, SelectedItem.ID);
                    if (success)
                    {
                        p.Close();
                        CustomerList = new ObservableCollection<CustomerDTO>(await CustomerService.Ins.GetAllCus());
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Đã sửa khách hàng thành công");
                    }
                    else
                    {
                        MessageBox.Show(messageEdit);
                    }
                }

            });
            DeleteCusListCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                Warning wd = new Warning();
                wd.ShowDialog();
                if (wd.DialogResult == true)
                {
                    (bool sucess, string messageDelete) = await CustomerService.Ins.DeleteCustomer(SelectedItem.ID);
                    if (sucess)
                    {
                        CustomerList.Remove(SelectedItem);
                        MessageBoxCustom.Show(MessageBoxCustom.Success, "Đã xóa khách hàng thành công");
                    }
                    else
                    {
                        MessageBox.Show(messageDelete);
                    }
                }

            });
            CloseWdCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            DebtCusCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DebtingWindow wd = new DebtingWindow();
                wd.ShowDialog();
            });
        }
        void resetData()
        {
            Name = null;
            Description = null;
            Email = null;
            PhoneNumber = null;
            Spend = null;
            Address = null;
        }
    }
}
