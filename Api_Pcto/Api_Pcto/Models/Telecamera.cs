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
    }
}
