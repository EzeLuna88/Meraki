using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Meraki
{
    public partial class Clientes : Form
    {
        BECliente beCliente;
        BLLCliente bllCliente;
        bool ComentariosHabilitado;
        private bool ComentariosModificados = false;
        private const string placeholderText = "   Buscar...";


        public Clientes()
        {
            beCliente = new BECliente();
            bllCliente = new BLLCliente();
            InitializeComponent();
            CargarDataGridClientes();
            ComentariosHabilitado = false;
            ComprobarComentarios();
            richTextBoxComentarios.Enabled = true;
            richTextBoxComentarios.ReadOnly = true;
            richTextBoxComentarios.BackColor = System.Drawing.Color.FromArgb(199, 91, 122);

        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            if (dataGridViewClientes.Rows.Count > 0 && dataGridViewClientes.Rows[0].Cells.Count > 0 && dataGridViewClientes.Rows[0].Cells[0].Value != null)
            {
                dataGridViewClientes.Rows[0].Selected = true;
                EscribirDatos();
                richTextBoxComentarios.BackColor = System.Drawing.Color.FromArgb(199, 91, 122);
                textBoxFiltrar.Text = placeholderText;
                textBoxFiltrar.ForeColor = System.Drawing.Color.Gray;
            }
        }

        public void CargarDataGridClientes()
        {
            dataGridViewClientes.DataSource = null;
            List<BECliente> listClientes = bllCliente.ListaClientes().OrderBy(cliente => cliente.Nombre).ToList();
            var bindingList = new BindingList<BECliente>(listClientes);
            dataGridViewClientes.DataSource = bindingList;
            ConfigurarDataGrid(dataGridViewClientes);


        }


        private void EscribirDatos()
        {
            if (dataGridViewClientes.Rows.Count > 0 && dataGridViewClientes.Rows[0].Cells.Count > 0 && dataGridViewClientes.Rows[0].Cells[0].Value != null)
            {
                if (dataGridViewClientes.CurrentRow.Cells[0].Value != null)
                {
                    beCliente = (BECliente)dataGridViewClientes.CurrentRow.DataBoundItem;

                    richTextBoxComentarios.Text = beCliente.Comentarios.ToString();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un cliente");
            }
        }

        public void ComprobarComentarios()
        {
            if (!ComentariosHabilitado)
            {
                iconButtonComentariosEditar.Visible = true;
                iconButtonComentariosGuardar.Visible = false;
                dataGridViewClientes.Enabled = true;
            }
            else
            {
                iconButtonComentariosEditar.Visible = false;
                iconButtonComentariosGuardar.Visible = true;
                //dataGridViewClientes.Enabled = false;
            }
        }

        public void ConfigurarDataGrid(DataGridView dataGridView)
        {

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
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = dataGridViewClientes.ColumnHeadersDefaultCellStyle.BackColor;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = dataGridViewClientes.ColumnHeadersDefaultCellStyle.ForeColor;
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns["HorarioDeApertura"].HeaderText = "Horario apertura";
            dataGridView.Columns["HorarioDeCierre"].HeaderText = "Horario cierre";
            dataGridView.Columns["TelefonoAlternativo"].HeaderText = "Telefono alternativo";
            dataGridView.AllowUserToAddRows = false;
            dataGridView.Columns[8].Visible = false;
            dataGridView.Columns[9].Visible = false;

            dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns[4].Width = 90;
            dataGridView.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns[5].Width = 90;
            dataGridView.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns[6].Width = 70;
            dataGridView.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns[7].Width = 70;


        }

        private void iconButtonAlta2_Click(object sender, EventArgs e)
        {
            ClientesAlta clientesAlta = new ClientesAlta();
            clientesAlta.ShowDialog();
            CargarDataGridClientes();
            if (dataGridViewClientes.Rows.Count > 0)
            {
                dataGridViewClientes.Rows[0].Selected = true;
            }
        }

        private void iconButtonBaja2_Click(object sender, EventArgs e)
        {
            if (dataGridViewClientes.SelectedRows.Count > 0)
            {
                if (dataGridViewClientes.CurrentRow != null)
                {
                    DialogResult confirmacion;
                    confirmacion = MessageBox.Show("Confirmar baja de cliente", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirmacion == DialogResult.Yes)
                    { bllCliente.BorrarCliente(beCliente); }
                    CargarDataGridClientes();
                }


            }
            else
            { MessageBox.Show("Debe seleccionar un cliente"); }
        }

        private void dataGridViewClientes_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                EscribirDatos();
                beCliente = (BECliente)dataGridViewClientes.Rows[e.RowIndex].DataBoundItem;

            }
            catch (Exception)
            {

                throw;
            }

        }

        private void iconButtonModificar2_Click(object sender, EventArgs e)
        {
            if (dataGridViewClientes.CurrentRow != null)
            {

                beCliente = (BECliente)dataGridViewClientes.CurrentRow.DataBoundItem;

                string codigoClienteActual = beCliente.Codigo;

                ClientesModificar modificar = new ClientesModificar();
                modificar.textBoxCodigo.Text = beCliente.Codigo;
                modificar.textBoxNombre.Text = beCliente.Nombre;
                modificar.textBoxDireccion.Text = beCliente.Direccion;
                modificar.textBoxLocalidad.Text = beCliente.Localidad;
                modificar.textBoxTelefono.Text = beCliente.Telefono;
                modificar.textBoxTelefonoAlternativo.Text = beCliente.TelefonoAlternativo;
                modificar.maskedTextBoxHorarioDeApertura.Text = beCliente.HorarioDeApertura.ToString();
                modificar.maskedTextBoxHorarioDeCierre.Text = beCliente.HorarioDeCierre.ToString();
                modificar.ShowDialog();

                CargarDataGridClientes();

                if (!string.IsNullOrEmpty(codigoClienteActual))
                {
                    // Desenganchamos el evento un segundo
                    dataGridViewClientes.SelectionChanged -= dataGridViewClientes_SelectionChanged_1;

                    dataGridViewClientes.ClearSelection();

                    foreach (DataGridViewRow row in dataGridViewClientes.Rows)
                    {
                        if (row.DataBoundItem is BECliente clienteEnGrilla && clienteEnGrilla.Codigo == codigoClienteActual)
                        {
                            dataGridViewClientes.CurrentCell = row.Cells[1]; // Foco en el nombre
                            row.Selected = true;

                            // Scrolleamos hasta el cliente si estaba muy abajo
                            if (row.Index >= 0)
                                dataGridViewClientes.FirstDisplayedScrollingRowIndex = row.Index;

                            break;
                        }
                    }

                    // Volvemos a enganchar el evento
                    dataGridViewClientes.SelectionChanged += dataGridViewClientes_SelectionChanged_1;

                    // Escribimos los datos actualizados en el panel de abajo
                    EscribirDatos();
                }

            }
            else
            { MessageBox.Show("Debe seleccionar un cliente"); }
        }

        private void textBoxFiltrar_TextChanged_1(object sender, EventArgs e)
        {
            string textoABuscar = textBoxFiltrar.Text.ToLower();

            if (string.IsNullOrWhiteSpace(textoABuscar) || textoABuscar == "   buscar...")
            {
                // Si el texto está vacío, carga toda la lista de clientes
                dataGridViewClientes.DataSource = bllCliente.ListaClientes().OrderBy(row => row.Nombre).ToList();
            }
            else
            {
                // Si hay texto en el TextBox, filtra la lista de clientes
                var tablaFiltrada = bllCliente.ListaClientes().Where(row => row.Nombre.ToLower().Contains(textoABuscar) ||
                                                                            row.Direccion.ToLower().Contains(textoABuscar));
                // Ordena la lista filtrada por nombre
                tablaFiltrada = tablaFiltrada.OrderBy(row => row.Nombre);
                dataGridViewClientes.DataSource = tablaFiltrada.ToList();
            }
        }

        private void iconButtonComentariosBorrar_Click(object sender, EventArgs e)
        {
            var filaSeleccionada = dataGridViewClientes.CurrentRow;

            ComentariosHabilitado = true;
            ComprobarComentarios();
            richTextBoxComentarios.Enabled = true;
            richTextBoxComentarios.BackColor = System.Drawing.Color.FromArgb(217, 171, 171);

            if (filaSeleccionada != null)
            {
                filaSeleccionada.Selected = true;
            }
        }

        private void HabilitarEdicionComentarios()
        {
            if (dataGridViewClientes.CurrentRow != null)
            {
                ComentariosHabilitado = true;
                ComprobarComentarios(); // Magia: Oculta "Editar" y muestra "Guardar"

                // Magia Visual: Desbloqueamos y cambiamos el color a uno más clarito
                richTextBoxComentarios.ReadOnly = false;
                richTextBoxComentarios.BackColor = System.Drawing.Color.FromArgb(217, 171, 171);

                // Ponemos el cursor parpadeando al final del texto listo para escribir
                richTextBoxComentarios.Focus();
                richTextBoxComentarios.SelectionStart = richTextBoxComentarios.Text.Length;
            }
            else
            {
                MessageBox.Show("Seleccione un cliente primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void iconButtonComentariosGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificamos si realmente escribió algo nuevo
                if (richTextBoxComentarios.Text.Trim() != beCliente.Comentarios)
                {
                    string codigoClienteActual = beCliente.Codigo;
                    // ¡CLAVE! Ponemos esto en false PRIMERO para que el SelectionChanged no salte.
                    ComentariosModificados = false;
                    ComentariosHabilitado = false;

                    beCliente.Comentarios = richTextBoxComentarios.Text.Trim();
                    bllCliente.AgregarModificarComentarios(beCliente);
                    MessageBox.Show("Se guardó el comentario", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Ahora sí recargamos la grilla, de forma segura
                    CargarDataGridClientes();

                    if (!string.IsNullOrEmpty(codigoClienteActual))
                    {
                        // Desenganchamos el evento un milisegundo para que no haga ruido
                        dataGridViewClientes.SelectionChanged -= dataGridViewClientes_SelectionChanged_1;

                        dataGridViewClientes.ClearSelection();

                        foreach (DataGridViewRow row in dataGridViewClientes.Rows)
                        {
                            if (row.DataBoundItem is BECliente clienteEnGrilla && clienteEnGrilla.Codigo == codigoClienteActual)
                            {
                                dataGridViewClientes.CurrentCell = row.Cells[1]; // Foco en el nombre
                                row.Selected = true;

                                // Hacemos que la grilla scrollee hasta donde está el cliente si estaba muy abajo
                                if (row.Index >= 0)
                                    dataGridViewClientes.FirstDisplayedScrollingRowIndex = row.Index;

                                break;
                            }
                        }

                        // Volvemos a enganchar el evento
                        dataGridViewClientes.SelectionChanged += dataGridViewClientes_SelectionChanged_1;
                    }
                }
                else
                {
                    MessageBox.Show("No se realizaron cambios en el comentario.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ResetearEstadoComentarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el comentario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetearEstadoComentarios()
        {
            ComentariosHabilitado = false;
            ComentariosModificados = false;

            richTextBoxComentarios.ReadOnly = true;
            richTextBoxComentarios.BackColor = System.Drawing.Color.FromArgb(199, 91, 122);

            ComprobarComentarios();

            // Le devolvemos el foco a la grilla para asegurar que el cursor salga del cuadro oscuro
            dataGridViewClientes.Focus();
        }

        private void dataGridViewClientes_SelectionChanged_1(object sender, EventArgs e)
        {
            try
            {
                // Solo salta la advertencia si estábamos editando Y si hubo cambios
                if (ComentariosHabilitado && ComentariosModificados)
                {
                    var resultado = MessageBox.Show(
                        "Estás editando un comentario. ¿Deseás guardar los cambios antes de cambiar de cliente?",
                        "Comentario en edición",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question
                    );

                    if (resultado == DialogResult.Yes)
                    {
                        // Ponemos las banderas en false para evitar el bucle infinito
                        ComentariosModificados = false;

                        beCliente.Comentarios = richTextBoxComentarios.Text.Trim();
                        bllCliente.AgregarModificarComentarios(beCliente);

                        ResetearEstadoComentarios();
                    }
                    else if (resultado == DialogResult.No)
                    {
                        ResetearEstadoComentarios();
                        // Al poner que "No", descartamos los cambios visuales y volvemos a escribir los datos originales
                        EscribirDatos();
                    }
                    else if (resultado == DialogResult.Cancel)
                    {
                        // Desconectamos el evento temporalmente
                        dataGridViewClientes.SelectionChanged -= dataGridViewClientes_SelectionChanged_1;

                        // Volvemos a la fila anterior (la que estábamos editando)
                        foreach (DataGridViewRow row in dataGridViewClientes.Rows)
                        {
                            if (row.DataBoundItem == beCliente)
                            {
                                dataGridViewClientes.CurrentCell = row.Cells[1]; // Seleccionamos la celda del nombre
                                break;
                            }
                        }

                        // Reconectamos el evento
                        dataGridViewClientes.SelectionChanged += dataGridViewClientes_SelectionChanged_1;

                        // Le devolvemos el foco al cuadro de texto para que siga editando
                        richTextBoxComentarios.Focus();
                        return; // Cortamos acá
                    }
                }

                // Lógica normal de carga de datos al hacer clic en un cliente (solo si NO estamos cancelando)
                if (!ComentariosHabilitado && dataGridViewClientes.SelectedRows.Count > 0)
                {
                    int rowIndex = dataGridViewClientes.SelectedRows[0].Index;
                    if (rowIndex >= 0 && rowIndex < dataGridViewClientes.Rows.Count)
                    {
                        EscribirDatos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar cliente: " + ex.Message);
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

        private void richTextBoxComentarios_TextChanged(object sender, EventArgs e)
        {
            if (ComentariosHabilitado)
                ComentariosModificados = true;
        }

        private void iconButtonComentariosEditar_Click(object sender, EventArgs e)
        {
            HabilitarEdicionComentarios();
        }




        private void Clientes_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                CargarDataGridClientes();
            }
        }

        private void richTextBoxComentarios_MouseDown_1(object sender, MouseEventArgs e)
        {
            // Si el cuadro está oscuro (no habilitado), no dejamos que el cursor se quede ahí
            if (!ComentariosHabilitado)
            {
                dataGridViewClientes.Focus(); // Mandamos el foco a la grilla instantáneamente
            }
        }

        private void richTextBoxComentarios_DoubleClick_1(object sender, EventArgs e)
        {
            HabilitarEdicionComentarios();
        }
    }
}
