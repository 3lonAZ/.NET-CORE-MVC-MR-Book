using Microsoft.AspNetCore.Mvc;

namespace MR_Book.Controllers
{
    public class Contact : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
