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
        private bool puntoMayorista = false;
        private bool puntoMinorista = false;
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

        private void textBoxPrecioMayorista_KeyPress_1(object sender, KeyPressEventArgs e)
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

        private void textBoxPrecioMinorista_KeyPress_1(object sender, KeyPressEventArgs e)
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
            // ConfigurarEstilosColumnas(dataGridView);
        }

        private void iconButtonModificar_Click(object sender, EventArgs e)
        {
            if (dataGridViewCombo.Visible == false)
            {
                beProductoIndividual.Codigo = textBoxCodigo.Text;
                beProductoIndividual.Unidad = Convert.ToInt32(textBoxUnidades.Text);

                beProductoIndividual.PrecioMayorista = Convert.ToDecimal(textBoxPrecioMayorista.Text);
                beProductoIndividual.PrecioMinorista = Convert.ToDecimal(textBoxPrecioMinorista.Text);
                DialogResult confirmacion;
                confirmacion = MessageBox.Show("Confirmar la modificacion de los datos?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion == DialogResult.Yes)
                {
                    bllProducto.ModificarProducto(beProductoIndividual);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            else
            {
                beProductoCombo.Codigo = textBoxCodigo.Text;
                beProductoCombo.Unidad = Convert.ToInt32(textBoxUnidades.Text);
                beProductoCombo.Nombre = textBoxNombre.Text;
                beProductoCombo.PrecioMayorista = Convert.ToDecimal(textBoxPrecioMayorista.Text);
                beProductoCombo.PrecioMinorista = Convert.ToDecimal(textBoxPrecioMinorista.Text);
                DialogResult confirmacion;
                confirmacion = MessageBox.Show("Confirmar la modificacion de los datos?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion == DialogResult.Yes)
                {
                    bllProductoCombo.ModificarProducto(beProductoCombo);
                    DialogResult = DialogResult.OK;
                    Close();
                }
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

        private void ProductosModificar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
