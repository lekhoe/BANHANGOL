using System;

namespace ManagerRestaurant.API.Models
{
    public class KhachHangModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SoDienThoai { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
    public class KhachHangCreateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SoDienThoai { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; } 
    }
    public class KhachHangUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SoDienThoai { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }

    }
}
