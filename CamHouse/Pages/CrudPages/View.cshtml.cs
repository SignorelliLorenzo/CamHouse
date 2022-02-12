using CamHouse.Data;
using CamHouse.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamHouse.Pages
{

    [Authorize]

    public class ViewModel : PageModel
    {

        private readonly AppDbContext _context;
        public ViewModel(AppDbContext context)
        {
            this._context = context;
            elecamere = _context.CameraAppDb.ToList();
        }
        [BindProperty]
        public Camera camera { get; set; }
        [BindProperty]
        public IList<Camera> elecamere { get; set; }

        public void OnGet(string searchString, string Ordina)
        {
            
        }
      
    }
}
