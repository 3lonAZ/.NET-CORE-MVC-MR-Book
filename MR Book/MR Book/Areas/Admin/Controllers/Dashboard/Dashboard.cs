using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MR_Book.Areas.Admin.Models;
using MR_Book.Areas.Admin.Models.Crud_Operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace MR_Book.Areas.Admin.Controllers.Dashboard
{
    [Area("Admin")]
    public class Dashboard : Controller
    {
        private readonly ICrud<BookModel> _bookModel;
        private readonly ICrud<CategoryModel> _categoryModel;
        private readonly ICrud<LanguageModel> _languageModel;
        public Dashboard(ICrud<BookModel> bookModel, ICrud<CategoryModel> categoryModel, ICrud<LanguageModel> languageModel)
        {
            _bookModel = bookModel;
            _categoryModel = categoryModel;
            _languageModel = languageModel;
        }
        public IActionResult Book()
        {
            var books = _bookModel.Read();
            return View(books);
        }
        [Route("Admin/Dashboard/Book/Add")]
        [HttpGet]
        public IActionResult AddBook()
        {
            var categoryList = _categoryModel.Read().Select(p => new SelectListItem
            {
                Text = p.Category,
                Value = p.Id.ToString()
            }).ToList();
            var languageList = _languageModel.Read().Select(p => new SelectListItem
            {
                Text = p.Category,
                Value = p.Id.ToString()
            }).ToList();
            var tupleData = (new BookModel(), categoryList, languageList);
            return View("AddBook",tupleData);
        }
        [HttpPost]
        public IActionResult AddBook([Bind(Prefix = "Item1")] BookModel bookModel)
        {
            _bookModel.Create(bookModel);
            return RedirectToAction("Book");
        }
        [Route("Admin/Dashboard/Book/Edit/{id}")]
        public IActionResult EditBook(int id)
        {
            var books = _bookModel.Read().Where(x => x.Id == id).FirstOrDefault();
            var categoryList = _categoryModel.Read().Select(p => new SelectListItem
            {
                Text = p.Category,
                Value = p.Id.ToString()
            }).ToList();
            int a = 4;
            string c = "213";
            var languageList = _languageModel.Read().Select(p => new SelectListItem
            {
                Text = p.Category,
                Value = p.Id.ToString()
            }).ToList();

            var tupleData = (books, categoryList, languageList);
            return PartialView("~/Areas/Admin/Views/Shared/Partials/Book/EditBook.cshtml",tupleData);
        }

        [Route("Admin/Dashboard/Book/Delete/{id}")]
        public IActionResult DeleteById(int id)
        {
            _bookModel.Remove(id);
            return RedirectToAction("Book");
        }

        [HttpPost] 
        public IActionResult DeleteBook()
        {
            _bookModel.Delete();
            return RedirectToAction("Book");
        }
    }
}
