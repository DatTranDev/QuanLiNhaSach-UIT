using QuanLiNhaSach.DTOs;
using QuanLiNhaSach.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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
                    return cus;
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return null;
            }
        }
        public async Task<(bool, string)> EditData(SystemValue systemValue, int ID)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var value = await context.SystemValue.Where(p => p.ID == ID).FirstOrDefaultAsync();
                    if (value == null) return (false, "Không tìm thấy ID");
                    value.MinSaleInventory = systemValue.MinSaleInventory;
                    value.MaxInventory = systemValue.MaxInventory;
                    value.MaxDebts = systemValue.MaxDebts;
                    value.MinSaleInventory = systemValue.MinSaleInventory;
                    value.Profit = systemValue.Profit;
                    value.DebtsPolicy = systemValue.DebtsPolicy;
                    await context.SaveChangesAsync();
                    return (true, "Chỉnh sửa thành công");
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi khi chỉnh sửa khách hàng");
                return (false, null);
            }
        }
    }
}
