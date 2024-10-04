using E_Expedisi_Express.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Expedisi_Express.Controllers
{
    public class AuditTrailController : Controller
    {
        // Dummy data - you can replace this with a database call if needed
        private List<AuditTrail> GetAuditTrails()
        {
            return new List<AuditTrail>
            {
                new AuditTrail { Id = 1, EmployeeName = "MAFATIKHUL ILMI, S Kom", UserName = "MAFATIKHUL.ILMI@KALBE.CO.ID", AreaAccessed = "Login User", TransactionDetails = "User successfully logged in", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 2, EmployeeName = "MAFATIKHUL ILMI, S Kom", UserName = "MAFATIKHUL.ILMI@KALBE.CO.ID", AreaAccessed = "Menu", TransactionDetails = "Menu: 'UoM' successfully added", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                // Add more records here
            };
        }

        // GET: AuditTrail/Index
        public IActionResult Index()
        {
            var auditTrails = GetAuditTrails(); // Fetch the audit trail data
            return View(auditTrails);
        }
    }
}
