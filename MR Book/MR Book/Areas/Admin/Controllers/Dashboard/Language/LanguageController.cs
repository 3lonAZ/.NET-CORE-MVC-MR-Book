using Microsoft.AspNetCore.Mvc;
using MR_Book.Areas.Admin.Models;
using MR_Book.Areas.Admin.Models.Crud_Operations;
using MR_Book.Models.Filter;
using System.Linq;

namespace MR_Book.Areas.Admin.Controllers.Dashboard.Language
{
    [AdminFilter]

    [Area("Admin")]
    public class LanguageController : Controller
    {
        private readonly ICrud<LanguageModel> _languageModel;
        public LanguageController(ICrud<LanguageModel> languageModel)
        {
            _languageModel = languageModel;
        }
        [Route("Admin/Dashboard/Language")]
        public IActionResult Index()
        {
            var languages = _languageModel.Read();
            return View(languages);
        }

        [HttpGet]
        [Route("Admin/Dashboard/Language/Add")]
        public IActionResult AddLanguage()
        {
            return View();
        }

        [HttpPost]
        [Route("Admin/Dashboard/Language/Add")]
        public IActionResult AddLanguage(LanguageModel model)
        {
            _languageModel.Create(model);
            return RedirectToAction("Index");
        }


        [Route("~/Admin/Dashboard/Language/Delete/{id}")]
        [HttpGet]
        public IActionResult DeleteById(int id)
        {
            _languageModel.Remove(id);
            return RedirectToAction("Index");
        }
        [Route("~/Admin/Dashboard/Language/Delete")]
        [HttpPost]
        public IActionResult DeleteLanguage()
        {
            _languageModel.Delete();

            return RedirectToAction("Index");
        }

        [Route("Admin/Dashboard/Language/Edit/{id}")]
        [HttpGet]
        public IActionResult EditLanguage(int id)
        {
            var selectedLanguage = _languageModel.Read().Where(x => x.Id == id).First();
            return View(selectedLanguage);
        }

        [Route("Admin/Dashboard/Language/Edit/{id}")]
        [HttpPost]
        public IActionResult EditLanguage(int id,LanguageModel model)
        {
            model.Id = id;
            _languageModel.Update(model);

            return RedirectToAction("Index");
        }
    }
}
