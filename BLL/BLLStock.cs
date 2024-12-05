using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class BLLStock
    {
        DALStock dalStock;

        public BLLStock() { dalStock = new DALStock(); }



        public void GuardarNuevoProducto(BEStock beStock)
        { dalStock.GuardarNuevoProducto(beStock); }

        public List<BEStock> CargarStock()
        { return dalStock.cargarStock(); }

        public bool ComprobarRepetido(BEStock beStock)
        {
            return dalStock.ComprobarRepetido(beStock);
        }

        public void BorrarProductoDeStock(BEStock beStock)
        {
            dalStock.BorrarProductoDeStock(beStock);
        }

        public void AgregarStock(BEStock bestock, int unidades)
        {
            dalStock.AgregarStock(bestock, unidades);
        }

        public void ActualizarStock(List<BEStock> listaStock)
        {
            dalStock.ActualizarStock(listaStock);
        }

        public void ModificarStock(BEStock beStock)
        {
            dalStock.ModificarStock(beStock);
        }

        public void CantidadReservadaStock(BEStock beStock)
        {
            dalStock.CantidadReservadaStock(beStock);
        }

        public void AcomodarCantidadReservada()
        {
            dalStock.AcomodarCantidadReservadaStock();
        }
    }
}
