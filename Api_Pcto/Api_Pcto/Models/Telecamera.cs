using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
//https://newswwc.com/technology/dotnet-technologies/using-dapper-with-asp-net-core-web-api-code-maze/
namespace Api_Pcto
{
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

        public Telecamera(int id,string nome, string link, int num_like, int num_salvati)
        {
            this.id = id;
            this.nome = nome;
            this.link = link;
            this.num_like = num_like;
            this.num_salvati = num_salvati;
        }
    }

    public class Telecamera_Data : Telecamera
    {
        [Required]
        public DateTime data_creazione { get; set; }

        public Telecamera_Data(int id, string nome, string link, int num_like, int num_salvati) : base(id, nome, link, num_like, num_salvati)
        {
            data_creazione = DateTime.Now;
        }
    }
}
