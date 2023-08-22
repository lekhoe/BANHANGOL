using System;

namespace ManagerRestaurant.API.Models
{
    public class BarModule
    {
        public Guid Id { get; set; }
        public string MaMatHang { get; set; }
        public string TenMatHang { get; set; }
        public string NhomMatHang { get; set; }
        public int SoLuong { get; set; }
        public string DonVi { get; set; }
        public string DonGia { get; set; }
        public float ThanhTien { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
    public class BarCreateModule
    { 
        public string MaMatHang { get; set; }
        public string TenMatHang { get; set; }
        public string NhomMatHang { get; set; }
        public int SoLuong { get; set; }
        public string DonVi { get; set; }
        public string DonGia { get; set; }
        public float ThanhTien { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; } 
    }
    public class BarUpdateModule
    {
        public Guid Id { get; set; }
        public string MaMatHang { get; set; }
        public string TenMatHang { get; set; }
        public string NhomMatHang { get; set; }
        public int SoLuong { get; set; }
        public string DonVi { get; set; }
        public string DonGia { get; set; }
        public float ThanhTien { get; set; } 
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
}
