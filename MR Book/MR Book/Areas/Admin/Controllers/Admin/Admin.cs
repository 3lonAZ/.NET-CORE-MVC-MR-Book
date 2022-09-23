using Microsoft.AspNetCore.Http;
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
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("admin_usr")))
                return Redirect("~/Admin/Dashboard/Book");


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
                    HttpContext.Session.SetString("admin_usr", adminLog.Username);
                    HttpContext.Session.SetString("admin_pass", adminLog.Password);

                    return Redirect("~/Admin/Dashboard/Book");
                }
                else
                    ViewBag.FailLogMsg = "İstifadəçi adı və ya şifrə səhvdir !";
            }
            return View(adminLog);
        }
    }
}
