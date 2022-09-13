using Microsoft.AspNetCore.Mvc;

namespace MR_Book.Controllers.Error
{
    public class ErrorPage : Controller
    {
        public IActionResult Error(int statusCode)
        {
            return View();
        }
    }
}
