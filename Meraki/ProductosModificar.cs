using BE;
using BLL;
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

namespace Meraki
{
    public partial class ProductosModificar : Form
    {
        BEProductoIndividual beProductoIndividual;
        BEProductoCombo beProductoCombo;
        BLLProducto bllProducto;
        BLLProductoCombo bllProductoCombo;

        public ProductosModificar()
        {
            beProductoIndividual = new BEProductoIndividual();
            beProductoCombo = new BEProductoCombo();
            bllProducto = new BLLProducto();
            bllProductoCombo = new BLLProductoCombo();
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;

        }


        private void textBoxPrecioMayorista_Leave_1(object sender, EventArgs e)
        {
            decimal precio;
            if (decimal.TryParse(textBoxPrecioMayorista.Text, out precio))
            {
                if (dataGridViewCombo.Visible == false)
                {
                    beProductoIndividual.PrecioMayorista = Math.Round(precio, 2);
                    textBoxPrecioMayorista.Text = beProductoIndividual.PrecioMayorista.ToString("0.00");
                }
                else
                {
                    beProductoCombo.PrecioMayorista = Math.Round(precio, 2);
                    textBoxPrecioMayorista.Text = beProductoCombo.PrecioMayorista.ToString("0.00");
                }
            }
        }

        private void textBoxPrecioMinorista_Leave_1(object sender, EventArgs e)
        {
            decimal precio;
            if (decimal.TryParse(textBoxPrecioMinorista.Text, out precio))
            {
                if (dataGridViewCombo.Visible == false)
                {
                    beProductoIndividual.PrecioMinorista = Math.Round(precio, 2);
                    textBoxPrecioMinorista.Text = beProductoIndividual.PrecioMinorista.ToString("0.00");
                }
                else
                {
                    beProductoCombo.PrecioMinorista = Math.Round(precio, 2);
                    textBoxPrecioMinorista.Text = beProductoCombo.PrecioMinorista.ToString("0.00");
                }
            }
        }

        private void textBoxUnidades_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
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

            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(48, 8, 21); // Bordó oscuro
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(244, 217, 208); // Crema claro

            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = dataGridView.ColumnHeadersDefaultCellStyle.BackColor;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = dataGridView.ColumnHeadersDefaultCellStyle.ForeColor;
            // Configuración específica de estilo para columnas
            // ConfigurarEstilosColumnas(dataGridView);
        }

        private void iconButtonModificar_Click(object sender, EventArgs e)
        {
            // --- PATOVICA DE UI (Evita vacíos y formatos raros) ---
            if (string.IsNullOrWhiteSpace(textBoxUnidades.Text) ||
                string.IsNullOrWhiteSpace(textBoxPrecioMayorista.Text))
            {
                MessageBox.Show("Los campos 'Unidades' y 'Precio Mayorista' no pueden estar vacíos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Por las dudas, forzamos el Precio Minorista a 0 si lo dejaron vacío
            if (string.IsNullOrWhiteSpace(textBoxPrecioMinorista.Text))
            {
                textBoxPrecioMinorista.Text = "0";
            }
            // -----------------------------------------------------

            try
            {
                if (dataGridViewCombo.Visible == false) // Es Individual
                {
                    beProductoIndividual.Codigo = textBoxCodigo.Text;
                    beProductoIndividual.Unidad = Convert.ToInt32(textBoxUnidades.Text);
                    beProductoIndividual.PrecioMayorista = Convert.ToDecimal(textBoxPrecioMayorista.Text);
                    beProductoIndividual.PrecioMinorista = Convert.ToDecimal(textBoxPrecioMinorista.Text);

                    DialogResult confirmacion = MessageBox.Show("¿Confirmar la modificación de los datos?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirmacion == DialogResult.Yes)
                    {
                        bllProducto.ModificarProducto(beProductoIndividual);
                        MessageBox.Show("Producto modificado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
                else // Es Combo
                {
                    beProductoCombo.Codigo = textBoxCodigo.Text;
                    beProductoCombo.Unidad = Convert.ToInt32(textBoxUnidades.Text);
                    beProductoCombo.Nombre = textBoxNombre.Text;
                    beProductoCombo.PrecioMayorista = Convert.ToDecimal(textBoxPrecioMayorista.Text);
                    beProductoCombo.PrecioMinorista = Convert.ToDecimal(textBoxPrecioMinorista.Text);

                    DialogResult confirmacion = MessageBox.Show("¿Confirmar la modificación de los datos?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirmacion == DialogResult.Yes)
                    {
                        bllProductoCombo.ModificarProducto(beProductoCombo);
                        MessageBox.Show("Combo modificado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar modificar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconButtonCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; Close();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);



        private void ProductosModificar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
