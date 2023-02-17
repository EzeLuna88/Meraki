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

namespace Meraki
{
    public partial class StockAgregar : Form
    {
        BLLStock bllStock;
        BEStock beStock;
        
        public StockAgregar()
        {
            bllStock = new BLLStock();
            beStock= new BEStock(); 
            InitializeComponent();
            
        }

        public void AsignarProducto(BEStock stock)
        {
            beStock = stock;
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; Close();
        }

        private void buttonAgregarStock_Click(object sender, EventArgs e)
        {
            
            int totalUnidades = int.Parse(textBoxPacks.Text) * int.Parse(textBoxUnidad.Text);
            
            DialogResult confirmacion;

            confirmacion = MessageBox.Show("Agregar " + totalUnidades.ToString() + " unidades de " + beStock.Nombre + " " + beStock.Medida + beStock.TipoMedida, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question); ;
            if (confirmacion == DialogResult.Yes)
            { bllStock.AgregarStock(beStock, totalUnidades); }
            
            DialogResult = DialogResult.OK;
            Close();
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
    }

   
}
