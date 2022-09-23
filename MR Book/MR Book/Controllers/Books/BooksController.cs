using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MR_Book.Models.Pages;
using MR_Book.Models.Pages.Order;
using MR_Book.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MR_Book.Controllers
{
    public class BooksController : Controller
    {
        private readonly IPage _booksPage;
        public BooksController(IPage booksPage)
        {
            _booksPage = booksPage;
        }
        public IActionResult Index()
        {
            var interfaceData = _booksPage.SelectData().Select(p => new ShowBookInterfaceVM()
            {
                Id = p.Id,
                Name = p.Name,
                About = p.About,
                Image = p.Image,
                Price = p.Price
            }).ToList();

            return View(interfaceData);
        }
        [Route("Books/Category/{category}")]
        public IActionResult ExternalCategory(string category)
        {

            var interfaceData = _booksPage.SelectData().Where(x => x.Category == category).Select(p => new ShowBookInterfaceVM()
            {
                Id = p.Id,
                Name = p.Name,
                About = p.About,
                Image = p.Image,
                Price = p.Price
            }).ToList();

            return View("Index", interfaceData);
        }


        [Route("Books/Language/{language}")]

        public IActionResult ExternalLanguage(string language)
        {
            var interfaceData = _booksPage.SelectData().Where(x => x.Languages == language).Select(p => new ShowBookInterfaceVM()
            {
                Id = p.Id,
                Name = p.Name,
                About = p.About,
                Image = p.Image,
                Price = p.Price
            }).ToList();

            return View("Index", interfaceData);
        }
        public IActionResult Search(string name)
        {
            if (!string.IsNullOrEmpty(name?.Trim()))
            {
                string searchValue = name;
                List<ShowBookInterfaceVM> searchedBook = _booksPage.SelectData().Where(x => x.Name.ToLower().Contains(searchValue.ToLower())).Select(p => new ShowBookInterfaceVM()
                {
                    Id = p.Id,
                    Name = p.Name,
                    About = p.About,
                    Image = p.Image,
                    Price = p.Price
                }).ToList();
                ViewBag.srcValue = searchValue;

                if (searchedBook.Count > 0)
                    return View(searchedBook);

                return PartialView("~/Views/Shared/Partials/Search/_NoResultPartial.cshtml");
            }
            return PartialView("~/Views/Shared/Partials/Search/_NoResultPartial.cshtml");
        }
        [Route("Books/About/{id}")]
        public IActionResult About(int id)
        {
            var bookData = _booksPage.SelectData().Where(x => x.Id == id).ToList();

            return View("~/Views/Shared/Partials/Details/_AboutPartial.cshtml", bookData);
        }

        [HttpGet]
        [Route("Books/Order/{id}")]
        public IActionResult Order(int id)
        {
            var bookData = _booksPage.SelectData().Where(x => x.Id == id).Select(p => new OrderVM
            {
                BookID = p.Id,
                Name = p.Name,
                Image = p.Image
            }).ToList();
            var tupleData = (bookData, new OrderDetail());
            return View("~/Views/Shared/Partials/Order/_Buy.cshtml", tupleData);
        }
        [HttpPost]
        [Route("Books/Order/{book_id}")]
        public IActionResult Order(int book_id,[Bind(Prefix = "Item2")] OrderDetail order)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();

                return View("~/Views/Shared/Partials/Order/_IsValidAlertPartial.cshtml",allErrors);
            }
            order.BookID = book_id;
            _booksPage.InsertOrder(order);


            return View("~/Views/Shared/Partials/Order/_ReceivedOrderPartial.cshtml");
        }
    }
}
