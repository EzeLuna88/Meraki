using BE;
using BLL;
using QuestPDF.Fluent;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Meraki
{
    

    public partial class PedidosEditar : Form
    {
        private BEPedido pedidoActual;
        private List<BECarrito> listaCarritoEdicion;
        private BLLProducto bllProducto;
        private BLLPedido bllPedido;
        private BECarrito beCarrito;
        private BLLStock bllStock;
        private List<BEStock> listaStock;
        public string numeroPedidoActual;
        private bool modificarPrecio;

        public PedidosEditar(string numeroPedido)
        {
            InitializeComponent();
            beCarrito = new BECarrito();
            pedidoActual = new BEPedido();
            listaCarritoEdicion = new List<BECarrito>();
            bllProducto = new BLLProducto();
            bllPedido = new BLLPedido();
            bllStock = new BLLStock();
            numeroPedidoActual = numeroPedido;
            listaStock = bllStock.CargarStock();
            modificarPrecio = false;
        }

        private void CalcularTotal()
        {
            decimal total = 0;
            foreach (var producto in listaCarritoEdicion)
            {
                total += producto.Total;
            }

            // Guardamos el total en el pedido
            pedidoActual.Total = total;

            // Actualizamos el botón gigante de abajo con el mismo formato que en Compras
            buttonTotal.Text = "TOTAL: $ " + pedidoActual.Total.ToString("N2");
        }

        private void PedidosEditar_Load(object sender, EventArgs e)
        {
            try
            {
                CargarDataGrid();
                pedidoActual = bllPedido.ObtenerPedidoCompleto(numeroPedidoActual);
                CalendarioEnvio.SetDate(pedidoActual.FechaEnvio.Date);
                listaCarritoEdicion = new List<BECarrito>();
                List<BEProducto> todosLosProductos = bllProducto.listaProductos();
                foreach (BEItem itemGuardado in pedidoActual.ListaItems)
                {
                    // Buscamos el producto posta en el sistema para tener sus datos de stock y tipo (combo/ind)
                    BEProducto prodOriginal = todosLosProductos.FirstOrDefault(p => p.Codigo == itemGuardado.Codigo);

                    if (prodOriginal != null)
                    {
                        BECarrito carritoRow = new BECarrito
                        {
                            Producto = prodOriginal,
                            Cantidad = itemGuardado.Cantidad,
                            Total = itemGuardado.Subtotal
                        };
                        listaCarritoEdicion.Add(carritoRow);
                    }
                }
                dataGridViewCarrito.DataSource = null;
                dataGridViewCarrito.DataSource = listaCarritoEdicion;
                ConfigurarEstilosColumnasCarrito(dataGridViewCarrito);
                CalcularTotal();
                ComprobarCarritoParaModificar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar el pedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public void CargarDataGrid()
        {
            dataGridViewProductos.DataSource = null;

            var listaProductosOrdenada = bllProducto.listaProductos()
                .OrderBy(p => (p is BEProductoCombo combo) ? combo.Nombre : p.ToString())
                .ToList();

            dataGridViewProductos.DataSource = listaProductosOrdenada;

            // 🛠️ Mandamos a configurar cada grilla con su método correspondiente
            ConfigurarDataGridProductos(dataGridViewProductos);

            // El carrito ya se configura abajo en el Load con 'ConfigurarEstilosColumnasCarrito',
            // pero si querés asegurarte de que tenga el traje de Meraki, lo llamamos acá también:
            dataGridViewCarrito.AplicarEstiloMeraki();
        }

        public void ConfigurarDataGridProductos(DataGridView grilla)
        {
            // 1. Le ponemos el traje de Meraki a la grilla de productos (¡Acá estaba el error!)
            grilla.AplicarEstiloMeraki();

            // 2. Suscribimos el evento de forma segura
            grilla.CellFormatting -= dataGridViewProductos_CellFormatting;
            grilla.CellFormatting += dataGridViewProductos_CellFormatting;

            // 3. Asegurarse de que la columna "nombre" exista antes de ordenarla
            if (grilla.Columns["nombre"] == null)
            {
                grilla.Columns.Add("Nombre", "Nombre");
            }

            // 4. Ocultamos lo que no va para los productos en este panel chiquito
            grilla.Columns["Codigo"].Visible = false;
            grilla.Columns["Tipo"].Visible = false;
            grilla.Columns["Unidad"].Visible = false;
            grilla.Columns["NombreMostrar"].Visible = false;
            grilla.Columns["precioMinorista"].Visible = false;

            // 5. Ordenamos y damos formato a lo que sí se ve
            grilla.Columns["nombre"].DisplayIndex = 0;

            grilla.Columns["precioMayorista"].HeaderText = "Precio";
            grilla.Columns["precioMayorista"].DefaultCellStyle.Format = "c2";
            grilla.Columns["precioMayorista"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            grilla.Columns["precioMayorista"].Width = 80;
            grilla.Columns["precioMayorista"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            grilla.Columns["precioMayorista"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void dataGridViewProductos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewProductos.Columns["nombre"].Index)
            {
                var producto = dataGridViewProductos.Rows[e.RowIndex].DataBoundItem;
                if (producto != null)
                {
                    // Determina qué propiedad de la clase mostrar
                    if (producto is BEProductoIndividual)
                    {
                        BEProductoIndividual c1 = (BEProductoIndividual)producto;
                        e.Value = c1.ToString();
                    }
                    else if (producto is BEProductoCombo)
                    {
                        BEProductoCombo c2 = (BEProductoCombo)producto;
                        e.Value = c2.Nombre;
                    }
                }
            }
        }

        

        private void ConfigurarEstilosColumnasProductos(DataGridView dataGridView)
        {
            // Configuración de estilo para la columna "Unidad"
            dataGridView.Columns["Unidad"].HeaderText = "Un.";
            dataGridView.Columns["Unidad"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["Unidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["Unidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns["Unidad"].Width = 50;

            dataGridView.Columns["precioMayorista"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns["precioMayorista"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns["precioMayorista"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns["precioMayorista"].Width = 80;

        }

        private void ConfigurarEstilosColumnasCarrito(DataGridView dataGridView)
        {
            dataGridView.Columns["Cantidad"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["Cantidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns["Cantidad"].Width = 70;

            dataGridView.Columns["Total"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns["Total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns["Total"].Width = 80;

        }

        private void dataGridViewProductos_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewProductos.CurrentRow.DataBoundItem is BEProductoIndividual productoIndividual)
                {
                    ProcesarProductoIndividual(productoIndividual);
                }
                else if (dataGridViewProductos.CurrentRow.DataBoundItem is BEProductoCombo productoCombo)
                {
                    ProcesarProductoCombo(productoCombo);
                }
            }
            catch (Exception ex)
            {
                // Mostrar un mensaje de error o registrar el error según sea necesario
                MessageBox.Show($"Error al agregar el producto: {ex.Message}");
            }
        }

        private void ProcesarProductoIndividual(BEProductoIndividual producto)
        {
            try
            {
                var productoEnCarrito = listaCarritoEdicion.Find(p => p.Producto.Codigo == producto.Codigo);

                // Calculamos cuánto sería la cantidad si lo agregamos
                int cantidadActual = productoEnCarrito != null ? productoEnCarrito.Cantidad : 0;
                int cantidadDeseada = cantidadActual + 1;

                // EL PATOVICA:
                if (!HayStockDisponible(producto, cantidadDeseada))
                {
                    MessageBox.Show($"Límite de stock alcanzado para '{producto.ToString()}'.", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (productoEnCarrito != null)
                {
                    productoEnCarrito.Cantidad += 1;
                    productoEnCarrito.Total = productoEnCarrito.Cantidad * producto.PrecioMayorista;
                }
                else
                {
                    beCarrito = new BECarrito
                    {
                        Producto = producto,
                        Cantidad = 1, 
                        Total = producto.PrecioMayorista
                    };


                    listaCarritoEdicion.Add(beCarrito);
                }
                dataGridViewCarrito.DataSource = null;
                dataGridViewCarrito.DataSource = listaCarritoEdicion;
                ConfigurarEstilosColumnasCarrito(dataGridViewCarrito); // Asegurate de tener este método copiado
                CalcularTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso de Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ProcesarProductoCombo(BEProductoCombo productoCombo)
        {
            try
            {
                var productoEnCarrito = listaCarritoEdicion.Find(p => p.Producto.Codigo == productoCombo.Codigo);

                // Calculamos cuánto sería la cantidad si lo agregamos
                int cantidadActual = productoEnCarrito != null ? productoEnCarrito.Cantidad : 0;
                int cantidadDeseada = cantidadActual + 1;

                // EL PATOVICA:
                if (!HayStockDisponible(productoCombo, cantidadDeseada))
                {
                    MessageBox.Show($"Límite de stock alcanzado para '{productoCombo.ToString()}'.", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (productoEnCarrito != null)
                {
                    productoEnCarrito.Cantidad += 1;
                    productoEnCarrito.Total = productoEnCarrito.Cantidad * productoCombo.PrecioMayorista;
                }
                else
                {
                    BECarrito beCarrito = new BECarrito
                    {
                        Producto = productoCombo,
                        Cantidad = 1,
                        Total = productoCombo.PrecioMayorista
                    };

                    listaCarritoEdicion.Add(beCarrito);
                }

                // Refrescamos la grilla
                dataGridViewCarrito.DataSource = null;
                dataGridViewCarrito.DataSource = listaCarritoEdicion;
                ConfigurarEstilosColumnasCarrito(dataGridViewCarrito);

                CalcularTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al agregar producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void iconButtonPedidoGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (listaCarritoEdicion.Count == 0)
                {
                    MessageBox.Show("El pedido no puede quedar vacío.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult res = MessageBox.Show("¿Confirmás que querés guardar los cambios de este pedido?", "Guardar Cambios", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                {
                    // --- 1. GESTIÓN DE STOCK (El Trueque) ---
                    List<BEProducto> todosLosProductos = bllProducto.listaProductos();

                    // A. Devolvemos el stock original
                    foreach (BEItem itemViejo in pedidoActual.ListaItems)
                    {
                        BEProducto prodOriginal = todosLosProductos.FirstOrDefault(p => p.Codigo == itemViejo.Codigo);
                        if (prodOriginal != null)
                        {
                            BECarrito carritoViejo = new BECarrito { Producto = prodOriginal, Cantidad = itemViejo.Cantidad };
                            AjustarStock(carritoViejo, revertir: true);
                        }
                    }

                    // B. Reservamos el stock nuevo
                    foreach (BECarrito itemNuevo in listaCarritoEdicion)
                    {
                        AjustarStock(itemNuevo, revertir: false);
                    }

                    // --- 2. ACTUALIZAMOS EL BEPEDIDO ---
                    List<BEItem> itemsParaGuardar = new List<BEItem>();
                    foreach (BECarrito carrito in listaCarritoEdicion)
                    {
                        itemsParaGuardar.Add(new BEItem
                        {
                            Codigo = carrito.Producto.Codigo,
                            Cantidad = carrito.Cantidad,
                            Nombre = carrito.Producto.ToString(),
                            PrecioUnitario = carrito.Cantidad > 0 ? (carrito.Total / carrito.Cantidad) : 0,
                            Subtotal = carrito.Total
                        });
                    }
                    pedidoActual.ListaItems = itemsParaGuardar;

                    // 🛑 ¡EL ESCUDO LIMPIADOR DE PDF VIEJOS! 🛑
                    // Capturamos las dos fechas para compararlas
                    DateTime fechaVieja = pedidoActual.FechaEnvio.Date;
                    DateTime fechaNueva = CalendarioEnvio.SelectionStart.Date;

                    if (fechaVieja != fechaNueva)
                    {
                        // Si la fecha cambió, buscamos el PDF en la carpeta de la fecha vieja
                        string rutaBase = Properties.Settings.Default.CarpetaDestinoPDF;
                        string rutaViejaPDF = Servicios.GestorRutas.GenerarRutaDestino(
                            rutaBase,
                            fechaVieja,
                            "Pedidos",
                            pedidoActual.Numero
                        );

                        // Si el archivo viejo existe, lo eliminamos
                        if (System.IO.File.Exists(rutaViejaPDF))
                        {
                            try
                            {
                                System.IO.File.Delete(rutaViejaPDF);
                            }
                            catch (System.IO.IOException)
                            {
                                // Falla si el operario tiene el PDF viejo abierto en el Adobe Reader
                                MessageBox.Show("El pedido cambió de fecha, pero el PDF viejo está abierto en tu PC.\n\nPor favor, cerrá el PDF viejo y volvé a guardar para evitar duplicados en el depósito.",
                                                "Archivo en uso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return; // Frenamos acá para no romper nada
                            }
                        }
                    }

                    // Guardamos la nueva fecha en el objeto (ahora sí pisamos la vieja)
                    pedidoActual.FechaEnvio = fechaNueva;

                    // --- 3. IMPACTAMOS EN LA BASE DE DATOS Y PDF ---
                    bllPedido.ActualizarPedidoEditado(pedidoActual);

                    MessageBox.Show("¡El pedido se modificó con éxito!", "Meraki", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Genera el PDF nuevo, el cual automáticamente se guardará en la carpeta de la "fechaNueva"
                    GenerarPDFPedido(pedidoActual);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GenerarPDFPedido(BEPedido pedido)
        {
            try
            {
                string rutaBaseConfigurada = Properties.Settings.Default.CarpetaDestinoPDF;
                DateTime fechaDestino = CalendarioEnvio.SelectionStart.Date;

                // El servicio nos devuelve la misma ruta de siempre (Ej: ...\Pedidos\PEDI-1234.pdf)
                string rutaFinalPDF = Servicios.GestorRutas.GenerarRutaDestino(
                    rutaBaseConfigurada,
                    fechaDestino,
                    "Pedidos",
                    pedido.Numero
                );

                // 🛡️ ESCUDO CONTRA ARCHIVOS ABIERTOS
                // Verificamos si el archivo ya existe (es una edición) y si está bloqueado
                if (File.Exists(rutaFinalPDF))
                {
                    try
                    {
                        // Intentamos abrir el archivo en modo escritura. Si está abierto en otro lado, esto falla.
                        using (FileStream fs = File.OpenWrite(rutaFinalPDF)) { }
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("El PDF del pedido ya existe y está abierto en otro programa.\n\nPor favor, cerrá el PDF y volvé a guardar la edición.",
                                        "Archivo en uso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Cortamos la ejecución acá para que no explote la librería de PDF
                    }
                }

                // Si pasamos el escudo, pisamos el PDF con la versión nueva
                var documento = new Servicios.PedidosDocument(pedido);
                documento.GeneratePdf(rutaFinalPDF);

                // Opcional: Volvemos a abrir el PDF actualizado
                System.Diagnostics.Process.Start(rutaFinalPDF);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo generar o abrir el PDF de la Hoja de Pedido: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AjustarStock(BECarrito carrito, bool revertir)
        {
            BEProducto producto = carrito.Producto;

            if (producto is BEProductoIndividual productoIndividual)
            {
                var productoEnStock = listaStock.Find(p => p.Codigo == productoIndividual.Stock.Codigo);

                if (productoEnStock != null)
                {
                    // Multiplicar la unidad por la cantidad para obtener el ajuste total
                    int ajuste = productoIndividual.Unidad * carrito.Cantidad;

                    // Si estamos revirtiendo, el ajuste debe ser negativo
                    if (revertir)
                    {
                        ajuste = -ajuste;
                    }

                    // Aplicar el ajuste a la cantidad reservada
                    productoEnStock.CantidadReservada += ajuste;

                    // Actualizar el stock en la base de datos o donde corresponda
                    bllStock.CantidadReservadaStock(productoEnStock);
                }
            }
            else if (producto is BEProductoCombo productoCombo)
            {
                foreach (var stock in productoCombo.ListaProductos)
                {
                    var productoEnStock = listaStock.Find(p => p.Codigo == stock.Codigo);

                    if (productoEnStock != null)
                    {
                        int ajuste = revertir ? -carrito.Cantidad : carrito.Cantidad;
                        productoEnStock.CantidadReservada += ajuste;
                        bllStock.CantidadReservadaStock(productoEnStock);  // Asegúrate de actualizar si reservas en combos
                    }
                }
            }
        }

        private void iconButtonSacar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificamos que haya filas y que haya una seleccionada
                if (dataGridViewCarrito.Rows.Count > 0 && dataGridViewCarrito.CurrentRow != null)
                {
                    // 1. Agarramos el producto seleccionado de la grilla
                    BECarrito beCarritoSeleccionado = (BECarrito)dataGridViewCarrito.CurrentRow.DataBoundItem;

                    // 2. Le restamos 1 a la cantidad (todo en memoria)
                    beCarritoSeleccionado.Cantidad -= 1;

                    // 3. Recalculamos el subtotal de ese renglón
                    beCarritoSeleccionado.Total = beCarritoSeleccionado.Cantidad * beCarritoSeleccionado.Producto.PrecioMayorista;

                    // 4. Si la cantidad llegó a 0, lo volamos de nuestra lista temporal
                    if (beCarritoSeleccionado.Cantidad == 0)
                    {
                        listaCarritoEdicion.Remove(beCarritoSeleccionado);
                    }

                    // 5. Refrescamos la grilla visualmente
                    dataGridViewCarrito.DataSource = null;
                    dataGridViewCarrito.DataSource = listaCarritoEdicion;

                    // Asegurate de tener tu método de estilos acá, o cambialo por CargarDataGridCarrito() si lo armaste aparte
                    ConfigurarEstilosColumnasCarrito(dataGridViewCarrito);

                    // 6. Actualizamos el botón rojo grande con el total final
                    CalcularTotal();
                }
                else
                {
                    MessageBox.Show("El carrito está vacío o no seleccionaste ningún producto.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al quitar producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconButtonCarritoAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificamos que haya filas y que haya una seleccionada
                if (dataGridViewCarrito.Rows.Count > 0 && dataGridViewCarrito.CurrentRow != null)
                {
                    // 1. Agarramos el producto seleccionado de la grilla
                    BECarrito beCarritoSeleccionado = (BECarrito)dataGridViewCarrito.CurrentRow.DataBoundItem;

                    // PREGUNTAMOS: ¿Qué pasa si intento poner 1 más?
                    int cantidadDeseada = beCarritoSeleccionado.Cantidad + 1;

                    if (!HayStockDisponible(beCarritoSeleccionado.Producto, cantidadDeseada))
                    {
                        MessageBox.Show($"¡Límite de stock alcanzado! No podés llevar más de {beCarritoSeleccionado.Cantidad}.", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Cortamos acá, no dejamos que sume
                    }

                    // 2. Le sumamos 1 a la cantidad (todo en memoria)
                    beCarritoSeleccionado.Cantidad += 1;

                    // 3. Recalculamos el subtotal de ese renglón multiplicando por el precio
                    beCarritoSeleccionado.Total = beCarritoSeleccionado.Cantidad * beCarritoSeleccionado.Producto.PrecioMayorista;

                    // 4. Refrescamos la grilla visualmente para que se vea el nuevo número
                    dataGridViewCarrito.DataSource = null;
                    dataGridViewCarrito.DataSource = listaCarritoEdicion;

                    // Reaplicamos el ancho y centrado de las columnas
                    ConfigurarEstilosColumnasCarrito(dataGridViewCarrito);

                    // 5. Actualizamos el botón rojo gigante de abajo con la suma total
                    CalcularTotal();
                }
                else
                {
                    MessageBox.Show("El carrito está vacío o no seleccionaste ningún producto.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al sumar producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool HayStockDisponible(BEProducto productoAModificar, int cantidadDeseadaEnCarrito)
        {
            if (productoAModificar is BEProductoIndividual prodInd)
            {
                // 1. Buscamos el stock de la BD
                var stockReal = listaStock.Find(s => s.Codigo == prodInd.Stock.Codigo);
                if (stockReal == null) return false;

                // 2. Buscamos cuántos teníamos apartados en el pedido ORIGINAL (antes de editar)
                int cantidadOriginalEnPedido = 0;
                var itemViejo = pedidoActual.ListaItems.FirstOrDefault(i => i.Codigo == prodInd.Codigo);
                if (itemViejo != null)
                {
                    cantidadOriginalEnPedido = itemViejo.Cantidad;
                }

                // 3. Pasamos todo a unidades sueltas
                int unidadesAntiguas = cantidadOriginalEnPedido * prodInd.Unidad;
                int unidadesNuevas = cantidadDeseadaEnCarrito * prodInd.Unidad;

                // 4. LA FÓRMULA MÁGICA: (Actual - Reservado + Lo que devuelvo de este pedido)
                int stockDisponible = stockReal.CantidadActual - stockReal.CantidadReservada + unidadesAntiguas;

                // ¿Nos alcanza?
                return stockDisponible >= unidadesNuevas;
            }
            else if (productoAModificar is BEProductoCombo prodCombo)
            {
                // 1. Buscamos cuántos combos enteros teníamos apartados en el pedido ORIGINAL
                int cantidadOriginalCombos = 0;
                var itemViejo = pedidoActual.ListaItems.FirstOrDefault(i => i.Codigo == prodCombo.Codigo);
                if (itemViejo != null)
                {
                    cantidadOriginalCombos = itemViejo.Cantidad;
                }

                // 2. ¡LA MAGIA NUEVA!: Agrupamos los productos repetidos adentro del combo
                // Si la lista es [Fernet, Coca, Coca], esto crea un grupo de 1 Fernet y un grupo de 2 Cocas.
                var componentesAgrupados = prodCombo.ListaProductos.GroupBy(c => c.Codigo);

                foreach (var grupo in componentesAgrupados)
                {
                    string codigoComponente = grupo.Key; // El ID de la bebida (ej: la Coca)
                    int cantidadRequeridaPorUnCombo = grupo.Count(); // Cuántas veces aparece en el combo (ej: 2)

                    var stockReal = listaStock.Find(s => s.Codigo == codigoComponente);
                    if (stockReal != null)
                    {
                        // Pasamos todo a unidades sueltas matemáticas
                        int unidadesAntiguas = cantidadOriginalCombos * cantidadRequeridaPorUnCombo;
                        int unidadesNuevas = cantidadDeseadaEnCarrito * cantidadRequeridaPorUnCombo;

                        // Calculamos el stock disponible real
                        int stockDisponible = stockReal.CantidadActual - stockReal.CantidadReservada + unidadesAntiguas;

                        // Si el stock disponible no alcanza para todas las unidades juntas...
                        if (stockDisponible < unidadesNuevas)
                        {
                            return false; // Cortamos todo, no hay stock suficiente para armar estos combos
                        }
                    }
                }
                return true; // Si hay stock de todo, damos luz verde
            }
            return false;
        }

        private void ComprobarCarritoParaModificar()
        {
            if (modificarPrecio == true)
            {
                // Modo Edición: Ocultamos Modificar, mostramos Guardar
                iconButtonModificarPrecio.Visible = false;
                iconButtonGuardarPrecio.Visible = true;

                // Ponemos el fondo blanco para que parezca un cuadro de texto editable
                dataGridViewCarrito.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                dataGridViewCarrito.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.SteelBlue;
            }
            else
            {
                // Modo Lectura: Mostramos Modificar, ocultamos Guardar
                iconButtonModificarPrecio.Visible = true;
                iconButtonGuardarPrecio.Visible = false;

                // Volvemos a tu color rosado/bordó original
                dataGridViewCarrito.DefaultCellStyle.BackColor = ColoresMeraki.RosaPalido;
                dataGridViewCarrito.DefaultCellStyle.SelectionBackColor = ColoresMeraki.BordoPrincipal;
                dataGridViewCarrito.ReadOnly = true;
            }
        }

        private void iconButtonModificarPrecio_Click(object sender, EventArgs e)
        {
            if (dataGridViewCarrito.Rows.Count > 0)
            {
                // 1. Destrabamos toda la grilla
                dataGridViewCarrito.ReadOnly = false;

                // 2. Bloqueamos SOLO la columna del Producto para que no le cambien el nombre por error
                dataGridViewCarrito.Columns["Producto"].ReadOnly = true;
                dataGridViewCarrito.Columns["Cantidad"].ReadOnly = true;

                // 3. Levantamos la bandera de edición
                modificarPrecio = true;

                // 4. Llamamos al semáforo visual
                ComprobarCarritoParaModificar();
            }
            else
            {
                MessageBox.Show("El pedido se encuentra vacío.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void iconButtonGuardarPrecio_Click(object sender, EventArgs e)
        {
            // 1. FUNDAMENTAL: Si el usuario dejó el cursor titilando en la celda, forzamos a que guarde ese valor
            if (dataGridViewCarrito.IsCurrentCellInEditMode)
            {
                dataGridViewCarrito.EndEdit();
            }

            try
            {
                // 2. Recorremos fila por fila leyendo los números nuevos
                for (int i = 0; i < dataGridViewCarrito.Rows.Count; i++)
                {
                    DataGridViewRow fila = dataGridViewCarrito.Rows[i];

                    // Validamos que hayan escrito un número válido mayor a 0 en la Cantidad
                    if (int.TryParse(fila.Cells["Cantidad"].Value?.ToString(), out int nuevaCantidad) && nuevaCantidad > 0)
                    {
                        // Validamos que hayan escrito un monto de plata válido
                        if (decimal.TryParse(fila.Cells["Total"].Value?.ToString(), out decimal nuevoTotal))
                        {
                            var itemCarrito = listaCarritoEdicion[i];

                            // 3. ¡EL PATOVICA ATACA DE NUEVO!: Verificamos si hay stock para este número tipeado a mano
                            // Si tipeó "50" y no hay, lo frenamos.
                            if (!HayStockDisponible(itemCarrito.Producto, nuevaCantidad))
                            {
                                MessageBox.Show($"No hay suficiente stock para cubrir {nuevaCantidad} unidades de '{itemCarrito.Producto.ToString()}'.", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                continue; // Saltamos a la siguiente fila sin guardar este cambio puntual
                            }

                            // 4. Si el stock alcanza y los números están bien, actualizamos el producto "en memoria"
                            itemCarrito.Cantidad = nuevaCantidad;
                            itemCarrito.Total = Math.Round(nuevoTotal, 2);
                        }
                        else
                        {
                            MessageBox.Show("El valor ingresado en 'Total' no es un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El valor ingresado en 'Cantidad' no es válido. Debe ser un número mayor a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // 5. Bajamos la bandera de edición
                modificarPrecio = false;

                // 6. Volvemos al semáforo normal (bloquea la grilla y devuelve el color rosado)
                ComprobarCarritoParaModificar();

                // 7. Refrescamos la grilla para que quede prolija y limpie cualquier celda que haya dado error
                dataGridViewCarrito.DataSource = null;
                dataGridViewCarrito.DataSource = listaCarritoEdicion;
                ConfigurarEstilosColumnasCarrito(dataGridViewCarrito);

                // 8. Actualizamos el botón rojo gigante con el resultado final
                CalcularTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al guardar los precios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconButtonGenerarPresupuesto_Click(object sender, EventArgs e)
        {
            try
            {
                if (listaCarritoEdicion.Count == 0)
                {
                    MessageBox.Show("El pedido está vacío. No hay productos para presupuestar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                GenerarImagenPresupuesto();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar el presupuesto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GenerarImagenPresupuesto()
        {
            SaveFileDialog guardar = new SaveFileDialog
            {
                // Le ponemos un nombre por defecto lindo con el número de pedido
                FileName = "Presupuesto_Pedido_" + pedidoActual.Numero + ".jpg",
                Filter = "JPEG Image (*.jpg)|*.jpg",
                DefaultExt = "jpg"
            };

            // Traemos la plantilla HTML de tus recursos
            string paginaHtml = Properties.Resources.plantilla2.ToString();

            // 1. Reemplazamos los datos del cliente que ya vinieron cargados en el pedidoActual
            paginaHtml = paginaHtml.Replace("{{fechaComprobante}}", DateTime.Today.ToString("d"));
            paginaHtml = paginaHtml.Replace("{{nombreCliente}}", pedidoActual.Cliente.Nombre);
            paginaHtml = paginaHtml.Replace("{{direccionCliente}}", $"{pedidoActual.Cliente.Direccion}, {pedidoActual.Cliente.Localidad}");
            paginaHtml = paginaHtml.Replace("{{telefonoCliente}}", $"{pedidoActual.Cliente.Telefono} / {pedidoActual.Cliente.TelefonoAlternativo}");
            paginaHtml = paginaHtml.Replace("{{horarioApertura}}", pedidoActual.Cliente.HorarioDeApertura.ToString());
            paginaHtml = paginaHtml.Replace("{{horarioCierre}}", pedidoActual.Cliente.HorarioDeCierre.ToString());

            // 2. Armamos las filas de la tabla leyendo nuestra lista en memoria
            string filasItems = string.Empty;
            int cantidad = 0;

            foreach (BECarrito item in listaCarritoEdicion)
            {
                cantidad += item.Cantidad;
                filasItems += "<tr>";
                filasItems += $"<td>{item.Producto.Codigo}</td>";
                filasItems += $"<td class='producto'>{item.Producto.ToString()}</td>";

                // Como pudimos haber modificado el precio, calculamos el precio unitario real dividiendo el Total por la Cantidad
                decimal precioUnitarioReal = item.Cantidad > 0 ? (item.Total / item.Cantidad) : item.Producto.PrecioMayorista;

                filasItems += $"<td>{precioUnitarioReal.ToString("N2")}</td>";
                filasItems += $"<td>{item.Cantidad}</td>";
                filasItems += $"<td class='precio'>{item.Total.ToString("N2")}</td>";
                filasItems += "</tr>";
            }

            // 3. Reemplazamos totales
            paginaHtml = paginaHtml.Replace("{{productosCarrito}}", filasItems);
            paginaHtml = paginaHtml.Replace("{{cantidadTotalProductos}}", cantidad.ToString());
            paginaHtml = paginaHtml.Replace("{{totalCompra}}", pedidoActual.Total.ToString("N2"));

            // 4. El motor del navegador oculto que "saca la foto"
            WebBrowser webBrowser = new WebBrowser
            {
                ScrollBarsEnabled = false,
                ScriptErrorsSuppressed = true,
                Width = 827,
                Height = 1169 // Tamaño A4 estándar
            };

            webBrowser.DocumentText = paginaHtml;

            // Este evento se dispara solo cuando el HTML termina de cargar en memoria
            webBrowser.DocumentCompleted += (s, ev) =>
            {
                Bitmap bitmap = new Bitmap(webBrowser.Width, webBrowser.Height);
                webBrowser.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height));

                if (guardar.ShowDialog() == DialogResult.OK)
                {
                    bitmap.Save(guardar.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    MessageBox.Show("Imagen guardada correctamente.", "Meraki", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        // Abrimos la imagen recién creada para que la vea el cliente/vendedor
                        System.Diagnostics.Process.Start(guardar.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo abrir el archivo de imagen: " + ex.Message);
                    }
                }

                // Limpiamos el componente de memoria
                webBrowser.Dispose();
            };
        }

        private void iconButtonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
    }
}
