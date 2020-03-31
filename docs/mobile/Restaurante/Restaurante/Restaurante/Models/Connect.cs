using MySql.Data.MySqlClient;
using MySqlConnector;
using System;
using System.Collections.Generic;

namespace Restaurante.Models
{
    class Connect
    {
        private static string server, database, uid, password;
        private static MySqlConnection connection;
        static private void Initialize()
        {
            server = "sgr.mysql.database.azure.com";
            database = "mydb";
            uid = "sgr@sgr";
            password = "projetoLI4";
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
            int i = 0;
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

        static public Funcionario login(string user, string pass)
        {
            try
            {
                Initialize();
                OpenConnection();
                string query = "SELECT * FROM funcionario WHERE nome='" + user + "' and password='" + pass + "';";
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
