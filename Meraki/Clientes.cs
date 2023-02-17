using System;
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

namespace Meraki
{
    public partial class Clientes : Form
    {
        BECliente beCliente;
        BLLCliente bllCliente;
        public Clientes()
        {
            beCliente= new BECliente();
            bllCliente = new BLLCliente();
            InitializeComponent();
            CargarDataGridClientes();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            if (dataGridViewClientes.Rows.Count > 0)
            {
                dataGridViewClientes.Rows[0].Selected = true;
                EscribirDatos();
            }
        }

        private void buttonAlta_Click(object sender, EventArgs e)
        {
            ClientesAlta clientesAlta = new ClientesAlta();
            clientesAlta.ShowDialog();
            CargarDataGridClientes();
            dataGridViewClientes.Rows[0].Selected = true;

        }

        public void CargarDataGridClientes()
        {
            dataGridViewClientes.DataSource = null;
            List<BECliente> listClientes = bllCliente.ListaClientes();
            var bindingList = new BindingList<BECliente>(listClientes);
            dataGridViewClientes.DataSource = bindingList;

            dataGridViewClientes.Columns[0].Visible = false;
            dataGridViewClientes.Columns["HorarioDeApertura"].HeaderText = "Horario de apertura";
            dataGridViewClientes.Columns["HorarioDeCierre"].HeaderText = "Horario de cierre";


        }

        private void dataGridViewClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                EscribirDatos();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void EscribirDatos()
        {
            beCliente = (BECliente)dataGridViewClientes.CurrentRow.DataBoundItem;
            labelNombre.Text = beCliente.Nombre;
            labelDireccion.Text = beCliente.Direccion;
            labelLocalidad.Text = beCliente.Localidad;
            labelTelefono.Text = beCliente.Telefono;
            labelHorarioDeApertura.Text = beCliente.HorarioDeApertura.ToString();
            labelHorarioDeCierre.Text = beCliente.HorarioDeCierre.ToString();
        }

        private void buttonBaja_Click(object sender, EventArgs e)
        {
            if (dataGridViewClientes.SelectedRows.Count > 0)
            {
                
                DialogResult confirmacion;
                confirmacion = MessageBox.Show("Confirmar baja de cliente", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(confirmacion == DialogResult.Yes) 
                { bllCliente.BorrarCliente(beCliente); }
                CargarDataGridClientes();
                Limpiar();

                

            }
            else
            { MessageBox.Show("Debe seleccionar un cliente"); }
        }

        public void Limpiar()
        {
            try
            {
                labelNombre.Text = string.Empty;
                labelDireccion.Text = string.Empty;
                labelLocalidad.Text = string.Empty;
                labelTelefono.Text = string.Empty;
                labelHorarioDeApertura.Text = string.Empty;
                labelHorarioDeCierre.Text = string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void buttonModificar_Click(object sender, EventArgs e)
        {
            beCliente = (BECliente)dataGridViewClientes.CurrentRow.DataBoundItem;
            ClientesModificar modificar = new ClientesModificar();
            modificar.textBoxCodigo.Text = beCliente.Codigo;
            modificar.textBoxNombre.Text = beCliente.Nombre;
            modificar.textBoxDireccion.Text= beCliente.Direccion;
            modificar.textBoxLocalidad.Text = beCliente.Localidad;
            modificar.textBoxTelefono.Text = beCliente.Telefono;
            modificar.maskedTextBoxHorarioDeApertura.Text = beCliente.HorarioDeApertura.ToString();
            modificar.maskedTextBoxHorarioDeCierre.Text = beCliente.HorarioDeCierre.ToString();
            modificar.ShowDialog();
            CargarDataGridClientes();
            
        }

        
    }
}
