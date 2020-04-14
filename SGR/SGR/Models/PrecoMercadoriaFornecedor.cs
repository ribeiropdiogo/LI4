using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGR.Models
{
    public partial class PrecoMercadoriaFornecedor
    {
        public int Id { get; set; }
        [Required]
        public int Fornecedor { get; set; }
        [Required]
        public int Mercadoria { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }

        public virtual Fornecedor FornecedorNavigation { get; set; }
        public virtual Mercadoria MercadoriaNavigation { get; set; }
    }
}
