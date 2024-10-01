using Microsoft.AspNetCore.Mvc;

namespace E_Expedisi_Express.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult DocumentNumber()
        {
            return View();
        }
    }
}
