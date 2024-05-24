using OfficeOpenXml.ConditionalFormatting.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.ViewModel.AdminVM.StaffManagementVM
{
    public class MotSoPTBoTro
    {
        public static DateTime formatDate(string date)
        {

            try
            {
                string[] s = date.Split('/'); 
                return new DateTime(int.Parse(s[2]), int.Parse(s[0]), int.Parse(s[1]));
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
    }
}
