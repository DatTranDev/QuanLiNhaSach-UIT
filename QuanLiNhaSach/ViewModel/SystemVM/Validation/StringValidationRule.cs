using System;
using System.Globalization;
using System.Windows.Controls;

namespace QuanLiNhaSach.ViewModel.SystemVM.Validation
{
    public class IntValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string inputText = value as string;

            if (string.IsNullOrWhiteSpace(inputText))
            {
                return new ValidationResult(false, "Ô này không được bỏ trống.");
            }
            int number = -1;

            if (!int.TryParse(inputText, out number))
            {
                return new ValidationResult(false, "Hãy nhập một số nguyên dương hợp lệ.");
            }

            if (int.Parse(inputText) <= 0)
            {
                return new ValidationResult(false, "Giá trị được nhập phải là một số nguyên dương.");
            }
            return ValidationResult.ValidResult;
        }
    }
    public class DoubleValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string inputText = value as string;

            if (string.IsNullOrWhiteSpace(inputText))
            {
                return new ValidationResult(false, "Ô này không được bỏ trống.");
            }
            double number = -1;

            if (!double.TryParse(inputText, out number))
            {
                return new ValidationResult(false, "Hãy nhập một số dương hợp lệ.");
            }

            if (double.Parse(inputText) < 0)
            {
                return new ValidationResult(false, "Giá trị được nhập phải là một số dương.");
            }

            return ValidationResult.ValidResult;
        }
    }
    public class ValidationUtil
    {
        static public bool IntValidationRule(string inputText)
        {
            if (string.IsNullOrWhiteSpace(inputText))
            {
                return false;
            }
            int number = -1;

            if (!int.TryParse(inputText, out number))
            {
                return false;
            }

            if (int.Parse(inputText) <= 0)
            {
                return false;
            }
            return true;
        }
        static public bool DoubleValidationRule(string inputText)
        {
            if (string.IsNullOrWhiteSpace(inputText))
            {
                return false;
            }
            double number = -1;

            if (!double.TryParse(inputText, out number))
            {
                return false;
            }

            if (double.Parse(inputText) <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
