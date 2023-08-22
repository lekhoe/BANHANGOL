using System;
using System.Collections.Generic;

namespace ManagerRestaurant.API.Models
{
    public class PhieuOderModel
    {
        public Guid Id { get; set; }
        public BanModel Ban { get; set; }
        public UserModel ThuNgan { get; set; }
        public KhachHangModel KhachHang { get; set; }
        public List<DoAnV2Model> DoAns { get; set; }
        public float TongTien { get; set; }
        public float ThucThu { get; set; }
        public string Vocher { get; set; }
        public float SoTienGiam { get; set; }
        public int TrangThai { get; set; }
        public DateTime ThoiGianThanhToan { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; } 
    }

    public class PhieuOderCreateModel
    { 
        public Guid IdBan { get; set; }
        public Guid IdThuNgan { get; set; } = Guid.Empty; 
        public Guid IdKhachHang { get; set; } = Guid.Empty;
        public List<OderCreateModule> MonAns { get; set; }
        public float TongTien { get; set; } = 0;
        public float ThucThu { get; set; } = 0;
        public float SoTienGiam { get; set; } = 0;
        public string Vocher { get; set; } = "";
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; } = "";
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; } 
        public string LastModifiedByUserName { get; set; } = "";
    }
    public class OderCreateModule
    {
        public Guid Id { get; set; }
        public int SoLuong { get; set; }
        public Guid? CreatedByUserId{get;set;}
        public string CreatedByUserName{get;set;}
        public string CreatedOnDate{get;set;}
        public Guid? LastModifiedByUserId{get;set;}
        public string LastModifiedByUserName{ get; set; }
    }
    public class PhieuOderUpdateModel
    {
        public Guid Id { get; set; }
        public Guid IdBan { get; set; }
        public List<OderCreateModule> MonAns { get; set; }
        public Guid IdThuNgan { get; set; }
        public Guid IdKhachHang { get; set; }
        public float TongTien { get; set; }
        public float ThucThu { get; set; }
        public string Vocher { get; set; }
        public float SoTienGiam { get; set; }
        public int TrangThai { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
}
