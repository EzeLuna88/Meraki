using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using BE;
using BLL;

namespace Meraki
{
    public partial class ProductosCrearCombo : Form
    {
        BLLStock bllStock;
        BLLProductoCombo bllProductoCombo;
        BEStock beStock;
        BEProductoCombo productoCombo;
        private BindingSource bindingSource = new BindingSource();


        public ProductosCrearCombo()
        {
            beStock = new BEStock();
            productoCombo = new BEProductoCombo();
            bllStock = new BLLStock();
            bllProductoCombo = new BLLProductoCombo();
            InitializeComponent();
            CargarDataGrid();

            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
        }

        public void CargarDataGrid()
        {
            dataGridViewStock.DataSource = null;
            dataGridViewStock.DataSource = bllStock.CargarStock();

            ConfigurarDataGrid(dataGridViewStock);

        }

        public void ConfigurarDataGrid(DataGridView dataGridView)
        {
            // Verificamos y agregamos la columna combinada si no existe
            if (!dataGridView.Columns.Contains("columnaCombinada"))
            {
                DataGridViewTextBoxColumn columnaCombinada = new DataGridViewTextBoxColumn();
                columnaCombinada.HeaderText = "Medida";
                columnaCombinada.Name = "columnaCombinada";
                dataGridView.Columns.Add(columnaCombinada);
            }

            // Ocultamos TODO primero por seguridad
            foreach (DataGridViewColumn col in dataGridView.Columns)
            {
                col.Visible = false;
            }

            // Mostramos SOLO lo que queremos ver, usando NOMBRES de columnas
            if (dataGridView.Columns.Contains("Nombre"))
            {
                dataGridView.Columns["Nombre"].Visible = true;
                dataGridView.Columns["Nombre"].HeaderText = "Nombre";
                dataGridView.Columns["Nombre"].DisplayIndex = 0; // Primera
            }

            if (dataGridView.Columns.Contains("columnaCombinada"))
            {
                dataGridView.Columns["columnaCombinada"].Visible = true;
                dataGridView.Columns["columnaCombinada"].HeaderText = "Medida";
                dataGridView.Columns["columnaCombinada"].DisplayIndex = 1; // Segunda
            }

            // Diseño general
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

            // Re-asociamos el evento de formateo (¡muy importante!)
            dataGridView.CellFormatting -= dataGridViewStock_CellFormatting; // Lo desenganchamos por si acaso
            dataGridView.CellFormatting += dataGridViewStock_CellFormatting;
        }

        private void ConfigurarEstilosColumnas(DataGridView dataGridView)
        {



            //dataGridView.Columns[7].Width = 90;


        }

        private void CargarDataGridCombo()
        {
            dataGridViewCombo.Columns.Clear();
            dataGridViewCombo.DataSource = null;
            dataGridViewCombo.DataSource = productoCombo.ListaProductos;
            dataGridViewCombo.Columns["Codigo"].Visible = false;
            dataGridViewCombo.Columns["CantidadActual"].Visible = false;
            dataGridViewCombo.Columns["CantidadIngresada"].Visible = false;
            dataGridViewCombo.Columns["FechaIngreso"].Visible = false;
            dataGridViewCombo.Columns[2].Visible = false;
            dataGridViewCombo.Columns[3].Visible = false;
            ConfigurarDataGrid(dataGridViewCombo);

        }



        private void textBoxPrecioMinorista_Leave(object sender, EventArgs e)
        {
            decimal precio;
            if (decimal.TryParse(textBoxPrecioMinorista.Text, out precio))
            {
                productoCombo.PrecioMinorista = Math.Round(precio, 2);
                textBoxPrecioMinorista.Text = productoCombo.PrecioMinorista.ToString("0.00");
            }
        }

        private void textBoxPrecioMayorista_Leave(object sender, EventArgs e)
        {
            decimal precio;
            if (decimal.TryParse(textBoxPrecioMayorista.Text, out precio))
            {
                productoCombo.PrecioMayorista = Math.Round(precio, 2);
                textBoxPrecioMayorista.Text = productoCombo.PrecioMayorista.ToString("0.00");
            }
        }


        private string GenerarCodigoUnico()
        {
            Random random = new Random();
            string codigo;

            // Verificar que el código sea único
            do
            {
                codigo = random.Next(10000, 100000).ToString(); // Genera un número de 5 dígitos
            } while (bllProductoCombo.CodigoYaExiste(codigo));

            return codigo;
        }

