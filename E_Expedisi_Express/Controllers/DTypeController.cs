using E_Expedisi_Express.Data;
using E_Expedisi_Express.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace E_Expedisi_Express.Controllers
{
    public class DTypeController : Controller
    {
        private readonly ExpedisiDbContext _context;

        public DTypeController(ExpedisiDbContext context)
        {
            _context = context;
        }

        public IActionResult Doctype()
        {
            var documentTypes = _context.DocumentType.ToList();
            return View(documentTypes);
        }

        [HttpPost]
        public JsonResult AddDocumentType([FromBody] DocumentType model)
        {
            if (ModelState.IsValid)
            {
                // Set CreatedAt and UpdatedAt timestamps
                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;

                _context.DocumentType.Add(model);
                _context.SaveChanges();

                // Return the new document type's ID for use in the frontend
                return Json(new { success = true, newId = model.Id });
            }
            return Json(new { success = false, message = "Model tidak valid." });
        }

        [HttpGet]
        public JsonResult GetDocumentTypes()
        {
            var documentTypes = _context.DocumentType.ToList();
            return Json(documentTypes);
        }
        [HttpGet]
        public JsonResult GetDocumentType(int id)
        {
            var documentType = _context.DocumentType.FirstOrDefault(dt => dt.Id == id);
            if (documentType != null)
            {
                return Json(documentType);
            }
            return Json(new { success = false, message = "Document Type tidak ditemukan." });
        }
    }
}
