using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Servicios;
using System.IO;

namespace Meraki
{
    public partial class UC_ConfigListaPrecios : UserControl
    {
        BLLProducto bllProducto;
        BLLCategoriaPDF bllCategoriaPDF;
        private List<BEProducto> _productosSinAsignarCompleta;
        private List<BEProducto> _productosAsignadosCompleta;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        public UC_ConfigListaPrecios()
        {
            bllProducto = new BLLProducto();
            bllCategoriaPDF = new BLLCategoriaPDF();
            _productosSinAsignarCompleta = new List<BEProducto>();
            _productosAsignadosCompleta = new List<BEProducto>();
            InitializeComponent();
        }




        #region 🔄 Carga y Refresco de Datos

        private void CargarTipos()
        {
            try
            {
                dataGridViewTipos.DataSource = null;
                dataGridViewTipos.DataSource = bllCategoriaPDF.ListarCategorias();

                if (dataGridViewTipos.Columns["Id"] != null)
                    dataGridViewTipos.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefrescarProductosSinAsignar()
        {
            // Buscamos una sola vez en la DB y guardamos la foto completa
            _productosSinAsignarCompleta = bllProducto.ListarProductosSinCategoriaPDF();

            // Ejecutamos el filtrado (que si el TextBox está vacío, los muestra a todos)
            FiltrarSinAsignar();
        }

        private void RefrescarProductosAsignados()
        {
            if (dataGridViewTipos.CurrentRow != null && dataGridViewTipos.CurrentRow.DataBoundItem is BECategoriaPDF tipoSeleccionado)
            {
                // Buscamos una sola vez en la DB para este tipo
                _productosAsignadosCompleta = bllProducto.ListarProductosPorCategoria(tipoSeleccionado.Id);
            }
            else
            {
                _productosAsignadosCompleta = new List<BEProducto>();
            }

            // Ejecutamos el filtrado
            FiltrarAsignados();
        }

        #endregion

        

        

        // 🔄 EVENTO CLAVE: Une el Nombre + Medida del Stock de forma dinámica
        private void dataGridViewProductos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            // 1. Validamos que estemos pintando la columna "nombre" (por string, así sirve para las dos grillas)
            if (e.RowIndex >= 0 && dgv.Columns[e.ColumnIndex].Name == "nombre")
            {
                var producto = dgv.Rows[e.RowIndex].DataBoundItem;

                if (producto != null)
                {
                    // 2. Si es un producto común, dejamos que tu ToString() haga su magia
                    if (producto is BEProductoIndividual c1)
                    {
                        e.Value = c1.ToString();
                    }
                    // 3. Si es un combo, mostramos el nombre del combo directo
                    else if (producto is BEProductoCombo c2)
                    {
                        e.Value = c2.Nombre;
                    }
                }
            }
        }


        

        #region 🎨 Ajuste y Armado de Columnas del Catálogo

        private void ConfigurarGrillaProductosCat(DataGridView dataGridView)
        {
            if (dataGridView.Columns.Count == 0) return;

            dataGridView.AplicarEstiloMeraki();

            // ⚡ Más aire visual: hacemos que el fondo vacío coincida con el formulario
            dataGridView.BackgroundColor = this.BackColor;
            dataGridView.RowTemplate.Height = 24; // 📏 Le damos un poquito más de alto a las filas

            dataGridView.CellFormatting -= dataGridViewProductos_CellFormatting;
            dataGridView.CellFormatting += dataGridViewProductos_CellFormatting;

            if (dataGridView.Columns["nombre"] == null)
            {
                dataGridView.Columns.Add("nombre", "PRODUCTO");
            }

            // Ocultar lo que no va
            if (dataGridView.Columns["codigo"] != null) dataGridView.Columns["codigo"].Visible = false;
            if (dataGridView.Columns["precioMinorista"] != null) dataGridView.Columns["precioMinorista"].Visible = false;
            if (dataGridView.Columns["Tipo"] != null) dataGridView.Columns["Tipo"].Visible = false;
            if (dataGridView.Columns["NombreMostrar"] != null) dataGridView.Columns["NombreMostrar"].Visible = false;
            if (dataGridView.Columns["Id"] != null) dataGridView.Columns["Id"].Visible = false;

            // Ordenar
            if (dataGridView.Columns["nombre"] != null) dataGridView.Columns["nombre"].DisplayIndex = 0;
            if (dataGridView.Columns["Unidad"] != null) dataGridView.Columns["Unidad"].DisplayIndex = 1;
            if (dataGridView.Columns["precioMayorista"] != null) dataGridView.Columns["precioMayorista"].DisplayIndex = 2;

            // 📐 Ajuste milimétrico de anchos para matar el Scrollbar Horizontal
            if (dataGridView.Columns["nombre"] != null)
            {
                dataGridView.Columns["nombre"].HeaderText = "PRODUCTO";
                dataGridView.Columns["nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dataGridView.Columns["Unidad"] != null)
            {
                dataGridView.Columns["Unidad"].HeaderText = "UN.";
                dataGridView.Columns["Unidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridView.Columns["Unidad"].Width = 45; // 🛠️ Lo bajamos a 45 (total entra "10" o "12" cómodo)
                dataGridView.Columns["Unidad"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView.Columns["Unidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dataGridView.Columns["precioMayorista"] != null)
            {
                dataGridView.Columns["precioMayorista"].HeaderText = "PRECIO";
                dataGridView.Columns["precioMayorista"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridView.Columns["precioMayorista"].Width = 85; // 🛠️ Lo dejamos clavado en 85
                dataGridView.Columns["precioMayorista"].DefaultCellStyle.Format = "c2";
                dataGridView.Columns["precioMayorista"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView.Columns["precioMayorista"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            }
        }

        #endregion

        private void FiltrarSinAsignar()
        {
            // Cambiá txtFiltrarSinAsignar por el Name real de tu TextBox izquierdo
            string textoABuscar = textBoxFiltrarSinAsignar.Text.ToLower().Trim();

            IEnumerable<BEProducto> query = _productosSinAsignarCompleta;

            if (!string.IsNullOrWhiteSpace(textoABuscar) && textoABuscar != "buscar...")
            {
                query = query.Where(row =>
                    row.Codigo.ToLower().Contains(textoABuscar) ||
                    (row is BEProductoIndividual ind && ind.Stock.Nombre.ToLower().Contains(textoABuscar)) ||
                    (row is BEProductoCombo combo && combo.Nombre.ToLower().Contains(textoABuscar))
                );
            }

            // Ordenamos alfabéticamente usando tu misma lógica
            var listaOrdenada = query.OrderBy(p =>
            {
                if (p is BEProductoIndividual ind) return ind.Stock.Nombre;
                if (p is BEProductoCombo com) return com.Nombre;
                return "";
            }).ToList();

            dataGridViewProductosSinAsignar.DataSource = null;
            dataGridViewProductosSinAsignar.DataSource = listaOrdenada;
            ConfigurarGrillaProductosCat(dataGridViewProductosSinAsignar);
        }

        private void FiltrarAsignados()
        {
            // Cambiá txtFiltrarAsignados por el Name real de tu TextBox derecho/filtro de abajo
            string textoABuscar = textBoxFiltrarAsignados.Text.ToLower().Trim();

            IEnumerable<BEProducto> query = _productosAsignadosCompleta;

            if (!string.IsNullOrWhiteSpace(textoABuscar) && textoABuscar != "buscar...")
            {
                query = query.Where(row =>
                    row.Codigo.ToLower().Contains(textoABuscar) ||
                    (row is BEProductoIndividual ind && ind.Stock.Nombre.ToLower().Contains(textoABuscar)) ||
                    (row is BEProductoCombo combo && combo.Nombre.ToLower().Contains(textoABuscar))
                );
            }

            // Ordenamos alfabéticamente
            var listaOrdenada = query.OrderBy(p =>
            {
                if (p is BEProductoIndividual ind) return ind.Stock.Nombre;
                if (p is BEProductoCombo com) return com.Nombre;
                return "";
            }).ToList();

            dataGridViewProductosAsignados.DataSource = null;
            dataGridViewProductosAsignados.DataSource = listaOrdenada;
            ConfigurarGrillaProductosCat(dataGridViewProductosAsignados);
        }

        private void dataGridViewTipos_SelectionChanged_1(object sender, EventArgs e)
        {
            RefrescarProductosAsignados();
        }

        private void dataGridViewProductosAsignados_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var prod = (BEProducto)dataGridViewProductosAsignados.Rows[e.RowIndex].DataBoundItem;

            bllProducto.ActualizarCategoriaPDF(prod.Codigo, null);

            RefrescarProductosSinAsignar();
            RefrescarProductosAsignados();
        }

        private void dataGridViewProductosSinAsignar_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dataGridViewTipos.CurrentRow != null && dataGridViewTipos.CurrentRow.DataBoundItem is BECategoriaPDF tipoSeleccionado)
            {
                var prod = (BEProducto)dataGridViewProductosSinAsignar.Rows[e.RowIndex].DataBoundItem;

                bllProducto.ActualizarCategoriaPDF(prod.Codigo, tipoSeleccionado.Id);

                RefrescarProductosSinAsignar();
                RefrescarProductosAsignados();
            }
            else
            {
                MessageBox.Show("Por favor, seleccioná un Tipo en el panel central para asignarle este producto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void iconButtonTipoAgregar_Click_1(object sender, EventArgs e)
        {
            string nuevoNombre = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nombre del nuevo Tipo de Bebida:", "Nuevo Tipo", "");

            if (!string.IsNullOrWhiteSpace(nuevoNombre))
            {
                bllCategoriaPDF.Insertar(nuevoNombre);
                CargarTipos();
            }
        }

        private void iconButtonTipoQuitar_Click_1(object sender, EventArgs e)
        {
            if (dataGridViewTipos.CurrentRow != null && dataGridViewTipos.CurrentRow.DataBoundItem is BECategoriaPDF tipoSeleccionado)
            {
                var seguro = MessageBox.Show($"¿Seguro querés borrar '{tipoSeleccionado.Nombre}'?\nLos productos de adentro volverán a quedar sin asignar.", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (seguro == DialogResult.Yes)
                {
                    bllCategoriaPDF.Eliminar(tipoSeleccionado.Id);
                    CargarTipos();
                    RefrescarProductosSinAsignar();
                }
            }
        }

        private void textBoxFiltrarSinAsignar_TextChanged_1(object sender, EventArgs e)
        {
            FiltrarSinAsignar();
        }

        private void textBoxFiltrarAsignados_TextChanged_1(object sender, EventArgs e)
        {
            FiltrarAsignados();
        }

        private void iconButtonQuitarAsignado_Click_1(object sender, EventArgs e)
        {
            // 1. Validamos que haya un producto seleccionado en la grilla de la derecha
            if (dataGridViewProductosAsignados.CurrentRow != null &&
                dataGridViewProductosAsignados.CurrentRow.DataBoundItem is BEProducto prodSeleccionado)
            {
                // 2. Lo sacamos de la asignación mandando un null a la DB
                bllProducto.ActualizarCategoriaPDF(prodSeleccionado.Codigo, null);

                // Refrescamos
                RefrescarProductosSinAsignar();
                RefrescarProductosAsignados();
            }
        }

        private void UC_ConfigListaPrecios_Load(object sender, EventArgs e)
        {
            // 🎨 Vestimos la de tipos y le apagamos el encabezado de columna redundante
            dataGridViewTipos.AplicarEstiloMeraki();
            dataGridViewTipos.ColumnHeadersVisible = false; // ❌ Vuela el texto "Nombre"
            dataGridViewTipos.BackgroundColor = this.BackColor; // ⚡ El fondo se vuelve cremita invisible

            if (dataGridViewTipos.Columns["Id"] != null)
                dataGridViewTipos.Columns["Id"].Visible = false;
            dataGridViewTipos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CargarTipos();
            RefrescarProductosSinAsignar();
            SendMessage(textBoxFiltrarAsignados.Handle, EM_SETCUEBANNER, 0, "Filtrar...");
            SendMessage(textBoxFiltrarSinAsignar.Handle, EM_SETCUEBANNER, 0, "Filtrar...");
        }

        private void iconButtonImprimir_Click(object sender, EventArgs e)
        {
            // Parámetro 1: ¿Fondo Blanco? -> TRUE
            // Parámetro 2: ¿Subir a Drive? -> FALSE

            ProcesarCatalogo(fondoBlanco: true, subirADrive: false);
        }

        private void iconButtonGenerarPDF_Click(object sender, EventArgs e)
        {
            // Parámetro 1: ¿Fondo Blanco? -> TRUE
            // Parámetro 2: ¿Subir a Drive? -> TRUE
            ProcesarCatalogo(fondoBlanco: true, subirADrive: true);
        }

        private async void ProcesarCatalogo(bool fondoBlanco, bool subirADrive)
        {
            try
            {
                // 1. Instanciamos las BLL
                BLL.BLLCategoriaPDF bllCat = new BLL.BLLCategoriaPDF();
                BLL.BLLProducto bllProd = new BLL.BLLProducto();

                // 2. Fabricamos la estructura agrupada
                Dictionary<string, List<BEProducto>> datosAgrupados = new Dictionary<string, List<BEProducto>>();
                List<BECategoriaPDF> categorias = bllCat.ListarCategorias();

                foreach (var cat in categorias)
                {
                    List<BEProducto> productosDeLaCat = bllProd.ListarProductosPorCategoria(cat.Id);
                    if (productosDeLaCat.Count > 0)
                    {
                        datosAgrupados.Add(cat.Nombre, productosDeLaCat);
                    }
                }

                // 3. LA MAGIA DE RUTAS AUTOMÁTICAS
                // Dependiendo de qué botón tocaron, le ponemos un nombre distinto al archivo para que no se pisen
                string sufijo = subirADrive ? "OFICIAL-DRIVE" : "COPIA-LOCAL";
                string rutaBase = Properties.Settings.Default.CarpetaDestinoPDF;

                // ¡Usamos el servicio que creamos hoy! Lo guarda en: \2026\06 - Junio\11\Catalogos\CATA-DIGITAL.pdf
                string rutaArchivo = Servicios.GestorRutas.GenerarRutaDestino(
                    rutaBase,
                    DateTime.Now,
                    "Catalogos",
                    sufijo
                );

                string nombreArchivoLogo = fondoBlanco ? "LOGO invert.png" : "LOGO.png";
                string rutaLogo = Path.Combine(Application.StartupPath, "Resources", nombreArchivoLogo);

                // 4. Invocamos al generador pasándole la bandera de color
                Servicios.GeneradorListaDePrecios.Generar(datosAgrupados, rutaArchivo, rutaLogo, fondoBlanco);

                if (subirADrive) // ¡Ahora la subida depende exclusivamente de esta bandera!
                {
                    string idDriveConfigurado = Properties.Settings.Default.GoogleDriveFileId;

                    if (!string.IsNullOrEmpty(idDriveConfigurado))
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        // Subimos la versión que se acaba de generar a Drive
                        await Servicios.GoogleDriveService.ActualizarCatalogoEnDrive(idDriveConfigurado, rutaArchivo);

                        Cursor.Current = Cursors.Default;
                    }
                }


                // 5. Avisamos y abrimos
                string tipoAviso = subirADrive ? "actualizado en Google Drive ✔️" : "(Solo copia local)";
                MessageBox.Show($"¡Catálogo {tipoAviso} generado con éxito!", "Meraki", MessageBoxButtons.OK, MessageBoxIcon.Information);

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(rutaArchivo) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }





    

}
