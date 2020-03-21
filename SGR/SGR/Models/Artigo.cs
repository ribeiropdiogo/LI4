using System;
using System.Collections.Generic;

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
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public virtual ICollection<ArtigoInPedido> ArtigoInPedido { get; set; }
        public virtual ICollection<MercadoriaInArtigo> MercadoriaInArtigo { get; set; }
    }
}
