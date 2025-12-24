using DAL;
using iText.Layout.Properties;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP
{
    public class MPPConfiguracion
    {

        private readonly AccesoDAL acceso;

        public MPPConfiguracion()
        {
            acceso = new AccesoDAL();
        }

        public int ObtenerDiasAvisoVencimiento()
        {
            string query = "SELECT valor FROM configuracion WHERE clave = 'dias_aviso_vencimiento'";
            AccesoDAL acceso = new AccesoDAL();
            DataTable tabla = acceso.EjecutarConsulta(query);
            if (tabla.Rows.Count > 0)
            {
                return Convert.ToInt32(tabla.Rows[0]["valor"]);
            }
            else
            {
                string query2 = "INSERT INTO configuracion(clave, valor) VALUES('dias_aviso_vencimiento', '7')";
                acceso.EjecutarNonQuery(query2);
                return 7; // valor por defecto si no está configurado
            }
        }

        public void GuardarDiasAvisoVencimiento(int dias)
        {
            string query = @"
        UPDATE configuracion 
        SET valor = @valor 
        WHERE clave = 'dias_aviso_vencimiento'";

            List<MySqlParameter> parametros = new List<MySqlParameter>
    {
        new MySqlParameter("@valor", dias.ToString())
    };

            AccesoDAL acceso = new AccesoDAL();
            acceso.EjecutarNonQueryConParametros(query, parametros);
        }
    }
}
