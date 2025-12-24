using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using MPP;

namespace BLL
{
    public class BLLStock
    {
        MPPStock mppStock;

        public BLLStock() { mppStock = new MPPStock(); }



        public void GuardarNuevoProducto(BEStock beStock)
        { mppStock.GuardarNuevoProducto(beStock); }

        public List<BEStock> CargarStock()
        { return mppStock.CargarStock(); }

        public bool ComprobarRepetido(BEStock beStock)
        {
            return mppStock.ComprobarRepetido(beStock);
        }

        public void BorrarProductoDeStock(BEStock beStock)
        {
            mppStock.BorrarProductoDeStock(beStock);
        }

        public void AgregarStock(BEStock bestock, int unidades)
        {
            mppStock.AgregarStock(bestock, unidades);
        }

        public void ActualizarStock(List<BEStock> listaStock)
        {
            mppStock.ActualizarStock(listaStock);
        }

        public void ModificarStock(BEStock beStock)
        {
            mppStock.ModificarStock(beStock);
        }

        public void CantidadReservadaStock(BEStock beStock)
        {
            mppStock.CantidadReservadaStock(beStock);
        }

        public void AcomodarCantidadReservada()
        {
            mppStock.AcomodarCantidadReservadaStock();
        }

        public void CargarFechaDeVencimiento(BEStock beStock, DateTime fechaDeVencimiento, int totalUnidades)
        {
            mppStock.CargarFechaDeVencimiento(beStock, fechaDeVencimiento, totalUnidades);
        }

        public void DescontarStockPorVencimiento(BECompraMayorista compra)
        {
            mppStock.DescontarStockPorVencimiento(compra); 
        }

        public DataTable ObtenerStockConVencimiento()
        {
            return mppStock.ObtenerStockConVencimiento();
        }

        public void CantidadAviso(BEStock beStock)
        {
             mppStock.CantidadAviso(beStock);
        }

        public List<string> ObtenerProductosProximosAVencer()
        {
            return mppStock.ObtenerProductosProximosAVencer();
        }

        public List<string> ObtenerProductosConStockBajo()
        {
            return mppStock.ObtenerProductosConStockBajo();
        }
    }
}
