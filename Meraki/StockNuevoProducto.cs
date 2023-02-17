using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        }

        private void buttonCargar_Click(object sender, EventArgs e)
        {
            try
            {
                beStock.Codigo = Guid.NewGuid().ToString();
                if(String.IsNullOrEmpty(textBoxNombre.Text))
                { MessageBox.Show("Debe colocar un nombre"); }
                else {
                    beStock.Nombre = textBoxNombre.Text.ToUpper();
                    if (String.IsNullOrEmpty(textBoxMedida.Text))
                    { beStock.Medida = 0;
                        beStock.TipoMedida = "-";  }
                    else
                    {
                        beStock.Medida = Convert.ToDouble(textBoxMedida.Text);
                        beStock.TipoMedida = comboBoxTipoMedida.SelectedItem.ToString();
                        if(String.IsNullOrEmpty(textBoxCantidad.Text))
                        {
                            MessageBox.Show("Debe ingresar la cantidad de stock que ingresó del producto");
                        }
                        else
                        {
                            beStock.CantidadActual = Convert.ToInt32(textBoxCantidad.Text);
                        }
                        
                    }
                    if (bllStock.ComprobarRepetido(beStock))
                    {
                        MessageBox.Show("El producto a cargar se encuentra en el stock");
                    }
                    else
                    {
                        bllStock.GuardarNuevoProducto(beStock);
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

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; Close();
        }

        private void StockNuevoProducto_Load(object sender, EventArgs e)
        {
            comboBoxTipoMedida.SelectedIndex = 0;
        }
    }
}
