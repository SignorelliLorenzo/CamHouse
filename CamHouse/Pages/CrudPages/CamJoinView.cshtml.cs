using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Telecamere_Library;
using CamHouse.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace CamHouse.Pages.CrudPages
{
    public class CamJoinViewModel : PageModel
    {

        public IConfiguration Configuration { get; }
        private readonly AppDbContext _context;

        public CamJoinViewModel(AppDbContext context, IConfiguration configuration)
        {
            this._context = context;
        }

        public void OnGet()
        {
        }



    }
}
