using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Infratructure.Datatables
{
    public class Oder
    {
        public Guid Id { get; set; }
        [ForeignKey("PhieuOder")]
        public Guid IdPhieuOder { get; set; }
        public PhieuOder PhieuOder { get; set; }
        [ForeignKey("DoAn")]
        public Guid IdDoAn { get; set; }
        public DoAn DoAn { get; set; }
        public int SoLuong { get; set; }
        public Guid IdBan { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        
    }
}
