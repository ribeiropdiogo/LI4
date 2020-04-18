using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGR.Models
{
    public partial class Mercadoria
    {
        public Mercadoria()
        {
            MercadoriaInArtigo = new HashSet<MercadoriaInArtigo>();
            PrecoMercadoriaFornecedor = new HashSet<PrecoMercadoriaFornecedor>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor forneça o nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor forneça a marca")]
        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Por favor forneça o stock")]
        public int Stock { get; set; }
        public string Observacoes { get; set; }

        [Required(ErrorMessage = "Por favor forneça o stock mínimo")]
        public int QuantidadeMinima { get; set; }

        [Required(ErrorMessage = "Por favor forneça o tipo de embalagem")]
        [Display(Name = "Embalagem")]
        public string Embalagem { get; set; }

        public virtual ICollection<MercadoriaInArtigo> MercadoriaInArtigo { get; set; }
        public virtual ICollection<PrecoMercadoriaFornecedor> PrecoMercadoriaFornecedor { get; set; }
    }
}
