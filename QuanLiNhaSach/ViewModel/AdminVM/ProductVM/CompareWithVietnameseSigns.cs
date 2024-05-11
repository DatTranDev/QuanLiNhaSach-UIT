using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.ViewModel.AdminVM.ProductVM
{
    public class CompareWithVietnameseSigns : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            CultureInfo culture = new CultureInfo("vi-VN", false);
            return string.Compare(x, y, true, culture);
        }
    }
}
