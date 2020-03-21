using System;
using System.Collections.Generic;

namespace SGR.Migrations
{
    public partial class Reserva
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string NomeCliente { get; set; }
        public string NifCliente { get; set; }
        public int IdGerente { get; set; }

        public virtual Gerente IdGerenteNavigation { get; set; }
    }
}
