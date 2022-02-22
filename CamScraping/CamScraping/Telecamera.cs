using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamScraping
{
    public class Telecamera
    {

        public int id { get; set; }
        public string nome { get; set; }
        public string link { get; set; }

        /// <summary>
        /// Costruttore
        /// </summary>
        /// <param name="nome">Nome della telecamera</param>
        /// <param name="link">Link della telecamera</param>
        public Telecamera(string nome, string link)
        {
            this.nome = nome;
            this.link = link;

        }
    }
}
