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
        private const string placeholderText = "   Buscar...";

        public Stock()
        {
            beStock = new BEStock();
            bllStock = new BLLStock();
            InitializeComponent();
            CargarDataGrid();
        }



        public void CargarDataGrid()
        {
            dataGridViewStock.DataSource = null;

            List<BEStock> listStock = bllStock.CargarStock().OrderBy(stock => stock.Nombre).ToList();
            var bindingList = new BindingList<BEStock>(listStock);
            dataGridViewStock.DataSource = bindingList;


            dataGridViewStock.Columns[1].HeaderText = "Nombre";
            dataGridViewStock.Columns[2].Visible = false;
            dataGridViewStock.Columns[3].Visible = false;
            dataGridViewStock.Columns[4].HeaderText = "Cantidad actual";
            dataGridViewStock.Columns[6].Visible = false;
            dataGridViewStock.Columns[5].Visible = false;
            dataGridViewStock.Columns[7].Visible = false;
            dataGridViewStock.Columns[8].HeaderText = "Aviso poco stock";
            ConfigurarDataGrid(dataGridViewStock);

        }

        public void ConfigurarDataGrid(DataGridView dataGridView)
        {
            // Configuración general del DataGridView
            dataGridView.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(217, 171, 171);
            dataGridView.RowHeadersVisible = false;
            dataGridView.Font = new System.Drawing.Font("Segoe UI", 9);
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.RowTemplate.Height = 25;
            dataGridView.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(146, 26, 64);
            dataGridView.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = dataGridView.ColumnHeadersDefaultCellStyle.BackColor;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = dataGridView.ColumnHeadersDefaultCellStyle.ForeColor;

            dataGridView.Columns["Codigo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns["Codigo"].Width = 60;
            dataGridView.Columns["Codigo"].HeaderText = "Cod.";
            dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns[4].Width = 80;
            dataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns[8].Width = 110;
            dataGridView.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


            // Configuración específica de estilo para columnas
            //ConfigurarEstilosColumnas(dataGridView);
        }




        private void Stock_Load(object sender, EventArgs e)
        {
            if (dataGridViewStock.Rows.Count > 0)
            {
                dataGridViewStock.Rows[0].Selected = true;
            }

            textBoxFiltrar.Text = placeholderText;
            textBoxFiltrar.ForeColor = System.Drawing.Color.Gray;
        }





        private void dataGridViewStock_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex != this.dataGridViewStock.NewRowIndex)
            {
                e.Value = dataGridViewStock.Rows[e.RowIndex].Cells["nombre"].Value.ToString() + " "
                    + dataGridViewStock.Rows[e.RowIndex].Cells["medida"].Value.ToString() + " "
                    + dataGridViewStock.Rows[e.RowIndex].Cells["tipoMedida"].Value.ToString();
                e.FormattingApplied = true;
            }
        }

        private void textBoxFiltrar_TextChanged_1(object sender, EventArgs e)
        {
            string textoABuscar = textBoxFiltrar.Text.ToLower();

            if (string.IsNullOrWhiteSpace(textoABuscar) || textoABuscar == "   buscar...")
            {
                CargarDataGrid();
            }
            else
            {
                var tablaFiltrada = bllStock.CargarStock().Where(row => row.Nombre.ToLower().Contains(textoABuscar) ||
                                                                        row.Codigo.ToLower().Contains(textoABuscar)
                                                                        );
                dataGridViewStock.DataSource = tablaFiltrada.ToList();
            }

        }

        private void iconButtonAgregarStock_Click(object sender, EventArgs e)
        {
            beStock = (BEStock)dataGridViewStock.CurrentRow.DataBoundItem;
            StockAgregar stockAgregar = new StockAgregar();
            stockAgregar.labelProducto.Text = beStock.Nombre + " " + beStock.Medida.ToString() + " " + beStock.TipoMedida;

            stockAgregar.AsignarProducto(beStock);

            stockAgregar.ShowDialog();
            CargarDataGrid();
            dataGridViewStock.Rows[0].Selected = true;
        }

        private void iconButtonNuevo_Click(object sender, EventArgs e)
        {
            StockNuevoProducto cargaStock = new StockNuevoProducto();
            cargaStock.ShowDialog();
            CargarDataGrid();
        }

        private void iconButtonBorrar_Click(object sender, EventArgs e)
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
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "No se puede borrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconButtonModificar_Click(object sender, EventArgs e)
        {
            beStock = (BEStock)dataGridViewStock.CurrentRow.DataBoundItem;
            StockModificar stockModificar = new StockModificar();
            stockModificar.textBoxCodigo.Text = beStock.Codigo.ToString();
            stockModificar.textBoxNombre.Text = beStock.Nombre;
            stockModificar.textBoxMedida.Text = beStock.Medida.ToString();
            stockModificar.comboBoxTipoMedida.Text = beStock.TipoMedida;

            stockModificar.AsignarProducto(beStock);
            stockModificar.ShowDialog();
            CargarDataGrid();
            dataGridViewStock.Rows[0].Selected = true;
            dataGridViewStock.Rows[0].Selected = true;
        }

        private void textBoxFiltrar_Enter(object sender, EventArgs e)
        {
            if (textBoxFiltrar.Text == placeholderText)
            {
                textBoxFiltrar.Text = ""; // Limpia el TextBox
                textBoxFiltrar.ForeColor = System.Drawing.Color.Black; // Cambia el color del texto
            }
        }

        private void textBoxFiltrar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxFiltrar.Text))
            {
                textBoxFiltrar.Text = placeholderText; // Restaura el texto del placeholder
                textBoxFiltrar.ForeColor = System.Drawing.Color.Gray; // Cambia el color del texto
            }
        }

        private void iconButtonFechasDeVencimiento_Click(object sender, EventArgs e)
        {
            FechasDeVencimiento fechasDeVencimiento = new FechasDeVencimiento();
            fechasDeVencimiento.ShowDialog();
            CargarDataGrid();
        }

        private void iconButtonAvisoPocoStock_Click(object sender, EventArgs e)
        {
            beStock = (BEStock)dataGridViewStock.CurrentRow.DataBoundItem;
            AvisoPocoStock avisoPocoStock = new AvisoPocoStock(beStock);
            avisoPocoStock.AsignarProducto(beStock);
            avisoPocoStock.ShowDialog();
            CargarDataGrid();
        }
    }
}
