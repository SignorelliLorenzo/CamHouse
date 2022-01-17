using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamHouse.Data;
using CamHouse.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CamHouse.Pages.CrudPages
{
    [Authorize]
    public class CreateModel : PageModel
    {

        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Camera camera { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.CameraAppDb.Add(camera);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
