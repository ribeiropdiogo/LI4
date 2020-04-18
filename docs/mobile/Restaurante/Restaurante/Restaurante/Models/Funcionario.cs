using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Restaurante.Models
{
    public class Funcionario
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime dataNascimento { get; set; }
        public string morada { get; set; }
        public string nome { get; set; }
        public string cargo { get; set; }
        public int idHorario { get; set; }
        public int idGerente { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }

        public Funcionario(int id, string email, string password, DateTime dataNascimento, string morada, string nome, string cargo, int idHorario, int idGerente)
        {
            this.id = id;
            this.email = email;
            this.password = password;
            this.dataNascimento = dataNascimento;
            this.morada = morada;
            this.nome = nome;
            this.cargo = cargo;
            this.idHorario = idHorario;
            this.idGerente = idGerente;
        }

        public Funcionario(Funcionario f)
        {
            this.id = f.id;
            this.email = f.email;
            this.password = f.password;
            this.dataNascimento = f.dataNascimento;
            this.morada = f.morada;
            this.nome = f.nome;
            this.cargo = f.cargo;
            this.idHorario = f.idHorario;
            this.idGerente = f.idGerente;
        }

        public static Funcionario Clone<Funcionario>(Funcionario source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<Funcionario>(serialized);
        }

    }
}