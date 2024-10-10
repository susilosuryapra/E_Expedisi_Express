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
            var departmentCodes = await _context.DepartmentCode.ToListAsync();

            var departmentCodeDTOs = departmentCodes.Select(d => new DepartmentCodeDTO
            {
                NewId = d.NewId,
                DepartmentName = d.DepartmentName,  // Renamed
                DepartmentCode = d.DepartmentCode,  // Renamed
                Description = d.Description,
                CompCode = d.CompCode ?? "N/A", // Set nilai default jika null
                DivCode = d.DivCode ?? "N/A", // Set nilai default jika null
                IsActive = d.IsActive
            }).ToList();

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
        public async Task<IActionResult> Create(DepartmentCodeAddDto departmentCodeDTO)
        {
            if (ModelState.IsValid)
            {
                var nameExists = await _context.DepartmentCode.AnyAsync(d => d.DepartmentName == departmentCodeDTO.DepartmentName);
                var codeExists = await _context.DepartmentCode.AnyAsync(d => d.DepartmentCode == departmentCodeDTO.DepartmentCode);

                if (nameExists)
                {
                    ModelState.AddModelError("DepartmentName", "Department name already exists.");
                }

                if (codeExists)
                {
                    ModelState.AddModelError("DepartmentCode", "Department code already exists.");
                }

                if (!nameExists && !codeExists)
                {
                    var departmentCode = new DepartmentCodeModel
                    {
                        NewId = Guid.NewGuid().ToString(),
                        DepartmentName = departmentCodeDTO.DepartmentName,
                        DepartmentCode = departmentCodeDTO.DepartmentCode,
                        Description = departmentCodeDTO.Description,
                        CompCode = "01",
                        DivCode = "KF.036",
                        IsActive = true,
                        CreatedBy = "Admin",
                        CreatedDate = DateTime.Now
                    };

                    _context.DepartmentCode.Add(departmentCode);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index), new { success = true });
                }
            }

            return View(departmentCodeDTO);
        }

        // GET: DepartmentCode/Edit/{newId}
        public async Task<IActionResult> Edit(string newId)
        {
            var departmentCode = await _context.DepartmentCode.FirstOrDefaultAsync(d => d.NewId == newId);

            if (departmentCode == null)
            {
                return NotFound();
            }

            var departmentCodeDTO = new DepartmentCodeDTO
            {
                NewId = departmentCode.NewId,
                DepartmentName = departmentCode.DepartmentName,
                DepartmentCode = departmentCode.DepartmentCode,
                Description = departmentCode.Description,
                CompCode = departmentCode.CompCode,
                DivCode = departmentCode.DivCode,
                IsActive = departmentCode.IsActive
            };

            return View(departmentCodeDTO);
        }

        // POST: DepartmentCode/Edit/{newId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string newId, DepartmentCodeDTO departmentCodeDTO)
        {
            var departmentCode = await _context.DepartmentCode.FirstOrDefaultAsync(d => d.NewId == newId);

            if (departmentCode == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var nameExists = await _context.DepartmentCode
                    .AnyAsync(d => d.DepartmentName == departmentCodeDTO.DepartmentName && d.NewId != newId);

                var codeExists = await _context.DepartmentCode
                    .AnyAsync(d => d.DepartmentCode == departmentCodeDTO.DepartmentCode && d.NewId != newId);

                if (nameExists)
                {
                    ModelState.AddModelError("DepartmentName", "Department name already exists.");
                }

                if (codeExists)
                {
                    ModelState.AddModelError("DepartmentCode", "Department code already exists.");
                }

                if (!nameExists && !codeExists)
                {
                    departmentCode.DepartmentName = departmentCodeDTO.DepartmentName;
                    departmentCode.DepartmentCode = departmentCodeDTO.DepartmentCode;
                    departmentCode.Description = departmentCodeDTO.Description;
                    departmentCode.CompCode = departmentCodeDTO.CompCode;
                    departmentCode.DivCode = departmentCodeDTO.DivCode;
                    departmentCode.IsActive = departmentCodeDTO.IsActive;
                    departmentCode.UpdatedBy = "AdminEdit";
                    departmentCode.UpdatedDate = DateTime.Now;

                    _context.Update(departmentCode);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index), new { success = true });
                }
            }

            return View(departmentCodeDTO);
        }
    }
}
