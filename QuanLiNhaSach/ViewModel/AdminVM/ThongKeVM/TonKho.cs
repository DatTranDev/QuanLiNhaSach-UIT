using QuanLiNhaSach.DTOs;
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
        private List<InventoryReportDTO> inventoryList;
        private ObservableCollection<InventoryReportDTO> _inventoryList;
        public ObservableCollection<InventoryReportDTO> InventoryList
        {
            get { return _inventoryList; }
            set { _inventoryList = value; OnPropertyChanged(nameof(InventoryList)); }
        }
    }
}