        private void ValidarIngresoDecimal(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                char separadorDecimal = Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                if (e.KeyChar == '.' || e.KeyChar == ',')
                {
                    e.KeyChar = separadorDecimal;
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
                    e.Handled = true;
                }
            }
        }




        private void iconButtonAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewStock.CurrentRow != null)
                {
                    beStock = (BEStock)dataGridViewStock.CurrentRow.DataBoundItem;
                    productoCombo.ListaProductos.Add(beStock);
                    CargarDataGridCombo();
                }
                else
                {
                    MessageBox.Show("Seleccione un producto del stock para agregar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void iconButtonCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; Close();
        }

        private void iconButtonAlta_Click(object sender, EventArgs e)
        {
            try
            {
                // --- PATOVICA DE UI ---
                if (string.IsNullOrWhiteSpace(textBoxNombreCombo.Text))
                {
                    MessageBox.Show("Debe colocarle un nombre al combo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxPrecioMayorista.Text))
                {
                    MessageBox.Show("Debe colocar un precio mayorista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validamos que el combo tenga al menos un ingrediente
                if (productoCombo.ListaProductos == null || productoCombo.ListaProductos.Count == 0)
                {
                    MessageBox.Show("Debe agregar al menos un producto a la lista del combo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // -----------------------

                productoCombo.Nombre = textBoxNombreCombo.Text.ToUpper();
                productoCombo.PrecioMayorista = Convert.ToDecimal(textBoxPrecioMayorista.Text);

                if (string.IsNullOrWhiteSpace(textBoxPrecioMinorista.Text))
                {
                    productoCombo.PrecioMinorista = 0;
                }
                else
                {
                    productoCombo.PrecioMinorista = Convert.ToDecimal(textBoxPrecioMinorista.Text);
                }

                productoCombo.Unidad = 1;
                productoCombo.Codigo = GenerarCodigoUnico();
                productoCombo.Tipo = "combo";

                bllProductoCombo.GuardarProducto(productoCombo);

                MessageBox.Show("Combo creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al guardar el combo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconButtonQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                // Patovica: Validamos que haya algo para quitar
                if (dataGridViewCombo.CurrentRow != null && dataGridViewCombo.Rows.Count > 0)
                {
                    var itemAQuitar = (BEStock)dataGridViewCombo.CurrentRow.DataBoundItem;
                    productoCombo.ListaProductos.Remove(itemAQuitar);
                    CargarDataGridCombo();
                }
                else
                {
                    MessageBox.Show("Seleccione un producto de la lista del combo para quitarlo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al quitar el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxFiltrar_TextChanged_1(object sender, EventArgs e)
        {
            string filtro = textBoxFiltrar.Text.ToLower();

            if (string.IsNullOrWhiteSpace(filtro) || filtro == "   buscar...")
            {
                dataGridViewStock.DataSource = bllStock.CargarStock();
            }
            else
            {
                var listaOriginal = bllStock.CargarStock();
                var listaFiltrada = listaOriginal.Where(item => item.Nombre.ToLower().Contains(filtro)).ToList();
                dataGridViewStock.DataSource = listaFiltrada;
            }

            // SIMPLEMENTE VOLVEMOS A LLAMAR AL MÉTODO MAESTRO
            ConfigurarDataGrid(dataGridViewStock);
        }

        private void dataGridViewStock_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;

            // Verificar si la columna es la columna combinada
            if (dataGridView.Columns[e.ColumnIndex].Name == "columnaCombinada")
            {
                // Obtener los valores de las columnas "medida" y "tipoMedida"
                string medida = dataGridView.Rows[e.RowIndex].Cells["medida"].Value.ToString();
                string tipoMedida = dataGridView.Rows[e.RowIndex].Cells["tipoMedida"].Value.ToString();

                // Combinar los valores en un solo string
                e.Value = $"{medida} {tipoMedida}";
                e.FormattingApplied = true; // Indicar que el formato ha sido aplicado
            }
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);







        private void ProductosCrearCombo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void ProductosCrearCombo_Load(object sender, EventArgs e)
        {
            textBoxFiltrar.Text = "   Buscar...";
            textBoxFiltrar.ForeColor = System.Drawing.Color.Gray;
        }

        private void textBoxFiltrar_Enter(object sender, EventArgs e)
        {
            if (textBoxFiltrar.Text == "   Buscar...")
            {
                textBoxFiltrar.Text = "";
                textBoxFiltrar.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void textBoxFiltrar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxFiltrar.Text))
            {
                textBoxFiltrar.Text = "   Buscar...";
                textBoxFiltrar.ForeColor = System.Drawing.Color.Gray;
            }
        }
    }
}
