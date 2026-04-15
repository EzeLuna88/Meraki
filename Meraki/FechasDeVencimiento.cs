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
using System.Runtime.InteropServices;

namespace Meraki
{
    public partial class FechasDeVencimiento : Form
    {
        BLLStock bllStock;
        BLLConfiguracion bllConfiguracion;
        public FechasDeVencimiento()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;

            bllStock = new BLLStock();
            bllConfiguracion = new BLLConfiguracion();
            InitializeComponent();
            CargarDataGrid();
            textBoxDiasDeAviso.Text = bllConfiguracion.ObtenerDiasAvisoVencimiento().ToString();
            textBoxDiasDeAviso.KeyPress += textBoxDiasDeAviso_KeyPress;

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

            dataGridView.Columns["ColumnaProducto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["ColumnaProducto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dataGridView.Columns["CantidadDisponible"].HeaderText = "Cantidad";
            dataGridView.Columns["CantidadDisponible"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns["CantidadDisponible"].Width = 90;

            dataGridView.Columns["CantidadDisponible"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["CantidadDisponible"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns["FechaVencimiento"].HeaderText = "Vence el";
            dataGridView.Columns["FechaVencimiento"].DefaultCellStyle.Format = "d";
            dataGridView.Columns["FechaVencimiento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns["FechaVencimiento"].Width = 110;

            dataGridView.Columns["FechaVencimiento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["FechaVencimiento"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;



        }

        private void dataGridViewFechasDeVencimiento_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                var row = dataGridViewFechasDeVencimiento.Rows[e.RowIndex];
                var nombre = row.Cells["NombreProducto"].Value?.ToString() ?? "";
                var medida = row.Cells["Medida"].Value?.ToString() ?? "";
                var tipoMedida = row.Cells["TipoMedida"].Value?.ToString() ?? "";

                // Si la medida es 0 o el tipo es un guion, solo mostramos el nombre
                if (medida == "0" || tipoMedida == "-")
                {
                    e.Value = nombre;
                }
                else
                {
                    // Si tiene medida válida, la mostramos completa (Ej: COCA COLA 1.5 lt.)
                    e.Value = $"{nombre} {medida} {tipoMedida}";
                }

                e.FormattingApplied = true;
            }
        }

        private void iconButtonGuardar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxDiasDeAviso.Text, out int dias))
            {
                MessageBox.Show("Por favor, ingresá un número válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bllConfiguracion.GuardarDiasAvisoVencimiento(dias);

                MessageBox.Show("Los días de aviso de vencimiento fueron guardados correctamente.", "Guardado exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Valor incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al guardar la configuración:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxDiasDeAviso_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitimos números o la tecla de borrar (Control)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void iconButtonCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void FechasDeVencimiento_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }


}
