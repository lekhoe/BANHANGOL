using System;

namespace ManagerRestaurant.API.Models
{
    public class YKienDongGopModel
    {
        public Guid Id { get; set; }
        public string TenKH { get; set; }
        public string SoDienThoai { get; set; }
        public string NoiDung { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }

    public class YKienDongGopCreateModel
    {
        public string TenKH { get; set; }
        public string SoDienThoai { get; set; }
        public string NoiDung { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; } 
    }
    public class YKienDongGopUpdateModel
    { 
        public string TenKH { get; set; }
        public string SoDienThoai { get; set; }
        public string NoiDung { get; set; } 
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
}
