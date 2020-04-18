using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGR.Models
{
    public partial class MercadoriaInArtigo
    {
        public int Id { get; set; }
        [Display(Name = "Mercadoria")]
        [Required(ErrorMessage = "Por favor forneça o ID da Mercadoria")]
        public int IdMercadoria { get; set; }
        [Display(Name = "Artigo")]
        [Required(ErrorMessage = "Por favor forneça o ID do Artigo")]
        public int IdArtigo { get; set; }

        public virtual Artigo IdArtigoNavigation { get; set; }
        public virtual Mercadoria IdMercadoriaNavigation { get; set; }
    }
}
