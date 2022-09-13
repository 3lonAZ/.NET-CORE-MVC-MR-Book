using Microsoft.AspNetCore.Mvc;
using MR_Book.Models.Connections;
using MR_Book.Models.Pages;
using MR_Book.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MR_Book.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly IPage _homePage;
        public HomeController(IPage homePage)
        {
            _homePage = homePage;
        }
        public IActionResult Index()
        {
            var interfaceData = _homePage.SelectData().Select(p => new ShowBookInterfaceVM()
            {
                Id = p.Id,
                Name = p.Name,
                About = p.About,
                Image = p.Image,
                Price = p.Price
            }).ToList();
            interfaceData.Reverse();
            var books = interfaceData.Take(4).ToList();

            return View(books);
        }
       
    }
}
