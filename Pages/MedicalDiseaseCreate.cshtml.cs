using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmileMate.Common;
using SmileMate.Common.Entities;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace SmileMate.Pages
{
    public class MedicalDiseaseCreateModel : PageModel
    {
        private readonly SmileMateContext _context;
        private readonly IWebHostEnvironment _environment;

        public MedicalDiseaseCreateModel(SmileMateContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Reception Reception { get; set; }
        [BindProperty]
        public string RouteValue { get; set; }

        public async Task<IActionResult> OnGet(int? receptionid, string? routevalue)
        {
            if(routevalue is not null)
                RouteValue = routevalue;

            Reception = await _context.Set<Reception>()
                .Include(r => r.Client)
                .Include(r => r.MedicalHistory)
                .FirstOrDefaultAsync(r => r.Id == receptionid);

            if (Reception == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? receptionid, string diagnosis, decimal price, string comment, List<IFormFile> photos)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Reception");
            }

            var reception = await _context.Receptions
                .Include(r => r.Client)
                .Include(r => r.MedicalHistory)
                .FirstOrDefaultAsync(r => r.Id == receptionid);

            if (reception == null)
            {
                return NotFound();
            }

            var medicalHistory = new MedicalHistory
            {
                Diagnosis = diagnosis,
                Price = price,
                Comment = comment,
                PhotoCollection = new List<string>(),
                ReceptionId = reception.Id
            };

            var uploadPath = Path.Combine(_environment.WebRootPath, "MedicalHistoryPhotos");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            foreach (var photo in photos)
            {
                if (photo.Length > 0)
                {
                    var fileName = Path.GetFileName(photo.FileName);
                    var filePath = Path.Combine(uploadPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }

                    medicalHistory.PhotoCollection.Add($"MedicalHistoryPhotos/{fileName}");
                }
            }

            reception.MedicalHistory.Add(medicalHistory);
            await _context.SaveChangesAsync();

            if(RouteValue is not null)
                return RedirectToPage("/DoctorPatient");

            return RedirectToPage("/Reception");
        }
    }
}
