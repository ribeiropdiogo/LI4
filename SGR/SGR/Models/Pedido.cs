using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGR.Models
{
    public partial class Pedido
    {
        public Pedido()
        {
            ArtigoInPedido = new HashSet<ArtigoInPedido>();
        }

        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd H:mm:ss}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Por favor forneça a data e hora")]
        [Display(Name = "Data E Hora")]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "Por favor forneça o Número")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Por favor forneça o Estado")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Por favor forneça o Id do Funcionário")]
        public int IdFuncionario { get; set; }

        [Required(ErrorMessage = "Por favor forneça a Mesa")]
        public int Mesa { get; set; }

        public virtual Funcionario IdFuncionarioNavigation { get; set; }
        public virtual Mesa MesaNavigation { get; set; }
        public virtual ICollection<ArtigoInPedido> ArtigoInPedido { get; set; }
    }
}
