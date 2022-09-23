using Microsoft.AspNetCore.Mvc;

namespace MR_Book.Models.Pages.Order
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int BookID { get; set; }
        public string FullName{ get; set; }
        public string ContactNumber { get; set; }
        public int? Count { get; set; }
        public string Address{ get; set; }
    }
}
