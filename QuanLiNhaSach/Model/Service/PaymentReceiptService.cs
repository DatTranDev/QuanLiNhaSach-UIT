using QuanLiNhaSach.DTOs;
using QuanLiNhaSach.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.Model.Service
{
    public class PaymentReceiptService
    {
        public PaymentReceiptService() { }
        private static PaymentReceiptService _ins;

        public static PaymentReceiptService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new PaymentReceiptService();
                }
                return _ins;
            }
            private set { _ins = value; }
        }
        public async Task<List<PaymentReceiptDTO>> GetAllPayment()
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var payList = (from s in context.PaymentReceipt
                                   where s.IsDeleted == false
                                   select new PaymentReceiptDTO
                                   {
                                       ID = s.ID,
                                       IDCus = s.IDCus,
                                       CusAddress = s.Customer.Address,
                                       CusName = s.Customer.DisplayName,
                                       CusNumber = s.Customer.PhoneNumber,
                                       CusEmail = s.Customer.Email,
                                       CreateAt = s.CreatAt,
                                       AmountReceived = s.AmountReceived,
                                       IsDeleted = s.IsDeleted,
                                       
                                   }).ToListAsync();
                    return await payList;
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return null;
            }

        }



        public async Task<(bool, string)> DeletePaymentRecipt(PaymentReceiptDTO paymentReceiptDTO)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var phieuThu = await context.PaymentReceipt.Where(p => p.ID == paymentReceiptDTO.ID).FirstOrDefaultAsync();
                    if (phieuThu == null)
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi xảy ra!");
                        return (false, null);
                    }
                    if (phieuThu.IsDeleted == false) phieuThu.IsDeleted = true;

                    await context.SaveChangesAsync();
                    return (true, "Xóa thành công");
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi xảy ra!");
                return (false, null);
            }
        }

        public async Task<(bool, string)> AddNewPay(PaymentReceipt newPay)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {                                          
                    context.PaymentReceipt.Add(newPay);
                    await context.SaveChangesAsync();
                    return (true, "Thêm thành công");
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return (false, null);
            }


        }
        //public async Task<(bool, string)> EditPayList(Customer newCus, int ID)
        //{
        //    try
        //    {
        //        using (var context = new QuanLiNhaSachEntities())
        //        {
                    
        //            await context.SaveChangesAsync();
        //            return (true, "Chỉnh sửa thành công");
        //        }
        //    }
        //    catch
        //    {
        //        MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi khi chỉnh sửa khách hàng");
        //        return (false, null);
        //    }


        //}
        //public async Task<(bool, string)> DeleteCustomer(int ID)
        //{
        //    try
        //    {
        //        using (var context = new QuanLiNhaSachEntities())
        //        {
        //            var cus = await context.Customer.Where(p => p.ID == ID).FirstOrDefaultAsync();
        //            if (cus.IsDeleted == true) return (false, "Đã xóa khách hàng này rồi");
        //            cus.IsDeleted = true;
        //            await context.SaveChangesAsync();
        //            return (true, "Xóa thành công");
        //        }
        //    }
        //    catch
        //    {
        //        MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
        //        return (false, null);
        //    }

        //}

    }
}
