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
    public class BillService
    {
        public BillService()
        {

        }


        private static BillService _ins;

        public static BillService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BillService();
                }
                return _ins;
            }
            private set { _ins = value; }
        }

        // Danh sách Biil chưa xóa
        public async Task<List<BillDTO>> GetAllBill()
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var billList = (from c in context.Bill
                                    where c.IsDeleted == false
                                    select new BillDTO
                                    {
                                        ID = c.ID,
                                        IDCus = c.IDCus,
                                        IDStaff = c.IDStaff,
                                        CreateAt = c.CreateAt,
                                        TotalPrice = c.TotalPrice,
                                        Paid = c.Paid,
                                        Customer = c.Customer,
                                        Staff = c.Staff,

                                        BillInfo = (from x in c.BillInfoes
                                                    where x.IsDeleted == false
                                                    select new BillInfoDTO
                                                    {
                                                        IDBill = x.IDBill,
                                                        IDBook = x.IDBook,
                                                        Quantity = x.Quantity,
                                                        PriceItem = x.PriceItem,
                                                        Bill = x.Bill,
                                                        Book = x.Book,
                                                    }).ToList(),
                                        IsDeleted = c.IsDeleted
                                    }).OrderByDescending(m => m.CreateAt).ToListAsync();

                    return await billList;
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi xảy ra!");
                return null;
            }
        }





    }
}
