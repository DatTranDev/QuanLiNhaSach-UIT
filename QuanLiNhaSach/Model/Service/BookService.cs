using QuanLiNhaSach.DTOs;
using QuanLiNhaSach.View.MessageBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLiNhaSach.Model.Service
{
    public class BookService
    {
        public BookService() { }
        private static BookService _ins;

        public static BookService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BookService();
                }
                return _ins;
            }
            private set { _ins = value; }
        }
        //Get all product
        public async Task<List<BookDTO>> GetAllBook()
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var productList = (from c in context.Book
                                       where c.IsDeleted == false
                                       select new BookDTO
                                       {
                                           ID = c.ID,
                                           DisplayName = c.DisplayName,
                                           Price = c.Price,
                                           IDGenre = c.IDGenre,
                                           GenreName = c.GenreBook.DisplayName,
                                           Inventory = c.Inventory,
                                           Author = c.Author,
                                           PublishYear = c.PublishYear,
                                           Publisher = c.Publisher,
                                           Description = c.Description,
                                           Image = c.Image,
                                           IsDeleted = c.IsDeleted,
                                       }).ToListAsync();
                    return await productList;
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return null;
            }
        }
        public async Task<List<BookDTO>> GetAllProductCounted()
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var productList = (from c in context.Book
                                       where c.IsDeleted == false && c.Inventory > 0
                                       select new BookDTO
                                       {
                                           ID = c.ID,
                                           DisplayName = c.DisplayName,
                                           Price = c.Price,
                                           IDGenre = c.IDGenre,
                                           GenreName = c.GenreBook.DisplayName,
                                           Inventory = c.Inventory,
                                           PublishYear= c.PublishYear,
                                           Publisher = c.Publisher,
                                           Author = c.Author,
                                           Description = c.Description,
                                           Image = c.Image,
                                           IsDeleted = c.IsDeleted,
                                       }).ToListAsync();
                    return await productList;
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi khi tìm sản phẩm");
                return null;
            }

        }

        //Add new product

        public async Task<(bool, string)> AddNewPrD(Book newPrD)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var prD = await context.Book.Where(p => p.DisplayName == newPrD.DisplayName && p.Publisher==newPrD.Publisher && p.PublishYear==newPrD.PublishYear && p.Author== newPrD.Author &&p.IDGenre==newPrD.IDGenre && p.IsDeleted==false ).FirstOrDefaultAsync();
                    if (prD != null)
                    {
                        if (prD.IsDeleted == true)
                        {
                            prD.DisplayName = newPrD.DisplayName;
                            prD.Price = newPrD.Price;
                            prD.IDGenre = newPrD.IDGenre;
                            prD.Inventory = newPrD.Inventory;
                            prD.PublishYear = newPrD.PublishYear;
                            prD.Publisher = newPrD.Publisher;
                            prD.Author = newPrD.Author;
                            prD.Description = newPrD.Description;
                            prD.Image = newPrD.Image;
                            prD.IsDeleted = false;
                            await context.SaveChangesAsync();
                            return (true, "Thêm thành công");
                        }
                        else
                        {
                            return (false, "Đã tồn tại sản phẩm");
                        }
                    }
                    context.Book.Add(newPrD);
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

        //Delete product
        public async Task<(bool, string)> DeletePrD(int ID)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var prD = await context.Book.Where(p => p.ID == ID).FirstOrDefaultAsync();
                    if (prD.IsDeleted == false) prD.IsDeleted = true;
                    await context.SaveChangesAsync();
                    return (true, "Xóa thành công");
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return (false, null);
            }

        }

        //Edit product
        public async Task<(bool, string)> EditPrD(Book newPrD, int ID)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var prD = await context.Book.Where(p => p.ID == newPrD.ID && p.IsDeleted==false).FirstOrDefaultAsync();

                    if (prD == null) return (false, "Không tìm thấy ID");
                    prD.DisplayName = newPrD.DisplayName;
                    prD.Price = newPrD.Price;
                    prD.IDGenre = newPrD.IDGenre;
                    prD.GenreBook = newPrD.GenreBook;
                    prD.Inventory = newPrD.Inventory;
                    prD.Author = newPrD.Author;
                    prD.PublishYear = newPrD.PublishYear;
                    prD.Publisher = newPrD.Publisher;
                    prD.Description = newPrD.Description;
                    prD.Image = newPrD.Image;
                    prD.IsDeleted = false;
                    await context.SaveChangesAsync();
                    return (true, "Cập nhật thành công");
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi khi sửa sản phẩm");
                return (false, null);
            }
        }
        //update count product 
        public async Task<(bool, string)> EditCountPrd(int id, int? countDelta)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var prd = await context.Book.Where(p => p.ID == id).FirstOrDefaultAsync();
                    if (prd == null) return (false, null);
                    prd.Inventory = prd.Inventory - countDelta;
                    if (prd.Inventory < 0)
                    {
                        return (false, null);
                    }
                    await context.SaveChangesAsync();
                    return (true, "Chỉnh sửa thành công");
                }

            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi khi sửa sản phẩm");
                return (false, null);
            }
        }

        public async Task<(bool, Book)> findIdBook(string Name, string Genre, string Author, string Publisher, int publishYear)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var book = await context.Book.Where(p => p.DisplayName==Name && p.GenreBook.DisplayName==Genre && p.Author==Author && p.Publisher==Publisher && p.PublishYear==publishYear && p.IsDeleted==false).FirstOrDefaultAsync();
                    if (book == null)
                    {
                        return (false, null);
                    }
                    else
                    {
                        return (true, book);
                    }
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return (false, null);
            }

        }
    }
}
