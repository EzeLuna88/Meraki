using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    
    public class BLLProducto
    {
        DALProducto access;

        public BLLProducto()
        {
            access= new DALProducto();
        }

        public List<BEProducto> listaProductos()
        {
            return access.listaProductos();
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

        public BEProducto BuscarProducto(string codigo)
        {
            return access.BuscarProducto(codigo);
        }
    }
}
