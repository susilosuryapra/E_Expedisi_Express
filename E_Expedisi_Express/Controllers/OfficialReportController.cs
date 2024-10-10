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
        public async Task<IActionResult> Create(OfficialReportDTO reportDTO)
        {
            if (ModelState.IsValid)
            {
                var report = new OfficialReport
                {
                    NewId = Guid.NewGuid(),
                    ReportTitle = reportDTO.ReportTitle,
                    GiverName = reportDTO.GiverName,
                    ReceiverName = reportDTO.ReceiverName,
                    CreatedBy = "system",
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reportDTO);
        }

        // UPDATE (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var report = await _context.OfficialReports.FindAsync(id);
            if (report == null)
                return NotFound();

            var reportDTO = new OfficialReportDTO
            {
                Id = report.Id,
                ReportTitle = report.ReportTitle,
                GiverName = report.GiverName,
                ReceiverName = report.ReceiverName,
                IsActive = report.IsActive
            };

            return View(reportDTO);
        }

        // UPDATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OfficialReportDTO reportDTO)
        {
            if (id != reportDTO.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var report = await _context.OfficialReports.FindAsync(id);
                if (report == null)
                    return NotFound();

                report.ReportTitle = reportDTO.ReportTitle;
                report.GiverName = reportDTO.GiverName;
                report.ReceiverName = reportDTO.ReceiverName;
                report.IsActive = reportDTO.IsActive;
                report.UpdatedBy = "system"; // Replace with actual user if needed
                report.UpdatedDate = DateTime.Now;

                _context.Update(report);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(reportDTO);
        }
    }
}
