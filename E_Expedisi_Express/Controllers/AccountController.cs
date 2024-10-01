using Microsoft.AspNetCore.Mvc;
using E_Expedisi_Express.Data;
using E_Expedisi_Express.Models;

namespace E_Expedisi_Express.Controllers
{
    public class AccountController : Controller
    {
        private readonly ExpedisiDbContext _context;

        public AccountController(ExpedisiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string usernameOrEmail, string password)
        {
            var user = _context.MST_User
                .FirstOrDefault(u => (u.Username == usernameOrEmail || u.Email == usernameOrEmail) && u.Password == password);

            if (user != null)
            {
                // Logic untuk login berhasil, bisa set session atau cookie
                return RedirectToAction("Index", "Home"); // Ganti dengan action sesuai kebutuhan
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }
    }
}
