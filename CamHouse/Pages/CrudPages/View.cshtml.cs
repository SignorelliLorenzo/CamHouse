using CamHouse.Data;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Telecamere_Library;
using Api_Telecamere_Library.Models;
using Microsoft.Extensions.Configuration;

namespace CamHouse.Pages
{

    [Authorize]

    public class ViewModel : PageModel
    {
        public IConfiguration Configuration { get; }
        private readonly AppDbContext _context;
  
        public ViewModel(AppDbContext context, IConfiguration configuration)
        {
            this._context = context;
            Configuration = configuration;
            elecamere = _context.CameraAppDb.ToList();
            
        }
        [BindProperty]
        public Telecamera_Data camera { get; set; }
        [BindProperty]
        public IList<Telecamera_Data> elecamere { get; set; }

        public void OnGet()
        {           
            try
            {
               elecamere = MyApiService.GetAll(Configuration.GetSection("token").Value).Result;
            }
            catch
            {

            }
        }
      
    }
}
