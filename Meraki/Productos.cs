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
            // Obtener la lista de productos ordenados por el método ToString()
            return bllProducto.listaProductos().OrderBy(producto => producto.ToString()).ToList();
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
            if (dataGridViewProductos.Rows.Count > 0)
            {
                dataGridViewProductos.Rows[0].Selected = true;
            }

            textBoxFiltrar.Text = placeholderText;
            textBoxFiltrar.ForeColor = System.Drawing.Color.Gray;
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
                int rowIndex = e.RowIndex;
                if (rowIndex >= 0)
                {
                    DataGridViewRow selectedRow = dataGridViewProductos.Rows[rowIndex];
                    if (selectedRow.Cells["Tipo"].Value.ToString() == "individual")
                    {
                        beProductoIndividual = (BEProductoIndividual)dataGridViewProductos.CurrentRow.DataBoundItem;
                        labelNombre.Text = beProductoIndividual.Stock.Nombre;
                        labelMedida.Text = beProductoIndividual.Stock.Medida.ToString() + " " + beProductoIndividual.Stock.TipoMedida;
                        labelUnidades.Text = beProductoIndividual.Unidad.ToString();
                        labelPrecioMayorista.Text = beProductoIndividual.PrecioMayorista.ToString();
                        labelPrecioMinorista.Text = beProductoIndividual.PrecioMinorista.ToString();
                    }
                    else
                    {
                        beProductoCombo = (BEProductoCombo)dataGridViewProductos.CurrentRow.DataBoundItem;
                        labelNombre.Text = beProductoCombo.Nombre;
                        labelMedida.Text = string.Empty;
                        labelUnidades.Text = beProductoCombo.Unidad.ToString();
                        labelPrecioMayorista.Text = beProductoCombo.PrecioMayorista.ToString();
                        labelPrecioMinorista.Text = beProductoCombo.PrecioMinorista.ToString();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void textBoxFiltrar_TextChanged_1(object sender, EventArgs e)
        {
            string textoABuscar = textBoxFiltrar.Text.ToLower();

            if (string.IsNullOrWhiteSpace(textoABuscar) || textoABuscar == "   buscar...")
            {
                dataGridViewProductos.DataSource = bllProducto.listaProductos().OrderBy(row => row.Codigo).ToList();


            }
            else
            {
                var tablaFiltrada = bllProducto.listaProductos().Where(row => row.Codigo.ToLower().Contains(textoABuscar) ||
            (row is BEProductoIndividual && ((BEProductoIndividual)row).Stock.Nombre.ToLower().Contains(textoABuscar)) || // Buscar por nombre de Stock (para ProductoIndividual)
        (row is BEProductoCombo && ((BEProductoCombo)row).Nombre.ToLower().Contains(textoABuscar)) // Buscar por nombre (para ProductoCombo)
            );
                dataGridViewProductos.DataSource = tablaFiltrada.ToList();
            }

        }

        private void iconButtonBaja_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewProductos.SelectedRows.Count > 0)
                {
                    var producto = dataGridViewProductos.CurrentRow.DataBoundItem;
                    if (producto is BEProductoIndividual)
                    {
                        beProductoIndividual = (BEProductoIndividual)dataGridViewProductos.CurrentRow.DataBoundItem;
                        DialogResult confirmacion;
                        confirmacion = MessageBox.Show("Confirmar baja de producto", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirmacion == DialogResult.Yes)
                        { bllProducto.BorrarProducto(beProductoIndividual); }
                        CargarDataGrid();
                        Limpiar();
                        dataGridViewProductos.Rows[0].Selected = true;
                    }
                    else if (producto is BEProductoCombo)
                    {
                        beProductoCombo = (BEProductoCombo)dataGridViewProductos.CurrentRow.DataBoundItem;
                        DialogResult confirmacion;
                        confirmacion = MessageBox.Show("Confirmar baja de producto", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirmacion == DialogResult.Yes)
                        { bllProductoCombo.BorrarProducto(beProductoCombo); }
                        CargarDataGrid();
                        Limpiar();
                        dataGridViewProductos.Rows[0].Selected = true;
                    }
                }
                else
                { MessageBox.Show("Debe seleccionar un cliente"); }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void iconButtonModificar_Click(object sender, EventArgs e)
        {
            var producto = dataGridViewProductos.CurrentRow.DataBoundItem;
            if (producto != null)
            {
                if (producto is BEProductoIndividual)
                {
                    beProductoIndividual = (BEProductoIndividual)producto;
                    ProductosModificar modificar = new ProductosModificar();
                    modificar.textBoxCodigo.Text = beProductoIndividual.Codigo;
                    modificar.textBoxNombre.Text = beProductoIndividual.Stock.ToString();
                    modificar.textBoxUnidades.Text = beProductoIndividual.Unidad.ToString();

                    modificar.textBoxPrecioMayorista.Text = beProductoIndividual.PrecioMayorista.ToString();
                    modificar.textBoxPrecioMinorista.Text = beProductoIndividual.PrecioMinorista.ToString();

                    modificar.dataGridViewCombo.Visible = false;

                    modificar.ShowDialog();
                    CargarDataGrid();
                    dataGridViewProductos.Rows[0].Selected = true;
                }
                else if (producto is BEProductoCombo)
                {
                    beProductoCombo = (BEProductoCombo)producto;
                    ProductosModificar modificar = new ProductosModificar();
                    modificar.textBoxCodigo.Text = beProductoCombo.Codigo;
                    modificar.textBoxNombre.Text = beProductoCombo.Nombre;
                    modificar.textBoxUnidades.Text = beProductoCombo.Unidad.ToString();
                    modificar.textBoxNombre.Enabled = true;
                    modificar.textBoxPrecioMayorista.Text = beProductoCombo.PrecioMayorista.ToString();
                    modificar.textBoxPrecioMinorista.Text = beProductoCombo.PrecioMinorista.ToString();
                    modificar.dataGridViewCombo.DataSource = beProductoCombo.ListaProductos;
                    modificar.dataGridViewCombo.Columns["Codigo"].Visible = false;
                    modificar.dataGridViewCombo.Columns["CantidadActual"].Visible = false;
                    modificar.dataGridViewCombo.Columns["CantidadIngresada"].Visible = false;
                    modificar.dataGridViewCombo.Columns["FechaIngreso"].Visible = false;
                    modificar.ConfigurarDataGrid(modificar.dataGridViewCombo);


                    modificar.ShowDialog();
                    dataGridViewProductos.Rows[0].Selected = true;
                }
                CargarDataGrid();

            }
            else
            { MessageBox.Show("debe seleccionar un producto"); }
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
    }
}
