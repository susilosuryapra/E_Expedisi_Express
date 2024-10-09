using E_Expedisi_Express.Models;
using E_Expedisi_Express.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using E_Expedisi_Express.Data;

namespace E_Expedisi_Express.Controllers
{
    public class OfficialReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OfficialReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OfficialReport
        public async Task<IActionResult> Index()
        {
            var officialReports = await _context.OfficialReport.ToListAsync();
            var officialReportDTOs = officialReports.Select(o => new OfficialReportDTO
            {
                Id = o.Id,
                NewId = o.NewId,
                Title = o.Title,
                Giver = o.Giver,
                Receiver = o.Receiver,
                Date = o.Date,
                Status = o.Status
            }).ToList();

            return View(officialReportDTOs);
        }

        // GET: OfficialReport/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OfficialReport/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfficialReportDTO officialReportDTO)
        {
            if (ModelState.IsValid)
            {
                var officialReport = new OfficialReport
                {
                    Title = officialReportDTO.Title,
                    Giver = officialReportDTO.Giver,
                    Receiver = officialReportDTO.Receiver,
                    Date = officialReportDTO.Date,
                    Status = officialReportDTO.Status,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now
                };

                _context.OfficialReport.Add(officialReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { success = true });
            }

            return View(officialReportDTO);
        }

        // GET: OfficialReport/Edit/{newId}
        public async Task<IActionResult> Edit(Guid newId)
        {
            var officialReport = await _context.OfficialReport.FirstOrDefaultAsync(o => o.NewId == newId);
            if (officialReport == null)
            {
                return NotFound();
            }

            var officialReportDTO = new OfficialReportDTO
            {
                Id = officialReport.Id,
                NewId = officialReport.NewId,
                Title = officialReport.Title,
                Giver = officialReport.Giver,
                Receiver = officialReport.Receiver,
                Date = officialReport.Date,
                Status = officialReport.Status
            };

            return View(officialReportDTO);
        }

        // POST: OfficialReport/Edit/{newId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid newId, OfficialReportDTO officialReportDTO)
        {
            var officialReport = await _context.OfficialReport.FirstOrDefaultAsync(o => o.NewId == newId);
            if (officialReport == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                officialReport.Title = officialReportDTO.Title;
                officialReport.Giver = officialReportDTO.Giver;
                officialReport.Receiver = officialReportDTO.Receiver;
                officialReport.Date = officialReportDTO.Date;
                officialReport.Status = officialReportDTO.Status;
                officialReport.UpdatedBy = "AdminEdit";
                officialReport.UpdatedDate = DateTime.Now;

                _context.Update(officialReport);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { success = true });
            }

            return View(officialReportDTO);
        }
    }
}
