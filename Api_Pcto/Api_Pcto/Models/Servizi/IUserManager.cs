using Api_Pcto.Models.DTOS.Requests;
using Api_Pcto.Models.DTOS.Responses;
using Api_Pcto.Models.Modelli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Pcto.Models.Servizi
{
    public interface IUserManager
    {
        public Task<UserTokenResponse> AddUserToken(UserTokenRequest req);
        public Task<UserTokenResponse> GetUserToken(string Username);

        public Task<UserTokenResponse> DeleteUserToken(string Username);
    }
}
