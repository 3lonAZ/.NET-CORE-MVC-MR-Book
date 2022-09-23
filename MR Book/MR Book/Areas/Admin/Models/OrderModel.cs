using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MR_Book.Areas.Admin.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int BookID{ get; set; }
        public string BookName{ get; set; }
        public string FullName { get; set; }
        public string ContactNumber { get; set; }
        public int Count { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
    }
}

