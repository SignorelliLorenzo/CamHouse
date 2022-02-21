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

namespace CamHouse.Pages.CrudPages
{

    [Authorize]

    public class ViewModel : PageModel
    {
        public IConfiguration Configuration { get; }
        private readonly AppDbContext _context;
        [BindProperty]
        public int elementnumber { get; }
        public ViewModel(AppDbContext context, IConfiguration configuration)
        {
            this._context = context;
            Configuration = configuration;
            elementnumber = int.Parse(configuration.GetSection("ItemPerpage").Value);
        }
       
        [BindProperty]
        public List<Telecamera_Data> EleView { get; set; }
        [BindProperty]
        public List<Telecamera_Data> CompleteList { get; set; }
        [BindProperty]
        public int pageNumber { get; set; }
        private List<Telecamera_Data> GetPageView(List<Telecamera_Data> CompleteList, int Page, int ItemsPerPage)
        {
            List<Telecamera_Data>  View = new List<Telecamera_Data>();
            int? num = (Page + 1) * ItemsPerPage - ItemsPerPage;
            int x = 0;

            while (x < 10 && (num + x ) < CompleteList.Count())
            {
                View.Add(CompleteList[(int)num + x]);
                x++;
            }
            return View;
        }
        public async Task<IActionResult> OnGet()
        {
                try
                {
                CompleteList = new List<Telecamera_Data>();

               CompleteList = MyApiService.GetAll(Configuration.GetSection("token").Value).Result;
                }
                catch
                {
                    //Just for testing
                    for (int i = 0; i < 25; i++)
                    {
                    CompleteList.Add(new Telecamera_Data($"telecamera{i}", $"link{i}", 0, 0));
                    }

                }

            EleView = GetPageView(CompleteList, pageNumber, elementnumber);
            return Page();
        }

        public void OnGet(string SearchString)
        {
            if (!String.IsNullOrWhiteSpace(SearchString))
            {
                var stringresult = _context.CameraAppDb.Where(m => m.nome.Contains(SearchString));
                EleView = stringresult.ToList();
            }
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                CompleteList = new List<Telecamera_Data>();

                CompleteList = MyApiService.GetAll(Configuration.GetSection("token").Value).Result;
            }
            catch
            {
                //Just for testing
                for (int i = 0; i < 25; i++)
                {
                    CompleteList.Add(new Telecamera_Data($"telecamera{i}", $"link{i}", 0, 0));
                }

            }

            EleView = GetPageView(CompleteList, pageNumber, elementnumber);
            return Page();
        }

    }
}
