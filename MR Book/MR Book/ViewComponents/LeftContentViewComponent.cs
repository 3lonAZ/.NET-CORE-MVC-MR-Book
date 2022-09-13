using Microsoft.AspNetCore.Mvc;
using MR_Book.Models.Pages;

namespace MR_Book.ViewComponents
{
    public class LeftContentViewComponent : ViewComponent
    {
        private readonly IPage _bookProp;
        public LeftContentViewComponent(IPage bookProp)
        {
            _bookProp = bookProp;
        }
        public IViewComponentResult Invoke()
        {
            var categories = _bookProp.GetCategories();
            var languages = _bookProp.GetLanguages();
            var tupleData = (categories, languages);

            return View("Contents", tupleData);
        }
    }
}
