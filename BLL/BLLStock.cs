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



        public void GuardarNuevoProducto(BEStock beStock, DateTime fechaDeVencimiento, int unidades)
        { mppStock.GuardarNuevoProducto(beStock, fechaDeVencimiento, unidades); }

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

        public void ActualizarStock(BECarrito carrito, List<BEStock> listaStock)
        {
            // Usamos pattern matching (el nombre de variable al lado del tipo) para ahorrar líneas
            if (carrito.Producto is BEProductoIndividual productoIndividual)
            {
                var productoEnStock = listaStock.Find(p => p.Codigo == productoIndividual.Stock.Codigo);
                int cantidadDisponible = productoEnStock.CantidadActual - productoEnStock.CantidadReservada;

                if (cantidadDisponible >= productoIndividual.Unidad)
                {
                    // 1. Actualizar carrito
                    carrito.Cantidad++;
                    carrito.Total += carrito.Producto.PrecioMayorista;

                    // 2. Aumentar la reserva maestra
                    productoEnStock.CantidadReservada = productoEnStock.CantidadReservada + carrito.Producto.Unidad;

                    // 3. ¡LA SINCRONIZACIÓN! (Para no perder el dato en el carrito temporal)
                    productoIndividual.Stock.CantidadReservada = productoEnStock.CantidadReservada;

                    // 4. Guardar la reserva individual
                    CantidadReservadaStock(productoEnStock);
                }
                else
                {
                    // Corregido el mensaje de error
                    throw new Exception($"No hay stock suficiente del producto: {productoIndividual.Stock.Nombre}");
                }
            }
            else if (carrito.Producto is BEProductoCombo beProductoCombo)
            {
                bool hayStockSuficiente = true;

                foreach (var productoEnCombo in beProductoCombo.ListaProductos)
                {
                    var productoEnStock = listaStock.Find(p => p.Codigo == productoEnCombo.Codigo);

                    // CORREGIDO: Ahora verifica la cantidad DISPONIBLE real, restando lo ya reservado
                    if (productoEnStock == null || (productoEnStock.CantidadActual - productoEnStock.CantidadReservada) < 1)
                    {
                        hayStockSuficiente = false;
                        break;
                    }
                }

                if (hayStockSuficiente)
                {
                    carrito.Cantidad++;
                    carrito.Total += carrito.Producto.PrecioMayorista;

                    foreach (var productoEnCombo in beProductoCombo.ListaProductos)
                    {
                        var productoEnStock = listaStock.Find(p => p.Codigo == productoEnCombo.Codigo);
                        if (productoEnStock != null)
                        {
                            productoEnStock.CantidadReservada++;

                            // ¡LA SINCRONIZACIÓN para los combos!
                            productoEnCombo.CantidadReservada = productoEnStock.CantidadReservada;

                            CantidadReservadaStock(productoEnStock);
                        }
                    }
                }
                else
                {
                    throw new Exception("No hay stock suficiente para agregar este combo.");
                }
            }

            // Dejamos un solo llamado general a la BD al final para optimizar rendimiento
            mppStock.ActualizarStock(listaStock);
        }

        public void DisminuirStockEnCarrito(BECarrito carrito, List<BEStock> listaStock)
        {
            if (carrito.Producto is BEProductoIndividual productoIndividual)
            {
                var productoEnStock = listaStock.Find(p => p.Codigo == productoIndividual.Stock.Codigo);

                carrito.Cantidad--;
                carrito.Total -= carrito.Producto.PrecioMayorista;

                productoEnStock.CantidadReservada = productoEnStock.CantidadReservada - carrito.Producto.Unidad;

                productoIndividual.Stock.CantidadReservada = productoEnStock.CantidadReservada;

                CantidadReservadaStock(productoEnStock);
            }
            else if (carrito.Producto is BEProductoCombo beProductoCombo)
            {
                carrito.Cantidad--;
                carrito.Total -= carrito.Producto.PrecioMayorista;

                foreach (var productoEnCombo in beProductoCombo.ListaProductos)
                {
                    var productoEnStock = listaStock.Find(p => p.Codigo == productoEnCombo.Codigo);
                    if (productoEnStock != null)
                    {
                        productoEnStock.CantidadReservada--;

                        productoEnCombo.CantidadReservada = productoEnStock.CantidadReservada;

                        CantidadReservadaStock(productoEnStock);
                    }
                }
            }
        }

        public void ActualizarStock(List<BEStock> listaStock)
        {
            // Solo hace de pasamanos hacia la base de datos para un guardado masivo
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
            if (beStock.AvisoCantidadStock < 0)
            {
                throw new ArgumentException("La cantidad para el aviso no puede ser menor a cero.");
            }
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
