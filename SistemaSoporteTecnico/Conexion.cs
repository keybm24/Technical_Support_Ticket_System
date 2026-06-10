using System;
using MySql.Data.MySqlClient;

namespace SistemaSoporteTecnico
{
    public class Conexion
    {
        private static string connectionString =
            "Server=localhost;Database=soporte_tecnico;Uid=root;Pwd=luisyseidy;";

        public static MySqlConnection ObtenerConexion()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar a la base de datos: " + ex.Message);
            }
        }
    }
}