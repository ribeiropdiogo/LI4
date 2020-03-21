using System;
using System.Collections.Generic;

namespace SGR.Models
{
    public partial class Fornecedor
    {
        public Fornecedor()
        {
            PrecoMercadoriaFornecedor = new HashSet<PrecoMercadoriaFornecedor>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Contacto { get; set; }
        public int IdGerente { get; set; }

        public virtual Gerente IdGerenteNavigation { get; set; }
        public virtual ICollection<PrecoMercadoriaFornecedor> PrecoMercadoriaFornecedor { get; set; }
    }
}
