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
using Servicios;

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

            // Solo lógica de datos
            List<BEStock> listStock = bllStock.CargarStock().OrderBy(stock => stock.Nombre).ToList();
            var bindingList = new BindingList<BEStock>(listStock);
            dataGridViewStock.DataSource = bindingList;

            // Le pasamos el mando al configurador
            ConfigurarDataGrid(dataGridViewStock);
        }

        public void ConfigurarDataGrid(DataGridView grilla)
        {
            // 1. Vestimos la grilla con el traje global de Meraki (¡Adiós a las 15 líneas repetidas!)
            grilla.AplicarEstiloMeraki();

            // 2. Visibilidad: Ocultamos lo que el usuario no necesita ver
            grilla.Columns[2].Visible = false;
            grilla.Columns[3].Visible = false;
            grilla.Columns[5].Visible = false;
            grilla.Columns[6].Visible = false;
            grilla.Columns[7].Visible = false;
            grilla.Columns["CantidadReservada"].Visible = true; // Nos aseguramos de que esta se vea

            // 3. Configuración específica: CÓDIGO
            grilla.Columns["Codigo"].HeaderText = "Cod.";
            grilla.Columns["Codigo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            grilla.Columns["Codigo"].Width = 60;

            // 4. Configuración específica: NOMBRE (Columna 1)
            grilla.Columns[1].HeaderText = "Nombre";
            // El AutoSizeMode queda en Fill por defecto gracias al estilo global

            // 5. Configuración específica: CANTIDAD ACTUAL (Columna 4)
            grilla.Columns[4].HeaderText = "Cant. actual";
            grilla.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            grilla.Columns[4].Width = 80;
            grilla.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // 6. Configuración específica: CANTIDAD RESERVADA
            grilla.Columns["CantidadReservada"].HeaderText = "Cant. reservada";
            grilla.Columns["CantidadReservada"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            grilla.Columns["CantidadReservada"].Width = 80;
            grilla.Columns["CantidadReservada"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["CantidadReservada"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // 7. Configuración específica: AVISO POCO STOCK (Columna 8)
            grilla.Columns[8].HeaderText = "Aviso poco stock";
            grilla.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            grilla.Columns[8].Width = 110;
            grilla.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
            // Validación: ¿Hay algo seleccionado?
            if (dataGridViewStock.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un artículo del stock primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            beStock = (BEStock)dataGridViewStock.CurrentRow.DataBoundItem;
            string codigoStockActual = beStock.Codigo;

            StockAgregar stockAgregar = new StockAgregar();
            stockAgregar.labelProducto.Text = beStock.Nombre + " " + beStock.Medida.ToString() + " " + beStock.TipoMedida;

            stockAgregar.AsignarProducto(beStock);

            stockAgregar.ShowDialog();
            CargarDataGrid();

            if (!string.IsNullOrEmpty(codigoStockActual))
            {
                dataGridViewStock.ClearSelection();

                foreach (DataGridViewRow row in dataGridViewStock.Rows)
                {
                    if (row.DataBoundItem is BEStock stockEnGrilla && stockEnGrilla.Codigo == codigoStockActual)
                    {
                        dataGridViewStock.CurrentCell = row.Cells["Nombre"]; // Foco en la columna Nombre
                        row.Selected = true;

                        // Hacemos que la grilla scrollee hasta el producto
                        if (row.Index >= 0)
                            dataGridViewStock.FirstDisplayedScrollingRowIndex = row.Index;

                        break;
                    }
                }
            }
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
            // Validación: ¿Hay algo seleccionado?
            if (dataGridViewStock.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un artículo del stock para modificar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            beStock = (BEStock)dataGridViewStock.CurrentRow.DataBoundItem;
            string codigoStockActual = beStock.Codigo;
            StockModificar stockModificar = new StockModificar();
            stockModificar.textBoxCodigo.Text = beStock.Codigo.ToString();
            stockModificar.textBoxNombre.Text = beStock.Nombre;
            stockModificar.textBoxMedida.Text = beStock.Medida.ToString();
            stockModificar.comboBoxTipoMedida.Text = beStock.TipoMedida;

            stockModificar.AsignarProducto(beStock);
            stockModificar.ShowDialog();
            CargarDataGrid();

            if (!string.IsNullOrEmpty(codigoStockActual))
            {
                dataGridViewStock.ClearSelection();

                foreach (DataGridViewRow row in dataGridViewStock.Rows)
                {
                    if (row.DataBoundItem is BEStock stockEnGrilla && stockEnGrilla.Codigo == codigoStockActual)
                    {
                        dataGridViewStock.CurrentCell = row.Cells["Nombre"]; // Foco en la columna Nombre
                        row.Selected = true;

                        // Hacemos que la grilla scrollee hasta el producto
                        if (row.Index >= 0)
                            dataGridViewStock.FirstDisplayedScrollingRowIndex = row.Index;

                        break;
                    }
                }
            }

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
            // Validación: ¿Hay algo seleccionado?
            if (dataGridViewStock.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un artículo del stock primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            beStock = (BEStock)dataGridViewStock.CurrentRow.DataBoundItem;
            string codigoStockActual = beStock.Codigo;


            AvisoPocoStock avisoPocoStock = new AvisoPocoStock(beStock);
            avisoPocoStock.AsignarProducto(beStock);
            avisoPocoStock.ShowDialog();
            CargarDataGrid();

            if (!string.IsNullOrEmpty(codigoStockActual))
            {
                dataGridViewStock.ClearSelection();

                foreach (DataGridViewRow row in dataGridViewStock.Rows)
                {
                    if (row.DataBoundItem is BEStock stockEnGrilla && stockEnGrilla.Codigo == codigoStockActual)
                    {
                        dataGridViewStock.CurrentCell = row.Cells["Nombre"]; // Foco en la columna Nombre
                        row.Selected = true;

                        // Hacemos que la grilla scrollee hasta el producto
                        if (row.Index >= 0)
                            dataGridViewStock.FirstDisplayedScrollingRowIndex = row.Index;

                        break;
                    }
                }
            }

        }

        

        private void Stock_VisibleChanged_1(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                CargarDataGrid();
            }
        }
    }
}
