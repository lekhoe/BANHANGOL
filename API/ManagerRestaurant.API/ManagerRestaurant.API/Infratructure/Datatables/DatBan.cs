using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infratructure.Datatables
{
    public class DatBan
    {
        public Guid Id { get; set; }
        public Guid? IdBan { get; set; } = Guid.Empty;
        public Guid? MaKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        public DateTime? GioDen { get; set; } = DateTime.MinValue;
        public DateTime ThoiGian { get; set; }
        public int SoNguoiLon { get; set; } = 0;
        public int SoTreEm { get; set; } = 0;
        public string GhiChu { get; set; }
        public int TrangThai { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
   }
