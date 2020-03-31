using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurante.Models
{
    class Mercadoria
    {
        public int id { get; set; }
        public string nome { get; set; }
        public int marca { get; set; }
        public int stock { get; set; }
        public string observacoes { get; set; }
        public int quantidade_min { get; set; }
        public string embalagem { get; set; }
        public Mercadoria(int id, string nome, int marca, int stock, string observacoes, int quantidade_min, string embalagem)
        {
            this.id = id;
            this.nome = nome;
            this.marca = marca;
            this.stock = stock;
            this.observacoes = observacoes;
            this.quantidade_min = quantidade_min;
            this.embalagem = embalagem;

        }
    }
}
