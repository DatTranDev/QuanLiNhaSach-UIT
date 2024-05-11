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

        private List<DebtReportDTO> debtList;
        private ObservableCollection<DebtReportDTO> _debtList;
        public ObservableCollection<DebtReportDTO> DebtList
        {
            get { return _debtList; }
            set { _debtList = value; OnPropertyChanged(nameof(DebtList)); }
        }
    }
}
