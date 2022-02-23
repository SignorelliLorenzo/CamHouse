using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api_Telecamere_Library;
using Api_Telecamere_Library.Models.DTOS.Requests;
using Api_Telecamere_Library.Models.DTOS.Responses;
using CamHouse.Data;
using CamHouse.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace CamHouse.Pages.CrudPages
{
    [Authorize]
    public class CamJoinViewModel : PageModel
    {
        
        private IConfiguration Configuration { get; }
        private readonly AppDbContext _context;
        private readonly SignInManager<UserData> _signInManager;
        private readonly UserManager<UserData> _userManager;

        [BindProperty]
        public Telecamera_Data telecamera {get;set;}

        [BindProperty]
        public bool checkLike { get; set; }
        [BindProperty]
        public bool checkSalvati { get; set; }

        [BindProperty]
        public string link { get; set; }
        [BindProperty]
        public int like { get; set; }
        [BindProperty]
        public int favorite { get; set; }

        public CamJoinViewModel(AppDbContext context, IConfiguration configuration, SignInManager<UserData> signInManager, UserManager<UserData> userManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
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

                var a = _userManager.GetUserAsync(User).Result;

                if (_userManager.GetUserAsync(User).Result.Liked != null)
                {

                    if (_userManager.GetUserAsync(User).Result.Liked.Split("|").ToList().Contains(id.ToString()))
                    {
                        checkLike = true;
                    }
                    else
                    {
                        checkLike = false;
                    }

                }
                else
                {
                    checkLike = false;
                }

                if (_userManager.GetUserAsync(User).Result.Favorites != null)
                {
                    if (_userManager.GetUserAsync(User).Result.Favorites.Split("|").ToList().Contains(id.ToString()))
                    {
                        checkSalvati = true;
                    }
                    else
                    {
                        checkSalvati = false;
                    }
                }
                else
                {
                    checkSalvati = false;
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

        public async Task<IActionResult> OnPost(string Like, string Favorite)
        {
                try
                {
                    if(Like == "Like")
                    {

                        if (checkLike)
                        {
                            this.telecamera.num_like--;
                        }
                        else
                        {
                            this.telecamera.num_like++;
                        }

                        ModificaTelecameraRequest richiesta = new ModificaTelecameraRequest()
                        {
                            telecamera = this.telecamera
                        };
                        var risposta = new ModificaTelecameraResponse();
                        risposta = MyApiService.PutAsync(richiesta, Configuration.GetSection("token").Value).Result;
                        this.link = risposta.Edited_telecamera.link;

                        checkLike = !checkLike;

                        if (checkLike)
                        {
                            _userManager.GetUserAsync(User).Result.Liked = _userManager.GetUserAsync(User).Result.Liked+$"|{telecamera.id}";
                        }
                        else
                        {
                            _userManager.GetUserAsync(User).Result.Liked = _userManager.GetUserAsync(User).Result.Liked.Replace($"|{telecamera.id}", "");
                           //_userManager.GetUserAsync(User).Result.Liked = "";
                        }
                    }
                    else if(Favorite == "Favorite")
                    {

                        if (checkSalvati)
                        {
                            this.telecamera.num_salvati--;
                        }
                        else
                        {
                            this.telecamera.num_salvati++;
                        }

                        ModificaTelecameraRequest richiesta = new ModificaTelecameraRequest()
                        {
                            telecamera = this.telecamera
                        };

                        var risposta = new ModificaTelecameraResponse();
                        risposta = MyApiService.PutAsync(richiesta, Configuration.GetSection("token").Value).Result;
                        this.link = risposta.Edited_telecamera.link;

                        checkSalvati = !checkSalvati;

                        if (checkSalvati)
                        {
                            _userManager.GetUserAsync(User).Result.Favorites = _userManager.GetUserAsync(User).Result.Favorites + $"|{telecamera.id}";
                        }
                        else
                        {
                            _userManager.GetUserAsync(User).Result.Favorites = _userManager.GetUserAsync(User).Result.Favorites.Replace($"|{telecamera.id}", "");
                    }
                }

                    await _userManager.UpdateAsync(_userManager.GetUserAsync(User).Result);

                }
                catch (Exception ex)
                {

                    return RedirectToPage("/Error");
                }
                return Page();

        }
    }
}
