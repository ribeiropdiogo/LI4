using System;
using System.Collections.Generic;

namespace SGR
{
    public partial class Artigo
    {
        public Artigo()
        {
            Artigoinpedido = new HashSet<Artigoinpedido>();
            Mercadoriainartigo = new HashSet<Mercadoriainartigo>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public virtual ICollection<Artigoinpedido> Artigoinpedido { get; set; }
        public virtual ICollection<Mercadoriainartigo> Mercadoriainartigo { get; set; }
    }
}
