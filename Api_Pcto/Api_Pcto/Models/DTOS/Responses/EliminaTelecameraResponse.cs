using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Pcto.Models.DTOS.Responses
{
    public class EliminaTelecameraResponse
    {
        public bool Success { get; set; }

        public Telecamera_Data Deleted_telecamera { get; set; }

        public List<string> Errors { get; set; }
    }
}
