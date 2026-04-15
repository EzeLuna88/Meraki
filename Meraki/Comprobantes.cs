using BE;
using BLL;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Servicios;

using System.Data.Common;
using QuestPDF.Fluent;

namespace Meraki
{
    public partial class Comprobantes : Form
    {
        BLLComprobante bllComprobante;
        BEComprobante beComprobante;
        List<BEComprobante> listaComprobantes;
        private const string placeholderText = "   Buscar...";

        public Comprobantes()
        {
            bllComprobante = new BLLComprobante();
            beComprobante = new BEComprobante();
            
            InitializeComponent();
            CargarDataGrid();

        }


        public void CargarDataGrid()
        {
            listaComprobantes = bllComprobante.listaComprobantes();
            dataGridViewComprobantes.DataSource = null;
            dataGridViewComprobantes.DataSource = listaComprobantes;
            ConfigurarDataGrid(dataGridViewComprobantes);


        }

        private void dataGridViewComprobantes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewComprobantes.Columns[e.ColumnIndex].Name == "Cliente")
            {
                var comprobante = dataGridViewComprobantes.Rows[e.RowIndex].DataBoundItem as BEComprobante;
                if (comprobante != null && comprobante.Cliente != null)
                {
                    e.Value = comprobante.Cliente.Nombre;
                }
            }
        }

        private void dataGridViewComprobantes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                beComprobante = (BEComprobante)dataGridViewComprobantes.Rows[e.RowIndex].DataBoundItem;
                GenerarYMostrarPDF(); // Llamamos al método único de QuestPDF
            }
        }


        private void textBoxFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            if (textBoxFiltroNombre.Text != "   buscar...") // Solo aplicar filtro si el texto no es el placeholder
            {
                AplicarFiltros();
            }
        }

        private void textBoxFiltroFechaDesde_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void textBoxFiltroFechaHasta_TextChanged(object sender, EventArgs e)
        {

            AplicarFiltros();
        }

        private void AplicarFiltros()
        {
            DateTime fechaDesde;
            DateTime fechaHasta;

            bool formatoDesdeCorrecto = DateTime.TryParseExact(
                textBoxFiltroFechaDesde.Text,
                "dd/MM/yyyy",
                null,
                System.Globalization.DateTimeStyles.None,
                out fechaDesde
            );



            bool formatoHastaCorrecto = DateTime.TryParseExact(
                textBoxFiltroFechaHasta.Text,
                "dd/MM/yyyy",
                null,
                System.Globalization.DateTimeStyles.None,
                out fechaHasta
            );

            string textoABuscar = textBoxFiltroNombre.Text.ToLower();
            var listaFiltrada = listaComprobantes.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(textoABuscar) && textoABuscar != "   buscar...")
            {
                listaFiltrada = listaFiltrada
             .Where(c => c.Cliente.Nombre.ToLower().Contains(textoABuscar) ||
                         c.Numero.ToLower().Contains(textoABuscar));
            }


            if (formatoDesdeCorrecto && formatoHastaCorrecto)
            {
                fechaHasta = fechaHasta.AddDays(1);
                listaFiltrada = listaFiltrada
                    .Where(c => c.Fecha >= fechaDesde && c.Fecha < fechaHasta);
            }
            else if (formatoDesdeCorrecto)
            {
                listaFiltrada = listaFiltrada
                            .Where(c => c.Fecha >= fechaDesde);
            }
            else if (formatoHastaCorrecto)
            {
                fechaHasta = fechaHasta.AddDays(1);
                listaFiltrada = listaFiltrada
                    .Where(c => c.Fecha < fechaHasta);
            }
            else
            {
                ActualizarDataGridView(listaFiltrada.ToList());
            }

            ActualizarDataGridView(listaFiltrada.ToList());

        }

        private void ActualizarDataGridView(List<BEComprobante> listaFiltrada)
        {
            dataGridViewComprobantes.DataSource = null;
            dataGridViewComprobantes.DataSource = listaFiltrada;
            ConfigurarDataGrid(dataGridViewComprobantes);
        }

        public void ConfigurarDataGrid(DataGridView dataGridView)
        {
            dataGridView.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView.CellFormatting += dataGridViewComprobantes_CellFormatting;
            dataGridView.Columns[4].Visible = false;
            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(217, 171, 171);
            dataGridView.RowHeadersVisible = false;
            dataGridView.Font = new System.Drawing.Font("Segoe UI", 9);
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns["Total"].DefaultCellStyle.Format = "c2";
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.RowTemplate.Height = 25;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(146, 26, 64);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns[0].Width = 100;
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns[1].Width = 100;
            dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns[3].Width = 100;
            dataGridView.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = dataGridViewComprobantes.ColumnHeadersDefaultCellStyle.BackColor;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = dataGridViewComprobantes.ColumnHeadersDefaultCellStyle.ForeColor;

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (dataGridViewComprobantes.CurrentRow != null && dataGridViewComprobantes.CurrentRow.Index >= 0)
            {
                beComprobante = (BEComprobante)dataGridViewComprobantes.CurrentRow.DataBoundItem;
                GenerarYMostrarPDF(); // Llamamos al método único
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila válida.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Comprobantes_Load(object sender, EventArgs e)
        {
            textBoxFiltroNombre.Text = placeholderText;
            textBoxFiltroNombre.ForeColor = System.Drawing.Color.Gray;
        }

        private void textBoxFiltroNombre_Enter(object sender, EventArgs e)
        {
            if (textBoxFiltroNombre.Text == placeholderText)
            {
                textBoxFiltroNombre.Text = ""; // Limpia el TextBox
                textBoxFiltroNombre.ForeColor = System.Drawing.Color.Black; // Cambia el color del texto
            }
        }

        private void textBoxFiltroNombre_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxFiltroNombre.Text))
            {
                textBoxFiltroNombre.Text = placeholderText; // Restaura el texto del placeholder
                textBoxFiltroNombre.ForeColor = System.Drawing.Color.Gray; // Cambia el color del texto
            }
        }

        private void GenerarYMostrarPDF()
        {
            // 1. Validación de seguridad
            if (beComprobante == null)
            {
                MessageBox.Show("Por favor, selecciona un comprobante válido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Configurar la licencia (Obligatorio para QuestPDF)
                QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

                // 3. Crear el documento usando la clase que armamos para CompraMayorista
                var documento = new ComprobanteDocument(beComprobante);

                // 4. Crear un archivo temporal para mostrarlo rápido sin preguntar dónde guardar
                string tempFilePath = Path.Combine(Path.GetTempPath(), "Comprobante_" + beComprobante.Numero + ".pdf");

                // 5. Generar el PDF físicamente
                documento.GeneratePdf(tempFilePath);

                // 6. Abrir el PDF automáticamente con el visor predeterminado de Windows
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = tempFilePath,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR DETALLADO:\n\n" + ex.ToString(), "Error de Diagnóstico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Comprobantes_VisibleChanged(object sender, EventArgs e)
        {
            CargarDataGrid();
        }
    }
}
