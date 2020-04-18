using MySql.Data.MySqlClient;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Restaurante.Models
{
    class Connect
    {
        private static string server, database, uid, password;
        private static MySqlConnection connection;
        static private void Initialize()
        {
            server = "MYSQL5017.site4now.net";
            database = "db_a5a7f4_li4sgr";
            uid = "a5a7f4_li4sgr";
            password = "liquatrosgr4";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }


        //open connection to database
        static private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        static private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        static public void InsertArtigo(string nome, float preco)
        {
            string query = "INSERT INTO artigo (nome, preco) VALUES('" + nome + "'," + preco + ")";

            //open connection
            if (OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                CloseConnection();
            }
        }

        static public void UpdateArtigo(string nome, string preco)
        {
            string query = "UPDATE artigo SET preco=" + preco + "WHERE nome='" + nome + "';";

            //Open connection
            //create mysql command
            MySqlCommand cmd = new MySqlCommand();
            //Assign the query using CommandText
            cmd.CommandText = query;
            //Assign the connection using Connection
            cmd.Connection = connection;

            //Execute query
            cmd.ExecuteNonQuery();
        }

        static public List<Artigo> listaArtigos()
        {
            Initialize();
            OpenConnection();
            List<Artigo> lista = new List<Artigo>();
            string query = "SELECT * FROM artigo;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Prepare();
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    lista.Add(new Artigo(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetDecimal(2)));
                }
            }
            return lista;
        }

        static public Funcionario login(string email, string pass)
        {
            try
            {
                Initialize();
                OpenConnection();
                string query = "SELECT * FROM funcionario WHERE email='" + email + "' and password='" + pass + "';";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Prepare();
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Console.WriteLine(dataReader.GetInt32(0) + "| " + dataReader.GetString(1) + "| " + dataReader.GetString(2));
                        return new Funcionario(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetDateTime(3), dataReader.GetString(4), dataReader.GetString(5), dataReader.GetString(6), dataReader.GetInt32(7), dataReader.GetInt32(8));
                    }
                }
                else
                {
                    Console.WriteLine(" !!no rows found.!!");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Exception " + ex.Message);
            }
            return null;
        }

        static public List<Pedido> pedidosFeitos(Funcionario f)
        {
            try
            {
                Initialize();
                OpenConnection();
                List<Pedido> pedidos = new List<Pedido>();
                Funcionario ff = new Funcionario(f);
                string query;
                if (ff.cargo.Equals("Emp. Mesa"))
                    query = "SELECT * FROM pedido WHERE idFuncionario=" + ff.id + " and estado='" + "Por pagar" + "';";
                else
                    query = "SELECT * FROM pedido WHERE estado='" + "Por pagar" + "';";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Prepare();
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        pedidos.Add(new Pedido(dataReader.GetInt32(0), dataReader.GetDateTime(1), dataReader.GetInt32(2), dataReader.GetString(3), dataReader.GetInt32(4), dataReader.GetInt32(5)));
                    }
                    return pedidos;
                }
                else
                {
                    Console.WriteLine(" !!no rows found.!!");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Exception " + ex.Message);
            }
            return null;
        }

        static public bool addMesa(int id)
        {
            try
            {
                Initialize();
                OpenConnection();
                string query = "SELECT * FROM mesa WHERE id='" + id + "';";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Prepare();
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Console.WriteLine(dataReader.GetByte(2));
                        int x = dataReader.GetByte(2);
                        if (x == 0)
                            return true;
                    }
                }
                else
                {
                    Console.WriteLine(" !!no rows found.!!");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Exception " + ex.Message);
            }
            return false;
        }

        static public void mesaOcupada(int id)
        {
            try
            {
                Initialize();
                OpenConnection();
                string query = "UPDATE mesa SET reservada=" + 1 + " WHERE id=" + id+ ";";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Exception " + ex.Message);
            }
        }

        static public void addArtigos(ICollection<ArtigoInPedido> artigos)
        {
            try
            {
                Initialize();
                OpenConnection();
                string query;
                MySqlCommand cmd;
                foreach (var artigo in artigos)
                {
                    query = "INSERT INTO artigoinpedido (idPedido, idArtigo) VALUES(" + artigo.IdPedido + "," + artigo.IdArtigo + ")";
                    Console.WriteLine(artigo.IdPedido + ", " + artigo.IdArtigo);
                    cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
                 
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Exception " + ex.Message);
            }
        }


        static public void insertPedido(String dataHora, int numero, string estado, int idFuncionario, int mesa)
        {
            try
            {
                Initialize();
                OpenConnection();
                string query = "INSERT INTO pedido (dataHora, numero, estado, idFuncionario, mesa) VALUES(" + "'" + dataHora + "'," + numero + ",'" + estado + "'," + idFuncionario + "," + mesa +  ")";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Exception " + ex.Message);
            }
        }

        static public int linhasArtigoInPedido(int x)
        {
            try
            {
                Initialize();
                OpenConnection();
                string query;
                if(x == 1)
                    query = "SELECT COUNT(*) FROM artigoinpedido";
                else
                    query = "SELECT COUNT(*) FROM pedido";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                int linhas = Convert.ToInt32(cmd.ExecuteScalar());
                ++linhas;
                return linhas;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Exception " + ex.Message);
            }
            return 0;
        }

        static public void DeleteArtigo(string nome)
        {
            string query = "DELETE FROM artigo WHERE nome='" + nome + "';";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
        }

        //Select statement
        static public decimal GetPrecoArtigo(string nome)
        {
            string query = "SELECT nome,preco FROM artigo WHERE nome='" + nome + "';";
            decimal answer = 0;
            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                answer = (decimal)dataReader["preco"];
            }

            //close Data Reader
            dataReader.Close();
            //return list to be displayed
            return answer;
        }
         public static void teste()
        {
            decimal precoAzeite;
            Initialize();
            OpenConnection();

            //UpdateArtigo("Ovos", "10.3");
            precoAzeite = GetPrecoArtigo("Sal fino");
            CloseConnection();
            Console.WriteLine(precoAzeite);
        }
    }
}
