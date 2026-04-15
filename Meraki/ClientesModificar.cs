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
using BE;
using BLL;

namespace Meraki
{
    public partial class ClientesModificar : Form
    {
        BECliente beCliente;
        BLLCliente bllCliente;
        public ClientesModificar()
        {
            beCliente = new BECliente();
            bllCliente = new BLLCliente();
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
        }





        private void textBoxTelefonoAlternativo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si el carácter es un número, un guion o un paréntesis
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '(' && e.KeyChar != ')')
            {
                e.Handled = true; // Si no es ninguno de esos caracteres, se ignora
            }
        }

        private void iconButtonModificar_Click(object sender, EventArgs e)
        {
            try
            {
                beCliente.Codigo = textBoxCodigo.Text;

                // Usamos Trim() para limpiar espacios accidentales
                beCliente.Nombre = textBoxNombre.Text.Trim();
                beCliente.Direccion = textBoxDireccion.Text.Trim();
                beCliente.Localidad = textBoxLocalidad.Text.Trim();
                beCliente.Telefono = textBoxTelefono.Text.Trim();

                beCliente.TelefonoAlternativo = string.IsNullOrWhiteSpace(textBoxTelefonoAlternativo.Text) ? "-" : textBoxTelefonoAlternativo.Text.Trim();

                // 1. Protección de máscara
                if (!maskedTextBoxHorarioDeApertura.MaskFull || !maskedTextBoxHorarioDeCierre.MaskFull)
                {
                    MessageBox.Show("Debe completar los horarios de apertura y cierre correctamente (HH:MM).", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Protección de UI: Que la hora exista (evita que explote con horas falsas)
                if (!TimeSpan.TryParse(maskedTextBoxHorarioDeApertura.Text, out TimeSpan apertura) ||
                    !TimeSpan.TryParse(maskedTextBoxHorarioDeCierre.Text, out TimeSpan cierre))
                {
                    MessageBox.Show("Los horarios ingresados no son válidos (Formato 24hs).", "Error en horario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                beCliente.HorarioDeApertura = apertura;
                beCliente.HorarioDeCierre = cierre;

                DialogResult confirmacion = MessageBox.Show("¿Confirmar la modificación de los datos?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    // --- LA BLL TOMA EL CONTROL ---
                    bllCliente.ModificarCliente(beCliente);
                    MessageBox.Show("Cliente modificado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (ArgumentException ex)
            {
                // Atajamos a la BLL si falta algún dato clave
                MessageBox.Show(ex.Message, "Datos incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Operación no permitida", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado al modificar: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconButtonCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; Close();

        }

        private void panelBarra_MouseDown(object sender, MouseEventArgs e)
        {

        }

        //Drag form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);



        private void iconButtonCerrar_Click_1(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void iconButtonMaximizar_Click_1(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void iconButtonMinimizar_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void ClientesModificar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void textBoxTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '(' && e.KeyChar != ')')
            {
                e.Handled = true;
            }
        }
    }
}
