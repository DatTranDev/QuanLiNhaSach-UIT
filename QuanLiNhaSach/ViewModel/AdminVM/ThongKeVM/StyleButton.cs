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
        private int caseNav = -1;
        public int CaseNav
        {
            get => caseNav;
            set
            {
                caseNav = value;
                OnPropertyChanged(nameof(CaseNav));
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


        private void UpdateButtonColors()
        {

            ResetColors();
            Color highlightColor = Color.FromArgb(0xFF, 0xFF, 0xF4, 0xF4);
            switch (caseNav)
            {
                case 0:
                    LichSuThuTienColor = new SolidColorBrush(highlightColor);
                    break;
                case 1:
                    LichSuBanColor = new SolidColorBrush(highlightColor);
                    break;
                case 2:
                    DoanhThuColor = new SolidColorBrush(highlightColor);
                    break;
                case 3:
                    SachBanChayColor = new SolidColorBrush(highlightColor);
                    break;
                case 4:
                    CongNoColor = new SolidColorBrush(highlightColor);
                    break;
                case 5:
                    TonKhoColor = new SolidColorBrush(highlightColor);
                    break;
            }
        }


        private void ResetColors()
        {
            var defaultColor = new SolidColorBrush(Colors.White);
            LichSuThuTienColor = defaultColor;
            LichSuBanColor = defaultColor;
            DoanhThuColor = defaultColor;
            SachBanChayColor = defaultColor;
            CongNoColor = defaultColor;
            TonKhoColor = defaultColor;
        }
    }
}
