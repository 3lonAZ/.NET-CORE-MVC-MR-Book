using Microsoft.AspNetCore.Mvc;
using MR_Book.Areas.Admin.Models;
using MR_Book.Areas.Admin.Models.Crud_Operations;
using MR_Book.Models.Filter;
using System.Linq;

namespace MR_Book.Areas.Admin.Controllers.Dashboard.Category
{
    [AdminFilter]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICrud<CategoryModel> _categoryModel;
        public CategoryController(ICrud<CategoryModel> categoryModel)
        {
            _categoryModel = categoryModel;
        }
        [Route("Admin/Dashboard/Category")]
        public IActionResult Index()
        {
            var categories = _categoryModel.Read();
            return View(categories);
        }

        [HttpGet]
        [Route("Admin/Dashboard/Category/Add")]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [Route("Admin/Dashboard/Category/Add")]
        public IActionResult AddCategory(CategoryModel model)
        {
            _categoryModel.Create(model);
            return RedirectToAction("Index");
        }

        [Route("~/Admin/Dashboard/Category/Delete/{id}")]
        [HttpGet]
        public IActionResult DeleteById(int id)
        {
            _categoryModel.Remove(id);
            return RedirectToAction("Index");
        }
        [Route("~/Admin/Dashboard/Category/Delete")]
        [HttpPost]
        public IActionResult DeleteCategory()
        {
            _categoryModel.Delete();

            return RedirectToAction("Index");
        }

        [Route("Admin/Dashboard/Category/Edit/{id}")]
        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var selectedLanguage = _categoryModel.Read().Where(x => x.Id == id).First();
            return View(selectedLanguage);
        }

        [Route("Admin/Dashboard/Category/Edit/{id}")]
        [HttpPost]
        public IActionResult EditCategory(int id, CategoryModel model)
        {
            model.Id = id;
            _categoryModel.Update(model);

            return RedirectToAction("Index");
        }
    }
}
