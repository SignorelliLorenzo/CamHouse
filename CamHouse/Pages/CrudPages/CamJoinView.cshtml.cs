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
             

        public CamJoinViewModel(AppDbContext context, IConfiguration configuration)
        {
            this._context = context;
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

                //try
                //{
                //    GetTelecameraPerIdResponse risposta2= new GetTelecameraPerIdResponse();
                //    risposta2 =  MyApiService.GetByIdAsync((int)id, Configuration.GetSection("token").Value).Result; 
                //}
                //catch 
                

                    risposta = new GetTelecameraPerIdResponse()
                    {
                    Success = true, Found_telecamera = new Telecamera_Data()
                        {
                            id = (int)id, link = "http://79.26.47.176:60001/cgi-bin/snapshot.cgi?chn=0&u=admin&p=&q=0&", nome = "Telecamera1", data_creazione = default, num_like = 0, num_salvati = 0
                        }
                    };
               
                if (risposta.Success)
                {
                    telecamera = risposta.Found_telecamera;
                    telecamera.link = telecamera.link.Replace("?","%3F").Replace("/","%2F");
                    return Page();
                }
                else
                {
                    return NotFound();
                }

            }
        }



    }
}
