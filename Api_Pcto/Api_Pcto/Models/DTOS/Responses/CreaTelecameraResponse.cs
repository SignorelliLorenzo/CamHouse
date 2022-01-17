using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Pcto.Models.DTOS.Responses
{
    public class CreaTelecameraResponse
    {
        public bool Success { get; set; }

        public Telecamera Created_telecamera { get; set; }

        public List<string> Errors { get; set; }


    }
}
