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
                // Cek duplikasi nama department
                var nameExists = await _context.DepartmentCode.AnyAsync(d => d.Name == departmentCodeDTO.Name);
                var codeExists = await _context.DepartmentCode.AnyAsync(d => d.Code == departmentCodeDTO.Code);

                if (nameExists)
                {
                    ModelState.AddModelError("Name", "Department name already exists.");
                }

                if (codeExists)
                {
                    ModelState.AddModelError("Code", "Department code already exists.");
                }

                // Jika tidak ada error, lanjutkan penyimpanan ke database
                if (!nameExists && !codeExists)
                {
                    var departmentCode = new DepartmentCode
                    {
                        Name = departmentCodeDTO.Name,
                        Code = departmentCodeDTO.Code,
                        Description = departmentCodeDTO.Description,
                        IsActive = true,
                        CreatedBy = "Admin",
                        CreatedDate = DateTime.Now
                    };

                    _context.DepartmentCode.Add(departmentCode);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index), new { success = true });
                }
            }

            // Jika ada error, kembalikan ke view dan tampilkan error
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
            var departmentCode = await _context.DepartmentCode.FirstOrDefaultAsync(d => d.NewId == newId);

            if (departmentCode == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Cek apakah ada nama department yang sama, kecuali yang sedang diedit
                var nameExists = await _context.DepartmentCode
                    .AnyAsync(d => d.Name == departmentCodeDTO.Name && d.NewId != newId);

                // Cek apakah ada kode department yang sama, kecuali yang sedang diedit
                var codeExists = await _context.DepartmentCode
                    .AnyAsync(d => d.Code == departmentCodeDTO.Code && d.NewId != newId);

                if (nameExists)
                {
                    ModelState.AddModelError("Name", "Department name already exists.");
                }

                if (codeExists)
                {
                    ModelState.AddModelError("Code", "Department code already exists.");
                }

                if (!nameExists && !codeExists)
                {
                    // Update data department
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
            }

            return View(departmentCodeDTO);
        }

    }
}
