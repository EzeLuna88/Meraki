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
    public partial class ProductosCrearCombo : Form
    {
        BLLStock bllStock;
        BLLProductoCombo bllProductoCombo;
        BEStock beStock;
        BEProductoCombo productoCombo;
        private bool puntoMayorista = false;
        private bool puntoMinorista = false;

        public ProductosCrearCombo()
        {
            beStock = new BEStock();
            productoCombo = new BEProductoCombo();
            bllStock = new BLLStock();
            bllProductoCombo = new BLLProductoCombo();
            InitializeComponent();
            CargarDataGrid();
        }

        public void CargarDataGrid()
        {
            dataGridViewStock.DataSource = null;
            dataGridViewStock.DataSource = bllStock.CargarStock();
            dataGridViewStock.Columns[0].Visible = false;
            dataGridViewStock.Columns[1].HeaderText = "Nombre";
            dataGridViewStock.Columns[2].Visible = false;
            dataGridViewStock.Columns[3].Visible = false;
            dataGridViewStock.Columns[4].HeaderText = "Cantidad actual";
            dataGridViewStock.Columns[6].Visible = false;
            dataGridViewStock.Columns[5].Visible = false;


        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                beStock = (BEStock)dataGridViewStock.CurrentRow.DataBoundItem;
                productoCombo.ListaProductos.Add(beStock);
                CargarDataGridCombo();

            }
            catch (Exception)
            {

                throw;
            }

        }

        private void CargarDataGridCombo()
        {
            dataGridViewCombo.DataSource = null;
            dataGridViewCombo.DataSource = productoCombo.ListaProductos;
            dataGridViewCombo.Columns["Codigo"].Visible = false;
            dataGridViewCombo.Columns["CantidadActual"].Visible = false;
            dataGridViewCombo.Columns["CantidadIngresada"].Visible = false;
            dataGridViewCombo.Columns["FechaIngreso"].Visible = false;
        }

        private void textBoxPrecioMayorista_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxPrecioMinorista_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxPrecioMinorista_Leave(object sender, EventArgs e)
        {
            decimal precio;
            if (decimal.TryParse(textBoxPrecioMinorista.Text, out precio))
            {
                productoCombo.PrecioMinorista = Math.Round(precio, 2);
                textBoxPrecioMinorista.Text = productoCombo.PrecioMinorista.ToString("0.00");
            }
        }

        private void textBoxPrecioMayorista_Leave(object sender, EventArgs e)
        {
            decimal precio;
            if (decimal.TryParse(textBoxPrecioMayorista.Text, out precio))
            {
                productoCombo.PrecioMayorista = Math.Round(precio, 2);
                textBoxPrecioMayorista.Text = productoCombo.PrecioMayorista.ToString("0.00");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; Close();

        }

        private void buttonAlta_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(textBoxNombreCombo.Text))
                {
                    MessageBox.Show("Debe colocarle un nombre al combo");
                }
                else
                {
                    productoCombo.Nombre = textBoxNombreCombo.Text.ToUpper();
                    if (String.IsNullOrEmpty(textBoxPrecioMayorista.Text))
                    { MessageBox.Show("Debe colocar un precio mayorista"); }
                    else
                    {
                        productoCombo.PrecioMayorista = Convert.ToDecimal(textBoxPrecioMayorista.Text);
                        if (String.IsNullOrEmpty(textBoxPrecioMinorista.Text))
                        { MessageBox.Show("Debe colocar un precio minorista"); }
                        else
                        {
                            productoCombo.PrecioMinorista = Convert.ToDecimal(textBoxPrecioMinorista.Text);
                            productoCombo.Unidad = 1;
                            productoCombo.Codigo = Guid.NewGuid().ToString();
                            productoCombo.Tipo = "combo";
                            bllProductoCombo.GuardarProducto(productoCombo);
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

        private void buttonQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewCombo.Rows.Count > 0)
                {
                    beStock = (BEStock)dataGridViewCombo.CurrentRow.DataBoundItem;
                    productoCombo.ListaProductos.Remove(beStock);
                    CargarDataGridCombo();
                    beStock = null;
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
