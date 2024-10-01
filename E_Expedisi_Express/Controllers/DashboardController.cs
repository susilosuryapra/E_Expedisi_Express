using Microsoft.AspNetCore.Mvc;

namespace E_Expedisi_Express.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
