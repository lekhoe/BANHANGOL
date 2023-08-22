using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Infratructure.Datatables
{
    public class Ban
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int SoNguoiToiDa { get; set; }
        public string LoaiBan { get; set; }
        public string Top { get; set; }
        public string Left { get; set; }
        public int TrangThai { get; set; }
        public string KieuDang { get; set; }
        [ForeignKey("KhuVuc")]
        public Guid IdKhuVuc { get; set; }
        public string TenKhuVuc { get; set; }
        public KhuVuc KhuVuc { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
}
