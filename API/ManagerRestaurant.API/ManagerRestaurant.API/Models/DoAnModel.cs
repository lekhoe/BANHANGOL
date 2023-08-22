using System;

namespace ManagerRestaurant.API.Models
{
    public class DoAnModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid MaTheLoai { get; set; }
        public string TenTheLoai { get; set; }
        public string LinkAnh { get; set; } 
        public string GhiChu { get; set; }
        public string DanhSachMonAn { get; set; }
        public string DonViTinh { get; set; }
        public float DonGia { get; set; }
        public bool TrangThai { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
    public class DoAnV2Model
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid MaTheLoai { get; set; }
        public string TenTheLoai { get; set; }
        public string LinkAnh { get; set; }
        public string GhiChu { get; set; }
        public string DanhSachMonAn { get; set; }
        public string DonViTinh { get; set; }
        public float DonGia { get; set; }
        public bool TrangThai { get; set; }
        public int SoLuong { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
    public class DoAnCreateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string LinkAnh { get; set; }
        public Guid MaTheLoai { get; set; } 
        public string GhiChu { get; set; }
        public string DanhSachMonAn { get; set; }
        public string DonViTinh { get; set; }
        public float DonGia { get; set; }
        public bool TrangThai { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; } 
    }
    public class DoAnUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string LinkAnh { get; set; } 
        public string GhiChu { get; set; }
        public string DonViTinh { get; set; }
        public float DonGia { get; set; }
        public string DanhSachMonAn { get; set; }
        public Guid MaTheLoai { get; set; }
        public bool TrangThai { get; set; } 
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }

}
