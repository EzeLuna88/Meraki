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
using System.Text.RegularExpressions;
using System.Drawing.Text;

namespace Meraki
{
    public partial class ProductosAlta : Form
    {
        BEProductoIndividual beProducto;
        BLLProducto bllProducto;
        BLLStock bllStock;
        BEStock beStock;
        private bool puntoMayorista = false;
        private bool puntoMinorista = false;
        
        public ProductosAlta()
        {
            beStock = new BEStock();
            bllStock = new BLLStock();
            beProducto= new BEProductoIndividual();
            bllProducto= new BLLProducto();
            InitializeComponent();
            CargarDataGrid();
        }

        private void buttonAlta_Click(object sender, EventArgs e)
        {
            beStock = (BEStock)dataGridViewStock.CurrentRow.DataBoundItem;
            beProducto.Stock = beStock;
            beProducto.Unidad = Convert.ToInt32(textBoxUnidades.Text);
            beProducto.PrecioMayorista = Convert.ToDecimal(textBoxPrecioMayorista.Text);
            beProducto.PrecioMinorista = Convert.ToDecimal(textBoxPrecioMinorista.Text);
            beProducto.Tipo = "individual";
            bllProducto.GuardarProducto(beProducto);
            DialogResult = DialogResult.OK;
            Close();

        }

        

        private void textBoxPrecioMayorista_Leave(object sender, EventArgs e)
        {
            decimal precio;
            if (decimal.TryParse(textBoxPrecioMayorista.Text, out precio))
            {
                beProducto.PrecioMayorista = Math.Round(precio, 2);
                textBoxPrecioMayorista.Text = beProducto.PrecioMayorista.ToString("0.00");
            }
        }

        private void textBoxPrecioMinorista_Leave(object sender, EventArgs e)
        {
            decimal precio;
            if (decimal.TryParse(textBoxPrecioMinorista.Text, out precio))
            {
                beProducto.PrecioMinorista = Math.Round(precio, 2);
                textBoxPrecioMinorista.Text = beProducto.PrecioMinorista.ToString("0.00");
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; Close();

        }

        private void ProductosAlta_Load(object sender, EventArgs e)
        {
            if (dataGridViewStock.Rows.Count > 0)
            {
                dataGridViewStock.Rows[0].Selected = true;
            }
        }

        private void textBoxFiltrar_TextChanged(object sender, EventArgs e)
        {
            string textoABuscar = textBoxFiltrar.Text.ToLower();
            var tablaFiltrada = bllStock.CargarStock().Where(row => row.Nombre.ToLower().Contains(textoABuscar));
            dataGridViewStock.DataSource = tablaFiltrada.ToList();
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

        private void dataGridViewStock_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex != this.dataGridViewStock.NewRowIndex)
            {
                e.Value = dataGridViewStock.Rows[e.RowIndex].Cells["nombre"].Value.ToString() + " "
                    + dataGridViewStock.Rows[e.RowIndex].Cells["medida"].Value.ToString() + " "
                    + dataGridViewStock.Rows[e.RowIndex].Cells["tipoMedida"].Value.ToString();
                e.FormattingApplied = true;
            }
        }

        private void textBoxUnidades_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
