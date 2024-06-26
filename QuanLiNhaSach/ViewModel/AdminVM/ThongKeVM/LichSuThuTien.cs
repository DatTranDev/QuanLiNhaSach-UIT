﻿using QuanLiNhaSach.DTOs;
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
        private  List<PaymentReceiptDTO> danhSachThuTien;
        private ObservableCollection<PaymentReceiptDTO> _danhSachThuTien;
        public ObservableCollection<PaymentReceiptDTO> DanhSachThuTien
        {
            get { return _danhSachThuTien; }
            set { _danhSachThuTien = value; OnPropertyChanged(nameof(DanhSachThuTien)); }
        }

        //phục vụ xem chi tiết phiếu thu tiền
        private PaymentReceiptDTO _selectedItemPaymentRecept;
        public PaymentReceiptDTO SelectedItemPaymentRecept
        {
            get { return _selectedItemPaymentRecept; }
            set { _selectedItemPaymentRecept = value; OnPropertyChanged(nameof(SelectedItemPaymentRecept)); }
        }


    }
}
