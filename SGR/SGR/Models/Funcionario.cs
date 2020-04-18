using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGR.Models
{
    public partial class Funcionario
    {
        public Funcionario()
        {
            Pedido = new HashSet<Pedido>();
        }

        public int Id { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Por favor forneça o Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Por favor forneça a Password")]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Por favor forneça a data de nascimento")]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Por favor forneça a morada")]
        public string Morada { get; set; }

        [Required(ErrorMessage = "Por favor forneça o nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor forneça o cargo")]
        public string Cargo { get; set; }

        [Display(Name = "Horário")]
        [Required(ErrorMessage = "Por favor forneça o Id do horário")]
        public int IdHorario { get; set; }

        [Required(ErrorMessage = "Por favor forneça o Id do Gerente")]
        public int IdGerente { get; set; }

        public virtual Gerente IdGerenteNavigation { get; set; }
        public virtual Horario IdHorarioNavigation { get; set; }
        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}
