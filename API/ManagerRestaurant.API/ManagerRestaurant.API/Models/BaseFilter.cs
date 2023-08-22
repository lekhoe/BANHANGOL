using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerRestaurant.API.Models
{
    public class BaseFilter
    {
        public Guid Id { get; set; }
        public string TextSearch { get; set; } = "";
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
