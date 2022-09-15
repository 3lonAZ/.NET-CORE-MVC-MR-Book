using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MR_Book.Areas.Admin.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int LanguageId { get; set; }
        public int CategoryID { get; set; }
        public string Languages { get; set; }
        public int? PageCount { get; set; }
        public string Category { get; set; }
        public string About { get; set; }
        public double? Price { get; set; }
        public DateTime? Release_Date { get; set; }
        public string Size { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile BookIMG { get; set; }
    }
}

