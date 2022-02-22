using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Api_Telecamere_Library.Telecamera;

namespace Api_Telecamere_Library.Models.DTOS.Responses
{
    /// <summary>
    /// Oggetto DTO, viene restutuito all'utente che ha tentato di effettuare l'eliminazione di una telecamera, contiene i dettagli dell'operazione svolta.
    /// </summary>
    public class EliminaTelecameraResponse
    {
        /// <summary>
        /// Bool che ha valore true se l'operazione è andata a buon fine, false se non è andata a buon fine.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Oggetto che rappresenta la telecamera appena eliminata, null se l'operazione non è andata a buon fine.
        /// </summary>
        public Telecamera_Data Deleted_telecamera { get; set; }

        /// <summary>
        /// Lista di errori dell'operazione effettuata, null se l'operazione è andata a buon fine.
        /// </summary>
        public List<string> Errors { get; set; }
    }
}
