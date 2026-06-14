using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Servicios
{
    public static class EstilosGlobales
    {
        // El "this" antes del DataGridView es lo que hace la magia de la extensión
        public static void AplicarEstiloMeraki(this DataGridView grilla)
        {
            // 1. Comportamiento general
            grilla.RowHeadersVisible = false;
            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToResizeRows = false;
            grilla.AllowUserToResizeColumns = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // 2. Colores y fondos
            grilla.BackgroundColor = ColoresMeraki.GrisFondo;
            grilla.DefaultCellStyle.BackColor = ColoresMeraki.RosaPalido;
            grilla.DefaultCellStyle.SelectionBackColor = ColoresMeraki.BordoPrincipal;
            grilla.DefaultCellStyle.SelectionForeColor = ColoresMeraki.BlancoTexto;

            // 3. Fuentes y bordes de las celdas
            grilla.Font = new Font("Segoe UI", 9);
            grilla.RowTemplate.Height = 25;
            grilla.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // 4. Estilos del Encabezado (Títulos de las columnas)
            grilla.EnableHeadersVisualStyles = false;
            grilla.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grilla.ColumnHeadersDefaultCellStyle.BackColor = ColoresMeraki.BordoOscuro;
            grilla.ColumnHeadersDefaultCellStyle.ForeColor = ColoresMeraki.BlancoTexto;

            // Para que el encabezado no cambie de color si alguien le hace clic
            grilla.ColumnHeadersDefaultCellStyle.SelectionBackColor = grilla.ColumnHeadersDefaultCellStyle.BackColor;
            grilla.ColumnHeadersDefaultCellStyle.SelectionForeColor = grilla.ColumnHeadersDefaultCellStyle.ForeColor;

            // 4. Estilos del Encabezado (Títulos de las columnas)
            grilla.EnableHeadersVisualStyles = false;

            // 🛠️ ¡ESTA ES LA LÍNEA QUE SACA EL REBORDE!
            grilla.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

        
        }
    }
}
