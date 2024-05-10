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

        private ObservableCollection<BookDTO> _favorList;
        public ObservableCollection<BookDTO> FavorList
        {
            get { return _favorList; }
            set { _favorList = value; OnPropertyChanged(nameof(FavorList)); }
        }
    }
}
