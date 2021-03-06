using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Pcto.Models.DTOS.Responses
{
    /// <summary>
    /// Oggetto DTO, viene restutuito all'utente che ha tentato di effettuare la ricerca di una telecamera, contiene i dettagli dell'operazione svolta.
    /// </summary>
    public class GetTelecameraPerNomeResponse
    {

        /// <summary>
        /// Bool che ha valore true se l'operazione è andata a buon fine, false se non è andata a buon fine.
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Lista di telecamere corrispondenti al nome, null se l'operazione non è andata a buon fine.
        /// </summary>
        public List<Telecamera_Data> Found_telecameras { get; set; }
        /// <summary>
        /// Lista di errori dell'operazione effettuata, null se l'operazione è andata a buon fine.
        /// </summary>
        public List<string> Errors { get; set; }

    }
}
