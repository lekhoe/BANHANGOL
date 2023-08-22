using System;

namespace ManagerRestaurant.API.Infratructure.Datatables
{
    public class UuDai
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
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
}
