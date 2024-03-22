using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Contexts;
using QuanLiNhaSach.DTOs;
using QuanLiNhaSach.Model;
using QuanLiNhaSach.Utils;
using QuanLiNhaSach.View.MessageBox;

namespace QuanLiNhaSach.Model.Service
{
    public class StaffService
    {
        public StaffService() { }
        private static StaffService _ins;

        public static StaffService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new StaffService();
                }
                return _ins;
            }
            private set { _ins = value; }
        }// test

        //Get all staff
        public async Task<List<StaffDTO>> GetAllStaff()
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var staffList = await (from c in context.Staff
                                           where c.IsDeleted == false || c.IsDeleted == null
                                           select new StaffDTO
                                           {
                                               ID = c.ID,
                                               DisplayName = c.DisplayName,
                                               StartDate = c.StartDate,
                                               UserName = c.UserName,
                                               PassWord = c.PassWord,
                                               PhoneNumber = c.PhoneNumber,
                                               BirthDay = c.BirthDay,
                                               Wage = c.Wage,
                                               Status = c.Status,
                                               Email = c.Email,
                                               Gender = c.Gender,
                                               Role = c.Role,
                                               IsDeleted = c.IsDeleted,
                                           }).ToListAsync();
                    return staffList;
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return null;
            }
        }
        public async Task<(Staff, bool)> FindStaff(int staff)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var st = await context.Staff.Where(p => p.ID == staff).FirstOrDefaultAsync();
                    if (st == null)
                    {
                        return (null, false);
                    }
                    else
                        return (st, true);
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return (null, false);
            }
        }

        //Add staff
        public async Task<(bool, string)> AddNewStaff(Staff newStaff)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    bool IsEsixtEmail = await context.Staff.AnyAsync(p => p.Email == newStaff.Email);
                    bool IsExistPhone = await context.Staff.AnyAsync(p => p.PhoneNumber == newStaff.PhoneNumber);
                    bool IsExistUsername = await context.Staff.AnyAsync(p => p.UserName == newStaff.UserName);
                    var staff = await context.Staff.Where(p => p.Email == newStaff.Email || p.PhoneNumber == newStaff.PhoneNumber || p.UserName == newStaff.UserName).FirstOrDefaultAsync();
                    if (staff != null)
                    {
                        if (staff.IsDeleted == true)
                        {
                            staff.DisplayName = newStaff.DisplayName;
                            staff.StartDate = newStaff.StartDate;
                            staff.UserName = newStaff.UserName;
                            staff.PassWord = newStaff.PassWord;
                            staff.PhoneNumber = newStaff.PhoneNumber;
                            staff.BirthDay = newStaff.BirthDay;
                            staff.Wage = newStaff.Wage;
                            staff.Status = newStaff.Status;
                            staff.Email = newStaff.Email;
                            staff.Gender = newStaff.Gender;
                            staff.Role = newStaff.Role;
                            staff.IsDeleted = false;
                            await context.SaveChangesAsync();
                            return (true, "Them thanh cong");
                        }
                        else
                        {
                            if (IsEsixtEmail)
                            {
                                return (false, "Email đã tồn tại");
                            }
                            if (IsExistPhone)
                            {
                                return (false, "Số điện thoại đã tồn tại");
                            }
                            if (IsExistUsername)
                            {
                                return (false, "Tài khoản đã tồn tại");
                            }
                        }
                    }
                    context.Staff.Add(newStaff);
                    await context.SaveChangesAsync();
                    return (true, "Them thanh cong");
                }

            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return (false, null);
            }

        }


        // Edit staff
        public async Task<(bool, string)> EditStaff(Staff newStaff)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    bool IsExistUsername = await context.Staff.AnyAsync(p => p.ID != newStaff.ID && p.UserName == newStaff.UserName && p.IsDeleted == false);
                    if (IsExistUsername)
                    {
                        return (false, "Tài khoản đã tồn tại");
                    }
                    var staff = await context.Staff.Where(p => p.ID == newStaff.ID).FirstOrDefaultAsync();
                    staff.DisplayName = newStaff.DisplayName;
                    staff.StartDate = newStaff.StartDate;
                    staff.UserName = newStaff.UserName;
                    staff.PassWord = newStaff.PassWord == null ? staff.PassWord : newStaff.PassWord;
                    staff.PhoneNumber = newStaff.PhoneNumber;
                    staff.BirthDay = newStaff.BirthDay;
                    staff.Wage = newStaff.Wage;
                    staff.Status = newStaff.Status;
                    staff.Email = newStaff.Email;
                    staff.Gender = newStaff.Gender;
                    staff.Role = newStaff.Role;
                    await context.SaveChangesAsync();
                    return (true, "Cap that thanh cong");
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return (false, null);
            }

        }

        //Delete staff
        public async Task<(bool, string)> DeleteStaff(int ID)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var staff = await context.Staff.Where(p => p.ID == ID).FirstOrDefaultAsync();
                    if (staff.IsDeleted == false) staff.IsDeleted = true;
                    await context.SaveChangesAsync();
                    return (true, "Da xoa");
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return (false, null);
            }

        }
        //update mk
        public async Task<(bool, string, string)> UpdatePassword(string email, string newPass)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var staff = await context.Staff.Where(p => p.Email == email).FirstOrDefaultAsync();
                    if (staff != null && staff.IsDeleted == false)
                    {
                        staff.PassWord = Helper.MD5Hash(newPass);
                    }
                    else
                    {
                        return (false, "Không tồn tại email này", null);
                    }
                    await context.SaveChangesAsync();
                    return (true, "Update mật khẩu thành công", staff.UserName);
                }
            }
            catch (Exception ex)
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return (false, "Có lỗi xuất hiện", null);
            }
        }
    }
}
