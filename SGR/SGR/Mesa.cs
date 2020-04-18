using System;
using System.Collections.Generic;

namespace SGR
{
    public partial class Mesa
    {
        public Mesa()
        {
            Pedido = new HashSet<Pedido>();
        }

        public int Id { get; set; }
        public int Ocupacao { get; set; }
        public sbyte Reservada { get; set; }

        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}
