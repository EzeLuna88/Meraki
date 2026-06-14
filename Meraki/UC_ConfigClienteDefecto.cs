using BE;
using BLL;
using Servicios;
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

namespace Meraki
{
    public partial class UC_ConfigClienteDefecto : UserControl
    {
        BLLCliente bllCliente = new BLLCliente();
        List<BECliente> listaClientesCompleta;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        public UC_ConfigClienteDefecto()
        {
            InitializeComponent();
        }

        private void UC_ConfigClienteDefecto_Load(object sender, EventArgs e)
        {
            // 1. Ponemos el "Filtrar..." en el buscador
            SendMessage(textBoxFiltrarClientes.Handle, EM_SETCUEBANNER, 0, "Filtrar por nombre...");

            // 2. Traemos todos los clientes de la DB para llenar la grilla
            listaClientesCompleta = bllCliente.ListaClientes();
            dataGridViewClientes.DataSource = listaClientesCompleta;
            ConfigurarDataGrid(dataGridViewClientes);
            // 3. Acá deberías ir a buscar a la DB o a tu archivo de config cuál es el cliente actual
            string clienteGuardado = Properties.Settings.Default.ClientePorDefecto;

            if (!string.IsNullOrEmpty(clienteGuardado))
            {
                // Lo buscamos en la lista que trajimos de la DB.
                // OJO: Asumo que en Properties guardás el "Id" o el "Nombre". 
                // Cambiá "c.Id.ToString()" o "c.Nombre" dependiendo de qué valor guardabas en tu viejo SelectedValue
                BECliente clienteActual = listaClientesCompleta.FirstOrDefault(c => c.Codigo.ToString() == clienteGuardado);

                MostrarClienteEnBanner(clienteActual);
            }
            else
            {
                // Si estaba vacío, mostramos el cartel de NINGUNO
                MostrarClienteEnBanner(null);
            }
        }

        private void textBoxFiltrarClientes_TextChanged(object sender, EventArgs e)
        {
            string filtro = textBoxFiltrarClientes.Text.ToLower();

            // Filtramos la lista original usando LINQ
            var listaFiltrada = listaClientesCompleta
                .Where(c => c.Nombre.ToLower().Contains(filtro) )
                .ToList();

            dataGridViewClientes.DataSource = null;
            dataGridViewClientes.DataSource = listaFiltrada;
        }

        private void iconButtonEstablecer_Click(object sender, EventArgs e)
        {
            // Validamos que haya una fila seleccionada en la grilla
            if (dataGridViewClientes.CurrentRow != null &&
                dataGridViewClientes.CurrentRow.DataBoundItem is BECliente clienteSeleccionado)
            {
                // 1. ACÁ VA TU LÓGICA DE GUARDADO EN LA BASE DE DATOS O CONFIGURACIÓN
                Properties.Settings.Default.ClientePorDefecto = clienteSeleccionado.Codigo.ToString();
                Properties.Settings.Default.Save();

                // 2. Actualizamos el cartel gigante explícitamente
                MostrarClienteEnBanner(clienteSeleccionado);

                // 3. Feedback visual contundente
                MessageBox.Show($"Se ha configurado a '{clienteSeleccionado.Nombre}' como cliente para ventas rápidas.",
                                "Configuración Guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Por favor, buscá y seleccioná un cliente de la grilla primero.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MostrarClienteEnBanner(BECliente cliente)
        {
            if (cliente != null)
            {
                labelClienteActual.Text = cliente.Nombre.ToUpper();
                labelClienteActual.ForeColor = System.Drawing.ColorTranslator.FromHtml("#921A40"); // Tu Bordó
            }
            else
            {
                labelClienteActual.Text = "NINGUNO (SELECCIONAR MANUALMENTE)";
                labelClienteActual.ForeColor = System.Drawing.Color.DimGray;
            }
        }

        public void ConfigurarDataGrid(DataGridView dataGridView)
        {
            dataGridView.AplicarEstiloMeraki();

            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[8].Visible = false;
            dataGridView.Columns[9].Visible = false;

            dataGridView.Columns["HorarioDeApertura"].HeaderText = "Horario apertura";
            dataGridView.Columns["HorarioDeCierre"].HeaderText = "Horario cierre";
            dataGridView.Columns["TelefonoAlternativo"].HeaderText = "Telefono alternativo";
            dataGridView.Columns["TelefonoAlternativo"].Visible = false;
            dataGridView.Columns["HorarioDeApertura"].Visible = false;
            dataGridView.Columns["HorarioDeCierre"].Visible = false;
            dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns[4].Width = 90;
            dataGridView.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns[5].Width = 90;
            dataGridView.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns[6].Width = 70;
            dataGridView.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView.Columns[7].Width = 70;

        }
    }
}
