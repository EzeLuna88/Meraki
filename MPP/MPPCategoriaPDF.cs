using BE;
using BE;
using DAL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP
{
    public class MPPCategoriaPDF
    {
        private readonly AccesoDAL acceso;

        public MPPCategoriaPDF()
        {
            acceso = new AccesoDAL();
        }

        public List<BECategoriaPDF> ListarCategorias()
        {
            List<BECategoriaPDF> lista = new List<BECategoriaPDF>();
            string query = "SELECT Id, Nombre FROM CategoriaPdf ORDER BY Nombre ASC;";

            // Usamos tu método que devuelve el DataTable directo
            DataTable tabla = acceso.EjecutarConsulta(query);

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new BECategoriaPDF
                {
                    Id = Convert.ToInt32(fila["Id"]),
                    Nombre = fila["Nombre"].ToString()
                });
            }
            return lista;
        }

        public void Insertar(string nombre)
        {
            string query = "INSERT INTO CategoriaPdf (Nombre) VALUES (@nombre);";
            acceso.EjecutarNonQuery(query, new MySqlParameter("@nombre", nombre));
        }

        public void Eliminar(int id)
        {
            // Mandamos los dos comandos en cadena para asegurar el desvincule antes del borrado
            string query = @"UPDATE Producto SET IdCategoriaPdf = NULL WHERE IdCategoriaPdf = @id;
                             DELETE FROM CategoriaPdf WHERE Id = @id;";

            acceso.EjecutarNonQuery(query, new MySqlParameter("@id", id));
        }


    }
}
