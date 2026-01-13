using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;

namespace Meraki
{
    public partial class StockNuevoProducto : Form
    {
        BEStock beStock;
        BLLStock bllStock;
        public StockNuevoProducto()
        {
            beStock= new BEStock();
            bllStock = new BLLStock();
            InitializeComponent();
            CargarComboBox();
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
        }

     

        private int ObtenerUltimoCodigo()
        {
            try
            {
                // Cargar el stock desde el archivo XML
                List<BEStock> stock = bllStock.CargarStock();

                // Si no hay ningún producto en el stock, el último código sería 0
                if (stock.Count == 0)
                    return 0;

                // Ordenar la lista de productos por el código de forma descendente
                var productosOrdenados = stock.OrderByDescending(p => int.Parse(p.Codigo));

                // Obtener el primer producto (que tendría el código más alto)
                BEStock ultimoProducto = productosOrdenados.First();

                // Convertir el código a entero y devolverlo
                return int.Parse(ultimoProducto.Codigo);
            }
            catch (Exception)
            {
                // Manejar la excepción apropiadamente según tu lógica de aplicación
                throw;
            }
        }

        public void CargarComboBox()
        {
            comboBoxTipoMedida.Items.Add("lt.");
            comboBoxTipoMedida.Items.Add("ml.");
            comboBoxTipoMedida.Items.Add("cc.");
            comboBoxTipoMedida.Items.Add("grs.");
            comboBoxTipoMedida.Items.Add("kg.");

        }

        private void textBoxMedida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

      

        private void StockNuevoProducto_Load(object sender, EventArgs e)
        {
            comboBoxTipoMedida.SelectedIndex = 0;
        }

        private void textBoxMedida_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBoxCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {
            textBoxNombre.Text = textBoxNombre.Text.ToUpper();
            textBoxNombre.SelectionStart = textBoxNombre.Text.Length;
        }

        private void iconButtonCargar_Click(object sender, EventArgs e)
        {
            try
            {
                int ultimoCodigo = ObtenerUltimoCodigo();
                int nuevoCodigo = ultimoCodigo + 1;
                beStock.Codigo = nuevoCodigo.ToString();

                // VALIDACIÓN DE FECHA DE VENCIMIENTO
                DateTime fechaDeVencimiento;
                bool fechaValida = DateTime.TryParseExact(
                    maskedTextBoxFechaDeVencimiento.Text,
                    "dd/MM/yyyy",
                    null,
                    DateTimeStyles.None,
                    out fechaDeVencimiento
                );

                if (!fechaValida)
                {
                    MessageBox.Show("La fecha ingresada no es válida. Usá el formato dd/MM/yyyy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (fechaDeVencimiento <= DateTime.Today)
                {
                    MessageBox.Show("La fecha de vencimiento debe ser posterior al día de hoy.", "Fecha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // VALIDACIONES Y CARGA DE STOCK
                if (String.IsNullOrEmpty(textBoxNombre.Text))
                {
                    MessageBox.Show("Debe colocar un nombre");
                }
                else
                {
                    beStock.Nombre = textBoxNombre.Text.ToUpper();

                    if (String.IsNullOrEmpty(textBoxMedida.Text))
                    {
                        beStock.Medida = 0;
                        beStock.TipoMedida = "-";
                    }
                    else
                    {
                        beStock.Medida = Convert.ToDouble(textBoxMedida.Text);
                        beStock.TipoMedida = comboBoxTipoMedida.SelectedItem?.ToString() ?? "-";
                    }

                    if (String.IsNullOrEmpty(textBoxCantidad.Text))
                    {
                        MessageBox.Show("Debe ingresar la cantidad de stock que ingresó del producto");
                    }
                    else
                    {
                        beStock.CantidadActual = Convert.ToInt32(textBoxCantidad.Text);

                        if (bllStock.ComprobarRepetido(beStock))
                        {
                            MessageBox.Show("El producto a cargar ya se encuentra en el stock");
                        }
                        else
                        {
                            bllStock.GuardarNuevoProducto(beStock);
                            bllStock.CargarFechaDeVencimiento(beStock, fechaDeVencimiento, beStock.CantidadActual);
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

        private void iconButtonCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; Close();

        }

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

        //Drag form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        

        private void StockNuevoProducto_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
