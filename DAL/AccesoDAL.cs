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

        public class MigradorBaseDatos
        {
            // Reemplazá por tu cadena de conexión real o traela de tu configuración
            private string cadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;
            public void VerificarYActualizarEstructura()
            {
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    try
                    {
                        conexion.Open();

                        // 1. Crear la tabla CategoriaPdf si no existe
                        string sqlTabla = @"
                        CREATE TABLE IF NOT EXISTS CategoriaPdf (
                            Id INT AUTO_INCREMENT PRIMARY KEY,
                            Nombre VARCHAR(100) NOT NULL
                        );";

                        using (MySqlCommand cmd = new MySqlCommand(sqlTabla, conexion))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Chequear si la columna IdCategoriaPdf ya existe en la tabla Producto
                        string sqlCheckColumna = @"
                        SELECT COUNT(*) 
                        FROM INFORMATION_SCHEMA.COLUMNS 
                        WHERE TABLE_SCHEMA = DATABASE() 
                          AND TABLE_NAME = 'Producto' 
                          AND COLUMN_NAME = 'IdCategoriaPdf';";

                        bool columnaExiste = false;
                        using (MySqlCommand cmdCheck = new MySqlCommand(sqlCheckColumna, conexion))
                        {
                            int count = Convert.ToInt32(cmdCheck.ExecuteScalar());
                            columnaExiste = (count > 0);
                        }

                        // 3. Si no existe la columna, la agregamos con su clave foránea
                        if (!columnaExiste)
                        {
                            string sqlAlter = @"
                            ALTER TABLE Producto 
                            ADD IdCategoriaPdf INT NULL,
                            ADD CONSTRAINT FK_Producto_CategoriaPdf 
                            FOREIGN KEY (IdCategoriaPdf) REFERENCES CategoriaPdf(Id);";

                            using (MySqlCommand cmdAlter = new MySqlCommand(sqlAlter, conexion))
                            {
                                cmdAlter.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Lo ideal es loguearlo o avisar si falla la conexión crítica
                        throw new Exception($"Error crítico en la migración de la base de datos: {ex.Message}");
                    }
                }
            }
        }
    }
}

