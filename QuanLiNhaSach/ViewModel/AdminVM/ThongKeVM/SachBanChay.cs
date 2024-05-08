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
        private List<BookDTO> _favorList;
        public List<BookDTO> FavorList
        {
            get { return _favorList; }
            set { _favorList = value; OnPropertyChanged(); }
        }
    }
}
