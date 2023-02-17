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
    public partial class Stock : Form
    {
        BLLStock bllStock;
        BEStock beStock;
        public Stock()
        {
            beStock = new BEStock();
            bllStock= new BLLStock();
            InitializeComponent();
            CargarDataGrid();
        }

        private void buttonAgregarStock_Click(object sender, EventArgs e)
        {
            beStock = (BEStock)dataGridViewStock.CurrentRow.DataBoundItem;
            StockAgregar stockAgregar = new StockAgregar();
            stockAgregar.labelProducto.Text = beStock.Nombre + " " + beStock.Medida.ToString() + " " + beStock.TipoMedida;
            
            stockAgregar.AsignarProducto(beStock);

            stockAgregar.ShowDialog();
            CargarDataGrid();
            dataGridViewStock.Rows[0].Selected = true;
        }

        private void buttonNuevoProducto_Click(object sender, EventArgs e)
        {
            StockNuevoProducto cargaStock = new StockNuevoProducto();
            cargaStock.ShowDialog();
            CargarDataGrid();

        }

        public void CargarDataGrid()
        {
            dataGridViewStock.DataSource = null;
            dataGridViewStock.DataSource = bllStock.CargarStock();
            dataGridViewStock.Columns[0].Visible = false;
            dataGridViewStock.Columns[1].HeaderText = "Nombre";
            dataGridViewStock.Columns[2].Visible= false;
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

        private void textBoxFiltrar_TextChanged(object sender, EventArgs e)
        {
            string textoABuscar = textBoxFiltrar.Text.ToLower();
            var tablaFiltrada = bllStock.CargarStock().Where(row => row.Nombre.ToLower().Contains(textoABuscar));
            dataGridViewStock.DataSource = tablaFiltrada.ToList();
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            if (dataGridViewStock.Rows.Count > 0)
            {
                dataGridViewStock.Rows[0].Selected = true;
            }
        }

        private void buttonBorrarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewStock.SelectedRows.Count > 0)
                {
                    beStock = (BEStock)dataGridViewStock.CurrentRow.DataBoundItem;
                    if (beStock.CantidadActual > 0)
                    {
                        MessageBox.Show("No se puede borrar, todavia hay stock del producto");
                    }
                    else
                    {
                        DialogResult confirmacion;
                        confirmacion = MessageBox.Show("Confirmar baja de producto", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirmacion == DialogResult.Yes)
                        { bllStock.BorrarProductoDeStock(beStock); }
                        CargarDataGrid();
                        dataGridViewStock.Rows[0].Selected = true;

                    }
                }
                else
                { MessageBox.Show("Debe seleccionar un cliente"); }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void buttonModificar_Click(object sender, EventArgs e)
        {
            beStock = (BEStock)dataGridViewStock.CurrentRow.DataBoundItem;
            StockModificar stockModificar = new StockModificar();
            stockModificar.textBoxCodigo.Text = beStock.Codigo.ToString();
            stockModificar.textBoxNombre.Text = beStock.Nombre ;
            stockModificar.textBoxMedida.Text = beStock.Medida.ToString();
            stockModificar.comboBoxTipoMedida.Text = beStock.TipoMedida;
            
            stockModificar.AsignarProducto(beStock);
            stockModificar.ShowDialog();
            CargarDataGrid();
            dataGridViewStock.Rows[0].Selected = true;
            dataGridViewStock.Rows[0].Selected = true;
        }
    }
}
