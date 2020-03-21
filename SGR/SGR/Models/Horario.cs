using System;
using System.Collections.Generic;

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

        public virtual ICollection<DataHora> DataHora { get; set; }
        public virtual ICollection<Funcionario> Funcionario { get; set; }
    }
}
