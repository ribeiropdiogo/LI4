using System;
using System.Collections.Generic;

namespace SGR.Migrations
{
    public partial class Gerente
    {
        public Gerente()
        {
            Fornecedor = new HashSet<Fornecedor>();
            Funcionario = new HashSet<Funcionario>();
            Reserva = new HashSet<Reserva>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Fornecedor> Fornecedor { get; set; }
        public virtual ICollection<Funcionario> Funcionario { get; set; }
        public virtual ICollection<Reserva> Reserva { get; set; }
    }
}
