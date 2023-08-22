using System;

namespace ManagerRestaurant.API.Infratructure.Datatables
{
    public class Quyen
    {
        public Guid Id { get; set; }
        public Guid IdNhomQuyen { get; set; }
        public int Level { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
}
