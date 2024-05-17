using QuanLiNhaSach.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.ViewModel.AdminVM.ThongKeVM
{
    public partial class ThongKeViewModel : BaseViewModel
    {
        private List<GoodReceivedDTO> danhSachNhap;
        private ObservableCollection<GoodReceivedDTO> _danhSachNhap;
        public ObservableCollection<GoodReceivedDTO> DanhSachNhap
        {
            get { return _danhSachNhap; }
            set { _danhSachNhap = value; OnPropertyChanged(nameof(DanhSachNhap)); }
        }




        private GoodReceivedDTO _selectedItem_ForDsNhap;
        public GoodReceivedDTO SelectedItem_ForDsNhap
        {
            get { return _selectedItem_ForDsNhap; }
            set { _selectedItem_ForDsNhap = value; OnPropertyChanged(nameof(SelectedItem_ForDsNhap)); }
        }


        private ObservableCollection<GoodReceivedInfoDTO> detail_DsNhapList;

        public ObservableCollection<GoodReceivedInfoDTO> Detail_DsNhapList
        {
            get { return detail_DsNhapList; }
            set { detail_DsNhapList = value; OnPropertyChanged(nameof(Detail_DsNhapList)); }
        }


    
    }
}
