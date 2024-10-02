using E_Expedisi_Express.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Expedisi_Express.Controllers
{
    public class DepartmentCodeController : Controller
    {
        // Dummy data, replace with database call if needed
        private List<DepartmentCode> GetDepartmentCodes()
        {
            return new List<DepartmentCode>
            {
                new DepartmentCode { Id = 1, Code = "HR", Name = "Human Resources", Description = "HR Department" },
                new DepartmentCode { Id = 2, Code = "IT", Name = "Information Technology", Description = "IT Department" },
                new DepartmentCode { Id = 3, Code = "FIN", Name = "Finance", Description = "Finance Department" },
                new DepartmentCode { Id = 4, Code = "DMY", Name = "Dummy", Description = "Dummy Department" },
                new DepartmentCode { Id = 5, Code = "DMY", Name = "Dummy", Description = "Dummy Department" },
                new DepartmentCode { Id = 6, Code = "DMY", Name = "Dummy", Description = "Dummy Department" },
                new DepartmentCode { Id = 7, Code = "DMY", Name = "Dummy", Description = "Dummy Department" },
                new DepartmentCode { Id = 8, Code = "DMY", Name = "Dummy", Description = "Dummy Department" },
                new DepartmentCode { Id = 9, Code = "DMY", Name = "Dummy", Description = "Dummy Department" },
                new DepartmentCode { Id = 10, Code = "DMY", Name = "Dummy", Description = "Dummy Department" },
                new DepartmentCode { Id = 11, Code = "DMY", Name = "Dummy", Description = "Dummy Department" },
                new DepartmentCode { Id = 12, Code = "DMY", Name = "Dummy", Description = "Dummy Department" },
                new DepartmentCode { Id = 13, Code = "DMY", Name = "Dummy", Description = "Dummy Department" },
                new DepartmentCode { Id = 14, Code = "TSR", Name = "Treasure", Description = "Treasure Department" },
                new DepartmentCode { Id = 15, Code = "TSR", Name = "Treasure", Description = "Treasure Department" },
                new DepartmentCode { Id = 16, Code = "DMY", Name = "Dummy", Description = "Dummy Department" },
                new DepartmentCode { Id = 17, Code = "DMY", Name = "Dummy", Description = "Dummy Department" },
                new DepartmentCode { Id = 18, Code = "DMY", Name = "Dummy", Description = "Dummy Department" },
                new DepartmentCode { Id = 19, Code = "DMY", Name = "Dummy", Description = "Dummy Department" },
                new DepartmentCode { Id = 20, Code = "DMY", Name = "Dummy", Description = "Dummy Department" },
                new DepartmentCode { Id = 21, Code = "DMY", Name = "Dummy", Description = "Dummy Department" },
                new DepartmentCode { Id = 22, Code = "DMY", Name = "Dummy", Description = "Dummy Department" },
                new DepartmentCode { Id = 23, Code = "DMY", Name = "Dummy", Description = "Dummy Department" },
            };
        }

        public IActionResult Index()
        {
            var departmentCodes = GetDepartmentCodes();
            return View(departmentCodes);
        }

        // GET: Add Department Code Page
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Add Department Code
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentCode departmentCode)
        {
            if (ModelState.IsValid)
            {
                // Dapatkan daftar department yang ada
                var departmentCodes = GetDepartmentCodes();

                // Simulasikan penyimpanan data (tambahkan departmentCode baru ke list)
                departmentCode.Id = departmentCodes.Max(d => d.Id) + 1; // Generate ID baru
                departmentCodes.Add(departmentCode);

                // Di dunia nyata, data ini harus disimpan di database, bukan di memori seperti ini.
                return RedirectToAction("Index");
            }

            // Jika model tidak valid, tampilkan form lagi dengan pesan error
            return View(departmentCode);
        }
    }
}