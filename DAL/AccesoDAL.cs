using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace DAL
{

    public class AccesoDAL
    {
        private readonly string _cadenaConexion;

        public AccesoDAL()
        {
            _cadenaConexion = ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;
        }

        public int EjecutarNonQuery(string consulta, params MySqlParameter[] parametros)
        {
            using (var conexion = new MySqlConnection(_cadenaConexion))
            {
                conexion.Open();
                using (var comando = new MySqlCommand(consulta, conexion))
                {
                    if (parametros != null)
                        comando.Parameters.AddRange(parametros);

                    return comando.ExecuteNonQuery();
                }
            }
        }

        public DataTable EjecutarConsulta(string consulta, params MySqlParameter[] parametros)
        {
            using (var conexion = new MySqlConnection(_cadenaConexion))
            {
                conexion.Open();
                using (var comando = new MySqlCommand(consulta, conexion))
                {
                    if (parametros != null)
                        comando.Parameters.AddRange(parametros);

                    using (var adaptador = new MySqlDataAdapter(comando))
                    {
                        var tabla = new DataTable();
                        adaptador.Fill(tabla);
                        return tabla;
                    }
                }
            }
        }



        public object EjecutarEscalar(string consulta, IEnumerable<MySqlParameter> parametros = null)
        {
            using (var conexion = new MySqlConnection(_cadenaConexion))
            {
                conexion.Open();
                using (var comando = new MySqlCommand(consulta, conexion))
                {
                    if (parametros != null)
                        comando.Parameters.AddRange(parametros.ToArray());

                    return comando.ExecuteScalar();
                }
            }
        }
    }
}

