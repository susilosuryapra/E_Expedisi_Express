using E_Expedisi_Express.Data;
using E_Expedisi_Express.DTO;
using E_Expedisi_Express.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace E_Expedisi_Express.Controllers
{
    public class DepartmentCodeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentCodeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DepartmentCode
        public async Task<IActionResult> Index()
        {
            // Ambil data dari database
            var departmentCodes = await _context.DepartmentCode.ToListAsync();

            // Memetakan dari entity ke DTO
            var departmentCodeDTOs = departmentCodes.Select(d => new DepartmentCodeDTO
            {
                Name = d.Name,
                Code = d.Code,
                Description = d.Description,
                IsActive = d.IsActive
            }).ToList();

            // Kirim list DTO ke View
            return View(departmentCodeDTOs);
        }

        // GET: Add Department Code Page
        public IActionResult Create()
        {
            return View();
        }

        // POST: Add Department Code
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentCodeDTO departmentCodeDTO)
        {
            if (ModelState.IsValid)
            {
                var departmentCode = new DepartmentCode
                {
                    Name = departmentCodeDTO.Name,
                    Code = departmentCodeDTO.Code,
                    Description = departmentCodeDTO.Description,
                    IsActive = true,
                    CreatedBy = "Admin", // Atur nama user yang membuat record
                    CreatedDate = DateTime.Now
                };

                _context.DepartmentCode.Add(departmentCode);
                await _context.SaveChangesAsync();

                // Pass the success flag in the query string
                return RedirectToAction(nameof(Index), new { success = true });
            }

            return View(departmentCodeDTO);
        }



        // GET: DepartmentCode/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var departmentCode = await _context.DepartmentCode.FindAsync(id);
            if (departmentCode == null)
            {
                return NotFound();
            }
            return View(departmentCode);
        }

        // POST: DepartmentCode/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartmentCode departmentCode)
        {
            if (id != departmentCode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(departmentCode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departmentCode);
        }

        // POST: DepartmentCode/CheckDuplicate
        [HttpPost]
        public JsonResult CheckDuplicate(string name, string code)
        {
            // Check if the Name already exists in the database
            bool nameExists = _context.DepartmentCode.Any(d => d.Name == name);

            // Check if the Code already exists in the database
            bool codeExists = _context.DepartmentCode.Any(d => d.Code == code);

            // Return a JSON object with the existence check results
            return Json(new { nameExists = nameExists, codeExists = codeExists });
        }
    }
}
