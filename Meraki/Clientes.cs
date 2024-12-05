﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using BE;
using System.Runtime.Remoting;
using System.Data.Common;
using System.Windows.Media;

namespace Meraki
{
    public partial class Clientes : Form
    {
        BECliente beCliente;
        BLLCliente bllCliente;
        bool ComentariosHabilitado;
        private const string placeholderText = "   Buscar...";


        public Clientes()
        {
            beCliente = new BECliente();
            bllCliente = new BLLCliente();
            InitializeComponent();
            CargarDataGridClientes();
            ComentariosHabilitado = false;
            ComprobarComentarios();
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
                iconButtonComentariosBorrar.Visible = true;
                iconButtonComentariosGuardar.Visible = false;
                dataGridViewClientes.Enabled = true;
            }
            else
            {
                iconButtonComentariosBorrar.Visible = false;
                iconButtonComentariosGuardar.Visible = true;
                dataGridViewClientes.Enabled = false;
            }
        }







        private void buttonComentariosBorrar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro de que desea borrar el comentario?",
                                             "Confirmar borrado",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
            {
                beCliente.Comentarios = string.Empty;
                bllCliente.AgregarModificarComentarios(beCliente);
                CargarDataGridClientes();
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
            ComentariosHabilitado = true;
            ComprobarComentarios();
            richTextBoxComentarios.Enabled = true;
            richTextBoxComentarios.BackColor = System.Drawing.Color.FromArgb(217, 171, 171);

        }

        private void iconButtonComentariosGuardar_Click(object sender, EventArgs e)
        {
            ComentariosHabilitado = false;
            ComprobarComentarios();
            richTextBoxComentarios.Enabled = false;
            beCliente.Comentarios = richTextBoxComentarios.Text.Trim();
            bllCliente.AgregarModificarComentarios(beCliente);
            MessageBox.Show("Se guardo el comentario");
            richTextBoxComentarios.BackColor = System.Drawing.Color.FromArgb(199, 91, 122);
            CargarDataGridClientes();
        }

        private void dataGridViewClientes_SelectionChanged_1(object sender, EventArgs e)
        {
            try
            {
                // Verifica que hay una fila seleccionada
                if (dataGridViewClientes.SelectedRows.Count > 0)
                {
                    // Obtén el índice de la fila seleccionada
                    int rowIndex = dataGridViewClientes.SelectedRows[0].Index;

                    // Verifica que el índice sea válido
                    if (rowIndex >= 0 && rowIndex < dataGridViewClientes.Rows.Count)
                    {
                        // Obtén el cliente correspondiente a la fila seleccionada
                        BECliente clienteSeleccionado = (BECliente)dataGridViewClientes.Rows[rowIndex].DataBoundItem;

                        // Aquí puedes realizar las acciones necesarias con el cliente seleccionado
                        EscribirDatos(); // Pasamos el cliente seleccionado

                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores más específico para depuración
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
    }
}
