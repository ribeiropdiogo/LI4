using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGR.Models
{
    public partial class Artigo
    {
        public Artigo()
        {
            ArtigoInPedido = new HashSet<ArtigoInPedido>();
            MercadoriaInArtigo = new HashSet<MercadoriaInArtigo>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor forneça o Nome")]
        public string Nome { get; set; }


        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Por favor forneça o Preço")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Por favor forneça a Categoria")]
        public string Categoria { get; set; }

        public virtual ICollection<ArtigoInPedido> ArtigoInPedido { get; set; }
        public virtual ICollection<MercadoriaInArtigo> MercadoriaInArtigo { get; set; }
    }
}
