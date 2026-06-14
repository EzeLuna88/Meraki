using BE;
using MPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLCategoriaPDF
    {
        MPPCategoriaPDF mppCategoriaPDF;

        public BLLCategoriaPDF()
        {
            mppCategoriaPDF = new MPPCategoriaPDF();
        }

        public List<BECategoriaPDF> ListarCategorias()
        {
            return mppCategoriaPDF.ListarCategorias();
        }

        public void Insertar(string nombre)
        {
            mppCategoriaPDF.Insertar(nombre);
        }

        public void Eliminar(int id)
        {
            mppCategoriaPDF.Eliminar(id);
        }


    }
}
