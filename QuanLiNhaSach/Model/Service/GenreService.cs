using QuanLiNhaSach.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    var prD = await context.GenreBook.Where(p => p.DisplayName == name&&p.IsDeleted !=true).FirstOrDefaultAsync();
                    if (prD == null)
                    {
                        return (-1, null);
                    }
                    return (prD.ID, prD);
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
                    var productGenreList = (from c in context.GenreBook.Where(p=>p.IsDeleted != true) select c.DisplayName ).ToListAsync();
                    return await productGenreList;
                }
            }
            catch
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
                        return (true, "Thêm thành công");
                    }
                    else 
                    {
                        return (false, "Đã tồn tại"); 
                    }
                }
            }
            catch
            {
                return (false, null);
            }

        }
        public async Task<(bool, string)> EditGenre(GenreBook editedGenre)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var existingGenre = await context.GenreBook.FirstOrDefaultAsync(p => p.ID == editedGenre.ID && p.IsDeleted != true);
                    if (existingGenre != null)
                    {
                        existingGenre.DisplayName = editedGenre.DisplayName;                     
                        await context.SaveChangesAsync();
                        return (true, "Chỉnh sửa thành công");
                    }
                    else
                    {
                        return (false, "Thể loại không tồn tại hoặc đã bị xóa");
                    }
                }
            }
            catch
            {
                return (false, null);
            }
        }

        public async Task<(bool, string)> DeleteGenre(int genreId)
        {
            try
            {
                using (var context = new QuanLiNhaSachEntities())
                {
                    var genreToDelete = await context.GenreBook.FirstOrDefaultAsync(p => p.ID == genreId && p.IsDeleted != true);
                    if (genreToDelete != null)
                    {
                        genreToDelete.IsDeleted = true;
                        await context.SaveChangesAsync();
                        return (true, "Đã xóa thể loại");
                    }
                    else
                    {
                        return (false, "Thể loại không tồn tại hoặc đã bị xóa");
                    }
                }
            }
            catch
            {
                return (false, null);
            }
        }


    }
}
