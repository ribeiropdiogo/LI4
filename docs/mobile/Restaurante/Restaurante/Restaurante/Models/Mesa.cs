using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurante.Models
{
    public class Mesa
    {
        public int id { get; set; }
        public string ocupacao { get; set; }

        public Mesa(int id, string ocupacao)
        {
            this.id = id;
            this.ocupacao = ocupacao;
        }
    }
}
