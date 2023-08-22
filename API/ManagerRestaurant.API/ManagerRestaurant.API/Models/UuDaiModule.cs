using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerRestaurant.API.Models
{
    public class UuDaiModule
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Anh { get; set; }
        public string NoiDung { get; set; }
        public float GiaTri { get; set; }
        public int LoaiUuDai { get; set; }
        public int DonViTinh { get; set; }
        public int TrangThai { get; set; }
        public DoAnModel IdDoAn { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
    public class UuDaiCreateModule
    { 
        public string Name { get; set; }
        public string Anh { get; set; }
        public string NoiDung { get; set; }
        public float GiaTri { get; set; }
        public int LoaiUuDai { get; set; }
        public int DonViTinh { get; set; }
        public int TrangThai { get; set; }
        public Guid? IdDoAn { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
    }
    public class UuDaiUpdateModule
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Anh { get; set; }
        public string NoiDung { get; set; }
        public float GiaTri { get; set; }
        public int LoaiUuDai { get; set; }
        public int DonViTinh { get; set; }
        public int TrangThai { get; set; }
        public Guid? IdDoAn { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
}
