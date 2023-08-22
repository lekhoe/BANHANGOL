using System;
using System.Collections.Generic;

namespace ManagerRestaurant.API.Models
{
    public class BanTrongKhuVucModel
    {
        public Guid Id { get; set; }
        public Guid IdBan { get; set; }
        public Guid IdKhuVuc { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        public virtual ICollection<BanModel> Ban { get; set; }
        public virtual ICollection<KhuVucModel> KhuVuc { get; set; }
    }
    public class BanTrongKhuVucCreateModule
    {
        public Guid Id { get; set; }
        public Guid IdBan { get; set; }
        public Guid IdKhuVuc { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        public virtual ICollection<BanModel> Ban { get; set; }
        public virtual ICollection<KhuVucModel> KhuVuc { get; set; }
    }
    public class BanTrongKhuVucUpdateModule
    {
        public Guid Id { get; set; }
        public Guid IdBan { get; set; }
        public Guid IdKhuVuc { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
        public virtual ICollection<BanModel> Ban { get; set; }
        public virtual ICollection<KhuVucModel> KhuVuc { get; set; }
    }
}
