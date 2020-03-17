using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGR.Models
{
    public class Gerente
    {
        public int id { get; set;  }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Por favor forneca um email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Por favor forneca uma password")]
        public string password { get; set; }
    }

}
