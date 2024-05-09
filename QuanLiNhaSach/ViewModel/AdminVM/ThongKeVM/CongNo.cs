using QuanLiNhaSach.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.ViewModel.AdminVM.ThongKeVM
{
    public partial class ThongKeViewModel
    {
        private List<DebtReportDTO> _debtList;
        public List<DebtReportDTO> DebtList
        {
            get { return _debtList; }
            set { _debtList = value; OnPropertyChanged(); }
        }
    }
}
