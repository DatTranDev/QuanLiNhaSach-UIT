using QuanLiNhaSach.DTOs;
using QuanLiNhaSach.Model;
using QuanLiNhaSach.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLiNhaSach.Model.Service
{
    public class BillService
    {
        public BillService() { }
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
        // Get All Bill
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
                                        Customer = c.Customer,
                                        Paid = c.Paid,
                                        Staff = c.Staff,
                                        BillInfo = (from x in c.BillInfoes
                                                    where x.IsDeleted == false
                                                    select new BillInfoDTO
                                                    {
                                                        IDBill = x.IDBill,
                                                        PriceItem = x.PriceItem,
                                                        IDBook = x.IDBook,
                                                        Quantity = x.Quantity,
                                                        Bill = x.Bill,
                                                        Book = x.Book,
                                                        TotalPrice=x.PriceItem*x.Quantity
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

        public async Task<List<BillDTO>> GetBillBetweenDate(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    List<BillDTO> billDTOs = await BillService.Ins.GetAllBill();

                    List<BillDTO> billList = billDTOs
                        .Where(bill => bill.CreateAt >= dateFrom && bill.CreateAt <= dateTo)
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
        //Add new bill
        public async Task<(bool, string)> AddNewBill(BillDTO newBill)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {

                    int? maxID = await context.Bill.MaxAsync(b => (int?)b.ID);
                    int curID = 0;
                    if (maxID.HasValue)
                    {
                        curID = (int)maxID + 1;
                    }
                    else
                    {
                        curID = 1;
                    }
                    Bill bill = new Bill
                    {
                        ID = curID,
                        IDCus = newBill.IDCus,
                        IDStaff = newBill.IDStaff,
                        IsDeleted = false,
                        Paid = newBill.Paid,
                        CreateAt = newBill.CreateAt,
                        TotalPrice = newBill.TotalPrice,

                    };

                    List<BillInfo> billInfoList = new List<BillInfo>();

                    foreach (var bI in newBill.BillInfo)
                    {
                        BillInfo billInfo = new BillInfo
                        {
                            IDBill = curID,
                            IDBook = bI.IDBook,
                            IsDeleted = false,
                            PriceItem = bI.PriceItem,
                            Quantity = bI.Quantity
                        };
                        (bool success, string msg) = await BookService.Ins.EditCountPrd(bI.IDBook, bI.Quantity);
                        if (!success)
                        {
                            return (false, null);
                        }
                        billInfoList.Add(billInfo);

                    }

                    context.BillInfo.AddRange(billInfoList);
                    context.Bill.Add(bill);
                    await context.SaveChangesAsync();
                    return (true, "Thêm thành công");
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi xảy ra khi thêm bill!");
                return (false, null);
            }

        }
        //Delete bill
        public async Task<(bool, string)> DeleteBill(BillDTO Bill)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var bill = await context.Bill.Where(p => p.ID == Bill.ID).FirstOrDefaultAsync();
                    if (bill.IsDeleted == false) bill.IsDeleted = true;
                    foreach (var b in Bill.BillInfo)
                    {
                        var billInfo = await context.BillInfo.Where(p => p.IDBill == b.IDBill && p.IDBook == b.IDBook).FirstOrDefaultAsync();
                        if (billInfo.IsDeleted == false) billInfo.IsDeleted = true;
                    }
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

        //Edit bill
        public async Task<(bool, string)> EditBill(BillDTO newBill)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var bill = await context.Bill.Where(p => p.ID == newBill.ID).FirstOrDefaultAsync();
                    bill.IDStaff = newBill.IDStaff;
                    bill.Staff = newBill.Staff;
                    bill.Customer = newBill.Customer;
                    bill.IDCus = newBill.IDCus;
                    bill.CreateAt = newBill.CreateAt;
                    bill.Paid = newBill.Paid;
                    bill.TotalPrice = newBill.TotalPrice;
                    foreach (var b in newBill.BillInfo)
                    {
                        var billInfo = await context.BillInfo.Where(p => p.IDBill == b.IDBill && p.IDBook == b.IDBook).FirstOrDefaultAsync();
                        billInfo.IDBook = b.IDBook;
                        billInfo.PriceItem = b.PriceItem;
                        billInfo.IsDeleted = false;
                        billInfo.IDBook = b.IDBook;
                        billInfo.Quantity = b.Quantity;
                        billInfo.Bill = b.Bill;
                        bill.BillInfoes.Add(new BillInfo
                        {
                            IDBill = b.IDBill,
                            IDBook = b.IDBook,
                            PriceItem = b.PriceItem,
                            IsDeleted = false,
                            Book = b.Book,
                            Quantity = b.Quantity,
                            Bill = b.Bill,
                        });
                    }
                    await context.SaveChangesAsync();
                    return (true, "Cập nhật thành công");
                }
            }
            catch
            {

                MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi xảy ra!");
                return (false, null);
            }

        }


        //trả về tổng tiền thu được trong 1 ngày
        public async Task<int> getBillByDate(DateTime date)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var billTotalPaid = await context.Bill
                        .Where(p => p.CreateAt.HasValue
                                    && p.CreateAt.Value.Day == date.Day
                                    && p.CreateAt.Value.Month == date.Month
                                    && p.CreateAt.Value.Year == date.Year
                                    && p.IsDeleted == false)
                        .SumAsync(bill => (int?)bill.Paid);

                    return billTotalPaid ?? 0; 
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi xảy ra!");
                return -1;
            }
        }


    }

}
