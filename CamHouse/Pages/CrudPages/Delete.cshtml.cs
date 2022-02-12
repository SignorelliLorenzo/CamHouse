using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CamHouse.Data;
using CamHouse.Model;
using Microsoft.AspNetCore.Authorization;

namespace CamHouse.Pages.CrudPages
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;
        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Camera camera { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            camera = await _context.CameraAppDb.FirstOrDefaultAsync(m => m.id == id);
            if (camera == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!_context.CameraAppDb.Any(x => x.id == camera.id))
            {
                return NotFound();
            }

            var prod = await _context.CameraAppDb.Where(x => x.id == camera.id).FirstOrDefaultAsync();
            _context.CameraAppDb.Remove(prod);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
