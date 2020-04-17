using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGR.Models
{
    public partial class Fornecedor
    {
        public Fornecedor()
        {
            PrecoMercadoriaFornecedor = new HashSet<PrecoMercadoriaFornecedor>();
        }

        public int Id { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Por favor forneça o Email")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Por favor forneça o Email")]
        public string Contacto { get; set; }

        [Required(ErrorMessage = "Por favor forneça o Id fo Gerente")]
        public int IdGerente { get; set; }

        [Required(ErrorMessage = "Por favor forneça o Nome")]
        public string Nome { get; set; }

        public virtual Gerente IdGerenteNavigation { get; set; }
        public virtual ICollection<PrecoMercadoriaFornecedor> PrecoMercadoriaFornecedor { get; set; }
    }
}
