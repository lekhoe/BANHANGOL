using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infratructure.Datatables
{
    public class User
    {
        public Guid Id { get; set; }
        public string MaNV { get; set; }
        public string FullName { get; set; }
        public string Phai { get; set; }
        public string ChucVu { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string ChiChu { get; set; }
        public string Quyen { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsDelete { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
}
