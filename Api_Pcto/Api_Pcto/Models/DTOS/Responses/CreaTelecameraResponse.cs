using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Pcto.Models.DTOS.Responses
{
    /// <summary>
    /// Oggetto DTO, viene restutuito all'utente che ha tentato di effettuare la creazione di una telecamera, contiene i dettagli dell'operazione svolta.
    /// </summary>
    public class CreaTelecameraResponse
    {
        /// <summary>
        /// Bool che ha valore true se l'operazione è andata a buon fine, false se non è andata a buon fine.
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Oggetto che rappresenta la telecamera appena creata, null se l'operazione non è andata a buon fine.
        /// </summary>
        public Telecamera_Data Created_telecamera { get; set; }
        /// <summary>
        /// Lista di errori dell'operazione effettuata, null se l'operazione è andata a buon fine.
        /// </summary>

        public List<string> Errors { get; set; }


    }
}
