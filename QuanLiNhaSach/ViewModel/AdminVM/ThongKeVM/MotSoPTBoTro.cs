using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.ViewModel.AdminVM.ThongKeVM
{
    public partial class ThongKeViewModel
    {
        private void setListThang()
        {
            ListThang = new ObservableCollection<string>();
            for (int i = 1; i <= 12; i++)
            {
                ListThang.Add("Tháng "+i);
            }
        }


        private static string formatMonth(string month)
        {
            string[] s=month.Split(' ');

            if (s.Length > 1)
            {
                if (s[1].Length == 1)
                {
                    return "0" + s[1];
                }
                return s[1];
            }
            else
            {
                return null;
            }
        }


        private static string formatMonthYear(string month,string year)
        {
            return month + "-" + year;
        }

        private static bool IsYear(string ip)
        {
            if (int.TryParse(ip, out int year))
            {
                return year >= 1900 && year <= 2200;
            }
            return false;
        }

        public static string RandomFileName()
        {
            Random random = new Random();
            int passwordLength = random.Next(10, 20);
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var password = new char[passwordLength];

            for (int i = 0; i < passwordLength; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }
            return new string(password);
        }
    }
}
