using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;


namespace Dato
{
    public class DatoMantenedorDron
    {
        DatoConexionSqlServer conexion = new DatoConexionSqlServer();



        public DataTable ListarDron(Dron dron)
        {
            DataTable tabla = new DataTable();
            SqlDataReader leer;
            SqlCommand comando = new SqlCommand();

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "ListarDron";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@NumeroSerie", dron.NumeroSerie);
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;

        }

        public string InsertarDron(Dron dron)
        {
            string Respuesta;
            SqlCommand comando = new SqlCommand();
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "InsetarDron";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@NumeroSerie", dron.NumeroSerie);
                comando.Parameters.AddWithValue("@Modelo", dron.Modelo);
                comando.Parameters.AddWithValue("@PesoLimite", dron.PesoLimite);
                comando.Parameters.AddWithValue("@CapacidadBateria", dron.CapacidadBateria);
                comando.Parameters.AddWithValue("@Estado", dron.Estado);
                Respuesta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo insertar dron";
                comando.Parameters.Clear();
                conexion.CerrarConexion();
            }
            catch (Exception e)
            {

                Respuesta = e.Message.ToString();
            }

            return Respuesta;
        }

        public string InsertarCargarDron(Dron dron)
        {
            string Respuesta = string.Empty;

            try
            {

                foreach (Medicamento medicamentoItem in dron.Medicamentos)
                {
                    SqlCommand comando = new SqlCommand();

                    comando.Connection = conexion.AbrirConexion();
                    comando.CommandText = "InsertarCargarDron";
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@NumeroSerie", dron.NumeroSerie);
                    comando.Parameters.AddWithValue("@Codigo", medicamentoItem.Codigo);
                    comando.Parameters.AddWithValue("@Peso", medicamentoItem.Peso);
                    comando.Parameters.AddWithValue("@Nombre", medicamentoItem.Nombre);
                    comando.Parameters.AddWithValue("@Imagen", medicamentoItem.Imagen);

                    Respuesta = comando.ExecuteNonQuery() == 1 ? "NOK" : "OK";
                    if (Respuesta == "NOK")
                    {
                        Respuesta = "No se pudo agregar medicamentos a dron";
                    }
                    comando.Parameters.Clear();
                    conexion.CerrarConexion();
                }


            }
            catch (Exception e)
            {

                Respuesta = e.Message.ToString();
            }

            return Respuesta;
        }

        public DataTable MostrarDronDisponible()
        {
            DataTable tabla = new DataTable();
            SqlDataReader leer;
            SqlCommand comando = new SqlCommand();

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "MostrarDronDisponible";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            conexion.CerrarConexion();
            return tabla;

        }
    }


}
