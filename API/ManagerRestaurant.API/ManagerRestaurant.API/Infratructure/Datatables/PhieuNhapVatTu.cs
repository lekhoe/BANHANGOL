using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infratructure.Datatables
{
    public class PhieuNhapVatTu
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Kieu { get; set; } 
        public DateTime NgayHoaDon { get; set; }
        public string MatHangs { get; set; }
        public string HinhAnh { get; set; }
        public string GhiChu { get; set; }
        public float TongSoTien { get; set; } = 0;
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
}
