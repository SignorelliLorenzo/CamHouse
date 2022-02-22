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
            MyApiService.url = "https://localhost:44302/";
            try
            {
                CompleteList = new List<Telecamera_Data>();

                CompleteList = MyApiService.GetAll(Configuration.GetSection("token").Value).Result;
            }
            catch (Exception ex)
            {
                return RedirectToPage("/Error");
            }

            EleView = GetPageView(CompleteList, pageNumber, elementnumber);
            return Page();
        }


        public async Task<IActionResult> OnPost(string SearchString)
        {

            if (String.IsNullOrEmpty(SearchString))
            {
                try
                {
                    CompleteList = new List<Telecamera_Data>();

                    CompleteList = MyApiService.GetAll(Configuration.GetSection("token").Value).Result;
                }
                catch
                {
                    return RedirectToPage("/Error");

                }
            }
            else
            {

                try
                {

                    var result = MyApiService.GetByNameAsync(SearchString,Configuration.GetSection("token").Value).Result;
                    if(result.Success)
                    {
                        CompleteList = result.Found_telecameras;
                    }
                    else
                    {
                        CompleteList = new List<Telecamera_Data>();
                    }

                }
                catch
                {
                    return RedirectToPage("/Error");


                }

            }
            EleView = GetPageView(CompleteList, pageNumber, elementnumber);
            return Page();
        }

    }
}
