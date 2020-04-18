using System;
using System.Collections.Generic;

namespace SGR
{
    public partial class Horario
    {
        public Horario()
        {
            Datahora = new HashSet<Datahora>();
            Funcionario = new HashSet<Funcionario>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Datahora> Datahora { get; set; }
        public virtual ICollection<Funcionario> Funcionario { get; set; }
    }
}
