using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Meraki
{
    public partial class ClientesAlta : Form
    {
        BECliente beCliente;
        BLLCliente bllCliente;
        public ClientesAlta()
        {
            beCliente = new BECliente();
            bllCliente = new BLLCliente();
            InitializeComponent();

            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
        }

        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {
            textBoxNombre.Text = textBoxNombre.Text.ToUpper();
            textBoxNombre.SelectionStart = textBoxNombre.Text.Length;
        }

        private void textBoxDireccion_TextChanged(object sender, EventArgs e)
        {
            textBoxDireccion.Text = textBoxDireccion.Text.ToUpper();
            textBoxDireccion.SelectionStart = textBoxDireccion.Text.Length;
        }

        private void textBoxLocalidad_TextChanged(object sender, EventArgs e)
        {
            textBoxLocalidad.Text = textBoxLocalidad.Text.ToUpper();
            textBoxLocalidad.SelectionStart = textBoxLocalidad.Text.Length;
        }

        private void textBoxTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si el carácter es un número, un guion o un paréntesis
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '(' && e.KeyChar != ')')
            {
                e.Handled = true; // Si no es ninguno de esos caracteres, se ignora
            }
        }

        private void textBoxTelefonoAlternativo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si el carácter es un número, un guion o un paréntesis
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '(' && e.KeyChar != ')')
            {
                e.Handled = true; // Si no es ninguno de esos caracteres, se ignora
            }
        }

        private void iconButtonAlta2_Click(object sender, EventArgs e)
        {
            try
            {
                // El Trim() nos asegura que no guarden "JUAN   " por error
                beCliente.Nombre = textBoxNombre.Text.Trim();
                beCliente.Direccion = textBoxDireccion.Text.Trim();
                beCliente.Localidad = textBoxLocalidad.Text.Trim();
                beCliente.Telefono = textBoxTelefono.Text.Trim();

                beCliente.TelefonoAlternativo = string.IsNullOrWhiteSpace(textBoxTelefonoAlternativo.Text) ? "-" : textBoxTelefonoAlternativo.Text.Trim();

                // 1. Protección de UI: Que la máscara esté completa
                if (!maskedTextBoxHorarioDeApertura.MaskFull || !maskedTextBoxHorarioDeCierre.MaskFull)
                {
                    MessageBox.Show("Debe completar los horarios de apertura y cierre correctamente (HH:MM).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Protección de UI: Que la hora exista (evita que explote con "99:99")
                if (!TimeSpan.TryParse(maskedTextBoxHorarioDeApertura.Text, out TimeSpan apertura) ||
                    !TimeSpan.TryParse(maskedTextBoxHorarioDeCierre.Text, out TimeSpan cierre))
                {
                    MessageBox.Show("Los horarios ingresados no son válidos (Formato 24hs).", "Error en horario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                beCliente.HorarioDeApertura = apertura;
                beCliente.HorarioDeCierre = cierre;

                // --- LA BLL TOMA EL CONTROL ---
                bllCliente.GuardarCliente(beCliente);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ArgumentException ex)
            {
                // Acá atajamos si la BLL dice "Falta el nombre" o "Falta dirección"
                MessageBox.Show(ex.Message, "Faltan datos o son incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado al guardar: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; Close();
        }



        //Drag form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void iconButtonCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void iconButtonMaximizar_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void iconButtonMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void ClientesAlta_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
