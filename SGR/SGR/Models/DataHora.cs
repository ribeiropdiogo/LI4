using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGR.Models
{
    public partial class DataHora
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd H:mm:ss}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Por favor forneça a data e hora")]
        [Display(Name = "Data e Hora Início")]
        public DateTime Inicio { get; set; }

        [Required(ErrorMessage = "Por favor forneça o Horário")]
        [Display(Name = "Horário")]
        public int IdHorario { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd H:mm:ss}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Por favor forneça a data e hora")]
        [Display(Name = "Data e Hora Fim")]
        public DateTime Fim { get; set; }

        public virtual Horario IdHorarioNavigation { get; set; }
    }
}
