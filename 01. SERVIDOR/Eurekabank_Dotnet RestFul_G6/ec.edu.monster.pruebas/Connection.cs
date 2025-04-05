using System;
using MySql.Data.MySqlClient;

namespace Eurekabank_Dotnet_RestFul_G6.ec.edu.monster.pruebas
{
    public class Connection
    {
        private readonly string connectionString = "Server=localhost;Database=eurekabank;User=root;Password=password;";

        public bool TestConnection()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Conexión exitosa a la base de datos.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar con la base de datos: " + ex.Message);
                return false;
            }
        }
    }
}