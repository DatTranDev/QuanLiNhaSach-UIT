using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiNhaSach.DTOs
{
    public class GenreBookDTO
    {
        public Nullable<int> STT { get; set; }
        public int ID { get; set; }
        public string DisplayName { get; set; }
        public int Quantity { get; set; }
        public Nullable<bool> IsDeleted { get; set; }

        //public GenreBookDTO(int iD, string displayName, int quantity)
        //{
        //    ID = iD;
        //    DisplayName = displayName;
        //    Quantity = quantity;
        //}
    }
}
