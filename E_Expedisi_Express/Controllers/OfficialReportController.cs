using E_Expedisi_Express.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace E_Expedisi_Express.Controllers
{
    public class OfficialReportController : Controller
    {
        // Dummy data, replace with database call if needed
        private List<OfficialReport> GetOfficialReport()
        {
            return new List<OfficialReport>
            {
                new OfficialReport { Id = 1, Title = "Darkhold", Giver = "Agatha Harkness", Receiver = "Wanda Maximoff", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 2, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 3, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Draft" },
                new OfficialReport { Id = 4, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 5, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 6, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 7, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 8, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 9, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 10, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 11, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 12, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 13, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 14, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 15, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 16, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 17, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 18, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 19, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 20, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 21, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 22, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
                new OfficialReport { Id = 23, Title = "Dummy", Giver = "Dummious Givery", Receiver = "Rei Chi-Verdammy", Date = DateTime.Now, Status = "Publish" },
            };
        }

        // Index method
        public IActionResult Index()
        {
            var officialReport = GetOfficialReport();
            return View(officialReport);
        }

        // GET: OfficialReport/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OfficialReport/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OfficialReport officialReport)
        {
            if (ModelState.IsValid)
            {
                // Save the new record to the database or dummy data logic
                return RedirectToAction(nameof(Index));
            }
            return View(officialReport);
        }

        // GET: OfficialReport/Edit/{id}
        public IActionResult Edit(int id)
        {
            var report = GetOfficialReport().FirstOrDefault(r => r.Id == id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        // POST: OfficialReport/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, OfficialReport officialReport)
        {
            if (ModelState.IsValid)
            {
                // Update the record in the database or dummy data logic
                return RedirectToAction(nameof(Index));
            }
            return View(officialReport);
        }
    }
}
