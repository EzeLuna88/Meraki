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
using iText;
//using iText.Layout;
//using iText.StyledXmlParser.Jsoup.Nodes;
//using iText.Kernel.Pdf;
//using iText.Kernel.Font;
//using iText.Layout.Element;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iText.Kernel.Pdf;
using iTextSharp.tool.xml.pipeline.end;
using System.Drawing.Imaging;
using System.Globalization;


namespace Meraki
{
    public partial class CompraMayorista : Form
    {
        BLLCliente bllCliente;
        BEProductoIndividual beProductoIndividual;
        BEProductoCombo beProductoCombo;
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
        bool panelClientes = false;
        private const string placeholderText = "   Buscar...";
        int cantidadAnteriorEditada;
        public CompraMayorista()
        {
            bllStock = new BLLStock();
            listaStock = new List<BEStock>();
            listaStock = bllStock.CargarStock();
            listaComprobantes = new List<BEComprobante>();
            beCompraMayorista = new BECompraMayorista();
            beProductoIndividual = new BEProductoIndividual();
            beProductoCombo = new BEProductoCombo();
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
            var productoEnCarrito = beCompraMayorista.ListaCarrito.Find(p => p.Producto.Codigo == producto.Codigo);
            var productoEnStock = listaStock.Find(p => p.Codigo == producto.Stock.Codigo);

            int stockDisponible = productoEnStock.CantidadActual - productoEnStock.CantidadReservada;

            if (productoEnStock == null || stockDisponible < producto.Unidad)
            {
                MessageBox.Show("No hay suficiente stock");
                return;
            }

            if (productoEnCarrito != null)
            {
                // Actualizar el producto existente en el carrito
                productoEnCarrito.Cantidad++;
                productoEnCarrito.Total = producto.PrecioMayorista * productoEnCarrito.Cantidad;

            }
            else
            {
                // Agregar el nuevo producto al carrito
                beCarrito = new BECarrito
                {
                    Producto = producto,
                    Cantidad = 1,
                    Total = producto.PrecioMayorista
                };
                beCompraMayorista.ListaCarrito.Add(beCarrito);

            }
            productoEnStock.CantidadReservada = productoEnStock.CantidadReservada + producto.Unidad;
            bllStock.CantidadReservadaStock(productoEnStock);

            bllCliente.GuardarCompraMayoristaTemporal(clienteSeleccionado, beCompraMayorista);


            CalcularTotal();
            ActualizarInterfaz();
        }

        private void ProcesarProductoCombo(BEProductoCombo productoCombo)
        {
            bool hayStock = true;

            foreach (BEStock item in productoCombo.ListaProductos)
            {
                var itemEnStock = listaStock.Find(p => p.Codigo == item.Codigo);
                if (itemEnStock == null || itemEnStock.CantidadActual < 1)
                {
                    MessageBox.Show("No hay suficiente stock");
                    hayStock = false;
                    break;
                }
            }

            if (!hayStock)
            {
                MessageBox.Show("No hay suficiente stock para uno o más productos del combo.");
                return;
            }

            var productoEnCarrito = beCompraMayorista.ListaCarrito.Find(p => p.Producto.Codigo == productoCombo.Codigo);
            if (productoEnCarrito != null)
            {
                // Actualizar el producto combo existente en el carrito
                productoEnCarrito.Cantidad++;
                productoEnCarrito.Total = productoCombo.PrecioMayorista * productoEnCarrito.Cantidad;

            }
            else
            {
                // Agregar el nuevo producto combo al carrito
                beCarrito = new BECarrito
                {
                    Producto = productoCombo,
                    Cantidad = 1,
                    Total = productoCombo.PrecioMayorista
                };
                beCompraMayorista.ListaCarrito.Add(beCarrito);

            }
            bllCliente.GuardarCompraMayoristaTemporal(clienteSeleccionado, beCompraMayorista);

            foreach (BEStock item in productoCombo.ListaProductos)
            {
                var itemEnStock = listaStock.Find(p => p.Codigo == item.Codigo);
                itemEnStock.CantidadReservada++;
                bllStock.CantidadReservadaStock(itemEnStock);
            }

            CalcularTotal();
            ActualizarInterfaz();
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
            if (dataGridViewProductos.Rows.Count > 0)
            {
                dataGridViewProductos.Rows[0].Selected = true;
            }

            bindingSourceClientes.DataSource = bllCliente.ListaClientes();
            dataGridViewClientes.DataSource = bindingSourceClientes;
            CargarDataGridClientes();
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
                               beCompraMayorista.Total.ToString("N2", CultureInfo.InvariantCulture);
        }

