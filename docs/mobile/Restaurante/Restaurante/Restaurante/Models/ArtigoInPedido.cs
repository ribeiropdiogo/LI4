using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurante.Models
{
    public partial class ArtigoInPedido
    {
            public int Id { get; set; }
            public int IdPedido { get; set; }
            public int IdArtigo { get; set; }
            public ArtigoInPedido(int Id, int IdPedido, int IdArtigo)
            {
                this.Id = Id;
                this.IdPedido = IdPedido;
                this.IdArtigo = IdArtigo;
            }
            public virtual Artigo IdArtigoNavigation { get; set; }
            public virtual Pedido IdPedidoNavigation { get; set; }
        
    }
}
