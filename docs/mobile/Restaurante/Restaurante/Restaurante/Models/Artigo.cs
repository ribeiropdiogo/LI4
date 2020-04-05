using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurante.Models
{
    class Artigo
    {
        public int id { get; set; }
        public string nome { get; set; }
        public decimal preco { get; set; }

        public Artigo(int id, string nome, decimal preco)
        {
            this.id = id;
            this.nome = nome;
            this.preco = preco;
        }
    }
}
