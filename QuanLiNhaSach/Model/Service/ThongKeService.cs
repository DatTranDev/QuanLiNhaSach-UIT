using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using QuanLiNhaSach.DTOs;
using System.Threading.Tasks;
using QuanLiNhaSach.View.MessageBox;
using System.Windows;

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
        //public async Task<List<BookDTO>> GetTop10SalerBetween(DateTime from, DateTime to)
        //{
        //    try
        //    {
        //        using (var context = new QuanLiNhaSachEntities())
        //        {
        //            var prodStatistic = context.BillInfo.Where(b => b.Bill.CreateAt >= from && b.Bill.CreateAt <= to && b.IsDeleted == false)
        //            .GroupBy(pBill => pBill.IDBook)
        //            .Select(gr => new
        //            {
        //                IDProduct = gr.Key,
        //                Revenue = gr.Sum(pBill => (Decimal?)(pBill.PriceItem)) ?? 0,
        //                SalesQuantity = gr.Sum(pBill => (int?)pBill.Quantity) ?? 0
        //            })
        //            .OrderByDescending(m => m.SalesQuantity).Take(10)
        //            .Join(
        //            context.Book,
        //            statis => statis.IDProduct,
        //            prod => prod.ID,
        //            (statis, prod) => new BookDTO
        //            {
        //                ID = prod.ID,
        //                DisplayName = prod.DisplayName,
        //                Price = statis.Revenue,
        //                Inventory = statis.SalesQuantity
        //            }).ToListAsync();

        //            return await prodStatistic;
        //        }
        //    }
        //    catch
        //    {
        //        MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
        //        return null;
        //    }
        //}

        public async Task<List<BookDTO>> GetTop10SalerBetween(DateTime from, DateTime to)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var billInfoData = await context.BillInfo
                        .Where(b => b.Bill.CreateAt.HasValue && b.IsDeleted == false)
                        .ToListAsync();

                    var filteredData = billInfoData
                        .Where(b => b.Bill.CreateAt.Value.Date >= from.Date && b.Bill.CreateAt.Value.Date <= to.Date)
                        .GroupBy(pBill => pBill.IDBook)
                        .Select(gr => new
                        {
                            IDProduct = gr.Key,
                            Revenue = gr.Sum(pBill => (Decimal?)(pBill.PriceItem * pBill.Quantity)) ?? 0,
                            SalesQuantity = gr.Sum(pBill => (int?)pBill.Quantity) ?? 0
                        })
                        .OrderByDescending(m => m.SalesQuantity)
                        .Take(10)
                        .ToList();

                    var prodStatistic = filteredData
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
                            }).ToList();

                    return prodStatistic;
                }
            }
            catch (Exception ex)
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return null;
            }
        }



        public async Task UpdateInventoryForNewMonth()
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    await context.Database.ExecuteSqlCommandAsync("EXEC sp_UpdateInventoryForNewMonth");
                }
            }
            catch(Exception e)
            {
               
            }
        }

        public async Task UpdateDebtForNewMonth()
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    await context.Database.ExecuteSqlCommandAsync("EXEC sp_UpdateDebtForNewMonth");
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}