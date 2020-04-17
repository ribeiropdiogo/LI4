using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGR.Models
{
    public partial class Reserva
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd H:mm:ss}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Por favor forneça a data e hora")]
        [Display(Name = "Data e Hora")]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "Por favor forneça o nome do clinte")]
        [Display(Name = "Nome Cliente")]
        public string NomeCliente { get; set; }

        [MinLength(9)]
        [MaxLength(9)]
        [Required(ErrorMessage = "Por favor forneça o NIF do clinte")]
        [Display(Name = "NIF Cliente")]
        public string NifCliente { get; set; }


        [Required(ErrorMessage = "Por favor forneça o ID do gerente")]
        [Display(Name = "Id Gerente")]
        public int IdGerente { get; set; }

        public virtual Gerente IdGerenteNavigation { get; set; }
    }
}
