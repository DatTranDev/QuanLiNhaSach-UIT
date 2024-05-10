using MaterialDesignThemes.Wpf;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace QuanLiNhaSach.ViewModel.AdminVM.ThongKeVM
{
    public partial class ThongKeViewModel
    {
        private int caseNav;
        public int CaseNav
        {
            get => caseNav;
            set
            {
                caseNav = value;
                UpdateButtonColors();
            }
        }

        private SolidColorBrush lichSuThuTienColor;
        public SolidColorBrush LichSuThuTienColor
        {
            get => lichSuThuTienColor;
            set
            {
                lichSuThuTienColor = value;
                OnPropertyChanged(nameof(LichSuThuTienColor));
            }
        }


        private SolidColorBrush lichSuBanColor;
        public SolidColorBrush LichSuBanColor
        {
            get => lichSuBanColor;
            set
            {
                lichSuBanColor = value;
                OnPropertyChanged(nameof(LichSuBanColor));
            }
        }


        private SolidColorBrush doanhThuColor;
        public SolidColorBrush DoanhThuColor
        {
            get => doanhThuColor;
            set
            {
                doanhThuColor = value;
                OnPropertyChanged(nameof(DoanhThuColor));
            }
        }

        private SolidColorBrush sachBanChayColor = new SolidColorBrush(Colors.White);
        public SolidColorBrush SachBanChayColor
        {
            get => sachBanChayColor;
            set
            {
                sachBanChayColor = value;
                OnPropertyChanged(nameof(SachBanChayColor));
            }
        }

        private SolidColorBrush congNoColor = new SolidColorBrush(Colors.White);
        public SolidColorBrush CongNoColor
        {
            get => congNoColor;
            set
            {
                congNoColor = value;
                OnPropertyChanged(nameof(CongNoColor));
            }
        }

        private SolidColorBrush tonKhoColor = new SolidColorBrush(Colors.White);
        public SolidColorBrush TonKhoColor
        {
            get => tonKhoColor;
            set
            {
                tonKhoColor = value;
                OnPropertyChanged(nameof(TonKhoColor));
            }
        }



        private static SolidColorBrush colorSelect = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xF4, 0xF4));
        private void UpdateButtonColors()
        {

            ResetColors();
            switch (caseNav)
            {
                case 0:
                    LichSuThuTienColor = colorSelect;
                    break;
                case 1:
                    LichSuBanColor = colorSelect;
                    break;
                case 2:
                    DoanhThuColor = colorSelect;
                    break;
                case 3:
                    SachBanChayColor = colorSelect;
                    break;
                case 4:
                    CongNoColor = colorSelect;
                    break;
                case 5:
                    TonKhoColor = colorSelect;
                    break;
            }
        }


        private static SolidColorBrush defaultColor = new SolidColorBrush(Colors.White);
        private void ResetColors()
        {
            LichSuThuTienColor = defaultColor;
            LichSuBanColor = defaultColor;
            DoanhThuColor = defaultColor;
            SachBanChayColor = defaultColor;
            CongNoColor = defaultColor;
            TonKhoColor = defaultColor;
        }
    }
}
