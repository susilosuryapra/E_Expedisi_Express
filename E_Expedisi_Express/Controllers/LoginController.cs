using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Linq;
using E_Expedisi_Express.Models;

namespace E_Expedisi_Express.Controllers
{
    public class AccountController : Controller
    {
        private readonly E_Expedisi_ExpressContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(E_Expedisi_ExpressContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        } 

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string usernameOrEmail, string password)
        {
            if (string.IsNullOrEmpty(usernameOrEmail) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Username/Email and password are required.");
                return View();
            }

            var user = _context.MST_User
                .FirstOrDefault(u => (u.Username == usernameOrEmail || u.Email == usernameOrEmail) && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username!);
                HttpContext.Session.SetString("Name", user.Name ?? "");
                return RedirectToAction("AssetData", "Assets");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}