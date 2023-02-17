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
            bllProducto= new BLLProducto();
            bllProductoCombo = new BLLProductoCombo();
            InitializeComponent();
           
        }

          

        private void buttonModificar_Click(object sender, EventArgs e)
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

        private void textBoxMedida_KeyPress_1(object sender, KeyPressEventArgs e)
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

        private void ProductosModificar_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; Close();
        }
    }
}
