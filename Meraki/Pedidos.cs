using BE;
using BLL;
using QuestPDF.Fluent;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Meraki
{
    public partial class Pedidos : Form
    {
        BLLPedido bllPedido;
        BLLComprobante bllComprobante;
        public Pedidos()
        {
            bllPedido = new BLLPedido();
            InitializeComponent();
            bllComprobante = new BLLComprobante();
        }

        private void iconButtonPasarAReparto_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Verificamos que haya una fila seleccionada
                if (dataGridViewEnPreapracion.CurrentRow != null && dataGridViewEnPreapracion.CurrentRow.Index >= 0)
                {
                    // 2. Capturamos el número del pedido
                    string numeroPedido = dataGridViewEnPreapracion.CurrentRow.Cells["Pedido"].Value?.ToString();

                    // 3. LA TRAMPA: Evitamos las filas de Título (Fechas)
                    if (string.IsNullOrWhiteSpace(numeroPedido))
                    {
                        MessageBox.Show("Por favor, seleccioná un pedido válido (no la franja de fecha).", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 4. Pedimos confirmación para evitar clics accidentales
                    DialogResult res = MessageBox.Show($"¿Confirmás que el pedido {numeroPedido} ya está cargado en la camioneta y sale a reparto?", "Pasar a Reparto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (res == DialogResult.Yes)
                    {
                        // 5. Magia en la BD: Pasamos el pedido a la siguiente columna
                        bllPedido.CambiarEstadoPedido(numeroPedido, EstadoPedido.EnCamino);

                        // 6. Refrescamos AMBAS grillas
                        CargarGrillaEnPreparacion();
                        CargarGrillaEnCamino(); // (Este método lo armamos en el Paso 3)

                        MessageBox.Show("¡Pedido en camino!", "Meraki", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, seleccioná el pedido que querés despachar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al despachar el pedido: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Pedidos_Load(object sender, EventArgs e)
        {
            CargarGrillaEnPreparacion();
            CargarGrillaEnCamino();
        }

        private void CargarGrillaEnPreparacion()
        {
            try
            {
                // 1. Traemos la lista pura de la BD
                List<BEPedido> listaOriginal = bllPedido.ListarPedidosEnPreparacion();

                // 2. LA MAGIA MATEMÁTICA: Agrupamos por fecha usando LINQ
                // Esto crea "paquetes", donde la llave (Key) es la fecha, y adentro están sus pedidos.
                var pedidosAgrupados = listaOriginal
                                        .GroupBy(p => p.FechaEnvio.Date)
                                        .OrderBy(g => g.Key); // Los ordenamos de la fecha más vieja a la más nueva

                // 3. Pasamos la grilla a MODO MANUAL
                dataGridViewEnPreapracion.DataSource = null;
                dataGridViewEnPreapracion.Rows.Clear();

                // Como sacamos el DataSource, tenemos que avisarle cuáles son las columnas 
                // (Solo las creamos si no existen todavía)
                if (dataGridViewEnPreapracion.Columns.Count == 0)
                {
                    dataGridViewEnPreapracion.Columns.Add("Pedido", "Pedido");
                    dataGridViewEnPreapracion.Columns.Add("Cliente", "Cliente");
                    dataGridViewEnPreapracion.Columns.Add("Total", "Total");

                    // Volvemos a aplicar tu configuración de anchos
                    dataGridViewEnPreapracion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridViewEnPreapracion.Columns["Pedido"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                // 4. Llenamos la grilla dibujando los títulos y los datos
                foreach (var grupo in pedidosAgrupados)
                {
                    // -- A. CREAMOS LA FILA TÍTULO (El Separador) --
                    // Ponemos la fecha en la columna del medio para que resalte
                    int indexTitulo = dataGridViewEnPreapracion.Rows.Add("", $"📅 ENVÍOS DEL {grupo.Key:dd/MM/yyyy}", "", "");

                    // La pintamos de un color distinto (Podés usar el bordó de tu sistema)
                    DataGridViewRow filaTitulo = dataGridViewEnPreapracion.Rows[indexTitulo];
                    filaTitulo.DefaultCellStyle.BackColor = Color.FromArgb(199, 91, 122); // Color de fondo del título
                    filaTitulo.DefaultCellStyle.ForeColor = Color.Black;  // Letra blanca
                    filaTitulo.DefaultCellStyle.Font = new Font(dataGridViewEnPreapracion.Font, FontStyle.Bold);
                    filaTitulo.ReadOnly = true;

                    // -- B. CREAMOS LAS FILAS DE LOS PEDIDOS DE ESA FECHA --
                    foreach (BEPedido p in grupo)
                    {
                        dataGridViewEnPreapracion.Rows.Add(
                            p.Numero,
                            p.Cliente.Nombre,
                            p.Total.ToString("C2")
                        );
                    }
                }

                // Deseleccionamos la primera fila para que quede prolijo al abrir
                dataGridViewEnPreapracion.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ConfigurarDataGrid(dataGridViewEnPreapracion);
        }

        private void ConfigurarDataGrid(DataGridView dataGridView)
        {
            dataGridView.AplicarEstiloMeraki();
            
        }

      

        private void Pedidos_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                CargarGrillaEnPreparacion();
            }
        }

        private void dataGridViewEnPreapracion_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // 1. Evitamos clics accidentales en los encabezados de columna
                if (e.RowIndex >= 0)
                {
                    // 2. Capturamos el número de pedido de la fila a la que le hiciste doble clic
                    string numeroPedido = dataGridViewEnPreapracion.Rows[e.RowIndex].Cells["Pedido"].Value?.ToString();

                    // 3. LA TRAMPA: Si hiciste doble clic en la franja bordó de la fecha, 
                    if (!string.IsNullOrWhiteSpace(numeroPedido))
                    {
                        // 4. Instanciamos tu nuevo formulario pasándole el número por parámetro
                        PedidosEditar formEdicion = new PedidosEditar(numeroPedido);

                        // 5 y 6. Abrimos la ventana y nos quedamos "escuchando" cómo se cierra
                        if (formEdicion.ShowDialog() == DialogResult.OK)
                        {
                            // Solo entra acá y recarga la grilla SI el usuario tocó el botón "Guardar" 
                            // y nosotros le seteamos el DialogResult.OK en el otro form.
                            CargarGrillaEnPreparacion();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el pedido: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarGrillaEnCamino()
        {
            try
            {
                // Traemos de la BD solo los que están EnCamino
                List<BEPedido> listaEnCamino = bllPedido.ListarPedidosEnCamino();

                // Ojo de usar el nombre correcto de tu DataGridView de la derecha
                dataGridViewEnCamino.DataSource = null;
                dataGridViewEnCamino.Rows.Clear();

                if (dataGridViewEnCamino.Columns.Count == 0)
                {
                    dataGridViewEnCamino.Columns.Add("Pedido", "Pedido");
                    dataGridViewEnCamino.Columns.Add("Cliente", "Cliente");
                    dataGridViewEnCamino.Columns.Add("Total", "Total");

                    dataGridViewEnCamino.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }

                foreach (BEPedido p in listaEnCamino)
                {
                    dataGridViewEnCamino.Rows.Add(
                        p.Numero,
                        p.Cliente.Nombre,
                        p.Total.ToString("C2")
                    );
                }

                dataGridViewEnCamino.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ConfigurarDataGrid(dataGridViewEnCamino);
        }

        private void dataGridViewEnCamino_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // 1. Evitamos clics accidentales en los encabezados de columna
                if (e.RowIndex >= 0)
                {
                    // 2. Capturamos el número de pedido de la fila a la que le hiciste doble clic
                    string numeroPedido = dataGridViewEnCamino.Rows[e.RowIndex].Cells["Pedido"].Value?.ToString();

                    // 3. Validamos que sea un número de pedido válido
                    if (!string.IsNullOrWhiteSpace(numeroPedido))
                    {
                        // 4. Instanciamos la MISMA ventana de edición pasándole el número
                        PedidosEditar formEdicion = new PedidosEditar(numeroPedido);

                        // 5. Abrimos la ventana y nos quedamos escuchando si tocó "Guardar"
                        if (formEdicion.ShowDialog() == DialogResult.OK)
                        {
                            // 6. Al regresar, refrescamos ambas grillas por las dudas 
                            // (por si cambió el total o si le cambió la fecha de entrega y tiene que moverse)
                            CargarGrillaEnPreparacion();
                            CargarGrillaEnCamino();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el pedido en reparto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconButtonEntregado_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Verificamos que haya una fila seleccionada en la grilla "En Camino"
                if (dataGridViewEnCamino.CurrentRow != null && dataGridViewEnCamino.CurrentRow.Index >= 0)
                {
                    // 2. Capturamos el número del pedido de la celda correspondiente
                    string numeroPedido = dataGridViewEnCamino.CurrentRow.Cells["Pedido"].Value?.ToString();

                    if (string.IsNullOrWhiteSpace(numeroPedido))
                    {
                        MessageBox.Show("Por favor, seleccioná un pedido válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 3. Confirmación previa para evitar registrar entregas por error
                    DialogResult res = MessageBox.Show($"¿Confirmás que el pedido {numeroPedido} fue entregado con éxito?\nSe cambiará el estado y se guardará el comprobante.", "Confirmar Entrega", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (res == DialogResult.Yes)
                    {
                        // 4. Traemos el pedido completo (con sus productos e info del cliente) desde la base de datos
                        BEPedido pedidoFinalizado = bllPedido.ObtenerPedidoCompleto(numeroPedido);

                        // 5. Actualizamos el estado en MySQL a Entregado (3) para que salga del flujo logístico
                        bllPedido.CambiarEstadoPedido(numeroPedido, EstadoPedido.Entregado);

                        // 6. Invocamos tu método pasándole el objeto pedido que acabamos de recuperar
                        GenerarComprobanteDefinitivo(pedidoFinalizado);

                        // 7. Refrescamos la grilla de la derecha. Al tener estado 3, desaparecerá de la vista automáticamente
                        CargarGrillaEnCamino();

                        MessageBox.Show("¡Entrega registrada y comprobante procesado con éxito!", "Meraki", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, seleccioná el pedido de la grilla que querés marcar como entregado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar la entrega: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerarComprobanteDefinitivo(BEPedido pedido)
        {
            try
            {
                // 1. ARMAMOS EL OBJETO COMPROBANTE
                BEComprobante beComprobante = new BEComprobante();
                beComprobante.Cliente = pedido.Cliente;
                beComprobante.Fecha = DateTime.Now;
                beComprobante.Total = pedido.Total;
                beComprobante.ListaItems = pedido.ListaItems; // Pasa directo

                // 2. GENERADOR DEL NÚMERO SECUENCIAL (Tu lógica original intacta)
                var listaComprobantes = bllComprobante.listaComprobantes();
                if (listaComprobantes.Count == 0)
                {
                    beComprobante.Numero = "00000001";
                }
                else
                {
                    int numeroMaximo = 0;
                    foreach (var comprobante in listaComprobantes)
                    {
                        if (int.TryParse(comprobante.Numero, out int numeroComprobante))
                        {
                            if (numeroComprobante > numeroMaximo)
                            {
                                numeroMaximo = numeroComprobante;
                            }
                        }
                    }
                    beComprobante.Numero = (numeroMaximo + 1).ToString().PadLeft(8, '0');
                }

                // 3. GUARDAMOS EN LA BASE DE DATOS
                bllComprobante.GuardarNuevoComprobante(beComprobante);

                // 4. ¡LA MAGIA NUEVA!: Calculamos la ruta con el servicio de archivos
                string rutaBaseConfigurada = Properties.Settings.Default.CarpetaDestinoPDF;

                // Obtenemos la ruta completa estructurada por Año/Mes/Día/Comprobantes
                string rutaFinalPDF = Servicios.GestorRutas.GenerarRutaDestino(rutaBaseConfigurada,DateTime.Now, "Comprobantes", beComprobante.Numero);

                // Enviamos el comprobante y la ruta definitiva lista para escribir
                GenerarPDFComprobante(beComprobante, rutaFinalPDF);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el comprobante final: " + ex.Message, "Error interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GenerarPDFComprobante(BEComprobante comprobante, string rutaFinalPDF)
        {
            try
            {
                // 1. Usar tu clase de documento para armar la estructura visual
                var documento = new Servicios.ComprobanteDocument(comprobante);

                // 2. Generar y guardar DIRECTO en la ruta automática que armó nuestro servicio
                documento.GeneratePdf(rutaFinalPDF);

                // 3. Abrir el PDF automáticamente en pantalla para que lo puedan imprimir o ver
                System.Diagnostics.Process.Start(rutaFinalPDF);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo generar o abrir el PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
