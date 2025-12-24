using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using MPP;

namespace BLL
{
    
    public class BLLProducto
    {
        MPPProducto access;

        public BLLProducto()
        {
            access = new MPPProducto();
        }

        public List<BEProducto> listaProductos()
        {
            return access.ListaProductos();
        }

        public void GuardarProducto(BEProductoIndividual beProducto)
        {
            access.GuardarProducto(beProducto);
        }

        public void BorrarProducto(BEProductoIndividual beProducto) { access.BorrarProducto(beProducto);} 

        public void ModificarProducto(BEProducto beProducto)
        {
            access.ModificarProducto(beProducto);
        }

        /*public BEProducto BuscarProducto(string codigo)
        {
            return access.BuscarProducto(codigo);
        }*/
    }
}
