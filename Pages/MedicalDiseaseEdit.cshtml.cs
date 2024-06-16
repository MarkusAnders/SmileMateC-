using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SmileMate.Common;
using SmileMate.Common.Entities;

namespace SmileMate.Pages
{
    public class MedicalDiseaseEditModel : PageModel
    {
        private readonly SmileMateContext _context;
        private readonly IWebHostEnvironment _environment;

        public MedicalDiseaseEditModel(SmileMateContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public MedicalHistory MedicalHistory { get; set; }

        public async Task<IActionResult> OnGet(int? mhid)
        {
            MedicalHistory = await _context.Set<MedicalHistory>()
                .Include(mh => mh.Reception).ThenInclude(r => r.Client)
                .Include(mh => mh.Reception).ThenInclude(r => r.Doctor)
                .FirstOrDefaultAsync(r => r.Id == mhid);


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? mhid, string diagnosis, decimal price, string comment, List<IFormFile> photos)
        {
            if (!ModelState.IsValid)
            {
                return NotFound("Модель не валид");
                /*return RedirectToPage("/MedicalDisease", new { mhid = mhid });*/
            }

            var mhToEdit = await _context.Set<MedicalHistory>()
                .Include(mh => mh.Reception).ThenInclude(r => r.Client)
                .Include(mh => mh.Reception).ThenInclude(r => r.Doctor)
                .FirstOrDefaultAsync(r => r.Id == mhid);

            if (mhToEdit == null)
            {
                return NotFound();
            }

            if (diagnosis is not null)
                mhToEdit.Diagnosis = diagnosis;

            if (price != 0)
                mhToEdit.Price = price;

            if (comment is not null)
                mhToEdit.Comment = comment;

            if(photos.Count() != 0)
            {
                mhToEdit.PhotoCollection = new List<string>();

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

                        mhToEdit.PhotoCollection.Add($"MedicalHistoryPhotos/{fileName}");
                    }
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("/MedicalDisease", new { mhid = mhid });
        }

    }
}
