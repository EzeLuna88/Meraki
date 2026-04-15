using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;

namespace Meraki
{
    public partial class Configuracion : Form
    {
        BLLCliente bllCliente;

        public Configuracion()
        {
            InitializeComponent();
            bllCliente = new BLLCliente();
        }

                    
  

        private void Configuracion_Load(object sender, EventArgs e)
        {
            try
            {
                List<BECliente> listaClientes = bllCliente.ListaClientes()
                                               .OrderBy(c => c.Nombre)
                                               .ToList();

                // Le "enseñamos" al ComboBox cómo mostrar los datos
                comboBoxClientes.DataSource = listaClientes;
                comboBoxClientes.DisplayMember = "Nombre"; // Lo que lee el usuario
                comboBoxClientes.ValueMember = "Codigo";   // El dato oculto que nos importa (o "Id" si usás ID)

                // Buscamos si ya había un cliente guardado de antes
                string clienteGuardado = Properties.Settings.Default.ClientePorDefecto;

                if (!string.IsNullOrEmpty(clienteGuardado))
                {
                    // Si había uno guardado, le decimos al ComboBox que lo seleccione automáticamente
                    comboBoxClientes.SelectedValue = clienteGuardado;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message);
            }
        }

        private void iconButtonAceptar_Click_1(object sender, EventArgs e)
        {
            if (comboBoxClientes.SelectedValue != null)
            {
                string idAueGuardar = comboBoxClientes.SelectedValue.ToString();

                // Guardamos
                Properties.Settings.Default.ClientePorDefecto = idAueGuardar;
                Properties.Settings.Default.Save();

                // LEEMOS inmediatamente para verificar
                string chequeo = Properties.Settings.Default.ClientePorDefecto;
                MessageBox.Show($"Modificación guardada");

                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un cliente.");
            }
        }

        private void iconButtonCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
