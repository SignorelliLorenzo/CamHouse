using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
//https://newswwc.com/technology/dotnet-technologies/using-dapper-with-asp-net-core-web-api-code-maze/
namespace Api_Telecamere_Library
{
    /// <summary>
    /// Classe base di Telecamera_Data.
    /// </summary>
    public class Telecamera
    {

        [Key]
        public int id { get; set; }
        [Required(ErrorMessage ="Il link è un campo necessario")]
        public string nome { get; set; }
        [Required(ErrorMessage = "Il link è un campo necessario")]
        public string link { get; set; }
        [Required(ErrorMessage = "Il link è un campo necessario"), Range(0, int.MaxValue, ErrorMessage = "Il numero dei like deve essere >= 0")]
        public int num_like { get; set; }
        [Required(ErrorMessage = "Il link è un campo necessario"), Range(0, int.MaxValue, ErrorMessage = "Il numero dei salvati deve essere >= 0")]
        public int num_salvati { get; set; }

        /// <summary>
        /// Costruttore
        /// </summary>
        /// <param name="nome">Nome della telecamera</param>
        /// <param name="link">Link della telecamera</param>
        /// <param name="num_like">Numero dei mi piace della telecamera</param>
        /// <param name="num_salvati">Numero delle persone che hanno aggiunta la telecamera ai salvati</param>
        public Telecamera(string nome, string link, int num_like, int num_salvati)
        {
            this.nome = nome;
            this.link = link;
            this.num_like = num_like;
            this.num_salvati = num_salvati;
        }
        public Telecamera() { }

    }



    /// <summary>
    /// Derivata della classe Telecamera, in aggiunta è presente la data di creazione dell' oggetto tramite una property.
    /// Il database contiene questo modello.
    /// </summary>
    public class Telecamera_Data : Telecamera
    {
        /// <summary>
        /// Data di creazione, è inserita quella di quando è stato richiamato il costruttore.
        /// </summary>
        [Required]
        public DateTime data_creazione { get; set; }
        /// <summary>
        /// Costruttore
        /// </summary>
        /// <param name="nome">Nome della telecamera</param>
        /// <param name="link">Link della telecamera</param>
        /// <param name="num_like">Numero dei mi piace della telecamera</param>
        /// <param name="num_salvati">Numero delle persone che hanno aggiunto la telecamera ai salvati</param>
        public Telecamera_Data(string nome, string link, int num_like, int num_salvati) : base(nome, link, num_like, num_salvati)
        {
            data_creazione = DateTime.Now;
        }
        public Telecamera_Data(){ }
    }
}
