using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGR.Models
{
    public partial class Horario
    {
        public Horario()
        {
            DataHora = new HashSet<DataHora>();
            Funcionario = new HashSet<Funcionario>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor forneça o nome do horário")]
        [Display(Name = "Nome Horário")]
        public string Nome { get; set; }

        public virtual ICollection<DataHora> DataHora { get; set; }
        public virtual ICollection<Funcionario> Funcionario { get; set; }
    }
}
