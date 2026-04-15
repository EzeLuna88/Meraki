using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Servicios;



namespace Meraki
{
    public partial class CompraMayorista : Form
    {
        BLLCliente bllCliente;

        BLLProducto bllProducto;
        BECompraMayorista beCompraMayorista;
        BECarrito beCarrito;
        BEItem beItem;
        BLLStock bllStock;
        List<BEStock> listaStock;
        BLLCompraMayorista bllCompraMayorista;
        BLLComprobante bllComprobante;
        BEComprobante beComprobante;
        List<BEComprobante> listaComprobantes;
        BECliente clienteSeleccionado;

        bool ModificarPrecio;
        bool ModificarCantidad;
        private BindingSource bindingSourceClientes = new BindingSource();
        private const string placeholderText = "   Buscar...";
        int cantidadAnteriorEditada;
        bool cargandoGrilla = false;

        public CompraMayorista()
        {
            bllStock = new BLLStock();
            listaStock = new List<BEStock>();
            listaStock = bllStock.CargarStock();
            listaComprobantes = new List<BEComprobante>();
            beCompraMayorista = new BECompraMayorista();

            beComprobante = new BEComprobante();
            bllProducto = new BLLProducto();
            bllCompraMayorista = new BLLCompraMayorista();
            bllCliente = new BLLCliente();
            bllComprobante = new BLLComprobante();
            clienteSeleccionado = new BECliente();
            ModificarPrecio = false;
            ModificarCantidad = false;

            InitializeComponent();

            iconButtonAlertaPocoStock.Visible = false;
            iconButtonVencimiento.Visible = false;
            CargarDataGrid();
            CargarDataGridClientes();
            ComprobarCarritoParaModificar();
            ConfigurarDataGrid(dataGridViewCarrito);
            this.Click += CompraMayorista_Click;
            this.Click += tableLayoutPanel1_Click;
            ComprobarBajoStock();
            comprobarVencimiento();
        }



        public void CargarDataGrid()
        {

            dataGridViewProductos.DataSource = null;
            var listaProductosOrdenada = bllProducto.listaProductos()
        .OrderBy(p => (p is BEProductoCombo combo) ? combo.Nombre : p.ToString())
        .ToList();

            dataGridViewProductos.DataSource = listaProductosOrdenada;
            dataGridViewProductos.Columns["Codigo"].Visible = false;
            dataGridViewProductos.Columns["Tipo"].Visible = false;
            dataGridViewProductos.Columns["Unidad"].Visible = false;
            dataGridViewProductos.Columns["NombreMostrar"].Visible = false;
            if (dataGridViewProductos.Columns["nombre"] == null)
            {
                dataGridViewProductos.Columns.Add("Nombre", "Nombre");
            }
            dataGridViewProductos.Columns["nombre"].DisplayIndex = 0;
            dataGridViewProductos.CellFormatting += dataGridViewProductos_CellFormatting;
            dataGridViewProductos.Columns["precioMinorista"].Visible = false;
            dataGridViewProductos.Columns["precioMayorista"].HeaderText = "Precio";
            dataGridViewProductos.Columns["precioMayorista"].DefaultCellStyle.Format = "c2";

            ConfigurarDataGrid(dataGridViewProductos);
            ConfigurarEstilosColumnasProductos(dataGridViewProductos);
        }