        public void CargarDataGridClientes()
        {
            // Guardar el código del cliente seleccionado si existe
            string codigoClienteSeleccionado = clienteSeleccionado?.Codigo;

            // Cargar y ordenar los clientes
            List<BECliente> listClientes = bllCliente.ListaClientes().OrderBy(cliente => cliente.Nombre).ToList();
            foreach (var cliente in listClientes)
            {
                if (cliente.CompraMayoristaTemp == null)
                {
                    cliente.CompraMayoristaTemp = new BECompraMayorista();
                }
            }

            // Asignar al DataGridView
            bindingSourceClientes.DataSource = new BindingList<BECliente>(listClientes);
            dataGridViewClientes.DataSource = bindingSourceClientes;

            // Restaurar la selección usando clienteSeleccionado
            if (!string.IsNullOrEmpty(codigoClienteSeleccionado))
            {
                foreach (DataGridViewRow row in dataGridViewClientes.Rows)
                {
                    if (row.DataBoundItem is BECliente cliente && cliente.Codigo == codigoClienteSeleccionado)
                    {
                        row.Selected = true;
                        dataGridViewClientes.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }
            }

            // Ocultar columnas y configurar
            dataGridViewClientes.Columns[0].Visible = false;
            dataGridViewClientes.Columns[4].Visible = false;
            dataGridViewClientes.Columns[5].Visible = false;
            dataGridViewClientes.Columns[6].Visible = false;
            dataGridViewClientes.Columns[7].Visible = false;
            dataGridViewClientes.Columns[8].Visible = false;
            dataGridViewClientes.Columns[9].Visible = false;
            dataGridViewClientes.AllowUserToAddRows = false;

            ConfigurarDataGrid(dataGridViewClientes);
        }


        public void GenerarPDF(BEComprobante comprobante)
        {
            SaveFileDialog guardar = new SaveFileDialog
            {
                FileName = comprobante.Numero + ".pdf",
                Filter = "PDF Files (*.pdf)|*.pdf", // Filtro para archivos PDF
                DefaultExt = "pdf" // Extensión predeterminada
            };

            string paginaHtml = Properties.Resources.plantilla.ToString();
            paginaHtml = paginaHtml.Replace("{{numeroComprobante}}", comprobante.Numero.ToString());
            paginaHtml = paginaHtml.Replace("{{fechaComprobante}}", comprobante.Fecha.ToString("dd/MM/yyyy"));
            paginaHtml = paginaHtml.Replace("{{nombreCliente}}", comprobante.Cliente.Nombre.ToString());
            paginaHtml = paginaHtml.Replace("{{direccionCliente}}", comprobante.Cliente.Direccion.ToString() + ", " + comprobante.Cliente.Localidad.ToString());
            paginaHtml = paginaHtml.Replace("{{telefonoCliente}}", comprobante.Cliente.Telefono.ToString() + " / " + comprobante.Cliente.TelefonoAlternativo.ToString());
            paginaHtml = paginaHtml.Replace("{{horarioApertura}}", comprobante.Cliente.HorarioDeApertura.ToString());
            paginaHtml = paginaHtml.Replace("{{horarioCierre}}", comprobante.Cliente.HorarioDeCierre.ToString());
            paginaHtml = paginaHtml.Replace("{{comentariosCliente}}", comprobante.Cliente.Comentarios.ToString());

            string filasItems = string.Empty;
            int cantidad = 0;
            foreach (BEItem item in comprobante.ListaItems)
            {
                cantidad += item.Cantidad;
                filasItems += "<tr>";
                filasItems += $"<td>{item.Codigo}</td>";
                decimal precioUnitario = item.Precio / item.Cantidad;
                filasItems += $"<td class='producto'>{item.Nombre}</td>";
                filasItems += $"<td >{precioUnitario.ToString("c2")}</td>";
                filasItems += $"<td>{item.Cantidad}</td>";
                filasItems += $"<td class='precio'>{item.Precio.ToString("c2")}</td>";
                filasItems += "</tr>";
            }
            paginaHtml = paginaHtml.Replace("{{productosCarrito}}", filasItems);

            paginaHtml = paginaHtml.Replace("{{cantidadTotalProductos}}", cantidad.ToString());
            paginaHtml = paginaHtml.Replace("{{totalCompra}}", comprobante.Total.ToString("c2"));


            if (comprobante.PagoEfectivo == true)
            {
                paginaHtml = paginaHtml.Replace("{{formaPago}}", "Efectivo");
            }
            else
            {
                paginaHtml = paginaHtml.Replace("{{formaPago}}", "Transferencia");

            }

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 20, 20, 20, 20);
                    iTextSharp.text.pdf.PdfWriter pdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    using (StringReader stringReader = new StringReader(paginaHtml))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(pdfWriter, pdfDoc, stringReader);
                    }

