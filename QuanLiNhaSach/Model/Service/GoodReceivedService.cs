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
    public class GoodReceivedService
    {
        public GoodReceivedService() { }
        private static GoodReceivedService _ins;

        public static GoodReceivedService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new GoodReceivedService();
                }
                return _ins;
            }
            private set { _ins = value; }
        }
        // Get All Bill
        public async Task<List<GoodReceivedDTO>> GetAllGoodReceived()
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var goodRList = (from c in context.GoodReceived
                                    where c.IsDeleted == false
                                    select new GoodReceivedDTO
                                    {
                                        ID = c.ID,                                        
                                        CreateAt = c.CreatAt,          
                                        StaffId = (int)c.StaffId,
                                        Staff = c.Staff,
                                        Total = c.Total,
                                        GoodReceivedInfo = (from x in c.GoodReceivedInfoes
                                                    where x.IsDeleted == false
                                                    select new GoodReceivedInfoDTO
                                                    {
                                                        IDGoodReceived = x.IDGoodReceived,
                                                        IDBook = x.IDBook,
                                                        Quantity = x.Quantity,
                                                        GoodReceived = x.GoodReceived,
                                                        TotalPriceItem = x.TotalPriceItem,
                                                        Book = x.Book,
                                                        BookName=x.Book.DisplayName,
                                                        BookAuthor = x.Book.Author,
                                                        BookGenre = x.Book.GenreBook.DisplayName,
                                                        BookPrice = x.Book.Price,
                                                    }).ToList(),
                                        IsDeleted = c.IsDeleted
                                    }).OrderByDescending(m => m.CreateAt).ToListAsync();

                    return await goodRList;
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi xảy ra!");
                return null;
            }


        }

        public async Task<List<GoodReceivedDTO>> GetGRBetweenDate(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    List<GoodReceivedDTO> billDTOs = await GoodReceivedService.Ins.GetAllGoodReceived();

                    List<GoodReceivedDTO> billList = billDTOs
                        .Where(goodR => goodR.CreateAt >= dateFrom && goodR.CreateAt <= dateTo)
                        .ToList();

                    return billList;

                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi xảy ra!");
                return null;
            }
        }
        //Add new goodR
        public async Task<(bool, string)> AddNewGoodReceived(GoodReceivedDTO newGR) 
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {

                    int? maxID = await context.GoodReceived.MaxAsync(b => (int?)b.ID);
                    int curID = 0;
                    if (maxID.HasValue)
                    {
                        curID = (int)maxID + 1;
                    }
                    else
                    {
                        curID = 1;
                    }
                    GoodReceived goodR = new GoodReceived
                    {
                        ID = curID,                       
                        IsDeleted = false,                       
                        CreatAt = newGR.CreateAt,
                        StaffId = newGR.StaffId,
                        Total = newGR.Total,

                    };

                    List<GoodReceivedInfo> billInfoList = new List<GoodReceivedInfo>();

                    foreach (var bI in newGR.GoodReceivedInfo)
                    {
                        GoodReceivedInfo billInfo = new GoodReceivedInfo
                        {
                            IDGoodReceived = curID,
                            IDBook = bI.IDBook,
                            IsDeleted = false,
                            Quantity = bI.Quantity,
                            TotalPriceItem = bI.TotalPriceItem,
                           
                        };
                        (bool success, string msg) = await BookService.Ins.EditCountPrd(bI.IDBook, -bI.Quantity);
                        if (!success)
                        {
                            return (false, null);
                        }
                        billInfoList.Add(billInfo);

                    }

                    context.GoodReceivedInfo.AddRange(billInfoList);
                    context.GoodReceived.Add(goodR);
                    await context.SaveChangesAsync();
                    return (true, "Thêm thành công");
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi xảy ra khi thêm goodR!");
                return (false, null);
            }

        }
        ////Delete goodR
        //public async Task<(bool, string)> DeleteBill(BillDTO Bill)
        //{
        //    try
        //    {
        //        using (var context = new QuanLiNhaSachEntities())
        //        {
        //            var goodR = await context.Bill.Where(p => p.ID == Bill.ID).FirstOrDefaultAsync();
        //            if (goodR.IsDeleted == false) goodR.IsDeleted = true;
        //            foreach (var b in Bill.BillInfo)
        //            {
        //                var billInfo = await context.BillInfo.Where(p => p.IDBill == b.IDBill && p.IDBook == b.IDBook).FirstOrDefaultAsync();
        //                if (billInfo.IsDeleted == false) billInfo.IsDeleted = true;
        //            }
        //            await context.SaveChangesAsync();
        //            return (true, "Xóa thành công");
        //        }
        //    }
        //    catch
        //    {
        //        MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi xảy ra!");
        //        return (false, null);

        //    }

        //}
        
        //public async Task<int> getBillByDate(DateTime date)
        //{
        //    try
        //    {
        //        using (var context = new QuanLiNhaSachEntities())
        //        {
        //            var billTotal = await context.Bill.Where(p => p.CreateAt.Value.Day == date.Day
        //                                                   && p.CreateAt.Value.Month == date.Month
        //                                                   && p.CreateAt.Value.Year == date.Year
        //                                                   && p.IsDeleted == false).ToListAsync();
        //            int totalPrice = 0;
        //            foreach (var goodR in billTotal)
        //            {
        //                totalPrice = totalPrice + (int)goodR.TotalPrice;
        //            }
        //            return totalPrice;

        //        }
        //    }
        //    catch
        //    {
        //        MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi xảy ra!");
        //        return -1;
        //    }
        //}
    }
}
