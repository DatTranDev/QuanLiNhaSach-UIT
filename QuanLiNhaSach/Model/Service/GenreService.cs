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
                    var prD = await context.GenreBook.Where(p => p.DisplayName == name).FirstOrDefaultAsync();
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
                    var productGenreList = (from c in context.GenreBook select c.DisplayName).ToListAsync();
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
                    var prD = await context.GenreBook.Where(p => p.DisplayName == newGenre.DisplayName).FirstOrDefaultAsync();
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
    }
}