        public void ConfigurarDataGrid(DataGridView dataGridView)
        {
            // Configuración general del DataGridView
            dataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(217, 171, 171);
            dataGridView.RowHeadersVisible = false;
            dataGridView.Font = new System.Drawing.Font("Segoe UI", 8);
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.RowTemplate.Height = 20;
            dataGridView.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(146, 26, 64);
            dataGridView.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = dataGridView.ColumnHeadersDefaultCellStyle.BackColor;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = dataGridView.ColumnHeadersDefaultCellStyle.ForeColor;

            // Configuración específica de estilo para columnas
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

        private void ProcesarProductoIndividual(BEProductoIndividual producto)
        {
            try
            {
                var productoEnCarrito = beCompraMayorista.ListaCarrito.Find(p => p.Producto.Codigo == producto.Codigo);

                if (productoEnCarrito != null)
                {
                    bllStock.ActualizarStock(productoEnCarrito, listaStock);
                }
                else
                {
                    beCarrito = new BECarrito
                    {
                        Producto = producto,
                        Cantidad = 0, // ¡Lo ponemos en 0 porque la BLL le va a sumar 1!
                        Total = 0
                    };

                    bllStock.ActualizarStock(beCarrito, listaStock);

                    beCompraMayorista.ListaCarrito.Add(beCarrito);
                }

                bllCliente.GuardarCompraMayoristaTemporal(clienteSeleccionado, beCompraMayorista);
                CalcularTotal();
                ActualizarInterfaz();
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
                var productoEnCarrito = beCompraMayorista.ListaCarrito.Find(p => p.Producto.Codigo == productoCombo.Codigo);

                if (productoEnCarrito != null)
                {
                    bllStock.ActualizarStock(productoEnCarrito, listaStock);
                }
                else
                {
                    beCarrito = new BECarrito
                    {
                        Producto = productoCombo,
                        Cantidad = 0,
                        Total = 0
                    };

                    bllStock.ActualizarStock(beCarrito, listaStock);

                    beCompraMayorista.ListaCarrito.Add(beCarrito);
                }
                bllCliente.GuardarCompraMayoristaTemporal(clienteSeleccionado, beCompraMayorista);
                CalcularTotal();
                ActualizarInterfaz();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso de Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ActualizarInterfaz()
        {
            CargarDataGridClientes();
            CargarDataGridCarrito();
            CalcularTotal();
            ComprobarBajoStock();
            comprobarVencimiento();
        }


        private void CompraMayorista_Load(object sender, EventArgs e)
        {
            // 1. Empezamos de cero, sin clientes seleccionados
            clienteSeleccionado = null;



            // 3. Llamamos a la grilla para que se dibuje y seleccione
            CargarDataGridClientes();

            // 4. Textos de los buscadores
            textBoxFiltrar.Text = placeholderText;
            textBoxFiltrar.ForeColor = System.Drawing.Color.Gray;
            textBoxFiltrarCliente.Text = placeholderText;
            textBoxFiltrarCliente.ForeColor = System.Drawing.Color.Gray;
        }


        public void limpiarCompraMayorista()
        {
            beCompraMayorista.ListaCarrito.Clear();
            clienteSeleccionado.CompraMayoristaTemp.ListaCarrito.Clear();
            bllCliente.GuardarCompraMayoristaTemporal(clienteSeleccionado, beCompraMayorista);
            beCompraMayorista.Total = 0;
            dataGridViewCarrito.DataSource = null;
            dataGridViewCarrito.DataSource = beCompraMayorista.ListaCarrito;
            dataGridViewCarrito.Columns["Total"].DefaultCellStyle.Format = "c2";
            buttonTotal.Text = clienteSeleccionado.Nombre +
                               " // TOTAL: $ " +
                               beCompraMayorista.Total.ToString("N2");
        }

        public void CargarDataGridClientes()
        {
            // 1. Bloqueamos el inicio para evitar ruidos mientras cargamos el DataSource
            cargandoGrilla = true;

            // 2. Definimos qué cliente buscar (Mostrador por defecto o el seleccionado)
            string codigoABuscar = (clienteSeleccionado != null && !string.IsNullOrEmpty(clienteSeleccionado.Codigo))
                ? clienteSeleccionado.Codigo
                : Properties.Settings.Default.ClientePorDefecto;

            // 3. Obtenemos y preparamos la lista de clientes
            List<BECliente> listClientes = bllCliente.ListaClientes().OrderBy(c => c.Nombre).ToList();
            foreach (var c in listClientes)
            {
                if (c.CompraMayoristaTemp == null) c.CompraMayoristaTemp = new BECompraMayorista();
            }

            // 4. Asignamos los datos a la grilla
            bindingSourceClientes.DataSource = new BindingList<BECliente>(listClientes);
            dataGridViewClientes.DataSource = bindingSourceClientes;

            // 5. Configuración visual (ocultar IDs y columnas técnicas)
            dataGridViewClientes.Columns[0].Visible = false;
            dataGridViewClientes.Columns[4].Visible = false;
            dataGridViewClientes.Columns[5].Visible = false;
            dataGridViewClientes.Columns[6].Visible = false;
            dataGridViewClientes.Columns[7].Visible = false;
            dataGridViewClientes.Columns[8].Visible = false;
            dataGridViewClientes.Columns[9].Visible = false;
            dataGridViewClientes.AllowUserToAddRows = false;
            ConfigurarDataGrid(dataGridViewClientes);

            // --- EL CAMBIO CRÍTICO: Bajamos el escudo ANTES de la selección manual ---
            cargandoGrilla = false;

            // 6. BUSQUEDA Y SELECCIÓN DEL MOSTRADOR
            if (!string.IsNullOrEmpty(codigoABuscar))
            {
                dataGridViewClientes.ClearSelection();
                bool encontrado = false;

                foreach (DataGridViewRow row in dataGridViewClientes.Rows)
                {
                    if (row.DataBoundItem is BECliente cliente && cliente.Codigo?.Trim() == codigoABuscar.Trim())
                    {
                        // Al asignar esto, como cargandoGrilla es false, el evento SE DISPARA SOLO
                        dataGridViewClientes.CurrentCell = row.Cells[1];
                        row.Selected = true;

                        if (row.Index >= 0)
                            dataGridViewClientes.FirstDisplayedScrollingRowIndex = row.Index;

                        encontrado = true;
                        break;
                    }
                }

                // Si el cliente no existe (ej. lo borraste), seleccionamos el primero
                if (!encontrado && dataGridViewClientes.Rows.Count > 0)
                {
                    dataGridViewClientes.CurrentCell = dataGridViewClientes.Rows[0].Cells[1];
                }
            }
            else if (dataGridViewClientes.Rows.Count > 0)
            {
                dataGridViewClientes.CurrentCell = dataGridViewClientes.Rows[0].Cells[1];
            }

            // 7. Refuerzo final: nos aseguramos de que la UI esté sincronizada
            dataGridViewClientes_SelectionChanged_1(dataGridViewClientes, EventArgs.Empty);
        }


        public void GenerarPDF(BEComprobante comprobante)
        {

            SaveFileDialog guardar = new SaveFileDialog
            {
                FileName = comprobante.Numero + ".pdf",
                Filter = "PDF Files (*.pdf)|*.pdf",
                DefaultExt = "pdf"
            };

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    // 2. Usar tu nueva clase
                    var documento = new Servicios.ComprobanteDocument(comprobante);

                    // 3. Generar y guardar
                    documento.GeneratePdf(guardar.FileName);

                    // 4. Abrir
                    System.Diagnostics.Process.Start(guardar.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo generar o abrir el PDF: " + ex.Message);
                }
            }
        }


        private void CalcularTotal()
        {
            decimal total = 0;
            foreach (var producto in beCompraMayorista.ListaCarrito)
            {
                total += producto.Total;
                beCompraMayorista.Total = total;

            }
            buttonTotal.Text = clienteSeleccionado.Nombre +
                               " // TOTAL: $ " +
                               beCompraMayorista.Total.ToString("N2");
        }

        private void CargarDataGridCarrito()
        {

            dataGridViewCarrito.DataSource = null;
            dataGridViewCarrito.DataSource = beCompraMayorista.ListaCarrito;
            dataGridViewCarrito.Columns["Total"].DefaultCellStyle.Format = "c2";
            if (!ModificarPrecio)
            {
                dataGridViewCarrito.ReadOnly = true;

            }
            ConfigurarDataGrid(dataGridViewCarrito);
            ConfigurarEstilosColumnasCarrito(dataGridViewCarrito);

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

        private void ComprobarCarritoParaModificar()
        {
            if (ModificarPrecio == true)
            {
                iconButtonModificarPrecio.Visible = false;
                iconButtonGuardarPrecio.Visible = true;
                dataGridViewCarrito.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                dataGridViewCarrito.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Blue;

            }
            else
            {

                iconButtonModificarPrecio.Visible = true;
                iconButtonGuardarPrecio.Visible = false;
                dataGridViewCarrito.BackColor = System.Drawing.Color.LightGray;
                dataGridViewCarrito.DefaultCellStyle.BackColor = System.Drawing.Color.Gray;
                dataGridViewCarrito.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Gray;
                dataGridViewCarrito.ReadOnly = true;
            }
        }

        private void checkBoxCarritosTemporales_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCarritosTemporales.Checked)
            {
                // Filtrar clientes que tienen carritos temporales no vacíos
                var clientesConCarritos = bllCliente.ListaClientes()
                    .Where(c => c.CompraMayoristaTemp != null && c.CompraMayoristaTemp.ListaCarrito.Count > 0)
                    .OrderBy(c => c.Nombre)
                    .ToList();

                // Actualizar el BindingSource con la lista filtrada
                bindingSourceClientes.DataSource = clientesConCarritos;

                if (dataGridViewClientes.Rows.Count > 1)
                {


                }


            }
            else
            {
                // Mostrar todos los clientes si el checkbox no está marcado
                var todosClientes = bllCliente.ListaClientes()
                    .OrderBy(c => c.Nombre)
                    .ToList();

                // Actualizar el BindingSource con la lista completa
                bindingSourceClientes.DataSource = todosClientes;
                dataGridViewClientes.Rows[0].Selected = true;
                MostrarCarritoDelCliente((BECliente)dataGridViewClientes.Rows[0].DataBoundItem);

            }

            // Refrescar el DataGridView para mostrar los resultados filtrados
            dataGridViewClientes.Refresh();

        }

        private void MostrarCarritoDelCliente(BECliente cliente)
        {
            dataGridViewCarrito.DataSource = cliente.CompraMayoristaTemp.ListaCarrito;

        }

        private void dataGridViewClientes_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    // Obtén el cliente correspondiente a la fila seleccionada
                    clienteSeleccionado = (BECliente)dataGridViewClientes.Rows[e.RowIndex].DataBoundItem;

                    // Verifica si CompraMayoristaTemp está inicializada
                    if (clienteSeleccionado.CompraMayoristaTemp == null)
                    {
                        clienteSeleccionado.CompraMayoristaTemp = new BECompraMayorista();
                    }

                    // Inicializa o limpia el carrito
                    dataGridViewCarrito.DataSource = null;

                    // Verifica si el carrito tiene elementos
                    if (clienteSeleccionado.CompraMayoristaTemp.ListaCarrito != null && clienteSeleccionado.CompraMayoristaTemp.ListaCarrito.Count > 0)
                    {
                        dataGridViewCarrito.DataSource = clienteSeleccionado.CompraMayoristaTemp.ListaCarrito;
                        dataGridViewCarrito.Columns["Total"].DefaultCellStyle.Format = "c2";
                        dataGridViewCarrito.Columns["Cantidad"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridViewCarrito.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridViewCarrito.Columns["Cantidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dataGridViewCarrito.Columns["Cantidad"].Width = 70;

                        dataGridViewCarrito.Columns["Total"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridViewCarrito.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridViewCarrito.Columns["Total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dataGridViewCarrito.Columns["Total"].Width = 80;
                        beCompraMayorista = clienteSeleccionado.CompraMayoristaTemp;
                    }
                    else
                    {
                        // Inicializa beCompraMayorista si es necesario
                        beCompraMayorista = new BECompraMayorista();
                    }
                    CalcularTotal();
                    // Actualiza el total
                    buttonTotal.Text = clienteSeleccionado.Nombre +
                                       " // TOTAL: $ " +
                                       beCompraMayorista.Total.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores más específico para depuración
                MessageBox.Show("Error al seleccionar cliente: " + ex.Message);
            }
        }

        private void textBoxFiltrar_TextChanged_1(object sender, EventArgs e)
        {
            string filtro = textBoxFiltrar.Text.ToLower();

            // Verificar si el texto es el placeholder o si está vacío
            if (string.IsNullOrWhiteSpace(filtro) || filtro == "   buscar...")
            {
                // Mostrar todos los productos si no hay filtro o el filtro es el placeholder
                dataGridViewProductos.DataSource = bllProducto.listaProductos();
            }
            else
            {
                // Filtrar los productos basados en el texto de búsqueda
                List<BEProducto> productosFiltrados = new List<BEProducto>();

                foreach (BEProducto producto in bllProducto.listaProductos())
                {
                    if (producto.ToString().ToLower().Contains(filtro))
                    {
                        productosFiltrados.Add(producto);
                    }
                }

                dataGridViewProductos.DataSource = productosFiltrados;
            }

            // Actualizar las columnas del DataGridView
            ConfigurarColumnasDataGridView();
        }

        private void ConfigurarColumnasDataGridView()
        {
            dataGridViewProductos.Columns["Codigo"].Visible = false;
            dataGridViewProductos.Columns["Tipo"].Visible = false;
            dataGridViewProductos.Columns["Unidad"].HeaderText = "Unidades";

            if (dataGridViewProductos.Columns["nombre"] == null)
            {
                dataGridViewProductos.Columns.Add("nombre", "nombre");
            }

            dataGridViewProductos.Columns["nombre"].DisplayIndex = 0;
            dataGridViewProductos.CellFormatting += dataGridViewProductos_CellFormatting;
            dataGridViewProductos.Columns["precioMinorista"].Visible = false;
            dataGridViewProductos.Columns["precioMayorista"].HeaderText = "Precio";
            dataGridViewProductos.Columns["precioMayorista"].DefaultCellStyle.Format = "c2";
        }

        private void textBoxFiltrarCliente_TextChanged(object sender, EventArgs e)
        {
            string textoABuscar = textBoxFiltrarCliente.Text.ToLower();

            // Verificar si el texto es el placeholder o si está vacío
            if (string.IsNullOrWhiteSpace(textoABuscar) || textoABuscar == "   buscar...")
            {
                // Mostrar todos los clientes si no hay filtro o el filtro es el placeholder
                bindingSourceClientes.DataSource = bllCliente.ListaClientes().OrderBy(c => c.Nombre).ToList();
            }
            else
            {
                // Filtrar clientes por nombre o dirección
                bindingSourceClientes.DataSource = bllCliente.ListaClientes()
                    .Where(c => c.Nombre.ToLower().Contains(textoABuscar) ||
                                c.Direccion.ToLower().Contains(textoABuscar))
                    .OrderBy(c => c.Nombre)
                    .ToList();
            }

            if (bindingSourceClientes.Count > 0)
            {
                // Seleccionar la primera fila si hay resultados


            }
            dataGridViewClientes.Columns[0].Visible = false;
            // Refrescar el DataGridView para mostrar los resultados filtrados
            dataGridViewClientes.Refresh();
        }

        private void dataGridViewClientes_SelectionChanged_1(object sender, EventArgs e)
        {
            // Escudos de protección
            if (cargandoGrilla || dataGridViewClientes.CurrentRow == null) return;

            try
            {
                clienteSeleccionado = (BECliente)dataGridViewClientes.CurrentRow.DataBoundItem;

                // Verifica si CompraMayoristaTemp está inicializada
                if (clienteSeleccionado.CompraMayoristaTemp == null)
                {
                    clienteSeleccionado.CompraMayoristaTemp = new BECompraMayorista();
                }

                // Inicializa o limpia el carrito visualmente
                dataGridViewCarrito.DataSource = null;

                // Verifica si el carrito tiene elementos guardados
                if (clienteSeleccionado.CompraMayoristaTemp.ListaCarrito != null && clienteSeleccionado.CompraMayoristaTemp.ListaCarrito.Count > 0)
                {
                    // Cargar los productos del carrito en el DataGridView
                    dataGridViewCarrito.DataSource = clienteSeleccionado.CompraMayoristaTemp.ListaCarrito;

                    // Configuración de formato y alineación de las columnas
                    dataGridViewCarrito.Columns["Total"].DefaultCellStyle.Format = "c2";
                    dataGridViewCarrito.Columns["Cantidad"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridViewCarrito.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridViewCarrito.Columns["Cantidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridViewCarrito.Columns["Cantidad"].Width = 70;

                    dataGridViewCarrito.Columns["Total"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewCarrito.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewCarrito.Columns["Total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridViewCarrito.Columns["Total"].Width = 80;

                    // Actualizar el objeto general con el carrito del cliente
                    beCompraMayorista = clienteSeleccionado.CompraMayoristaTemp;
                }
                else
                {
                    // Si el carrito está vacío, inicializa uno nuevo
                    beCompraMayorista = new BECompraMayorista();
                }

                CalcularTotal();

                // Actualiza el botón grande de abajo
                if (clienteSeleccionado != null)
                {
                    buttonTotal.Text = clienteSeleccionado.Nombre + " // TOTAL: $ " + beCompraMayorista.Total.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar cliente: " + ex.Message);
            }
        }

        private void iconButtonSacar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewCarrito.Rows.Count > 0)
                {
                    beCarrito = (BECarrito)dataGridViewCarrito.CurrentRow.DataBoundItem;

                    // 1. Delegamos a la BLL la tarea de restar la reserva y actualizar totales
                    bllStock.DisminuirStockEnCarrito(beCarrito, listaStock);

                    // 2. ¿El producto llegó a cero? Lo borramos de la lista visual
                    if (beCarrito.Cantidad == 0)
                    {
                        beCompraMayorista.ListaCarrito.Remove(beCarrito);
                    }

                    // 3. Refrescamos la pantalla
                    CargarDataGridCarrito();
                    CalcularTotal();

                    // 4. Guardamos el temporal
                    bllCliente.GuardarCompraMayoristaTemporal(clienteSeleccionado, beCompraMayorista);
                }
                else
                {
                    MessageBox.Show("El carrito está vacío.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al quitar producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconButtonConfirmarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewCarrito.Rows.Count > 0)
                {
                    // 1. Validar forma de pago (Tu código original)
                    if (radioButtonEfectivo.Checked)
                    {
                        beComprobante.PagoEfectivo = true;
                    }
                    else if (radioButtonTransferencia.Checked)
                    {
                        beComprobante.PagoEfectivo = false;
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar forma de pago");
                        return;
                    }

                    DialogResult dialogResult = MessageBox.Show("Desea confirmar la compra? El total es de $ " + beCompraMayorista.Total.ToString(), "Confirmar compra", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        // 2. LA MAGIA NUEVA: Le pedimos a la BLL que descuente el stock y guarde la compra
                        bllCompraMayorista.ConfirmarCompra(beCompraMayorista);

                        // 3. RECUPERAMOS EL COMPROBANTE: Armamos el objeto con tu lógica original
                        beComprobante.Cliente = clienteSeleccionado;
                        beComprobante.Fecha = DateTime.Now;
                        beComprobante.Total = beCompraMayorista.Total;

                        // Generador de número de comprobante
                        listaComprobantes = bllComprobante.listaComprobantes();
                        if (listaComprobantes.Count == 0)
                        {
                            beComprobante.Numero = "00000001";
                        }
                        else
                        {
                            int numeroMaximo = 0;
                            foreach (var comprobante in listaComprobantes)
                            {
                                if (int.TryParse(comprobante.Numero, out int numeroComprobante))
                                {
                                    if (numeroComprobante > numeroMaximo)
                                    {
                                        numeroMaximo = numeroComprobante;
                                    }
                                }
                            }
                            beComprobante.Numero = (numeroMaximo + 1).ToString().PadLeft(8, '0');
                        }

                        // Pasar los items del carrito al comprobante
                        List<BEItem> items = new List<BEItem>();
                        foreach (BECarrito item in beCompraMayorista.ListaCarrito)
                        {
                            beItem = new BEItem
                            {
                                Codigo = item.Producto.Codigo,
                                Cantidad = item.Cantidad,
                                Nombre = item.Producto.ToString(),
                                Precio = Convert.ToDecimal(item.Total)
                            };
                            items.Add(beItem);
                        }
                        beComprobante.ListaItems = items;

                        // 4. GUARDAR Y GENERAR EL PDF
                        bllComprobante.GuardarNuevoComprobante(beComprobante);
                        GenerarPDF(beComprobante);

                        bllStock.DescontarStockPorVencimiento(beCompraMayorista);
                        listaStock = bllStock.CargarStock();

                        // 5. Limpieza y avisos finales
                        limpiarCompraMayorista();
                        ComprobarBajoStock();
                        clienteSeleccionado = null;
                        CargarDataGridClientes();
                    }
                }
                else
                {
                    MessageBox.Show("El carrito se encuentra vacio");
                }
            }
            catch (Exception ex)
            {
                // Si no hay stock o falla la base de datos, el código frena y cae acá
                MessageBox.Show(ex.Message, "Error en la operación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComprobarBajoStock()
        {
            List<string> productosBajoStock = bllStock.ObtenerProductosConStockBajo();
            if (productosBajoStock.Any())
            {
                iconButtonAlertaPocoStock.Visible = true;

            }
        }

        private void iconButtonModificarPrecio_Click(object sender, EventArgs e)
        {

            if (dataGridViewCarrito.Rows.Count > 0)
            {
                dataGridViewCarrito.ReadOnly = false;
                dataGridViewCarrito.Columns["Producto"].ReadOnly = true;
                ModificarPrecio = true;
                ComprobarCarritoParaModificar();
            }
            else
            { MessageBox.Show("El pedido se encuentra vacio"); }
        }

        private void dataGridViewCarrito_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewCarrito.Rows.Count)
                return;

            var fila = dataGridViewCarrito.Rows[e.RowIndex];
            if (fila.IsNewRow) return;

            string nombreColumna = dataGridViewCarrito.Columns[e.ColumnIndex].Name;

            if (nombreColumna == "Cantidad")
            {
                var valorCelda = fila.Cells["Cantidad"].Value;

                // Si la celda quedó vacía, no hacer nada
                if (valorCelda == null || string.IsNullOrWhiteSpace(valorCelda.ToString()))
                    return;

                if (int.TryParse(valorCelda.ToString(), out int nuevaCantidad) && nuevaCantidad > 0)
                {
                    var itemCarrito = beCompraMayorista.ListaCarrito[e.RowIndex];

                    if (itemCarrito.Producto.Tipo == "individual")
                    {
                        BEProductoIndividual producto = (BEProductoIndividual)itemCarrito.Producto;

                        int devolverStock = cantidadAnteriorEditada * producto.Unidad;
                        int nuevaCantidadReservada = producto.Stock.CantidadReservada - devolverStock;
                        int nuevaCantidadUnidades = nuevaCantidad * producto.Unidad;

                        if (producto.Stock.CantidadActual >= nuevaCantidadReservada + nuevaCantidadUnidades)
                        {
                            producto.Stock.CantidadReservada = nuevaCantidadReservada + nuevaCantidadUnidades;
                            bllStock.CantidadReservadaStock(producto.Stock); // si tenés persistencia acá

                        }
                        else
                        {
                            MessageBox.Show("No hay suficiente stock");
                            fila.Cells["Cantidad"].Value = cantidadAnteriorEditada;
                            return;
                        }

                    }
                    else if (itemCarrito.Producto.Tipo == "combo")
                    {
                        var productoCombo = (BEProductoCombo)itemCarrito.Producto;
                        bool hayStockSuficiente = true;

                        // Por cada unidad de stock que compone el combo...
                        int cantidadAnterior = itemCarrito.Cantidad;

                        foreach (BEStock stockEnCombo in productoCombo.ListaProductos)
                        {
                            int stockDisponible = stockEnCombo.CantidadActual - stockEnCombo.CantidadReservada + cantidadAnterior;

                            if (stockDisponible < nuevaCantidad)
                            {
                                hayStockSuficiente = false;
                                break;
                            }
                        }

                        if (!hayStockSuficiente)
                        {
                            MessageBox.Show("No hay suficiente stock para uno o más productos del combo.");
                            fila.Cells["Cantidad"].Value = itemCarrito.Cantidad;
                            return;
                        }

                        // Si hay stock, actualizar reservas
                        foreach (BEStock stockEnCombo in productoCombo.ListaProductos)
                        {
                            stockEnCombo.CantidadReservada = stockEnCombo.CantidadReservada - cantidadAnterior + nuevaCantidad;
                            bllStock.CantidadReservadaStock(stockEnCombo);
                        }

                        // ... luego actualizás cantidad y total del carrito como antes
                    }

                    itemCarrito.Cantidad = nuevaCantidad;

                    decimal nuevoTotal = Math.Round(itemCarrito.Producto.PrecioMayorista * nuevaCantidad, 2);
                    itemCarrito.Total = nuevoTotal;

                    fila.Cells["Total"].Value = nuevoTotal;
                    CalcularTotal();

                    bllCliente.GuardarCompraMayoristaTemporal(clienteSeleccionado, beCompraMayorista);

                }
                else
                {
                    // No mostrar mensaje acá, solo ignorar.
                }
            }



        }

        private void iconButtonGuardarPrecio_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewCarrito.Rows.Count; i++)
            {
                DataGridViewRow fila = dataGridViewCarrito.Rows[i];

                if (fila.Index != dataGridViewCarrito.NewRowIndex && !fila.IsNewRow)
                {
                    if (int.TryParse(fila.Cells["Cantidad"].Value?.ToString(), out int nuevaCantidad) && nuevaCantidad > 0)
                    {
                        if (decimal.TryParse(fila.Cells["Total"].Value?.ToString(), out decimal nuevoTotal))
                        {
                            nuevoTotal = Math.Round(nuevoTotal, 2);

                            var itemCarrito = beCompraMayorista.ListaCarrito[i];
                            int cantidadAnterior = itemCarrito.Cantidad;

                            // Actualizar stock reservado
                            if (itemCarrito.Producto.Tipo == "individual")
                            {
                                BEProductoIndividual producto = (BEProductoIndividual)itemCarrito.Producto;
                                int unidadesAntiguas = cantidadAnterior * producto.Unidad;
                                int unidadesNuevas = nuevaCantidad * producto.Unidad;

                                int stockDisponible = producto.Stock.CantidadActual - producto.Stock.CantidadReservada + unidadesAntiguas;

                                if (stockDisponible >= unidadesNuevas)
                                {
                                    producto.Stock.CantidadReservada = producto.Stock.CantidadReservada - unidadesAntiguas + unidadesNuevas;
                                    bllStock.CantidadReservadaStock(producto.Stock); // persistencia
                                }
                                else
                                {
                                    MessageBox.Show($"No hay suficiente stock para '{producto.ToString()}'.", "Stock insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    continue;
                                }
                            }
                            else if (itemCarrito.Producto.Tipo == "combo")
                            {
                                var combo = (BEProductoCombo)itemCarrito.Producto;
                                bool stockSuficiente = true;

                                foreach (var stock in combo.ListaProductos)
                                {
                                    int disponible = stock.CantidadActual - stock.CantidadReservada + cantidadAnterior;
                                    if (disponible < nuevaCantidad)
                                    {
                                        stockSuficiente = false;
                                        break;
                                    }
                                }

                                if (!stockSuficiente)
                                {
                                    MessageBox.Show($"No hay suficiente stock para uno o más productos del combo '{combo.Nombre}'.", "Stock insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    continue;
                                }

                                foreach (var stock in combo.ListaProductos)
                                {
                                    stock.CantidadReservada = stock.CantidadReservada - cantidadAnterior + nuevaCantidad;
                                    bllStock.CantidadReservadaStock(stock);
                                }
                            }

                            // Actualizar cantidad y total en el carrito
                            itemCarrito.Cantidad = nuevaCantidad;
                            itemCarrito.Total = nuevoTotal;

                            // Desactivar edición
                            ModificarPrecio = false;
                        }
                        else
                        {
                            MessageBox.Show("El valor ingresado en 'Total' no es un número válido.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("El valor ingresado en 'Cantidad' no es válido. Debe ser un número entero mayor que cero.");
                    }
                }
            }

            // Guardar los cambios del carrito temporal del cliente
            bllCliente.GuardarCompraMayoristaTemporal(clienteSeleccionado, beCompraMayorista);

            // Refrescar la grilla y recalcular total
            ComprobarCarritoParaModificar();
            CargarDataGridCarrito();
            CalcularTotal();
        }

        private void iconButtonCarritoAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewCarrito.Rows.Count > 0)
                {
                    // 1. Agarramos el producto seleccionado del carrito
                    beCarrito = (BECarrito)dataGridViewCarrito.CurrentRow.DataBoundItem;

                    // 2. ¡LA MAGIA! Reutilizamos el método que armaste recién en la BLL de Stock
                    bllStock.ActualizarStock(beCarrito, listaStock);

                    // 3. Si la BLL no frenó el proceso por falta de stock, refrescamos la pantalla
                    CargarDataGridCarrito();
                    CalcularTotal();

                    // 4. Guardamos el carrito temporal (Pasamanos a la BLL de Cliente)
                    bllCliente.GuardarCompraMayoristaTemporal(clienteSeleccionado, beCompraMayorista);
                }
            }
            catch (Exception ex)
            {
                // Si el método de la BLL tira el "throw new Exception", cae directo acá
                MessageBox.Show(ex.Message, "Aviso de Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void iconButtonClientes_Click(object sender, EventArgs e)
        {
            if (panelListaClientes.Visible == false)
            {
                panelListaClientes.Visible = true;
            }
            else
            {
                panelListaClientes.Visible = false;
            }
        }

        public void OcultarPanelClientes()
        {
            if (panelListaClientes.Visible && !panelListaClientes.ClientRectangle.Contains(panelListaClientes.PointToClient(Cursor.Position)))
            {
                panelListaClientes.Visible = false;
            }
        }

        private void CompraMayorista_Click(object sender, EventArgs e)
        {
            OcultarPanelClientes();
        }

        private void tableLayoutPanel1_Click(object sender, EventArgs e)
        {
            if (panelListaClientes.Visible && !panelListaClientes.ClientRectangle.Contains(panelListaClientes.PointToClient(Cursor.Position)))
            {
                panelListaClientes.Visible = false;
            }
        }

        private void textBoxFiltrar_Enter(object sender, EventArgs e)
        {
            if (textBoxFiltrar.Text == placeholderText)
            {
                textBoxFiltrar.Text = ""; // Limpia el TextBox
                textBoxFiltrar.ForeColor = System.Drawing.Color.Black; // Cambia el color del texto
            }
        }

        private void textBoxFiltrar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxFiltrar.Text))
            {
                textBoxFiltrar.Text = placeholderText; // Restaura el texto del placeholder
                textBoxFiltrar.ForeColor = System.Drawing.Color.Gray; // Cambia el color del texto
            }
        }

        private void textBoxFiltrarCliente_Enter(object sender, EventArgs e)
        {
            if (textBoxFiltrarCliente.Text == placeholderText)
            {
                textBoxFiltrarCliente.Text = ""; // Limpia el TextBox
                textBoxFiltrarCliente.ForeColor = System.Drawing.Color.Black; // Cambia el color del texto
            }
        }

        private void textBoxFiltrarCliente_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxFiltrarCliente.Text))
            {
                textBoxFiltrarCliente.Text = placeholderText; // Restaura el texto del placeholder
                textBoxFiltrarCliente.ForeColor = System.Drawing.Color.Gray; // Cambia el color del texto
            }
        }

        private void iconButtonGenerarPresupuesto_Click(object sender, EventArgs e)
        {
            GenerarImagenPresupuesto(beCompraMayorista, clienteSeleccionado);
        }

        public void GenerarImagenPresupuesto(BECompraMayorista compraMayorista, BECliente cliente)
        {
            SaveFileDialog guardar = new SaveFileDialog
            {
                FileName = ".jpg",
                Filter = "JPEG Image (*.jpg)|*.jpg",
                DefaultExt = "jpg"
            };

            string paginaHtml = Properties.Resources.plantilla2.ToString();

            // Usar la cultura por defecto (es-AR)
            paginaHtml = paginaHtml.Replace("{{fechaComprobante}}", DateTime.Today.ToString("d"));
            paginaHtml = paginaHtml.Replace("{{nombreCliente}}", clienteSeleccionado.Nombre);
            paginaHtml = paginaHtml.Replace("{{direccionCliente}}", $"{clienteSeleccionado.Direccion}, {clienteSeleccionado.Localidad}");
            paginaHtml = paginaHtml.Replace("{{telefonoCliente}}", $"{clienteSeleccionado.Telefono} / {clienteSeleccionado.TelefonoAlternativo}");
            paginaHtml = paginaHtml.Replace("{{horarioApertura}}", clienteSeleccionado.HorarioDeApertura.ToString());
            paginaHtml = paginaHtml.Replace("{{horarioCierre}}", clienteSeleccionado.HorarioDeCierre.ToString());

            string filasItems = string.Empty;
            int cantidad = 0;
            decimal totalPresupuesto = 0;

            foreach (BECarrito item in compraMayorista.ListaCarrito)
            {
                cantidad += item.Cantidad;
                filasItems += "<tr>";
                filasItems += $"<td>{item.Producto.Codigo}</td>";
                filasItems += $"<td class='producto'>{item.Producto.ToString()}</td>";

                decimal precioUnitario = item.Producto.PrecioMayorista;
                filasItems += $"<td>{precioUnitario.ToString("N2")}</td>";
                filasItems += $"<td>{item.Cantidad}</td>";
                filasItems += $"<td class='precio'>{item.Total.ToString("N2")}</td>";
                filasItems += "</tr>";

                totalPresupuesto += item.Total;
            }

            paginaHtml = paginaHtml.Replace("{{productosCarrito}}", filasItems);
            paginaHtml = paginaHtml.Replace("{{cantidadTotalProductos}}", cantidad.ToString());
            paginaHtml = paginaHtml.Replace("{{totalCompra}}", totalPresupuesto.ToString("N2"));

            WebBrowser webBrowser = new WebBrowser
            {
                ScrollBarsEnabled = false,
                ScriptErrorsSuppressed = true,
                Width = 827,
                Height = 1169
            };

            webBrowser.DocumentText = paginaHtml;
            webBrowser.DocumentCompleted += (s, ev) =>
            {
                Bitmap bitmap = new Bitmap(webBrowser.Width, webBrowser.Height);
                webBrowser.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height));

                if (guardar.ShowDialog() == DialogResult.OK)
                {
                    bitmap.Save(guardar.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    MessageBox.Show("Imagen guardada correctamente.");

                    try
                    {
                        System.Diagnostics.Process.Start(guardar.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo abrir el archivo de imagen: " + ex.Message);
                    }
                }
            };
        }

        private void iconButtonAgregar_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Verificar el tipo de producto seleccionado en la fila actual del DataGridView
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

        private void panel2_Click(object sender, EventArgs e)
        {
            OcultarPanelClientes();
        }

        private void panel8_Click(object sender, EventArgs e)
        {
            OcultarPanelClientes();
        }

        private void textBoxFiltrar_Click(object sender, EventArgs e)
        {
            OcultarPanelClientes();

        }

        private void panel3_Click(object sender, EventArgs e)
        {
            OcultarPanelClientes();

        }

        private void panel9_Click(object sender, EventArgs e)
        {
            OcultarPanelClientes();

        }

        private void buttonTotal_Click(object sender, EventArgs e)
        {
            OcultarPanelClientes();

        }

        private void dataGridViewCarrito_Click(object sender, EventArgs e)
        {
            OcultarPanelClientes();

        }

        private void dataGridViewProductos_Click(object sender, EventArgs e)
        {
            OcultarPanelClientes();

        }

        private void iconButtonLimpiarCarrito_Click(object sender, EventArgs e)
        {
            // Asegúrate de que el cliente seleccionado no sea null y que tenga un carrito temporal
            if (clienteSeleccionado == null || clienteSeleccionado.CompraMayoristaTemp == null)
            {
                MessageBox.Show("No hay un cliente seleccionado o el carrito está vacío.");
                return;
            }


            // Ajustar el stock según el carrito
            foreach (var item in beCompraMayorista.ListaCarrito)
            {
                AjustarStock(item, revertir: true);  // Revertimos la cantidad reservada
            }

            // Limpiar el carrito
            clienteSeleccionado.CompraMayoristaTemp.ListaCarrito.Clear();
            beCompraMayorista.ListaCarrito.Clear();
            beCompraMayorista.Total = 0;

            // Actualizar la interfaz
            ActualizarCarritoUI();

            // Guardar los cambios
            bllCliente.GuardarCompraMayoristaTemporal(clienteSeleccionado, beCompraMayorista);

            MessageBox.Show("El carrito de compras ha sido limpiado.");
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

        private void ActualizarCarritoUI()
        {
            // Actualizar el DataGridView del carrito
            dataGridViewCarrito.DataSource = null;
            dataGridViewCarrito.DataSource = clienteSeleccionado.CompraMayoristaTemp.ListaCarrito;

            // Actualizar el botón total
            buttonTotal.Text = clienteSeleccionado.Nombre +
                               " // TOTAL: $ " +
                               beCompraMayorista.Total.ToString("N2");
        }

        private void iconButtonAcomodarCantidadReservada_Click(object sender, EventArgs e)
        {
            bllStock.AcomodarCantidadReservada();
        }



        private void dataGridViewClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewClientes_CellClick_1(sender, e);
            panelListaClientes.Visible = false;

        }

        private void iconButtonAlertaPocoStock_Click(object sender, EventArgs e)
        {
            List<string> productosBajoStock = bllStock.ObtenerProductosConStockBajo();
            if (productosBajoStock.Any())
            {
                iconButtonAlertaPocoStock.Visible = true;
                string mensaje = "¡Atención! Los siguientes productos tienen bajo stock:\n\n" + string.Join("\n", productosBajoStock);
                MessageBox.Show(mensaje, "Stock bajo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void comprobarVencimiento()
        {
            var productosProximos = bllStock.ObtenerProductosProximosAVencer(); // o como lo tengas nombrado

            if (productosProximos.Any())
            {
                iconButtonVencimiento.Visible = true;
            }
        }

        private void iconButtonVencimiento_Click(object sender, EventArgs e)
        {
            var productosProximos = bllStock.ObtenerProductosProximosAVencer(); // o como lo tengas nombrado

            if (productosProximos.Any())
            {
                string mensaje = "Los siguientes productos están próximos a vencerse:\n\n" +
                                 string.Join("\n", productosProximos);

                MessageBox.Show(mensaje, "Aviso de vencimiento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridViewCarrito_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewCarrito.IsCurrentCellDirty)
            {
                var celda = dataGridViewCarrito.CurrentCell;

                if (celda != null && celda.Value != null && !string.IsNullOrWhiteSpace(celda.EditedFormattedValue.ToString()))
                {
                    // Solo hacer commit si la celda tiene contenido válido
                    dataGridViewCarrito.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }

        private void dataGridViewCarrito_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridViewCarrito.Columns[e.ColumnIndex].Name == "Cantidad")
            {
                var valor = dataGridViewCarrito.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                if (int.TryParse(valor, out int cantidad))
                {
                    cantidadAnteriorEditada = cantidad;
                }
            }
        }

       
        private void CompraMayorista_VisibleChanged_1(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                listaStock = bllStock.CargarStock();
                CargarDataGrid();
                ComprobarBajoStock();
                comprobarVencimiento();
            }
        }
    }
}
