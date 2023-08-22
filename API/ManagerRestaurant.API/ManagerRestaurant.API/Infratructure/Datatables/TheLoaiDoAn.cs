using System;
using System.Collections.Generic;

namespace Infratructure.Datatables
{
    public class TheLoaiDoAn
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool isMany { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        public ICollection<DoAn> DoAns { get; set; }
    }
}
