using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Meraki
{
    public partial class AvisoPocoStock : Form
    {
        BEStock beStock;
        BLLStock bllStock;
        public AvisoPocoStock(BEStock stock)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;

            beStock = stock;
            bllStock = new BLLStock();
            InitializeComponent();
            textBoxAviso.KeyPress += textBoxAviso_KeyPress;
            textBoxAviso.Text = beStock.AvisoCantidadStock.ToString();
        }

        public void AsignarProducto(BEStock stock)
        {
            beStock = stock;
        }

        private void iconButtonModificar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxAviso.Text, out int cantidadAviso))
            {
                MessageBox.Show("Por favor, ingresá un número válido.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show("¿Confirmar los cambios para el aviso de poco stock?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    beStock.AvisoCantidadStock = cantidadAviso;

                    bllStock.CantidadAviso(beStock);

                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Configuración inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxAviso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Cancela la tecla
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

        private void AvisoPocoStock_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
