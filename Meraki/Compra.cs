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
    public partial class Compra : Form
    {
        public Compra()
        {
            InitializeComponent();
        }

        private void buttonModificar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; Close();
        }

        private void buttonCompraMayorista_Click(object sender, EventArgs e)
        {
            try
            {
                CompraMayorista compraMayorista= new CompraMayorista();
                compraMayorista.ShowDialog();

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void buttonCompraMinorista_Click(object sender, EventArgs e)
        {
            try
            {
                CompraMinorista compraMinorista = new CompraMinorista();
                compraMinorista.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
