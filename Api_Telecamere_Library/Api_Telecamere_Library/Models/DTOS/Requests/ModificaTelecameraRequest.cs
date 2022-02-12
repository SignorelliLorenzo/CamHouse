using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Pcto.Models.DTOS.Requests
{
    /// <summary>
    /// Oggetto DTO, Deve essere fornito dall' utente che intende effettuare la modifica di una telecamera.
    /// </summary>
    public class ModificaTelecameraRequest
    {
        [Required]
        public Telecamera telecamera { get; set; }
    }
}
