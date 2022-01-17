using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Pcto.Models.DTOS.Requests
{
    public class CreaTelecameraRequest
    {
        [Required]
        public Telecamera telecamera { get; set; }
    }
}
