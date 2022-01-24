using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Pcto.Models.DTOS.Requests
{
    /// <summary>
    /// Oggetto DTO, Deve essere fornito dall' utente che intende effettuare la creazione di una telecamera.
    /// </summary>
    public class CreaTelecameraRequest
    {
        [Required(ErrorMessage = "Il nome è un campo necessario")]
        public string nome { get; set; }
        [Required(ErrorMessage = "Il link è un campo necessario")]
        public string link { get; set; }
        [Required(ErrorMessage = "Il numero dei like è un campo necessario"), Range(0, int.MaxValue, ErrorMessage = "Il numero dei like deve essere >= 0")]
        public int num_like { get; set; }
        [Required(ErrorMessage = "Il numero dei salvati è un campo necessario"), Range(0, int.MaxValue, ErrorMessage = "Il numero dei salvati deve essere >= 0")]
        public int num_salvati { get; set; }
    }
}
