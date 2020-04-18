using System;
using System.Collections.Generic;

namespace SGR
{
    public partial class Funcionario
    {
        public Funcionario()
        {
            Pedido = new HashSet<Pedido>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Morada { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public int? IdHorario { get; set; }
        public int IdGerente { get; set; }

        public virtual Gerente IdGerenteNavigation { get; set; }
        public virtual Horario IdHorarioNavigation { get; set; }
        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}
