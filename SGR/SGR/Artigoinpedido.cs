using System;
using System.Collections.Generic;

namespace SGR
{
    public partial class Artigoinpedido
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public int IdArtigo { get; set; }
        public int Quantidade { get; set; }

        public virtual Artigo IdArtigoNavigation { get; set; }
        public virtual Pedido IdPedidoNavigation { get; set; }
    }
}
