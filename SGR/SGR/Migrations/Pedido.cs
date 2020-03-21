using System;
using System.Collections.Generic;

namespace SGR.Migrations
{
    public partial class Pedido
    {
        public Pedido()
        {
            ArtigoInPedido = new HashSet<ArtigoInPedido>();
        }

        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public int Numero { get; set; }
        public string Estado { get; set; }
        public int IdFuncionario { get; set; }
        public int Mesa { get; set; }

        public virtual Funcionario IdFuncionarioNavigation { get; set; }
        public virtual Mesa MesaNavigation { get; set; }
        public virtual ICollection<ArtigoInPedido> ArtigoInPedido { get; set; }
    }
}
