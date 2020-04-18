using System;
using System.Collections.Generic;

namespace SGR
{
    public partial class Mercadoria
    {
        public Mercadoria()
        {
            Mercadoriainartigo = new HashSet<Mercadoriainartigo>();
            Precomercadoriafornecedor = new HashSet<Precomercadoriafornecedor>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public int Stock { get; set; }
        public string Observacoes { get; set; }
        public int QuantidadeMinima { get; set; }
        public string Embalagem { get; set; }

        public virtual ICollection<Mercadoriainartigo> Mercadoriainartigo { get; set; }
        public virtual ICollection<Precomercadoriafornecedor> Precomercadoriafornecedor { get; set; }
    }
}
