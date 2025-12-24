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

namespace Meraki
{
    public partial class AvisoPocoStock : Form
    {
        BEStock beStock;
        BLLStock bllStock;
        public AvisoPocoStock(BEStock stock)
        {
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
            if (!int.TryParse(textBoxAviso.Text, out int cantidadAviso) || cantidadAviso < 0)
            {
                MessageBox.Show("Por favor, ingresá un número válido.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            beStock.AvisoCantidadStock = cantidadAviso;

            DialogResult confirmacion = MessageBox.Show("¿Confirmar los días para dar aviso de poco stock?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.Yes)
            {
                bllStock.CantidadAviso(beStock);
                DialogResult = DialogResult.OK;
                Close();
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
    }
}
