using System;
using System.Collections.Generic;

namespace SGR.Migrations
{
    public partial class Mercadoria
    {
        public Mercadoria()
        {
            MercadoriaInArtigo = new HashSet<MercadoriaInArtigo>();
            PrecoMercadoriaFornecedor = new HashSet<PrecoMercadoriaFornecedor>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public int Marca { get; set; }
        public int Stock { get; set; }
        public string Observacoes { get; set; }
        public int QuantidadeMinima { get; set; }
        public string Embalagem { get; set; }

        public virtual ICollection<MercadoriaInArtigo> MercadoriaInArtigo { get; set; }
        public virtual ICollection<PrecoMercadoriaFornecedor> PrecoMercadoriaFornecedor { get; set; }
    }
}
