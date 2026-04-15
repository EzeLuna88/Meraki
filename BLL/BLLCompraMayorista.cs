using BE;
using MPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLCompraMayorista
    {
        MPPCompraMayorista mppCompraMayorista;
        BLLStock bllStock;
        MPPStock mppStock;

        public BLLCompraMayorista()
        {
            mppCompraMayorista = new MPPCompraMayorista();
            bllStock = new BLLStock();
            mppStock = new MPPStock();
        }

        public void GuardarCompraMayorista(BECompraMayorista compraMayorista)
        {
            mppCompraMayorista.GuardarCompraMayorista(compraMayorista);

        }

        public void ConfirmarCompra(BECompraMayorista compra)
        {
            // 1. Cargamos tu lista maestra de stock acá en la BLL
            List<BEStock> listaStockTemp = bllStock.CargarStock();

            // 2. Aplicamos TU lógica exacta para restar stock (validando antes)
            foreach (var item in compra.ListaCarrito)
            {
                if (item.Producto is BEProductoIndividual individual)
                {
                    BEStock stockTemp = listaStockTemp.FirstOrDefault(s => s.Codigo == individual.Stock.Codigo);
                    if (stockTemp != null)
                    {
                        int cantidadARestar = individual.Unidad * item.Cantidad;

                        // Pequeña validación de seguridad
                        if (stockTemp.CantidadActual < cantidadARestar)
                            throw new Exception($"Stock insuficiente para {individual.Stock.Nombre}");

                        stockTemp.CantidadActual -= cantidadARestar;
                        stockTemp.CantidadReservada -= cantidadARestar;

                        mppStock.CantidadReservadaStock(stockTemp);
                    }
                }
                else if (item.Producto is BEProductoCombo combo)
                {
                    foreach (BEStock stockItem in combo.ListaProductos)
                    {
                        BEStock stockTemp = listaStockTemp.FirstOrDefault(s => s.Codigo == stockItem.Codigo);
                        if (stockTemp != null)
                        {
                            int cantidadARestar = 1 * item.Cantidad;

                            // Validación para el combo
                            if (stockTemp.CantidadActual < cantidadARestar)
                                throw new Exception($"Stock insuficiente para el componente {stockItem.Nombre} del combo.");

                            stockTemp.CantidadActual -= cantidadARestar;
                            stockTemp.CantidadReservada -= cantidadARestar;

                            mppStock.CantidadReservadaStock(stockTemp);
                        }
                    }
                }
            }

            // 3. Guardamos la compra en la base de datos (usando la MPP transaccional que armamos)
            mppCompraMayorista.GuardarCompraCompleta(compra);

            // 4. Actualizamos el stock masivamente, exactamente como lo tenías vos
            bllStock.ActualizarStock(listaStockTemp);
        }


    }
}