                    pdfDoc.Close();
                    stream.Close();
                }
                try
                {
                    System.Diagnostics.Process.Start(guardar.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo abrir el archivo PDF: " + ex.Message);
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
                               beCompraMayorista.Total.ToString("N2", CultureInfo.InvariantCulture);
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
                dataGridViewCarrito.DefaultCellStyle.BackColor = Color.White;
                dataGridViewCarrito.DefaultCellStyle.SelectionBackColor = Color.Blue;

            }
            else
            {

                iconButtonModificarPrecio.Visible = true;
                iconButtonGuardarPrecio.Visible = false;
                dataGridViewCarrito.BackColor = Color.LightGray;
                dataGridViewCarrito.DefaultCellStyle.BackColor = Color.Gray;
                dataGridViewCarrito.DefaultCellStyle.SelectionBackColor = Color.Gray;
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
                    dataGridViewClientes.Rows[0].Selected = true;
                    MostrarCarritoDelCliente((BECliente)dataGridViewClientes.Rows[0].DataBoundItem);

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
                                       beCompraMayorista.Total.ToString("N2", CultureInfo.InvariantCulture);
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
                dataGridViewClientes.Rows[0].Selected = true;
                MostrarCarritoDelCliente((BECliente)dataGridViewClientes.Rows[0].DataBoundItem);
            }
            dataGridViewClientes.Columns[0].Visible = false;
            // Refrescar el DataGridView para mostrar los resultados filtrados
            dataGridViewClientes.Refresh();
        }

