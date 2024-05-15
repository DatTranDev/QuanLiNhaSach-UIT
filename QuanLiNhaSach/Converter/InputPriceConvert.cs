using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace QuanLiNhaSach.Converter
{
    internal class InputPriceConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal || value is int)
            {
                return string.Format("{0:#,0}", value);
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = value as string;
            if (!string.IsNullOrEmpty(text))
            {
                // Loại bỏ dấu phẩy khi chuyển đổi văn bản thành số
                decimal result;
                if (decimal.TryParse(text.Replace(",", ""), NumberStyles.AllowThousands, culture, out result))
                {
                    return result;
                }
            }
            return 0;
        }
    }
}
