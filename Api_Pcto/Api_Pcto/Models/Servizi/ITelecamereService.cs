using Api_Pcto.Models.DTOS.Requests;
using Api_Pcto.Models.DTOS.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Pcto.Models
{
    public interface ITelecamereService
    {
        public Task<IEnumerable<Telecamera_Data>> GetAll();

        public Task<GetTelecameraPerIdResponse> GetById(int id);

        public Task<CreaTelecameraResponse> Post(CreaTelecameraRequest request);

        public Task<ModificaTelecameraResponse> Put(ModificaTelecameraRequest request);

        public Task<EliminaTelecameraResponse> Delete(int id);
    }
}
