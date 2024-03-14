using QuanLiNhaSach.View.Admin.CustomerManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLiNhaSach.ViewModel.AdminVM.CustomerManagementVM
{
    public class CustomerManagementViewModel : BaseViewModel
    {
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
            AddCusWdCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddCustomerWindow wd = new AddCustomerWindow();
                wd.ShowDialog();
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
        }
    }
}
