using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Pcto.Models.DTOS.Responses
{
    public class GetTelecameraPerIdResponse
    {
        public bool Success { get; set; }

        public Telecamera_Data Found_telecamera { get; set; }

        public List<string> Errors { get; set; }
    }
}
