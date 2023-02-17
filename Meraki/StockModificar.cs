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

        private void buttonModificar_Click(object sender, EventArgs e)
        {
            try
            {
                beStock.Codigo = textBoxCodigo.Text;
                if(String.IsNullOrEmpty(textBoxNombre.Text))
                { MessageBox.Show("Debe colocar un nombre"); }
                else {
                    beStock.Nombre = textBoxNombre.Text.ToUpper();
                    if(String.IsNullOrEmpty(textBoxMedida.Text))
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

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; Close();
        }
    }
}