        private void dataGridViewClientes_SelectionChanged_1(object sender, EventArgs e)
        {
            var cultureInfo = new CultureInfo("en-US");

            try
            {
                if (dataGridViewClientes.SelectedRows.Count > 0)
                {
                    var filaSeleccionada = dataGridViewClientes.SelectedRows[0];

                    // Obtén el cliente correspondiente a la fila seleccionada
                    clienteSeleccionado = (BECliente)filaSeleccionada.DataBoundItem;

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
                        // Cargar los productos del carrito en el DataGridView
                        dataGridViewCarrito.DataSource = clienteSeleccionado.CompraMayoristaTemp.ListaCarrito;

                        // Configuración de formato y alineación de las columnas
                        dataGridViewCarrito.Columns["Total"].DefaultCellStyle.FormatProvider = cultureInfo;
                        dataGridViewCarrito.Columns["Total"].DefaultCellStyle.Format = "c2"; // Formato de moneda

                        dataGridViewCarrito.Columns["Cantidad"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridViewCarrito.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridViewCarrito.Columns["Cantidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dataGridViewCarrito.Columns["Cantidad"].Width = 70;

                        dataGridViewCarrito.Columns["Total"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridViewCarrito.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridViewCarrito.Columns["Total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dataGridViewCarrito.Columns["Total"].Width = 80;

                        // Actualizar el objeto beCompraMayorista con el carrito temporal del cliente seleccionado
                        beCompraMayorista = clienteSeleccionado.CompraMayoristaTemp;
                    }
                    else
                    {
                        // Si el carrito está vacío, inicializa un nuevo objeto BECompraMayorista
                        beCompraMayorista = new BECompraMayorista();
                    }

                    // Calcular y actualizar el total después de cargar el DataGrid
                    CalcularTotal();

                    // Actualiza el botón con el total formateado correctamente
                    buttonTotal.Text = clienteSeleccionado.Nombre + " // TOTAL: $ " +
                                       beCompraMayorista.Total.ToString("N2", CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores con información específica
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
                    if (beCarrito.Producto is BEProductoIndividual)
                    {
                        beProductoIndividual = (BEProductoIndividual)beCarrito.Producto;
                        var productoEnStock = listaStock.Find(p => p.Codigo == beProductoIndividual.Stock.Codigo);

                        if (beCarrito.Cantidad > 1)
                        {
                            decimal precioPorUnidad = beCarrito.Total / beCarrito.Cantidad;
                            beCarrito.Total -= precioPorUnidad;

                            beCarrito.Cantidad--;

                            // Disminuir la cantidad reservada en el stock
                            productoEnStock.CantidadReservada -= beCarrito.Producto.Unidad;
                            bllStock.CantidadReservadaStock(productoEnStock);

                            // Actualizar el carrito y recalcular el total
                            CargarDataGridCarrito();
                            CalcularTotal();

                        }
                        else
                        {
                            // Remover el producto si la cantidad es 1
                            beCompraMayorista.ListaCarrito.Remove(beCarrito);
                            beCompraMayorista.Total = 0;
                            // Actualizar stock y reservar la cantidad devuelta
                            productoEnStock.CantidadReservada -= beCarrito.Producto.Unidad;
                            bllStock.CantidadReservadaStock(productoEnStock);

                            // Refrescar la interfaz y recalcular el total
                            CargarDataGridCarrito();
                            CalcularTotal();


                        }
                    }
                    else if (beCarrito.Producto is BEProductoCombo)
                    {
                        var beProductoCombo = (BEProductoCombo)beCarrito.Producto;
                        if (beCarrito.Cantidad > 1)
                        {
                            beCarrito.Cantidad--;
                            beCarrito.Total -= beCarrito.Producto.PrecioMayorista;
                            CargarDataGridCarrito();
                            CalcularTotal();

                            foreach (BEStock item in beProductoCombo.ListaProductos)
                            {
                                var itemEnStock = listaStock.Find(p => p.Codigo == item.Codigo);
                                itemEnStock.CantidadReservada--;
                                bllStock.CantidadReservadaStock(itemEnStock);
                            }


                        }
                        else
                        {
                            var productoEnCarrito = beCompraMayorista.ListaCarrito.Find(p => p.Producto.Codigo == beCarrito.Producto.Codigo);
                            beCompraMayorista.ListaCarrito.Remove(productoEnCarrito);
                            dataGridViewCarrito.DataSource = null;
                            dataGridViewCarrito.DataSource = beCompraMayorista.ListaCarrito;
                            dataGridViewCarrito.Columns["Total"].DefaultCellStyle.Format = "c2";
                            decimal total = 0;
                            if (beCompraMayorista.ListaCarrito.Count > 0)
                            {
                                foreach (var producto in beCompraMayorista.ListaCarrito)
                                {
                                    total += producto.Total;
                                    beCompraMayorista.Total = total;
                                }
                            }
                            else
                            {
                                beCompraMayorista.Total = 0;
                            }
                            buttonTotal.Text = clienteSeleccionado.Nombre +
                                               " // TOTAL: $ " +
                                               beCompraMayorista.Total.ToString("N2", CultureInfo.InvariantCulture);
                            foreach (BEStock item in beProductoCombo.ListaProductos)
                            {
                                var itemEnStock = listaStock.Find(p => p.Codigo == item.Codigo);
                                itemEnStock.CantidadReservada--;
                                bllStock.CantidadReservadaStock(itemEnStock);
                            }

                        }
                    }
                    bllCliente.GuardarCompraMayoristaTemporal(clienteSeleccionado, beCompraMayorista);

                }
                else
                {
                    MessageBox.Show("El carrito esta vacio");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void iconButtonConfirmarCompra_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridViewCarrito.Rows.Count > 0)
                {
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
                        beComprobante.Cliente = clienteSeleccionado;
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
                                int numeroComprobante;
                                if (int.TryParse(comprobante.Numero, out numeroComprobante)) // Intentar convertir el número a entero
                                {
                                    if (numeroComprobante > numeroMaximo)
                                    {
                                        numeroMaximo = numeroComprobante;
                                    }
                                }
                            }


                            beComprobante.Numero = (numeroMaximo + 1).ToString().PadLeft(8, '0');
                        }

                        beComprobante.Fecha = DateTime.Now;
                        List<BEItem> items = new List<BEItem>();

                        List<BEStock> listaStockTemp = bllStock.CargarStock();


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

                            if (item.Producto is BEProductoIndividual individual)
                            {
                                BEStock stockTemp = listaStockTemp.FirstOrDefault(s => s.Codigo == individual.Stock.Codigo);
                                if (stockTemp != null)
                                {
                                    int cantidadARestar = individual.Unidad * item.Cantidad;
                                    stockTemp.CantidadActual -= cantidadARestar;
                                    stockTemp.CantidadReservada -= cantidadARestar;
                                    bllStock.CantidadReservadaStock(stockTemp);
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
                                        stockTemp.CantidadActual -= cantidadARestar;
                                        stockTemp.CantidadReservada -= cantidadARestar;
                                        bllStock.CantidadReservadaStock(stockTemp);
                                    }
                                }
                            }

                        }

                        beComprobante.ListaItems = items;

                        beComprobante.Total = beCompraMayorista.Total;


                        bllComprobante.GuardarNuevoComprobante(beComprobante);
                        GenerarPDF(beComprobante);
                        bllStock.ActualizarStock(listaStockTemp);
                        bllStock.DescontarStockPorVencimiento(beCompraMayorista);
                        listaStock = bllStock.CargarStock();
                        limpiarCompraMayorista();
                        ComprobarBajoStock();
                    }
                }
                else
                {
                    MessageBox.Show("El carrito se encuentra vacio");
                }

            }
            catch (Exception)
            {
                throw;
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
                        
                        if(producto.Stock.CantidadActual >= nuevaCantidadReservada + nuevaCantidadUnidades)
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
                    beCarrito = (BECarrito)dataGridViewCarrito.CurrentRow.DataBoundItem;
                    if (beCarrito.Producto is BEProductoIndividual)
                    {
                        beProductoIndividual = (BEProductoIndividual)beCarrito.Producto;
                        var productoEnStock = listaStock.Find(p => p.Codigo == beProductoIndividual.Stock.Codigo);

                        int cantidadDisponible = productoEnStock.CantidadActual - productoEnStock.CantidadReservada;


                        if (cantidadDisponible >= beProductoIndividual.Unidad)
                        {
                            // Actualizar la cantidad y el total en el carrito
                            beCarrito.Cantidad++;
                            beCarrito.Total += beCarrito.Producto.PrecioMayorista;

                            // Aumentar la cantidad reservada
                            productoEnStock.CantidadReservada = productoEnStock.CantidadReservada + beCarrito.Producto.Unidad;
                            bllStock.CantidadReservadaStock(productoEnStock);

                            CargarDataGridCarrito();
                            CalcularTotal();
                            bllStock.ActualizarStock(listaStock);

                        }
                        else
                        {
                            MessageBox.Show("No hay stock suficiente para agregar");
                        }
                    }

                    else if (beCarrito.Producto is BEProductoCombo)
                    {
                        var beProductoCombo = (BEProductoCombo)beCarrito.Producto;
                        bool hayStockSuficiente = true;

                        foreach (var productoEnCombo in beProductoCombo.ListaProductos)
                        {
                            var productoEnStock = listaStock.Find(p => p.Codigo == productoEnCombo.Codigo);
                            if (productoEnStock == null || productoEnStock.CantidadActual < 1)
                            {
                                hayStockSuficiente = false;
                                break;
                            }
                        }
                        if (hayStockSuficiente)
                        {
                            beCarrito.Cantidad++;
                            beCarrito.Total += beCarrito.Producto.PrecioMayorista;

                            // Disminuye el stock de cada producto en el combo
                            foreach (var productoEnCombo in beProductoCombo.ListaProductos)
                            {
                                var productoEnStock = listaStock.Find(p => p.Codigo == productoEnCombo.Codigo);
                                if (productoEnStock != null)
                                {
                                    productoEnStock.CantidadReservada++; // Restar 1 por cada producto en el combo
                                    bllStock.CantidadReservadaStock(productoEnStock);

                                }
                            }

                            CargarDataGridCarrito();
                            CalcularTotal();

                        }
                        else
                        {
                            MessageBox.Show("No hay stock suficiente para agregar este combo");
                        }
                    }
                    bllCliente.GuardarCompraMayoristaTemporal(clienteSeleccionado, beCompraMayorista);

                }
            }

            catch (Exception)
            {

                throw;
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

            var cultura = new CultureInfo("es-AR");

            paginaHtml = paginaHtml.Replace("{{fechaComprobante}}", DateTime.Today.ToString("d", cultura));
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
                filasItems += $"<td>{precioUnitario.ToString("N2", cultura)}</td>";
                filasItems += $"<td>{item.Cantidad}</td>";
                filasItems += $"<td class='precio'>{item.Total.ToString("N2", cultura)}</td>";
                filasItems += "</tr>";

                totalPresupuesto += item.Total;
            }

            paginaHtml = paginaHtml.Replace("{{productosCarrito}}", filasItems);
            paginaHtml = paginaHtml.Replace("{{cantidadTotalProductos}}", cantidad.ToString());
            paginaHtml = paginaHtml.Replace("{{totalCompra}}", totalPresupuesto.ToString("N2", cultura));

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
                    bitmap.Save(guardar.FileName, ImageFormat.Jpeg);
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
                               beCompraMayorista.Total.ToString("N2", CultureInfo.InvariantCulture);
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
    }
}
