using QuanLiNhaSach.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLiNhaSach.DTOs;


namespace QuanLiNhaSach.Model.Service
{
    public class GenreService
    {
        private static GenreService _ins;

        public static GenreService Ins
        {
            get
            {
                if (_ins == null) _ins = new GenreService();
                return _ins;
            }
            private set { _ins = value; }
        }
        public async Task<(int, GenreBook)> FindGenrePrD(string name)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var prD = await context.GenreBook.Where(p => p.DisplayName == name && p.IsDeleted != true).FirstOrDefaultAsync();
                    if (prD == null)
                    {
                        return (-1, null);
                    }
                    return ((int)prD.ID, prD);
                }
            }
            catch
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return (-1, null);
            }

        }

        // Get all genre prD
        public async Task<List<string>> GetAllGenreBook()
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var productGenreList = await (from c in context.GenreBook
                                                  where c.IsDeleted != true
                                                  select c.DisplayName).ToListAsync();
                    return productGenreList;
                }
            }
            catch (Exception ex)
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi");
                return null;
            }
        }

        public async Task<(bool, string)> AddNewGenre(GenreBook newGenre)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var prD = await context.GenreBook.Where(p => p.DisplayName == newGenre.DisplayName && p.IsDeleted != true).FirstOrDefaultAsync();
                    if (prD == null)
                    {
                        context.GenreBook.Add(newGenre);
                        await context.SaveChangesAsync();

                        return (true, "Thêm thành công.");
                    }
                    else
                    {
                        return (false, "Đã tồn tại.");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, null);
            }

        }
        public async Task<(bool, string)> EditGenre(GenreBook selectedGenre)
        {

            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    bool IsExistID = await context.GenreBook.AnyAsync(p => p.DisplayName == selectedGenre.DisplayName && p.ID == selectedGenre.ID && p.IsDeleted != true);

                    if (IsExistID)
                    {
                        return (false, "Danh mục đã tồn tại.");
                    }
                    var genre = await context.GenreBook.Where(p => p.ID == selectedGenre.ID).FirstOrDefaultAsync();
                    genre.DisplayName = selectedGenre.DisplayName;
                    await context.SaveChangesAsync();
                    return (true, "Cập nhật thành công.");
                }
            }
            catch (Exception ex)
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Xảy ra lỗi!");
                return (false, null);
            }
        }

        public async Task<(bool, string)> DeleteGenre(GenreBook selectedGenre)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var genreToDelete = await context.GenreBook.FirstOrDefaultAsync(g => g.ID == selectedGenre.ID && g.DisplayName == selectedGenre.DisplayName);
                    if (genreToDelete == null)
                    {
                        return (false, "Không tìm thấy thể loại sách để xóa.");
                    }

                    // Perform soft delete (mark as deleted)
                    genreToDelete.IsDeleted = true;

                    await context.SaveChangesAsync();
                    return (true, "Xóa thể loại sách thành công.");
                }
            }
            catch (Exception)
            {
                return (false, "Xảy ra lỗi khi xóa thể loại sách.");
            }
        }
    }
}
