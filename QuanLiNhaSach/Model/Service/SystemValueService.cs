using QuanLiNhaSach.DTOs;
using QuanLiNhaSach.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using QuanLiNhaSach.ViewModel.SystemVM;

namespace QuanLiNhaSach.Model.Service
{
    public class SystemValueService
    {
        public SystemValueService() { }
        private static SystemValueService _ins;
        public static SystemValueService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new SystemValueService();
                }
                return _ins;
            }
            private set { _ins = value; }
        }
        public async Task<SystemValue> GetData()
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var cus = await context.SystemValue.FirstOrDefaultAsync();
                    if (cus == null)
                    {
                        var SV_ins = new SystemValue
                        {
                            MinReceived = 150,
                            MaxInventory = 300,
                            MaxDebts = 1000000,
                            MinSaleInventory = 20,
                            Profit = 0.05,
                            DebtsPolicy = false,
                            ID = 0,
                        };
                        context.SystemValue.Add(SV_ins);
                        await context.SaveChangesAsync();

                    }
                    return cus;
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return null;
            }
        }
        //public async Task<(bool, string)> EditData(SystemValue systemValue, int ID)
        //{
        //    try
        //    {
        //        using (var context = new QuanLiNhaSachEntities())
        //        {
        //            var value = await context.SystemValue.FirstOrDefaultAsync();
        //            MessageBoxCustom.Show(MessageBoxCustom.Error, value.ToString());
        //            if (value == null) return (false, "Không tìm thấy ID");
        //            value.MinSaleInventory = systemValue.MinSaleInventory;
        //            value.MaxInventory = systemValue.MaxInventory;
        //            value.MaxDebts = systemValue.MaxDebts;
        //            value.MinSaleInventory = systemValue.MinSaleInventory;
        //            value.Profit = systemValue.Profit;
        //            value.DebtsPolicy = systemValue.DebtsPolicy;
        //            await context.SaveChangesAsync();
        //            return (true, "Chỉnh sửa thành công");
        //        }
        //    }
        //    catch
        //    {
        //        MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi!");
        //        return (false, null);
        //    }
        //}
        public async Task<(bool, string)> EditData(SystemValue systemValue, int ID)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    // Retrieve all SystemValue records
                    var values = await context.SystemValue.ToListAsync();

                    // If there are no records, return an error
                    if (values == null || !values.Any())
                    {
                        return (false, "Không tìm thấy bản ghi nào để cập nhật.");
                    }

                    // Update each record with the new property values
                    foreach (var value in values)
                    {
                        value.MinSaleInventory = systemValue.MinSaleInventory;
                        value.MaxInventory = systemValue.MaxInventory;
                        value.MaxDebts = systemValue.MaxDebts;
                        value.Profit = systemValue.Profit;
                        value.DebtsPolicy = systemValue.DebtsPolicy;
                    }

                    // Save changes to the database
                    await context.SaveChangesAsync();

                    return (true, "Chỉnh sửa thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi: " + ex.Message);
                return (false, null);
            }
        }
    }
}
