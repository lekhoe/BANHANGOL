using System;

namespace ManagerRestaurant.API.Models
{
    public class OderModel
    {

        public Guid Id { get; set; } 
        public PhieuOderModel PhieuOder { get; set; }  
        public DoAnModel DoAn { get; set; }
        public int SoLuong { get; set; }
        public BanModel Ban { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
    public class OderCreateModel
    {

        public Guid Id { get; set; }
        public Guid IdPhieuOder { get; set; }        
        public Guid IdDoAn { get; set; }
        public int SoLuong { get; set; }
        public Guid IdBan { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
    }
    public class OderUpdateModel
    {

        public Guid Id { get; set; } 
        public Guid IdPhieuOder { get; set; } 
        public Guid IdDoAn { get; set; } 
        public int SoLuong { get; set; }
        public Guid IdBan { get; set; } 
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
}
