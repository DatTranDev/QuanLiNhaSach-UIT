using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using QuanLiNhaSach.DTOs;
using System.Threading.Tasks;
using QuanLiNhaSach.View.MessageBox;

namespace QuanLiNhaSach.Model.Service
{
    public class ThongKeService
    {
        private ThongKeService() { }
        private static ThongKeService _ins;
        public static ThongKeService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ThongKeService();
                }
                return _ins;
            }
            private set => _ins = value;
        }
        public async Task<List<BookDTO>> GetTop10SalerBetween(DateTime from, DateTime to)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var prodStatistic = context.BillInfo.Where(b => b.Bill.CreateAt >= from && b.Bill.CreateAt <= to && b.IsDeleted == false)
                    .GroupBy(pBill => pBill.IDBook)
                    .Select(gr => new
                    {
                        IDProduct = gr.Key,
                        Revenue = gr.Sum(pBill => (Decimal?)(pBill.PriceItem)) ?? 0,
                        SalesQuantity = gr.Sum(pBill => (int?)pBill.Quantity) ?? 0
                    })
                    .OrderByDescending(m => m.SalesQuantity).Take(10)
                    .Join(
                    context.Book,
                    statis => statis.IDProduct,
                    prod => prod.ID,
                    (statis, prod) => new BookDTO
                    {
                        ID = prod.ID,
                        DisplayName = prod.DisplayName,
                        Price = statis.Revenue,
                        Inventory = statis.SalesQuantity
                    }).ToListAsync();

                    return await prodStatistic;
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return null;
            }
        }

    }
}