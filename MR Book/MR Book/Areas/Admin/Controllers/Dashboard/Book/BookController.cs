using Microsoft.AspNetCore.Mvc;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace MR_Book.Areas.Admin.Controllers.Dashboard
{
    public class BookController : Controller
    {
        [HttpPost]
        public IActionResult Index()
        {
             return View();
        }
    }
}
