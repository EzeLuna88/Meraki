using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLProductoCombo
    {
        DALProductoCombo access;

        public BLLProductoCombo()
        {
            access = new DALProductoCombo();
        }

        public void GuardarProducto(BEProductoCombo beProducto)
        {
            access.GuardarProducto(beProducto);
        }

        public void ModificarProducto(BEProductoCombo beProducto)
        {
            access.ModificarProducto(beProducto);
        }

        public void BorrarProducto(BEProductoCombo beProducto) { access.BorrarProducto(beProducto); }

    }
}
