using System;

namespace ManagerRestaurant.API.Models
{
    public class KhuVucModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HtmlObject { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
    public class KhuVucCreateModel
    {public Guid Id { get; set; }
        public string Name { get; set; }
        public string HtmlObject { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }

    }
    public class KhuVucUpdateModel
    {public Guid Id { get; set; }
        public string Name { get; set; }
        public string HtmlObject { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? CreatedOnDate { get; set; }
        public Guid? LastModifiedByUserId { get; set; }
        public string LastModifiedByUserName { get; set; }
    }
}
