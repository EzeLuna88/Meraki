using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Meraki
{
    public partial class Productos : Form
    {
        BEProductoIndividual beProductoIndividual;
        BEProductoCombo beProductoCombo;
        BLLProducto bllProducto;
        BLLProductoCombo bllProductoCombo;
        private const string placeholderText = "   Buscar...";

        public Productos()
        {
            string codigoProductoSeleccionado = "";
            beProductoIndividual = new BEProductoIndividual();
            beProductoCombo = new BEProductoCombo();
            bllProducto = new BLLProducto();
            bllProductoCombo = new BLLProductoCombo();
            InitializeComponent();
            CargarDataGrid();

        }

        public void CargarDataGrid()
        {
            dataGridViewProductos.DataSource = null;

            // Obtener y ordenar la lista de productos
            List<BEProducto> listaProductos = ObtenerListaProductosOrdenada();
            var bindingList = new BindingList<BEProducto>(listaProductos);

            // Asignar la lista enlazada al DataSource
            dataGridViewProductos.DataSource = bindingList;

            // Configurar columnas y su formato
            ConfigurarColumnasDataGrid(dataGridViewProductos);

            // Aplicar configuración general al DataGridView
            ConfigurarDataGrid(dataGridViewProductos);

            dataGridViewProductos.Columns["NombreMostrar"].Visible = false;
            dataGridViewProductos.Columns[2].Visible = false;

        }

        private List<BEProducto> ObtenerListaProductosOrdenada()
        {
            var listaCruda = bllProducto.listaProductos();

            // Ordenamos explícitamente sacando el nombre real de cada tipo de producto
            var listaOrdenada = listaCruda.OrderBy(producto =>
            {
                if (producto is BEProductoIndividual individual)
                {
                    // Si es un producto suelto, usamos el nombre de su stock
                    return individual.Stock.Nombre;
                }
                else if (producto is BEProductoCombo combo)
                {
                    // Si es un combo, usamos el nombre del combo
                    return combo.Nombre;
                }

                // Por las dudas, un valor por defecto para que no falle
                return "";
            }).ToList();

            return listaOrdenada;
        }

        private void ConfigurarColumnasDataGrid(DataGridView dataGridView)
        {
            // Configuración específica de columnas
            dataGridView.Columns["Tipo"].Visible = false;
            dataGridView.Columns["Unidad"].HeaderText = "Unidades";
            dataGridView.Columns["precioMayorista"].HeaderText = "Precio Mayorista";
            dataGridView.Columns["precioMinorista"].HeaderText = "Precio Minorista";
            dataGridView.Columns["precioMayorista"].DefaultCellStyle.Format = "c2";
            dataGridView.Columns["precioMinorista"].DefaultCellStyle.Format = "c2";

            // Asegurarse de que la columna "nombre" exista antes de modificar su índice de visualización
            if (dataGridView.Columns["nombre"] == null)
            {
                dataGridView.Columns.Add("nombre", "Nombre");
            }

            dataGridView.Columns["nombre"].DisplayIndex = 1;
            dataGridView.Columns["codigo"].DisplayIndex = 0;
            dataGridView.Columns["Unidad"].DisplayIndex = 2;

            // Agregar manejador de formato de celda
            dataGridViewProductos.CellFormatting += dataGridViewProductos_CellFormatting;
        }

        public void ConfigurarDataGrid(DataGridView dataGridView)
        {
            // Configuración general del DataGridView
            dataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(217, 171, 171);
            dataGridView.RowHeadersVisible = false;
            dataGridView.Font = new System.Drawing.Font("Segoe UI", 9);
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.RowTemplate.Height = 25;
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
            ConfigurarEstilosColumnas(dataGridView);
        }

        private void ConfigurarEstilosColumnas(DataGridView dataGridView)
        {
            // Configuración de estilo para la columna "Unidad"
            dataGridView.Columns["Unidad"].HeaderText = "Un.";
            dataGridView.Columns["Unidad"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["Unidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["Unidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns["Unidad"].Width = 70;

            // Configuración de estilo para la columna "Codigo"
            dataGridView.Columns["Codigo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns["Codigo"].Width = 60;
            dataGridView.Columns["Codigo"].HeaderText = "Cod.";

            // Configuración de estilo para la columna "PrecioMayorista"
            dataGridView.Columns["PrecioMayorista"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns["PrecioMayorista"].Width = 90;
            dataGridView.Columns["PrecioMayorista"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns["PrecioMayorista"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Configuración de estilo para la columna "PrecioMinorista"
            dataGridView.Columns["PrecioMinorista"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns["PrecioMinorista"].Width = 90;
            dataGridView.Columns["PrecioMinorista"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns["PrecioMinorista"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }






        public void Limpiar()
        {
            try
            {
                labelNombre.Text = string.Empty;
                labelUnidades.Text = string.Empty;
                labelMedida.Text = string.Empty;
                labelPrecioMayorista.Text = string.Empty;
                labelPrecioMinorista.Text = string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            textBoxFiltrar.Text = placeholderText;
            textBoxFiltrar.ForeColor = System.Drawing.Color.Gray;

            if (dataGridViewProductos.Rows.Count > 0)
            {
                // 1. Seleccionamos la fila visualmente
                dataGridViewProductos.Rows[0].Selected = true;

                // 2. Le ponemos el foco (el cursor) a esa fila
                dataGridViewProductos.CurrentCell = dataGridViewProductos.Rows[0].Cells[1];

                // 3. SIMULAMOS UN CLIC: Llamamos manualmente a tu método de clic
                // Le pasamos el número de fila 0, y la columna 0.
                DataGridViewCellEventArgs args = new DataGridViewCellEventArgs(0, 0);
                dataGridViewProductos_CellClick_1(this, args);
            }
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

        private void iconButtonAlta_Click(object sender, EventArgs e)
        {
            ProductosAlta productosAlta = new ProductosAlta();
            productosAlta.ShowDialog();
            CargarDataGrid();
        }

        private void dataGridViewProductos_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Verificamos que no hayan hecho clic en el encabezado
                if (e.RowIndex >= 0 && dataGridViewProductos.CurrentRow != null)
                {
                    var productoSeleccionado = dataGridViewProductos.CurrentRow.DataBoundItem;

                    // Limpiamos todo antes de asignar (por si un combo no tiene medida, no queda la medida del producto anterior)
                    Limpiar();

                    if (productoSeleccionado is BEProductoIndividual individual)
                    {
                        beProductoIndividual = individual;
                        labelNombre.Text = individual.Stock.Nombre;
                        labelMedida.Text = $"{individual.Stock.Medida} {individual.Stock.TipoMedida}";
                        labelUnidades.Text = individual.Unidad.ToString();
                        labelPrecioMayorista.Text = individual.PrecioMayorista.ToString("C2"); // C2 formatea como moneda
                        labelPrecioMinorista.Text = individual.PrecioMinorista.ToString("C2");
                    }
                    else if (productoSeleccionado is BEProductoCombo combo)
                    {
                        beProductoCombo = combo;
                        labelNombre.Text = combo.Nombre;
                        // El combo no tiene medida, así que queda vacío como lo definió Limpiar()
                        labelUnidades.Text = combo.Unidad.ToString();
                        labelPrecioMayorista.Text = combo.PrecioMayorista.ToString("C2");
                        labelPrecioMinorista.Text = combo.PrecioMinorista.ToString("C2");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al mostrar los detalles del producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxFiltrar_TextChanged_1(object sender, EventArgs e)
        {
            string textoABuscar = textBoxFiltrar.Text.ToLower();

            if (string.IsNullOrWhiteSpace(textoABuscar) || textoABuscar == "   buscar...")
            {
                // ACÁ ESTABA EL PROBLEMA: Llamamos al método que ya arma la lista ordenada alfabéticamente
                dataGridViewProductos.DataSource = ObtenerListaProductosOrdenada();
            }
            else
            {
                var tablaFiltrada = bllProducto.listaProductos().Where(row => row.Codigo.ToLower().Contains(textoABuscar) ||
                    (row is BEProductoIndividual individual && individual.Stock.Nombre.ToLower().Contains(textoABuscar)) ||
                    (row is BEProductoCombo combo && combo.Nombre.ToLower().Contains(textoABuscar))
                );

                // Aprovechamos y ordenamos también cuando el usuario busca algo
                var tablaOrdenada = tablaFiltrada.OrderBy(producto =>
                {
                    if (producto is BEProductoIndividual ind) return ind.Stock.Nombre;
                    if (producto is BEProductoCombo com) return com.Nombre;
                    return "";
                }).ToList();

                dataGridViewProductos.DataSource = tablaOrdenada;
            }
        }

        private void iconButtonBaja_Click(object sender, EventArgs e)
        {
            if (dataGridViewProductos.SelectedRows.Count == 0 || dataGridViewProductos.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un producto para darlo de baja.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show("¿Está seguro que desea eliminar este producto?", "Confirmar Baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    var producto = dataGridViewProductos.CurrentRow.DataBoundItem;

                    // Pasamos la responsabilidad a las BLL correspondientes
                    if (producto is BEProductoIndividual individual)
                    {
                        bllProducto.BorrarProducto(individual);
                    }
                    else if (producto is BEProductoCombo combo)
                    {
                        bllProductoCombo.BorrarProducto(combo);
                    }

                    MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarDataGrid();
                    Limpiar();
                }
                catch (InvalidOperationException ex) // <-- Atajaría una regla de negocio (Ej: "No se puede borrar porque tiene stock")
                {
                    MessageBox.Show(ex.Message, "Operación no permitida", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar eliminar el producto: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void iconButtonModificar_Click(object sender, EventArgs e)
        {
            if (dataGridViewProductos.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un producto");
                return;
            }

            var producto = (BEProducto)dataGridViewProductos.CurrentRow.DataBoundItem;
            string codigoABuscar = producto.Codigo; // Guardamos el código para re-ubicarlo después

            ProductosModificar modificar = new ProductosModificar();

            if (producto is BEProductoIndividual individual)
            {
                beProductoIndividual = individual;
                modificar.textBoxCodigo.Text = beProductoIndividual.Codigo;
                modificar.textBoxNombre.Text = beProductoIndividual.Stock.ToString();
                modificar.textBoxUnidades.Text = beProductoIndividual.Unidad.ToString();
                modificar.textBoxPrecioMayorista.Text = beProductoIndividual.PrecioMayorista.ToString();
                modificar.textBoxPrecioMinorista.Text = beProductoIndividual.PrecioMinorista.ToString();
                modificar.dataGridViewCombo.Visible = false;
            }
            else if (producto is BEProductoCombo combo)
            {
                beProductoCombo = combo;
                modificar.textBoxCodigo.Text = beProductoCombo.Codigo;
                modificar.textBoxNombre.Text = beProductoCombo.Nombre;
                modificar.textBoxUnidades.Text = beProductoCombo.Unidad.ToString();
                modificar.textBoxNombre.Enabled = true;
                modificar.textBoxPrecioMayorista.Text = beProductoCombo.PrecioMayorista.ToString();
                modificar.textBoxPrecioMinorista.Text = beProductoCombo.PrecioMinorista.ToString();

                // --- CONFIGURACIÓN DE LA GRILLA INTERNA DEL COMBO ---
                modificar.dataGridViewCombo.DataSource = beProductoCombo.ListaProductos;

                // 1. Creamos la columna "todo en uno" si no existe
                if (modificar.dataGridViewCombo.Columns["ColumnaDetalle"] == null)
                {
                    DataGridViewTextBoxColumn colDetalle = new DataGridViewTextBoxColumn();
                    colDetalle.Name = "ColumnaDetalle";
                    colDetalle.HeaderText = "Producto";
                    modificar.dataGridViewCombo.Columns.Insert(0, colDetalle);
                }

                // 2. Ocultamos todas las columnas originales y técnicas
                string[] columnasAOcultar = { "Nombre", "Medida", "TipoMedida", "Codigo", "CantidadActual", "CantidadReservada", "AvisoCantidadStock", "CantidadIngresada", "FechaIngreso" };
                foreach (string col in columnasAOcultar)
                {
                    if (modificar.dataGridViewCombo.Columns[col] != null)
                        modificar.dataGridViewCombo.Columns[col].Visible = false;
                }

                // 3. Formateo de celda: Nombre + Medida + TipoMedida
                modificar.dataGridViewCombo.CellFormatting += (s, ev) =>
                {
                    if (ev.ColumnIndex == 0 && ev.RowIndex >= 0)
                    {
                        var grid = (DataGridView)s;
                        var fila = grid.Rows[ev.RowIndex];
                        string n = fila.Cells["Nombre"].Value?.ToString() ?? "";
                        string m = fila.Cells["Medida"].Value?.ToString() ?? "";
                        string t = fila.Cells["TipoMedida"].Value?.ToString() ?? "";

                        if (m == "0" || t == "-") { ev.Value = n; }
                        else { ev.Value = $"{n} {m} {t}"; }

                        ev.FormattingApplied = true;
                    }
                };

                modificar.dataGridViewCombo.Columns["ColumnaDetalle"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                modificar.ConfigurarDataGrid(modificar.dataGridViewCombo);
            }

            modificar.ShowDialog();
            CargarDataGrid(); // Recargamos para ver cambios

            // --- RE-POSICIONAR LA SELECCIÓN ---
            if (!string.IsNullOrEmpty(codigoABuscar))
            {
                dataGridViewProductos.ClearSelection();
                foreach (DataGridViewRow row in dataGridViewProductos.Rows)
                {
                    if (row.DataBoundItem is BEProducto prodEnGrilla && prodEnGrilla.Codigo == codigoABuscar)
                    {
                        dataGridViewProductos.CurrentCell = row.Cells[1]; // Foco en el nombre
                        row.Selected = true;
                        if (row.Index >= 0) dataGridViewProductos.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }
            }
        }

        private void iconButtonCrearCombo_Click(object sender, EventArgs e)
        {
            ProductosCrearCombo productosCrearCombo = new ProductosCrearCombo();
            productosCrearCombo.ShowDialog();
            CargarDataGrid();
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

        private void Productos_VisibleChanged(object sender, EventArgs e)
        {

        }
    }
}
