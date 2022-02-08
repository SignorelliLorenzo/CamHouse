using Api_Pcto.Data;
using Api_Pcto.Models.DTOS.Requests;
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

        public async Task<UserTokenResponse> AddUserToken(UserTokenRequest req)
        {
            try
            {
                UserToken usertoken = new UserToken()
                {
                    Name = req.Name,
                    Token = req.Token,
                    CreationTime = DateTime.Now
                };
                var result = await _context.eletokens.AddAsync(usertoken);
                await _context.SaveChangesAsync();
                return new UserTokenResponse()
                {
                    Token = usertoken.Token,
                    CreationTime = usertoken.CreationTime,
                    Username = usertoken.Name,
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

        public async Task<UserTokenResponse> DeleteUserToken(string Username)
        {
            var result = await _context.eletokens.FirstOrDefaultAsync(x => x.Name == Username);           
            if (result != null)
            {
                _context.eletokens.Remove(result);
                await _context.SaveChangesAsync();
                return new UserTokenResponse()
                {
                    Token = result.Token,
                    CreationTime = result.CreationTime,
                    Username = result.Name,
                    Errors = null
                };
            }
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
