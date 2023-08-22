using System;
namespace Infratructure.Datatables
{
    public class ChiTieuTrongNgay
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MatHang { get; set; }
        public string Anh { get; set; }
        public float TongSoTien { get; set; } = 0;
        public string TrangThaiHienTai { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayHoaDon { get; set; }
        public DateTime ThoiGianKeToanDuyet { get; set; }
        public string NameKeToanDuyet { get; set; }
        public Guid IdKeToanDuyet { get; set; } 
        public DateTime ThoiGianQuanLyDuyet { get; set; }
        public string NameQuanLyDuyet { get; set; }
        public Guid IdQuanLyDuyet { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
}
