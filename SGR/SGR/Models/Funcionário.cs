using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGR.Models
{
    public class Funcionário
    {
        public int id { get; set;  }

        [Required(ErrorMessage = "Por favor forneca o nome")]
        public string Nome { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Por favor forneça a data de nascimento")]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Por favor forneca um email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor forneca um cargo")]
        public string Cargo { get; set; }
    }

}
