using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGR.Models
{
    public partial class ArtigoInPedido
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Por favor forneça o Id do Pedido")]
        public int IdPedido { get; set; }

        [Required(ErrorMessage = "Por favor forneça o Id do Artigo")]
        [Display(Name = "Artigo")]
        public int IdArtigo { get; set; }

        [Required(ErrorMessage = "Por favor forneça a quantidade")]
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

        public virtual Artigo IdArtigoNavigation { get; set; }
        public virtual Pedido IdPedidoNavigation { get; set; }
    }
}
