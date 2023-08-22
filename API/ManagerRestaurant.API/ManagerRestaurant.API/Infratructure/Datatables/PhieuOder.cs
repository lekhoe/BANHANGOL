using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Infratructure.Datatables
{
    public class PhieuOder
    {
        public Guid Id { get; set; }
        [ForeignKey("Ban")]
        public Guid IdBan { get; set; }
        public Ban Ban { get; set; } 
        [ForeignKey("User")]
        public Guid IdThuNgan { get; set; }
        [ForeignKey("KhachHang")]
        public Guid IdKhachHang { get; set; }
        public string Vocher { get; set; }
        public float TongTien { get; set; }
        public float ThucThu { get; set; }
        public float SoTienGiam { get; set; }
        public int TrangThai { get; set; } = 0;
        public DateTime ThoiGianThanhToan { get; set; }=DateTime.MinValue;
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }

    }
}
