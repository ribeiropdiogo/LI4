using System;
using System.Collections.Generic;

namespace SGR.Models
{
    public partial class ArtigoInPedido
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public int IdArtigo { get; set; }

        public virtual Artigo IdArtigoNavigation { get; set; }
        public virtual Pedido IdPedidoNavigation { get; set; }
    }
}
