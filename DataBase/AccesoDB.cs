using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataBase
{
    public class AccesoDB
    {
        private SqlConnection conexion;
        private SqlCommand command;
        private SqlDataReader reader;

        public AccesoDB()
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=CATALOGO_DB; integrated security=true");
            command = new SqlCommand();
        }

        public void SetConsulta(string consulta)
        {
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = consulta;
        }

        public void Lectura()
        {
            command.Connection = conexion;
            try
            {
                conexion.Open();
                reader = command.ExecuteReader();  
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SqlDataReader Reader 
            { get { return reader; } }  

        public void ExecuteAction()
        {
            command.Connection = conexion;
            try
            {
                conexion.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void SetParametro(string nombre, string valor)
        {
            command.Parameters.AddWithValue(nombre, valor);
        }

        public void CloseConexion()
        {
            if (reader != null)
            {
                reader.Close();
            }
            conexion.Close();
        }

        public void setearParametro(string nombre, object valor)
        {
            command.Parameters.AddWithValue(nombre, valor);
        }
    }
}
