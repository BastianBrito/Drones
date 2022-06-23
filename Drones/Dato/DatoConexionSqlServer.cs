using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dato
{
    public class DatoConexionSqlServer
    {
        //se deben escribir las credenciales de base de datos
        private SqlConnection Conexion = new SqlConnection("Server=DESKTOP-INSTANCIA;DataBase= Drones; user Id=sa;password=CONTRASEÑA; Integrated Security=true");


        public SqlConnection AbrirConexion()
        {
            if (Conexion.State == ConnectionState.Closed)
                Conexion.Open();
            return Conexion;
        }
        public SqlConnection CerrarConexion()
        {
            if (Conexion.State == ConnectionState.Open)
                Conexion.Close();
            return Conexion;
        }
    }
}
