using Api_Pcto.Data;
using Api_Pcto.Models.DTOS.Responses;
using Api_Pcto.Models.Modelli;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Pcto.Models.Servizi
{
    public class UserManager : IUserManager
    {

        private readonly MyTokenDbContext _context;

        public UserManager(MyTokenDbContext context)
        {
            this._context = context;
        }

        public async Task<UserTokenResponse> AddUserToken(UserToken req)
        {
            try
            {
                var result = await _context.eletokens.AddAsync(req);
                await _context.SaveChangesAsync();
                return new UserTokenResponse()
                {
                    Token = req.Token,
                    CreationTime = req.CreationTime,
                    Username = req.Name,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new UserTokenResponse()
                {
                    Token = null,
                    CreationTime = default(DateTime),
                    Username = null,
                    Errors = new List<string>() { ex.Message }
                };
            }
        }
        public async Task<UserTokenResponse> GetUserToken(string Username)
        {
            var result = await _context.eletokens.FirstOrDefaultAsync(x => x.Name == Username);
            if (result != null)
                return new UserTokenResponse()
                {
                    Token = result.Token,
                    CreationTime = result.CreationTime,
                    Username = result.Name,
                    Errors = null
                };

            return new UserTokenResponse()
            {
                Token = null,
                CreationTime = default(DateTime),
                Username = null,
                Errors = new List<string>() { "Not Found" }
            };
        }
    }
}
