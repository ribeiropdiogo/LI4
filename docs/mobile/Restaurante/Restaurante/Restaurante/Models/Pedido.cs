using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurante.Models
{
    public class Pedido
    {
        public int id { get; set; }
        public DateTime dataHora { get; set; }
        public int numero { get; set; }
        public string estado { get; set; }
        public int idFuncionario { get; set; }
        public int mesa { get; set; }
        public virtual Funcionario IdFuncionarioNavigation { get; set; }
        public virtual Mesa MesaNavigation { get; set; }
        public virtual ICollection<ArtigoInPedido> ArtigoInPedido { get; set; }
        public Pedido(int id, DateTime dataHora, int numero, string estado, int idFuncionario, int mesa)
        {
            this.id = id;
            this.dataHora = dataHora;
            this.numero = numero;
            this.estado = estado;
            this.idFuncionario = idFuncionario;
            this.mesa = mesa;
        }
    }
}
