using BE;
using MPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLProductoCombo
    {
        MPPProductoCombo access;

        public BLLProductoCombo()
        {
            access = new MPPProductoCombo();
        }

        public void GuardarProducto(BEProductoCombo beProducto)
        {
            access.GuardarProducto(beProducto);
        }

        public bool CodigoYaExiste(string codigo)
        {
            return access.CodigoYaExiste(codigo);
        }

        public void ModificarProducto(BEProductoCombo beProducto)
        {
            access.ModificarProducto(beProducto);
        }

        public void BorrarProducto(BEProductoCombo beProducto)
        {
            access.BorrarProducto(beProducto);
        }

        public bool ValidarStockEnComboActivo(string codigoStock)
        {
            // Acá le pasamos la pelota a tu capa de datos (MPP).
            // Tu MPPProductoCombo va a tener que hacer una consulta a la base de datos 
            // para ver si este 'codigoStock' existe en la tabla de detalle de algún combo ACTIVO.
            return access.ValidarStockEnComboActivo(codigoStock);
        }
    }
}
