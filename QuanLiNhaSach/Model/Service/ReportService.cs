using QuanLiNhaSach.Model;
using QuanLiNhaSach.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.DTOs
{
    public class ReportService
    {
        public ReportService() { }
        private static ReportService _ins;

        public static ReportService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ReportService();
                }
                return _ins;
            }
            private set { _ins = value; }
        }

        public async Task<List<InventoryReportDTO>> GetInventoryReportByMonth(string MonthYear)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var ivtReport = (from c in context.InventoryReport
                                     where c.MonthYear == MonthYear
                                     select new InventoryReportDTO
                                     {
                                         Id = c.Id,
                                         BookId = c.BookId,
                                         FirstIvt = c.FirstIvt,
                                         LastIvt = c.LastIvt,
                                         Arise = c.Arise,
                                         Book = c.Book,
                                         MonthYear = MonthYear,
                                     }).ToListAsync();
                    return await ivtReport;
                }
            }catch {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi xảy ra");
                return null;
            }
        }
        public async Task<List<DebtReportDTO>> GetDebtReportByMonth(string MonthYear)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var debtReport = (from c in context.DebtReport
                                     where c.MonthYear == MonthYear
                                     select new DebtReportDTO
                                     {
                                         Id = c.Id,
                                         CustomerId = c.CustomerId,
                                         FirstDebt = c.FirstDebt,
                                         LastDebt = c.LastDebt,
                                         Arise = c.Arise,
                                         Customer = c.Customer,
                                         MonthYear = MonthYear
                                     }).ToListAsync();
                    return await debtReport;
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Lỗi xảy ra");
                return null;
            }
        }
        //Get 
    }
}
