using System;
using System.Collections.Generic;

namespace SGR
{
    public partial class Fornecedor
    {
        public Fornecedor()
        {
            Precomercadoriafornecedor = new HashSet<Precomercadoriafornecedor>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Contacto { get; set; }
        public int IdGerente { get; set; }
        public string Nome { get; set; }

        public virtual Gerente IdGerenteNavigation { get; set; }
        public virtual ICollection<Precomercadoriafornecedor> Precomercadoriafornecedor { get; set; }
    }
}
