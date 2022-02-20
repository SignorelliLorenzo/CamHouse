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
using CamHouse.Models.Paging;

namespace CamHouse.Pages
{

    [Authorize]

    public class ViewModel : PageModel
    {
        public IConfiguration Configuration { get; }
        private readonly AppDbContext _context;
        private readonly int elementnumber = 10;
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
        [BindProperty]
        public IList<Telecamera_Data> lista { get; set; }
        [BindProperty]
        public int? pageNumber { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (pageNumber == null)
                pageNumber = 1;
            if (elecamere.Count == 0)
            {
                try
                {
                    lista = new List<Telecamera_Data>();
                    lista = MyApiService.GetAll(Configuration.GetSection("token").Value).Result;
                }
                catch
                {
                    for (int i = 0; i < 100; i++)
                    {
                        lista.Add(new Telecamera_Data($"telecamera{i}", $"link{i}", 0, 0));
                    }

                }
                elecamere = new List<Telecamera_Data>();
               
            }
            else
            {
                elecamere.Clear();
            }
               
            int? num = pageNumber * elementnumber - elementnumber;
            int x = 0;
            while (x < 10)
            {
                elecamere.Add(lista[(int)num + x]);
                x++;
            }
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (pageNumber == null)
                pageNumber = 1;
            if (elecamere.Count == 0)
            {
                try
                {
                    lista = new List<Telecamera_Data>();
                    lista = MyApiService.GetAll(Configuration.GetSection("token").Value).Result;
                }
                catch
                {
                    for (int i = 0; i < 100; i++)
                    {
                        lista.Add(new Telecamera_Data($"telecamera{i}", $"link{i}", 0, 0));
                    }

                }
                elecamere = new List<Telecamera_Data>();

            }
            else
            {
                elecamere.Clear();
            }

            int? num = pageNumber * elementnumber - elementnumber;
            int x = 0;
            while (x < 10)
            {
                elecamere.Add(lista[(int)num + x]);
                x++;
            }
            return Page();
        }
    }
}
