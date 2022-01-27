using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Pcto.Models.Modelli
{
    public class UserToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }
    }
}
