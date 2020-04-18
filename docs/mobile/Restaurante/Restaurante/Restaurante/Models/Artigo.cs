using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurante.Models
{
    public class Artigo
    {
        public int id { get; set; }
        public string nome { get; set; }
        public decimal preco { get; set; }

        public virtual ICollection<ArtigoInPedido> ArtigoInPedido { get; set; }
        public Artigo(int id, string nome, decimal preco)
        {
            this.id = id;
            this.nome = nome;
            this.preco = preco;
        }
        public Artigo() 
        {
            ArtigoInPedido = new HashSet<ArtigoInPedido>();
        }
    }
}
