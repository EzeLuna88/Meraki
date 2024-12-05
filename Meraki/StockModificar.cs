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
    public partial class StockModificar : Form
    {
        BEStock beStock;
        BLLStock bllStock;
        public StockModificar()
        {
            bllStock = new BLLStock();
            beStock= new BEStock();
            InitializeComponent();
            AsignarDatos(beStock);
            CargarComboBox();
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
        }

        public void AsignarProducto(BEStock stock)
        {
            beStock = stock;
        }

        public void AsignarDatos(BEStock stock) 
        { 
            textBoxCodigo.Text = beStock.Codigo;
            textBoxNombre.Text = beStock.Nombre;
            textBoxMedida.Text = beStock.Medida.ToString();
            comboBoxTipoMedida.Text = beStock.TipoMedida;
        }

        public void CargarComboBox()
        {
            comboBoxTipoMedida.Items.Add("lt.");
            comboBoxTipoMedida.Items.Add("ml.");
            comboBoxTipoMedida.Items.Add("cc.");
            comboBoxTipoMedida.Items.Add("grs.");
            comboBoxTipoMedida.Items.Add("kg.");

        }

        private void iconButtonCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; Close();

        }

        private void iconButtonModificar_Click(object sender, EventArgs e)
        {
            try
            {
                beStock.Codigo = textBoxCodigo.Text;
                if (String.IsNullOrEmpty(textBoxNombre.Text))
                { MessageBox.Show("Debe colocar un nombre"); }
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
                        beStock.TipoMedida = comboBoxTipoMedida.Text;
                    }
                    DialogResult confirmacion;
                    confirmacion = MessageBox.Show("Confirmar la modificacion de los datos?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirmacion == DialogResult.Yes)
                    {
                        bllStock.ModificarStock(beStock);
                        DialogResult = DialogResult.OK;
                        Close();
                    }



                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelBarra_MouseDown(object sender, MouseEventArgs e)
        {
           
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

        private void StockModificar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
