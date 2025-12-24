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

        public void EjecutarNonQueryConParametros(string query, List<MySqlParameter> parametros)
        {
            using (MySqlConnection conexion = new MySqlConnection(_cadenaConexion))
            {
                conexion.Open();
                using (MySqlCommand comando = new MySqlCommand(query, conexion))
                {
                    comando.Parameters.AddRange(parametros.ToArray());
                    comando.ExecuteNonQuery();
                }
            }
        }

        public object EjecutarEscalarConParametros(string query, List<MySqlParameter> parametros)
        {
            using (MySqlConnection conexion = new MySqlConnection(_cadenaConexion))
            {
                conexion.Open();
                using (MySqlCommand comando = new MySqlCommand(query, conexion))
                {
                    comando.Parameters.AddRange(parametros.ToArray());
                    return comando.ExecuteScalar();
                }
            }
        }
    }
    }

