using CloudinaryDotNet.Actions;
using QuanLiNhaSach.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace QuanLiNhaSach.Converter
{
    public class IndexInvoiceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is BookDTO a)
            {
                var item = (BookDTO)values[0];
                var list = values[1] as ObservableCollection<BookDTO>;

                if (list != null && item != null)
                {
                    return (list.IndexOf(item) + 1).ToString();
                }

            }

            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

