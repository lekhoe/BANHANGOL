using System;

namespace ManagerRestaurant.API.Models
{
    public class ChiTieuTrongNgayModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MatHang { get; set; }
        public string Anh { get; set; }
        public float TongSoTien { get; set; }
        public string TrangThaiHienTai { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayHoaDon { get; set; }
        public DateTime ThoiGianKeToanDuyet { get; set; }
        public string NameKeToanDuyet { get; set; }
        public DateTime ThoiGianQuanLyDuyet { get; set; }
        public string NameQuanLyDuyet { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
    public class ChiTieuTrongNgayCreateModule
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MatHang { get; set; }
        public string Anh { get; set; }
        public float TongSoTien { get; set; }
        public DateTime NgayHoaDon { get; set; }
        public string TrangThaiHienTai { get; set; }
        public string GhiChu { get; set; }
        public DateTime ThoiGianKeToanDuyet { get; set; }
        public string NameKeToanDuyet { get; set; }
        public DateTime ThoiGianQuanLyDuyet { get; set; }
        public string NameQuanLyDuyet { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; } 
    }
    public class ChiTieuTrongNgayUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MatHang { get; set; }
        public string Anh { get; set; }
        public float TongSoTien { get; set; }
        public DateTime NgayHoaDon { get; set; }
        public string TrangThaiHienTai { get; set; }
        public string GhiChu { get; set; }
        public DateTime ThoiGianKeToanDuyet { get; set; }
        public string NameKeToanDuyet { get; set; }
        public DateTime ThoiGianQuanLyDuyet { get; set; }
        public string NameQuanLyDuyet { get; set; } 
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
}
