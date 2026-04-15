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

        public void BorrarProducto(BEProductoIndividual beProducto)
        {

            if (beProducto.Stock != null && beProducto.Stock.CantidadActual > 0)
            {
                throw new InvalidOperationException($"No se puede eliminar el producto porque aún quedan {beProducto.Stock.CantidadActual} unidades físicas de '{beProducto.Stock.Nombre}' en el depósito. Descuente el stock primero.");
            }


            BLLProductoCombo bllCombos = new BLLProductoCombo(); //

            bool estaEnCombo = bllCombos.ValidarStockEnComboActivo(beProducto.Stock.Codigo);

            if (estaEnCombo)
            {
                throw new InvalidOperationException("No se puede eliminar este producto porque forma parte de un combo armado. Elimine o modifique el combo primero.");
            }


            access.BorrarProducto(beProducto);
        }

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
