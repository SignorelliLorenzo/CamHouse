using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Telecamere_Library;
using Api_Telecamere_Library.Models.DTOS.Responses;
using CamHouse.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace CamHouse.Pages.CrudPages
{
    public class CamJoinViewModel : PageModel
    {

        private IConfiguration Configuration { get; }
        private readonly AppDbContext _context;


        public Telecamera_Data telecamera {get;set;}

        [BindProperty]
        public string link { get; set; }
        [BindProperty]
        public int like { get; set; }
        [BindProperty]
        public int favorite { get; set; }

        public CamJoinViewModel(AppDbContext context, IConfiguration configuration)
        {
            this._context = context;
            Configuration = configuration;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                GetTelecameraPerIdResponse risposta= null;

                try
                {
                    
                    risposta = new GetTelecameraPerIdResponse();
                    risposta = MyApiService.GetByIdAsync((int)id, Configuration.GetSection("token").Value).Result;

                    like = risposta.Found_telecamera.num_like;
                    favorite = risposta.Found_telecamera.num_salvati;

                }
                catch (Exception ex)
                {

                    return RedirectToPage("/Error");
                }
               
                if (risposta.Success)
                {
                    telecamera = risposta.Found_telecamera;
                    telecamera.link = telecamera.link.Replace("?", "%3F").Replace("/","%2F");
                    link = risposta.Found_telecamera.link;
                    return Page();
                }
                else
                {
                    return NotFound();
                }

            }
        }
        public async Task<IActionResult> OnPost(string Request)
        {

            return Page();
        }



    }
}
