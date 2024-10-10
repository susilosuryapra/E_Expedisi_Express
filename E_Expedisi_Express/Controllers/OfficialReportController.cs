using Microsoft.AspNetCore.Mvc;
using E_Expedisi_Express.Data;
using E_Expedisi_Express.DTO;
using E_Expedisi_Express.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace E_Expedisi_Express.Controllers
{
    public class OfficialReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OfficialReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // READ (Index)
        public async Task<IActionResult> Index()
        {
            var reports = await _context.OfficialReports
                .Select(r => new OfficialReportDTO
                {
                    NewId = r.NewId,
                    ReportTitle = r.ReportTitle,
                    GiverName = r.GiverName,
                    ReceiverName = r.ReceiverName,
                    CreatedDate = r.CreatedDate ?? DateTime.Now,
                    IsActive = r.IsActive
                }).ToListAsync();

            return View(reports);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfficialReportDTO reportDTO, string submitAction)
        {
            if (ModelState.IsValid)
            {
                // Tentukan apakah report disimpan sebagai Draft atau Published
                bool isPublished = (submitAction == "publish");

                var report = new OfficialReport
                {
                    NewId = Guid.NewGuid().ToString(),
                    ReportTitle = reportDTO.ReportTitle,
                    GiverName = reportDTO.GiverName,
                    GiverCompanyName = reportDTO.GiverCompanyName,
                    GiverDepartmentName = reportDTO.GiverDepartmentName,
                    GiverAddress = reportDTO.GiverAddress,
                    MainDescription = reportDTO.MainDescription,
                    ReceiverName = reportDTO.ReceiverName,
                    ReceiverCompanyName = reportDTO.ReceiverCompanyName,
                    ReceiverDepartmentName = reportDTO.ReceiverDepartmentName,
                    ReceiverAddress = reportDTO.ReceiverAddress,
                    CreatedBy = "system", // Replace with actual user if needed
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    IsPublished = isPublished, // True for Published, False for Draft
                    ReportNumber = "REP-2024-004",
                    CompCode = "01",
                    DivCode = "KF.036",
                    DepartmentId = reportDTO.DepartmentId,
                    DocumentTypeId = reportDTO.DocumentTypeId
                };

                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reportDTO);
        }


        // UPDATE (GET)
        public async Task<IActionResult> Edit(string? newId)
        {
            if (string.IsNullOrEmpty(newId))
                return NotFound();

            // Cari report berdasarkan NewId
            var report = await _context.OfficialReports
                .FirstOrDefaultAsync(r => r.NewId == newId);

            if (report == null)
                return NotFound();

            var reportDTO = new OfficialReportDTO
            {
                Id = report.Id,
                NewId = report.NewId,
                ReportTitle = report.ReportTitle,
                ReportNumber = report.ReportNumber,
                DocumentTypeId = report.DocumentTypeId,
                DepartmentId = report.DepartmentId,
                GiverName = report.GiverName,
                GiverCompanyName = report.GiverCompanyName,
                GiverDepartmentName = report.GiverDepartmentName,
                GiverAddress = report.GiverAddress,
                MainDescription = report.MainDescription,
                ReceiverName = report.ReceiverName,
                ReceiverCompanyName = report.ReceiverCompanyName,
                ReceiverDepartmentName = report.ReceiverDepartmentName,
                ReceiverAddress = report.ReceiverAddress,
                CompCode = report.CompCode,
                DivCode = report.DivCode,
                IsActive = report.IsActive,
                IsPublished = report.IsPublished, // Menambahkan properti untuk mengecek status Published
                CreatedBy = report.CreatedBy,
                CreatedDate = (DateTime)report.CreatedDate
            };

            return View(reportDTO);
        }


        // UPDATE (POST)
        // UPDATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string newId, OfficialReportDTO reportDTO, string submitAction)
        {
            if (newId != reportDTO.NewId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var report = await _context.OfficialReports
                    .FirstOrDefaultAsync(r => r.NewId == newId);

                if (report == null)
                    return NotFound();

                // Update properties from DTO
                report.ReportTitle = reportDTO.ReportTitle;
                report.DocumentTypeId = reportDTO.DocumentTypeId; // Tambahkan ini
                report.DepartmentId = reportDTO.DepartmentId; // Tambahkan ini
                report.GiverName = reportDTO.GiverName;
                report.GiverCompanyName = reportDTO.GiverCompanyName;
                report.GiverDepartmentName = reportDTO.GiverDepartmentName; // Tambahkan ini
                report.GiverAddress = reportDTO.GiverAddress;
                report.MainDescription = reportDTO.MainDescription;
                report.ReceiverName = reportDTO.ReceiverName;
                report.ReceiverCompanyName = reportDTO.ReceiverCompanyName;
                report.ReceiverDepartmentName = reportDTO.ReceiverDepartmentName; // Tambahkan ini
                report.ReceiverAddress = reportDTO.ReceiverAddress;
                report.IsActive = reportDTO.IsActive;

                // Tentukan apakah report disimpan sebagai Draft atau Published
                bool isPublished = (submitAction == "publish");
                report.IsPublished = isPublished; // Menentukan status publish

                report.UpdatedBy = "system"; // Ganti dengan user yang sebenarnya jika diperlukan
                report.UpdatedDate = DateTime.Now;

                _context.Update(report);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(reportDTO);
        }
    }
}
