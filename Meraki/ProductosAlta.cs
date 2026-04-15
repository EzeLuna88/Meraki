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
using System.Text.RegularExpressions;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace Meraki
{
    public partial class ProductosAlta : Form
    {
        BEProductoIndividual beProducto;
        BLLProducto bllProducto;
        BLLStock bllStock;
        BEStock beStock;


        public ProductosAlta()
        {
            beStock = new BEStock();
            bllStock = new BLLStock();
            beProducto = new BEProductoIndividual();
            bllProducto = new BLLProducto();
            InitializeComponent();
            CargarDataGrid();

            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
        }




        private void textBoxPrecioMayorista_Leave(object sender, EventArgs e)
        {
            decimal precio;
            if (decimal.TryParse(textBoxPrecioMayorista.Text, out precio))
            {
                beProducto.PrecioMayorista = Math.Round(precio, 2);
                textBoxPrecioMayorista.Text = beProducto.PrecioMayorista.ToString("0.00");
            }
        }

        private void textBoxPrecioMinorista_Leave(object sender, EventArgs e)
        {
            decimal precio;
            if (decimal.TryParse(textBoxPrecioMinorista.Text, out precio))
            {
                beProducto.PrecioMinorista = Math.Round(precio, 2);
                textBoxPrecioMinorista.Text = beProducto.PrecioMinorista.ToString("0.00");
            }
        }



        private void ProductosAlta_Load(object sender, EventArgs e)
        {
            if (dataGridViewStock.Rows.Count > 0)
            {
                dataGridViewStock.Rows[0].Selected = true;
            }

            textBoxFiltrar.Text = "   Buscar...";
            textBoxFiltrar.ForeColor = System.Drawing.Color.Gray;
        }

        public void CargarDataGrid()
        {
            dataGridViewStock.DataSource = null;
            var listaStockOrdenada = bllStock.CargarStock().OrderBy(stock => stock.Nombre).ToList();
            dataGridViewStock.DataSource = listaStockOrdenada;
            dataGridViewStock.Columns[1].HeaderText = "Nombre";
            dataGridViewStock.Columns[2].Visible = false;
            dataGridViewStock.Columns[3].Visible = false;
            dataGridViewStock.Columns[4].HeaderText = "Cantidad actual";
            dataGridViewStock.Columns[6].Visible = false;
            dataGridViewStock.Columns[5].Visible = false;
            dataGridViewStock.Columns[7].Visible = false;
            dataGridViewStock.Columns[8].Visible = false;

            ConfigurarDataGrid(dataGridViewStock);

        }

        public void ConfigurarDataGrid(DataGridView dataGridView)
        {
            dataGridView.CellFormatting += dataGridViewStock_CellFormatting_1;
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(217, 171, 171);
            dataGridView.RowHeadersVisible = false;
            dataGridView.Font = new System.Drawing.Font("Segoe UI", 9);
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.RowTemplate.Height = 25;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(146, 26, 64);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns[0].Width = 60;
            dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns[4].Width = 60;
            dataGridView.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView.Columns[4].HeaderText = "Cant. actual";
            dataGridView.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = dataGridViewStock.ColumnHeadersDefaultCellStyle.BackColor;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = dataGridViewStock.ColumnHeadersDefaultCellStyle.ForeColor;

        }



        private void textBoxFiltrar_TextChanged_1(object sender, EventArgs e)
        {
            string textoABuscar = textBoxFiltrar.Text.ToLower();

            // Si está vacío o tiene el texto de adorno, cargamos la grilla completa (que ahora ya viene ordenada)
            if (string.IsNullOrWhiteSpace(textoABuscar) || textoABuscar == "   buscar...")
            {
                CargarDataGrid();
            }
            else
            {
                // Si el usuario escribió algo, filtramos Y TAMBIÉN ordenamos los resultados
                var tablaFiltrada = bllStock.CargarStock()
                    .Where(row => row.Nombre.ToLower().Contains(textoABuscar) || row.Codigo.ToLower().Contains(textoABuscar))
                    .OrderBy(stock => stock.Nombre) // Mantenemos el orden alfabético en la búsqueda
                    .ToList();

                dataGridViewStock.DataSource = tablaFiltrada;
            }
        }

        private void dataGridViewStock_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex != this.dataGridViewStock.NewRowIndex)
            {
                e.Value = dataGridViewStock.Rows[e.RowIndex].Cells["nombre"].Value.ToString() + " "
                    + dataGridViewStock.Rows[e.RowIndex].Cells["medida"].Value.ToString() + " "
                    + dataGridViewStock.Rows[e.RowIndex].Cells["tipoMedida"].Value.ToString();
                e.FormattingApplied = true;
            }
        }

        private void iconButtonCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void panelBarra_MouseDown(object sender, MouseEventArgs e)
        {

        }

        //Drag form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void iconButtonCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void iconButtonMaximizar_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void iconButtonMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void ProductosAlta_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        // Este evento se lo vas a asignar a AMBOS TextBox de precios (Mayorista y Minorista)
        // Podés borrar los viejos textBoxPrecioMayorista_KeyPress y textBoxPrecioMinorista_KeyPress
        private void ValidarIngresoDecimal(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;

            // Permitir borrar (Backspace) y números
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Obtenemos el separador oficial del sistema (la coma en es-AR)
                char separadorDecimal = Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                // Si el usuario toca el punto del teclado numérico, lo convertimos a coma automáticamente
                if (e.KeyChar == '.' || e.KeyChar == ',')
                {
                    e.KeyChar = separadorDecimal;

                    // Verificamos si ya hay una coma en el texto. Si hay, no dejamos poner otra.
                    if (txt.Text.Contains(separadorDecimal))
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        e.Handled = false;
                    }
                }
                else
                {
                    e.Handled = true; // Bloquea letras y otros símbolos
                }
            }
        }

        private void iconButtonAlta_Click(object sender, EventArgs e)
        {
            // --- EL PATOVICA DE UI (Validaciones) ---
            if (dataGridViewStock.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un producto del stock base.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Frenamos la ejecución acá
            }

            if (string.IsNullOrWhiteSpace(textBoxUnidades.Text) || string.IsNullOrWhiteSpace(textBoxPrecioMayorista.Text))
            {
                MessageBox.Show("Los campos 'Unidades' y 'Precio Mayorista' son obligatorios.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Frenamos la ejecución acá
            }
            // ----------------------------------------

            List<BEProducto> listaProductos = bllProducto.listaProductos();

            beStock = (BEStock)dataGridViewStock.CurrentRow.DataBoundItem;
            beProducto.Stock = beStock;

            string baseCodigo = beStock.Codigo.ToUpper();
            string nuevoCodigo = baseCodigo;

            var productosConMismoCodigo = listaProductos.Where(p => p.Codigo.StartsWith(baseCodigo));

            if (productosConMismoCodigo.Any())
            {
                char ultimoCaracter = productosConMismoCodigo.Max(p => p.Codigo.Last());
                char nuevoCaracter = (char)(ultimoCaracter + 1);
                nuevoCodigo = baseCodigo + "-" + nuevoCaracter;
            }
            else
            {
                nuevoCodigo = baseCodigo + "-A";
            }

            beProducto.Codigo = nuevoCodigo;
            beProducto.Unidad = Convert.ToInt32(textBoxUnidades.Text);
            beProducto.PrecioMayorista = Convert.ToDecimal(textBoxPrecioMayorista.Text);

            if (string.IsNullOrWhiteSpace(textBoxPrecioMinorista.Text))
            {
                beProducto.PrecioMinorista = 0;
            }
            else
            {
                beProducto.PrecioMinorista = Convert.ToDecimal(textBoxPrecioMinorista.Text);
            }

            beProducto.Tipo = "individual";

            bllProducto.GuardarProducto(beProducto);

            MessageBox.Show("El producto fue creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            textBoxUnidades.Text = string.Empty;
            textBoxPrecioMayorista.Text = string.Empty;
            textBoxPrecioMinorista.Text = string.Empty;
        }

        private void textBoxFiltrar_Enter(object sender, EventArgs e)
        {
            if (textBoxFiltrar.Text == "   Buscar...")
            {
                textBoxFiltrar.Text = ""; // Limpia el TextBox
                textBoxFiltrar.ForeColor = System.Drawing.Color.Black; // Cambia el color del texto
            }
        }

        private void textBoxFiltrar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxFiltrar.Text))
            {
                textBoxFiltrar.Text = "   Buscar..."; // Restaura el texto del placeholder
                textBoxFiltrar.ForeColor = System.Drawing.Color.Gray; // Cambia el color del texto
            }
        }

        
    }
}
