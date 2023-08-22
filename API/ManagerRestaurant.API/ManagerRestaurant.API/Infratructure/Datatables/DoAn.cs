using ManagerRestaurant.API.Infratructure.Datatables;
using System; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace Infratructure.Datatables
{
    public class DoAn
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("TheLoaiDoAn")]
        public Guid MaTheLoai { get; set; }
        public TheLoaiDoAn TheLoaiDoAn { get; set; }
        public string LinkAnh { get; set; } 
        public string GhiChu { get; set; }
        public string DanhSachMonAn { get; set; }
        public float DonGia { get; set; }
        public string DonViTinh { get; set; }
        public bool TrangThai { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
}
