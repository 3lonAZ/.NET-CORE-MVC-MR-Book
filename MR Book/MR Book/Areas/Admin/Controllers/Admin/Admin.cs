using Microsoft.AspNetCore.Mvc;
using MR_Book.Areas.Admin.Models.Admin;
using System.Linq;

namespace MR_Book.Areas.Admin_Panel.Controllers.Admin
{
    [Area("Admin")]
    public class Admin : Controller
    {
        private readonly ILogin _login;
        public Admin(ILogin login)
        {
            _login = login;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(AdminLog adminLog)
        {
            if (ModelState.IsValid)
            {
                var logStatus = _login.CheckLogin(adminLog);
                if (logStatus)
                {

                }
                else
                    ViewBag.FailLogMsg = "İstifadəçi adı və ya şifrə səhvdir !";
            }
          
            return View(adminLog);
        }
    }
}
