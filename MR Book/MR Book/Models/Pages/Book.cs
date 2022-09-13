using System;
using System.Collections.Generic;

namespace MR_Book.Models.Pages
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Languages { get; set; }
        public int PageCount { get; set; }
        public string Category { get; set; }
        public string About { get; set; }
        public double Price { get; set; }
        public DateTime Release_Date { get; set; }
        public string Size { get; set; }
        public string Image { get; set; }
    }

}
