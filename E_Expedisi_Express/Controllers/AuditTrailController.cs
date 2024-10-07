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
                new AuditTrail { Id = 1, EmployeeName = "LUMINE EIDENLIGHT", UserName = "LUMINEEDIEN@KALBE.CO.ID", AreaAccessed = "Login User", TransactionDetails = "User successfully logged in", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 2, EmployeeName = "AETHER EIDENLIGHT", UserName = "AETHEREIDEN@KALBE.CO.ID", AreaAccessed = "Menu", TransactionDetails = "Menu: 'UoM' successfully added", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 3, EmployeeName = "DAINSLEIF SATRIA KHAENRIAH", UserName = "DAINSAKHA@KALBE.CO.ID", AreaAccessed = "Menu", TransactionDetails = "Menu: 'UoM' successfully added", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 4, EmployeeName = "LUMINE EIDENLIGHT", UserName = "LUMINEEDIEN@KALBE.CO.ID", AreaAccessed = "Login User", TransactionDetails = "User successfully logged in", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 5, EmployeeName = "AETHER EIDENLIGHT", UserName = "AETHEREIDEN@KALBE.CO.ID", AreaAccessed = "Menu", TransactionDetails = "Menu: 'UoM' successfully added", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 6, EmployeeName = "DAINSLEIF SATRIA KHAENRIAH", UserName = "DAINSAKHA@KALBE.CO.ID", AreaAccessed = "Menu", TransactionDetails = "Menu: 'UoM' successfully added", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 7, EmployeeName = "LUMINE EIDENLIGHT", UserName = "LUMINEEDIEN@KALBE.CO.ID", AreaAccessed = "Login User", TransactionDetails = "User successfully logged in", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 8, EmployeeName = "AETHER EIDENLIGHT", UserName = "AETHEREIDEN@KALBE.CO.ID", AreaAccessed = "Menu", TransactionDetails = "Menu: 'UoM' successfully added", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 9, EmployeeName = "DAINSLEIF SATRIA KHAENRIAH", UserName = "DAINSAKHA@KALBE.CO.ID", AreaAccessed = "Menu", TransactionDetails = "Menu: 'UoM' successfully added", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 10, EmployeeName = "LUMINE EIDENLIGHT", UserName = "LUMINEEDIEN@KALBE.CO.ID", AreaAccessed = "Login User", TransactionDetails = "User successfully logged in", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 11, EmployeeName = "AETHER EIDENLIGHT", UserName = "AETHEREIDEN@KALBE.CO.ID", AreaAccessed = "Menu", TransactionDetails = "Menu: 'UoM' successfully added", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 12, EmployeeName = "DAINSLEIF SATRIA KHAENRIAH", UserName = "DAINSAKHA@KALBE.CO.ID", AreaAccessed = "Menu", TransactionDetails = "Menu: 'UoM' successfully added", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 13, EmployeeName = "LUMINE EIDENLIGHT", UserName = "LUMINEEDIEN@KALBE.CO.ID", AreaAccessed = "Login User", TransactionDetails = "User successfully logged in", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 14, EmployeeName = "AETHER EIDENLIGHT", UserName = "AETHEREIDEN@KALBE.CO.ID", AreaAccessed = "Menu", TransactionDetails = "Menu: 'UoM' successfully added", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 15, EmployeeName = "DAINSLEIF SATRIA KHAENRIAH", UserName = "DAINSAKHA@KALBE.CO.ID", AreaAccessed = "Menu", TransactionDetails = "Menu: 'UoM' successfully added", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 16, EmployeeName = "LUMINE EIDENLIGHT", UserName = "LUMINEEDIEN@KALBE.CO.ID", AreaAccessed = "Login User", TransactionDetails = "User successfully logged in", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 17, EmployeeName = "AETHER EIDENLIGHT", UserName = "AETHEREIDEN@KALBE.CO.ID", AreaAccessed = "Menu", TransactionDetails = "Menu: 'UoM' successfully added", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 18, EmployeeName = "DAINSLEIF SATRIA KHAENRIAH", UserName = "DAINSAKHA@KALBE.CO.ID", AreaAccessed = "Menu", TransactionDetails = "Menu: 'UoM' successfully added", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 19, EmployeeName = "LUMINE EIDENLIGHT", UserName = "LUMINEEDIEN@KALBE.CO.ID", AreaAccessed = "Login User", TransactionDetails = "User successfully logged in", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 20, EmployeeName = "AETHER EIDENLIGHT", UserName = "AETHEREIDEN@KALBE.CO.ID", AreaAccessed = "Menu", TransactionDetails = "Menu: 'UoM' successfully added", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 21, EmployeeName = "DAINSLEIF SATRIA KHAENRIAH", UserName = "DAINSAKHA@KALBE.CO.ID", AreaAccessed = "Menu", TransactionDetails = "Menu: 'UoM' successfully added", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 22, EmployeeName = "LUMINE EIDENLIGHT", UserName = "LUMINEEDIEN@KALBE.CO.ID", AreaAccessed = "Login User", TransactionDetails = "User successfully logged in", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 23, EmployeeName = "AETHER EIDENLIGHT", UserName = "AETHEREIDEN@KALBE.CO.ID", AreaAccessed = "Menu", TransactionDetails = "Menu: 'UoM' successfully added", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 24, EmployeeName = "DAINSLEIF SATRIA KHAENRIAH", UserName = "DAINSAKHA@KALBE.CO.ID", AreaAccessed = "Menu", TransactionDetails = "Menu: 'UoM' successfully added", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 25, EmployeeName = "LUMINE EIDENLIGHT", UserName = "LUMINEEDIEN@KALBE.CO.ID", AreaAccessed = "Login User", TransactionDetails = "User successfully logged in", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 26, EmployeeName = "AETHER EIDENLIGHT", UserName = "AETHEREIDEN@KALBE.CO.ID", AreaAccessed = "Menu", TransactionDetails = "Menu: 'UoM' successfully added", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
                new AuditTrail { Id = 27, EmployeeName = "DAINSLEIF SATRIA KHAENRIAH", UserName = "DAINSAKHA@KALBE.CO.ID", AreaAccessed = "Menu", TransactionDetails = "Menu: 'UoM' successfully added", Timestamp = DateTime.Now, Role = "User", Company = "Kalbe" },
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
