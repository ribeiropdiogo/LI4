using System;
using System.Collections.Generic;

namespace SGR.Migrations
{
    public partial class Mesa
    {
        public Mesa()
        {
            Pedido = new HashSet<Pedido>();
        }

        public int Id { get; set; }
        public string Ocupacao { get; set; }

        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}
