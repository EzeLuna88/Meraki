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
                if (String.IsNullOrEmpty(textBoxNombre.Text))
                { MessageBox.Show("Debe colocar un nombre"); }
                else
                {
                    beCliente.Nombre = textBoxNombre.Text;
                    if (String.IsNullOrEmpty(textBoxDireccion.Text))
                    { MessageBox.Show("Debe colocar una direccion"); }
                    else
                    {
                        beCliente.Direccion = textBoxDireccion.Text;
                        if (String.IsNullOrEmpty(textBoxLocalidad.Text))
                        { MessageBox.Show("Debe colocar una localidad"); }
                        else
                        {
                            beCliente.Localidad = textBoxLocalidad.Text;
                            if (String.IsNullOrEmpty(textBoxTelefono.Text))
                            { MessageBox.Show("Debe colocar un telefono"); }
                            else
                            {
                                beCliente.Telefono = textBoxTelefono.Text;
                                if (string.IsNullOrEmpty(textBoxTelefonoAlternativo.Text))
                                {
                                    beCliente.TelefonoAlternativo = "-";
                                    if (!maskedTextBoxHorarioDeApertura.MaskFull)
                                    { MessageBox.Show("debe colocar un horario de apertura"); }
                                    else
                                    {
                                        beCliente.HorarioDeApertura = TimeSpan.Parse(maskedTextBoxHorarioDeApertura.Text);
                                        if (!maskedTextBoxHorarioDeCierre.MaskFull)
                                        { MessageBox.Show("Debe colocar un horario de cierre"); }
                                        else
                                        {
                                            beCliente.HorarioDeCierre = TimeSpan.Parse(maskedTextBoxHorarioDeCierre.Text);
                                            bllCliente.GuardarCliente(beCliente);
                                            DialogResult = DialogResult.OK;
                                            Close();
                                        }

                                    }
                                }
                                else
                                {
                                    beCliente.TelefonoAlternativo = textBoxTelefonoAlternativo.Text;
                                    if (!maskedTextBoxHorarioDeApertura.MaskFull)
                                    { MessageBox.Show("debe colocar un horario de apertura"); }
                                    else
                                    {
                                        beCliente.HorarioDeApertura = TimeSpan.Parse(maskedTextBoxHorarioDeApertura.Text);
                                        if (!maskedTextBoxHorarioDeCierre.MaskFull)
                                        { MessageBox.Show("Debe colocar un horario de cierre"); }
                                        else
                                        {
                                            beCliente.HorarioDeCierre = TimeSpan.Parse(maskedTextBoxHorarioDeCierre.Text);
                                            bllCliente.GuardarCliente(beCliente);
                                            DialogResult = DialogResult.OK;
                                            Close();
                                        }

                                    }
                                }

                            }

                        }

                    }

                }

            }
            catch (Exception)
            {

                throw;
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
