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
        private bool puntoMayorista = false;
        private bool puntoMinorista = false;
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
            dataGridViewStock.Columns[0].Visible = false;
            dataGridViewStock.Columns[1].HeaderText = "Nombre";
            dataGridViewStock.Columns[4].Visible = false;
            dataGridViewStock.Columns[6].Visible = false;
            dataGridViewStock.Columns[5].Visible = false;
            dataGridViewStock.Columns[2].Visible = false;
            dataGridViewStock.Columns[3].Visible = false;
            ConfigurarDataGrid(dataGridViewStock);

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
            DataGridViewTextBoxColumn columnaCombinada = new DataGridViewTextBoxColumn();
            columnaCombinada.HeaderText = "Medida";
            columnaCombinada.Name = "columnaCombinada";
            dataGridView.Columns.Add(columnaCombinada);
            dataGridView.CellFormatting += dataGridViewStock_CellFormatting;

            ConfigurarEstilosColumnas(dataGridView);

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

        private void textBoxPrecioMayorista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                if ((e.KeyChar == '.') && (!puntoMayorista))
                {
                    puntoMayorista = true;
                    e.Handled = false; // permitir ingreso
                }

                else
                {
                    e.Handled = true; // no permitir ingreso
                }
            }
        }

        private void textBoxPrecioMinorista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                if ((e.KeyChar == '.') && (!puntoMinorista))
                {
                    puntoMinorista = true;
                    e.Handled = false; // permitir ingreso
                }

                else
                {
                    e.Handled = true; // no permitir ingreso
                }
            }
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






        private void iconButtonAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                beStock = (BEStock)dataGridViewStock.CurrentRow.DataBoundItem;
                productoCombo.ListaProductos.Add(beStock);
                CargarDataGridCombo();

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
                if (String.IsNullOrEmpty(textBoxNombreCombo.Text))
                {
                    MessageBox.Show("Debe colocarle un nombre al combo");
                }
                else
                {
                    productoCombo.Nombre = textBoxNombreCombo.Text.ToUpper();
                    if (String.IsNullOrEmpty(textBoxPrecioMayorista.Text))
                    { MessageBox.Show("Debe colocar un precio mayorista"); }
                    else
                    {
                        productoCombo.PrecioMayorista = Convert.ToDecimal(textBoxPrecioMayorista.Text);
                        if (String.IsNullOrEmpty(textBoxPrecioMinorista.Text))
                        { MessageBox.Show("Debe colocar un precio minorista"); }
                        else
                        {
                            productoCombo.PrecioMinorista = Convert.ToDecimal(textBoxPrecioMinorista.Text);
                            productoCombo.Unidad = 1;
                            productoCombo.Codigo = GenerarCodigoUnico();
                            productoCombo.Tipo = "combo";
                            bllProductoCombo.GuardarProducto(productoCombo);
                            DialogResult = DialogResult.OK;
                            Close();
                        }

                    }
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        private void iconButtonQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewCombo.Rows.Count > 0)
                {
                    beStock = (BEStock)dataGridViewCombo.CurrentRow.DataBoundItem;
                    productoCombo.ListaProductos.Remove(beStock);
                    CargarDataGridCombo();
                    beStock = null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void textBoxFiltrar_TextChanged_1(object sender, EventArgs e)
        {
            string filtro = textBoxFiltrar.Text.ToLower();
            var listaOriginal = bllStock.CargarStock();
            var listaFiltrada = listaOriginal.Where(item => item.Nombre.ToLower().Contains(filtro)).ToList();
            dataGridViewStock.DataSource = null;  // Limpiar el DataSource actual
            dataGridViewStock.DataSource = listaFiltrada;
            dataGridViewStock.Columns[0].Visible = false;
            dataGridViewStock.Columns[1].HeaderText = "Nombre";
            dataGridViewStock.Columns[3].HeaderText = "Tipo Medida";
            dataGridViewStock.Columns[4].Visible = false;
            dataGridViewStock.Columns[6].Visible = false;
            dataGridViewStock.Columns[5].Visible = false;
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
    }
}
