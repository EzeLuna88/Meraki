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
    public partial class FechasDeVencimiento : Form
    {
        BLLStock bllStock;
        BLLConfiguracion bllConfiguracion;
        public FechasDeVencimiento()
        {
            bllStock = new BLLStock();
            bllConfiguracion = new BLLConfiguracion();
            InitializeComponent();
            CargarDataGrid();
            textBoxDiasDeAviso.Text = bllConfiguracion.ObtenerDiasAvisoVencimiento().ToString();
        }

        public void CargarDataGrid()
        {
            dataGridViewFechasDeVencimiento.DataSource = null;

            DataTable dt = bllStock.ObtenerStockConVencimiento();
            dataGridViewFechasDeVencimiento.DataSource = dt;

            ConfigurarDataGrid(dataGridViewFechasDeVencimiento);
            
        }

        public void ConfigurarDataGrid(DataGridView dataGridView)
        {
            dataGridView.Columns.Insert(0, new DataGridViewTextBoxColumn()
            {
                HeaderText = "Producto",
                Name = "ColumnaProducto",
                ReadOnly = true
            });

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

            dataGridView.Columns["NombreProducto"].Visible = false;
            dataGridView.Columns["Medida"].Visible = false;
            dataGridView.Columns["TipoMedida"].Visible = false;
            dataGridView.Columns["StockID"].Visible = false;
            dataGridView.Columns["StockVencimientoID"].Visible = false;
            dataGridView.Columns["NombreProducto"].HeaderText = "Producto";
            dataGridView.Columns["TipoMedida"].HeaderText = "Tipo de Medida";
            dataGridView.Columns["Medida"].HeaderText = "Medida";
            dataGridView.Columns["CantidadDisponible"].HeaderText = "Cantidad";
            dataGridView.Columns["FechaVencimiento"].HeaderText = "Vence el";

            
        }

        private void dataGridViewFechasDeVencimiento_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                var row = dataGridViewFechasDeVencimiento.Rows[e.RowIndex];
                var nombre = row.Cells["NombreProducto"].Value?.ToString() ?? "";
                var medida = row.Cells["Medida"].Value?.ToString() ?? "";
                var tipoMedida = row.Cells["TipoMedida"].Value?.ToString() ?? "";

                e.Value = $"{nombre} {medida} {tipoMedida}";
                e.FormattingApplied = true;
            }
        }

        private void iconButtonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int dias = Convert.ToInt32(textBoxDiasDeAviso.Text);
                bllConfiguracion.GuardarDiasAvisoVencimiento(dias);

                MessageBox.Show("Los días de aviso de vencimiento fueron guardados correctamente.", "Guardado exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingresá un número válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al guardar la configuración:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }


    }
