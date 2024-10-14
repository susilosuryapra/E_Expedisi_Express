using Microsoft.AspNetCore.Mvc;
using E_Expedisi_Express.Data;
using E_Expedisi_Express.DTO;
using E_Expedisi_Express.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Borders;

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
                    IsActive = r.IsActive,
                    IsPublished = r.IsPublished
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

                TempData["SuccessMessage"] = isPublished ? "Official Report published successfully!" : "Official Report saved as draft successfully!";
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

                report.UpdatedBy = "SystemEdit"; // Ganti dengan user yang sebenarnya jika diperlukan
                report.UpdatedDate = DateTime.Now;

                _context.Update(report);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = isPublished ? "Official Report updated and published successfully!" : "Official Report updated and saved as draft successfully!";

                return RedirectToAction(nameof(Index));
            }
            foreach (var entry in ModelState)
            {
                Console.WriteLine($"{entry.Key}: {string.Join(", ", entry.Value.Errors.Select(e => e.ErrorMessage))}");
            }
            return View(reportDTO);
        }


        public async Task<IActionResult> DownloadPdf(string? newId)
        {
            if (string.IsNullOrEmpty(newId))
                return NotFound();

            // Ambil data report dari database
            var report = await _context.OfficialReports.FirstOrDefaultAsync(r => r.NewId == newId);
            if (report == null)
                return NotFound();

            // Siapkan memory stream untuk menyimpan PDF
            using (var memoryStream = new System.IO.MemoryStream())
            {
                // Buat PDF menggunakan iText7
                PdfWriter writer = new PdfWriter(memoryStream);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                // 1. Bagian Judul dan Nomor Dokumen
                document.Add(new Paragraph($"{report.ReportTitle}")
                    .SetFontSize(18)
                    .SetBold()
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                document.Add(new Paragraph($"{report.ReportNumber}")
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetMarginBottom(20)); // Menambah spasi bawah

                // 2. Bagian Teks Pemberitahuan
                document.Add(new Paragraph("Kami yang bertanda tangan dibawah ini:")
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetMarginBottom(10));

                // 3. Tabel untuk bagian Pemberi (Giver) tanpa border
                Table giverTable = new Table(UnitValue.CreatePercentArray(new float[] { 2, 0.1f, 3 })); // 2:0.1:3 ratio for name, colon, and value
                giverTable.SetWidth(UnitValue.CreatePercentValue(100)); // Tabel selebar halaman penuh
                giverTable.SetBorder(Border.NO_BORDER); // Hilangkan border pada tabel

                giverTable.AddCell(new Cell().Add(new Paragraph("Nama"))
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border
                giverTable.AddCell(new Cell().Add(new Paragraph(":"))
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border
                giverTable.AddCell(new Cell().Add(new Paragraph($"{report.GiverName}"))
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                giverTable.AddCell(new Cell().Add(new Paragraph("Department"))
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border
                giverTable.AddCell(new Cell().Add(new Paragraph(":"))
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border
                giverTable.AddCell(new Cell().Add(new Paragraph($"{report.GiverDepartmentName}"))
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                giverTable.AddCell(new Cell().Add(new Paragraph("Company"))
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border
                giverTable.AddCell(new Cell().Add(new Paragraph(":"))
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border
                giverTable.AddCell(new Cell().Add(new Paragraph($"{report.GiverCompanyName}"))
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                document.Add(giverTable);
                document.Add(new Paragraph(" ").SetMarginBottom(20)); // Menambah spasi antar elemen

                // 4. Bagian Deskripsi Pernyataan
                document.Add(new Paragraph(report.MainDescription)
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetMarginBottom(20)); // Spasi untuk deskripsi

                // 5. Bagian Teks Kepada
                document.Add(new Paragraph("Kepada:")
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetMarginBottom(10)); // Spasi bawah untuk memisahkan dengan penerima

                // 6. Tabel untuk bagian Penerima (Receiver) tanpa border
                Table receiverTable = new Table(UnitValue.CreatePercentArray(new float[] { 2, 0.1f, 3 })); // 2:0.1:3 ratio for name, colon, and value
                receiverTable.SetWidth(UnitValue.CreatePercentValue(100)); // Tabel selebar halaman penuh
                receiverTable.SetBorder(Border.NO_BORDER); // Hilangkan border pada tabel

                receiverTable.AddCell(new Cell().Add(new Paragraph("Nama"))
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border
                receiverTable.AddCell(new Cell().Add(new Paragraph(":"))
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border
                receiverTable.AddCell(new Cell().Add(new Paragraph($"{report.ReceiverName}"))
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                receiverTable.AddCell(new Cell().Add(new Paragraph("Department"))
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border
                receiverTable.AddCell(new Cell().Add(new Paragraph(":"))
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border
                receiverTable.AddCell(new Cell().Add(new Paragraph($"{report.ReceiverDepartmentName}"))
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                receiverTable.AddCell(new Cell().Add(new Paragraph("Company"))
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border
                receiverTable.AddCell(new Cell().Add(new Paragraph(":"))
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border
                receiverTable.AddCell(new Cell().Add(new Paragraph($"{report.ReceiverCompanyName}"))
                    .SetFontSize(12)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                document.Add(receiverTable);
                document.Add(new Paragraph(" ").SetMarginBottom(40)); // Spasi antara penerima dan tanda tangan

                // 7. Bagian Tanda Tangan tanpa border
                Table signatureTable = new Table(2); // Tabel untuk 2 kolom
                signatureTable.SetWidth(UnitValue.CreatePercentValue(100)); // Selebar halaman penuh

                signatureTable.AddCell(new Cell().Add(new Paragraph("Yang menyerahkan,")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("Yang menerima,")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border
                
                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border
                
                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border
                
                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border
                
                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border
                
                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border
                
                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border
                
                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border
                
                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph("")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border


                // Baris tanda tangan kosong
                signatureTable.AddCell(new Cell().Add(new Paragraph($"({report.GiverName})")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                signatureTable.AddCell(new Cell().Add(new Paragraph($"({report.ReceiverName})")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER))
                    .SetBorder(Border.NO_BORDER)); // Hilangkan border

                document.Add(signatureTable);

                // Tutup document dan PDF setelah selesai menulis
                document.Close();
                pdf.Close();
                writer.Close();

                // Ambil byte array dari MemoryStream
                var bytes = memoryStream.ToArray();

                // Kembalikan file PDF sebagai download
                return File(bytes, "application/pdf", "OfficialReport.pdf");
            }
        }

    }
}
