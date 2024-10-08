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
                Id = d.Id,
                NewId = d.NewId,
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



        // GET: DepartmentCode/Edit/{newId}
        public async Task<IActionResult> Edit(Guid newId)
        {
            var departmentCode = await _context.DepartmentCode.FirstOrDefaultAsync(d => d.NewId == newId);

            if (departmentCode == null)
            {
                return NotFound();
            }

            var departmentCodeDTO = new DepartmentCodeDTO
            {
                Id = departmentCode.Id,
                NewId = departmentCode.NewId,
                Name = departmentCode.Name,
                Code = departmentCode.Code,
                Description = departmentCode.Description,
                IsActive = departmentCode.IsActive
            };

            return View(departmentCodeDTO);
        }

        // POST: DepartmentCode/Edit/{newId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid newId, DepartmentCodeDTO departmentCodeDTO)
        {
            var departmentCode = await _context.DepartmentCode
                .FirstOrDefaultAsync(d => d.NewId == newId);

            if (departmentCode == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    departmentCode.Name = departmentCodeDTO.Name;
                    departmentCode.Code = departmentCodeDTO.Code;
                    departmentCode.Description = departmentCodeDTO.Description;
                    departmentCode.IsActive = departmentCodeDTO.IsActive;
                    departmentCode.UpdatedBy = "AdminEdit";
                    departmentCode.UpdatedDate = DateTime.Now;

                    _context.Update(departmentCode);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index), new { success = true });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.DepartmentCode.Any(e => e.NewId == newId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(departmentCodeDTO);
        }



        // POST: DepartmentCode/CheckDuplicate
        [HttpPost]
        public JsonResult CheckDuplicate(string name, string code, Guid? newId = null)
        {
            // Pengecekan duplikat nama, abaikan record yang sedang diedit (newId)
            bool nameExists = _context.DepartmentCode
                .Any(d => d.Name == name && d.NewId != newId);

            // Pengecekan duplikat kode, abaikan record yang sedang diedit (newId)
            bool codeExists = _context.DepartmentCode
                .Any(d => d.Code == code && d.NewId != newId);

            // Return hasil pengecekan dalam bentuk JSON
            return Json(new { nameExists = nameExists, codeExists = codeExists });
        }

    }
}
