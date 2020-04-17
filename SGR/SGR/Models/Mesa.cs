using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGR.Models
{
    public partial class Mesa
    {
        public Mesa()
        {
            Pedido = new HashSet<Pedido>();
        }

        public int Id { get; set; }

        [Display(Name = "Ocupação")]
        [Required(ErrorMessage = "Por favor forneça a Ocupação")]
        public int Ocupacao { get; set; }

        public bool Reservada { get; set; }

        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}
