using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Meraki
{
    public partial class Dashboard : Form
    {
        BECliente beCliente;
        BLLCliente bllCliente;
        BLLComprobante bllComprobante;
        List<BEComprobante> listaComprobantes;
        DateTime fechaInicio;
        DateTime fechaFinal;
        private BindingSource bindingSourceClientes = new BindingSource();


        public Dashboard()
        {
            beCliente = new BECliente();
            bllCliente = new BLLCliente();
            bllComprobante = new BLLComprobante();
            listaComprobantes = new List<BEComprobante>();
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            panelClientes.Visible = false;
            InicializarFechas();
            beCliente = null;
            ActualizarComprobantes(DateTime.Today, DateTime.Now, beCliente);
            ActualizarCharts(listaComprobantes);
            CargarDataGridClientes();

        }

        private void InicializarFechas()
        {
            labelFechaInicio.Text = dateTimePickerFechaInicio.Text;
            labelFechaFinal.Text = dateTimePickerFechaFinal.Text;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (panelClientes.Visible == false)
            {
                panelClientes.Visible = true;
            }
            else
            {
                panelClientes.Visible = false;
            }
        }

        private void labelFechaInicio_Click(object sender, EventArgs e)
        {
            dateTimePickerFechaInicio.Select();
            SendKeys.Send("%{DOWN}");
        }

        private void labelFechaFinal_Click(object sender, EventArgs e)
        {
            dateTimePickerFechaFinal.Select();
            SendKeys.Send("%{DOWN}");

        }

        private void dateTimePickerFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            labelFechaInicio.Text = dateTimePickerFechaInicio.Text;
            fechaInicio = dateTimePickerFechaInicio.Value;
        }

        private void dateTimePickerFechaFinal_ValueChanged(object sender, EventArgs e)
        {
            labelFechaFinal.Text = dateTimePickerFechaFinal.Text;
            fechaFinal = dateTimePickerFechaFinal.Value;
        }



        public void CargarDataGridClientes()
        {
            int selectedRowIndex = -1;
            if (dataGridViewClientes.SelectedRows.Count > 0)
            {
                selectedRowIndex = dataGridViewClientes.SelectedRows[0].Index;
            }

            // Cargar y ordenar los clientes
            List<BECliente> listClientes = bllCliente.ListaClientes().OrderBy(cliente => cliente.Nombre).ToList();
            foreach (var cliente in listClientes)
            {
                if (cliente.CompraMayoristaTemp == null)
                {
                    cliente.CompraMayoristaTemp = new BECompraMayorista();
                }
            }

            // Asignar la lista al BindingSource
            bindingSourceClientes.DataSource = new BindingList<BECliente>(listClientes);
            dataGridViewClientes.DataSource = bindingSourceClientes;

            if (selectedRowIndex >= 0 && selectedRowIndex < dataGridViewClientes.Rows.Count)
            {
                dataGridViewClientes.Rows[selectedRowIndex].Selected = true;
            }




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


        public void ConteoComprobantes(List<BEComprobante> listaComprobantes)
        {
            int pedidosCantidad = listaComprobantes.Count;
            decimal montoFacturado = 0;

            foreach (BEComprobante comprobante in listaComprobantes)
            {
                montoFacturado += comprobante.Total;

            }
            labelPedidosCantidad.Text = pedidosCantidad.ToString();
            labelMontoFacturado.Text = "$ " + montoFacturado.ToString();

        }

        public void ProductosMasVendidos(List<BEComprobante> listaComprobantes)
        {
            Dictionary<string, int> productosMasVendidos = new Dictionary<string, int>();


            foreach (BEComprobante comprobante in listaComprobantes)
            {
                foreach (BEItem item in comprobante.ListaItems)
                {
                    string nombreProducto = item.Nombre;
                    int cantidad = item.Cantidad;

                    if (productosMasVendidos.ContainsKey(nombreProducto))
                    {
                        productosMasVendidos[nombreProducto] += cantidad;
                    }
                    else
                    {
                        productosMasVendidos[nombreProducto] = cantidad;
                    }
                }

            }

            var productosOrdenados = productosMasVendidos.OrderBy(p => p.Value);

            chartProductosMasVendidos.Series.Clear();

            // Crear una nueva serie de tipo columna (barras)
            Series serie = new Series
            {
                Name = "ProductosVendidos",
                ChartType = SeriesChartType.Bar // Tipo de gráfico de barras horizontales
            };

            // Agregar los productos y sus cantidades al gráfico
            foreach (var producto in productosOrdenados)
            {
                serie.Points.AddXY(producto.Key, producto.Value);
            }

            // Añadir la serie al gráfico
            chartProductosMasVendidos.Series.Add(serie);

            // Ajustar el estilo del gráfico
            
            chartProductosMasVendidos.ChartAreas[0].AxisY.Title = "Producto";
            chartProductosMasVendidos.ChartAreas[0].AxisY.Interval = 1; // Mostrar todos los productos
            chartProductosMasVendidos.ChartAreas[0].AxisX.Interval = 1;
            chartProductosMasVendidos.ChartAreas[0].AxisY.LabelStyle.TruncatedLabels = true; // Truncar etiquetas largas
            chartProductosMasVendidos.ChartAreas[0].AxisY.LabelStyle.IsStaggered = false;   // Evitar que las etiquetas se apilen
            chartProductosMasVendidos.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            chartProductosMasVendidos.Series[0]["PointWidth"] = "0.8"; // Aumenta el ancho de las barras (0.6 a 0.8)


            // Configurar el Chart para habilitar scroll en el eje Y
            chartProductosMasVendidos.ChartAreas[0].AxisX.ScrollBar.Enabled = true;  // Activar la barra de desplazamiento
            chartProductosMasVendidos.ChartAreas[0].AxisX.ScrollBar.Size = 15;       // Tamaño de la barra de desplazamiento
            chartProductosMasVendidos.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;  // Estilo de los botones de desplazamiento
            chartProductosMasVendidos.ChartAreas[0].AxisX.ScaleView.Zoomable = true; // Permitir zoom
            chartProductosMasVendidos.ChartAreas[0].AxisX.ScaleView.Size = 10;       // Número de barras visibles antes de tener que desplazarse

            chartProductosMasVendidos.ChartAreas[0].InnerPlotPosition = new ElementPosition(20, 5, 75, 85); // Ajustar el área de las barras dentro del gráfico
            chartProductosMasVendidos.ChartAreas[0].Position = new ElementPosition(0, 0, 100, 100);  // Asegurar que el gráfico se use al máximo en la ventana
        }

        private void IconButtonHoy_Click(object sender, EventArgs e)
        {
            dateTimePickerFechaInicio.Value = DateTime.Today;
            dateTimePickerFechaFinal.Value = DateTime.Now;
            ActualizarComprobantes(DateTime.Today, DateTime.Now, beCliente);
            ActualizarCharts(listaComprobantes);
        }

        private void iconButtonSemana_Click(object sender, EventArgs e)
        {
            dateTimePickerFechaInicio.Value = DateTime.Today.AddDays(-7);
            dateTimePickerFechaFinal.Value = DateTime.Now;
            ActualizarComprobantes(DateTime.Today.AddDays(-7), DateTime.Now, beCliente);
            ActualizarCharts(listaComprobantes);
        }

        private void iconButtonMes_Click(object sender, EventArgs e)
        {
            dateTimePickerFechaInicio.Value = DateTime.Today.AddDays(-30);
            dateTimePickerFechaFinal.Value = DateTime.Now;
            ActualizarComprobantes(DateTime.Today.AddDays(-30), DateTime.Now, beCliente);
            ActualizarCharts(listaComprobantes);
        }

        private void ActualizarComprobantes(DateTime inicio, DateTime final, BECliente cliente)
        {
            fechaInicio = inicio;
            fechaFinal = final;

            // Limpiar y cargar los comprobantes filtrados
            listaComprobantes.Clear();
            listaComprobantes = bllComprobante.filtroComprobantes(fechaInicio, fechaFinal, cliente);

            // Actualizar el conteo de comprobantes en la etiqueta correspondiente
            ConteoComprobantes(listaComprobantes);


            ProductosMasVendidos(listaComprobantes);
        }

        private void ActualizarCharts(List<BEComprobante> listaComprobantes)
        {
            // Agrupar comprobantes por fecha para obtener la cantidad de pedidos y el total facturado por cada fecha
            var datosAgrupados = listaComprobantes
                .GroupBy(c => c.Fecha.Date) // Agrupar por la fecha (sin hora)
                .Select(g => new
                {
                    Fecha = g.Key,
                    CantidadPedidos = g.Count(),
                    TotalFacturado = g.Sum(comprobante => comprobante.Total) // Sumar el total facturado
                })
                .ToList();

            // Asignar la fuente de datos al chart unificado
            chartVentas.DataSource = datosAgrupados;

            // Configurar la primera serie para la cantidad de pedidos
            chartVentas.Series[0].Name = "Cantidad de Pedidos";
            chartVentas.Series[0].XValueMember = "Fecha"; // Establecer la propiedad X (Fecha)
            chartVentas.Series[0].XValueType = ChartValueType.DateTime;
            chartVentas.Series[0].YValueMembers = "CantidadPedidos"; // Establecer la propiedad Y (Cantidad de pedidos)
            chartVentas.Series[0].ChartType = SeriesChartType.Column; // Tipo de gráfico de columnas

            chartVentas.Series[0].ToolTip = "Fecha: #VALX{dd/MM/yyyy}\nCantidad: #VALY";


            // Configurar la segunda serie para el total facturado
            if (chartVentas.Series.Count < 2)
            {
                chartVentas.Series.Add("Total Facturado");
            }

            chartVentas.Series[1].Name = "Total Facturado";
            chartVentas.Series[1].XValueMember = "Fecha"; // Establecer la propiedad X (Fecha)
            chartVentas.Series[1].XValueType = ChartValueType.DateTime;
            chartVentas.Series[1].YValueMembers = "TotalFacturado"; // Establecer la propiedad Y (Total facturado)
            chartVentas.Series[1].ChartType = SeriesChartType.Line; // Tipo de gráfico de líneas para contraste

            // Asociar la segunda serie a un eje Y secundario
            chartVentas.Series[1].YAxisType = AxisType.Secondary;
            chartVentas.Series[1].ToolTip = "Fecha: #VALX{dd/MM/yyyy}\nTotal Facturado: $ #VALY{C}";

            // Configurar el ángulo de las etiquetas del eje X para que se muestren en vertical
            chartVentas.ChartAreas[0].AxisX.LabelStyle.Angle = 45;

            // Configurar los títulos de los ejes
            chartVentas.ChartAreas[0].AxisY.Title = "Cantidad de Pedidos";
            chartVentas.ChartAreas[0].AxisY2.Title = "Total Facturado";


            // Asegurarse de que solo se muestren las fechas con datos en el eje X
            chartVentas.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            chartVentas.ChartAreas[0].AxisX.Interval = 1;
            chartVentas.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = true;

            // Evitar que el eje X muestre días sin datos
            if (datosAgrupados.Count > 0)
            {
                chartVentas.ChartAreas[0].AxisX.Minimum = datosAgrupados.Min(d => d.Fecha).ToOADate();
                chartVentas.ChartAreas[0].AxisX.Maximum = datosAgrupados.Max(d => d.Fecha).ToOADate();
            }


            // ---- Habilitar zoom y scroll con la rueda del ratón ----
            chartVentas.ChartAreas[0].AxisX.ScaleView.Zoomable = true; // Permitir zoom en el eje X
            chartVentas.ChartAreas[0].AxisY.ScaleView.Zoomable = true; // Permitir zoom en el eje Y
            chartVentas.ChartAreas[0].CursorX.AutoScroll = true; // Permitir scroll horizontal
            chartVentas.ChartAreas[0].CursorY.AutoScroll = true; // Permitir scroll vertical



            // Refrescar el gráfico para mostrar los cambios
            chartVentas.DataBind();
        }



        private void iconButtonIntervalo_Click(object sender, EventArgs e)
        {


            ActualizarComprobantes(fechaInicio, fechaFinal, beCliente);
            ActualizarCharts(listaComprobantes);
        }

        private void dataGridViewClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Obtén el cliente correspondiente a la fila seleccionada
                beCliente = (BECliente)dataGridViewClientes.Rows[e.RowIndex].DataBoundItem;
                labelCliente.Text = beCliente.Nombre + " - " + beCliente.Direccion + " - " + beCliente.Localidad;
            }

            ActualizarComprobantes(fechaInicio, fechaFinal, beCliente);
            ActualizarCharts(listaComprobantes);
        }

        public void OcultarPanelClientes()
        {
            if (panelClientes.Visible == true)
            {
                panelClientes.Visible = false;
            }

        }

        private void Dashboard_Click(object sender, EventArgs e)
        {
            OcultarPanelClientes();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            OcultarPanelClientes();
        }

        private void tableLayoutPanel2_Click(object sender, EventArgs e)
        {
            OcultarPanelClientes();
        }

        private void panelLateral_Click(object sender, EventArgs e)
        {
            OcultarPanelClientes();
        }

        private void chartVentas_Click(object sender, EventArgs e)
        {
            OcultarPanelClientes();

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            OcultarPanelClientes();

        }

        private void panel6_Click(object sender, EventArgs e)
        {
            OcultarPanelClientes();

        }

        private void chart1_Click(object sender, EventArgs e)
        {
            OcultarPanelClientes();

        }

        private void iconButtonGeneral_Click(object sender, EventArgs e)
        {
            dateTimePickerFechaInicio.Value = DateTime.Today;
            dateTimePickerFechaFinal.Value = DateTime.Now;
            labelCliente.Text = "Todos";
            beCliente = null;
            ActualizarComprobantes(DateTime.Today, DateTime.Now, beCliente);
            ActualizarCharts(listaComprobantes);
        }
    }
}
