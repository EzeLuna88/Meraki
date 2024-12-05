using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;

namespace Meraki
{
    public partial class StockAgregar : Form
    {
        BLLStock bllStock;
        BEStock beStock;

        public StockAgregar()
        {
            bllStock = new BLLStock();
            beStock = new BEStock();
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;

        }

        public void AsignarProducto(BEStock stock)
        {
            beStock = stock;
        }





        private void textBoxPacks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void textBoxUnidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void iconButtonAgregarStock_Click(object sender, EventArgs e)
        {
            int totalUnidades = int.Parse(textBoxPacks.Text) * int.Parse(textBoxUnidad.Text);

            DialogResult confirmacion;

            confirmacion = MessageBox.Show("Agregar " + totalUnidades.ToString() + " unidades de " + beStock.Nombre + " " + beStock.Medida + beStock.TipoMedida, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question); ;
            if (confirmacion == DialogResult.Yes)
            { bllStock.AgregarStock(beStock, totalUnidades); }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void iconButtonCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; Close();
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

        private void StockAgregar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }


}
