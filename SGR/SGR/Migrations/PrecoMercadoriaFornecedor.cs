using System;
using System.Collections.Generic;

namespace SGR.Migrations
{
    public partial class PrecoMercadoriaFornecedor
    {
        public int Id { get; set; }
        public int Fornecedor { get; set; }
        public int Mercadoria { get; set; }
        public decimal Preco { get; set; }

        public virtual Fornecedor FornecedorNavigation { get; set; }
        public virtual Mercadoria MercadoriaNavigation { get; set; }
    }
}
