using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;

namespace Meraki
{
    public partial class ClientesAlta : Form
    {
        BECliente beCliente;
        BLLCliente bllCliente;
        public ClientesAlta()
        {
            beCliente = new BECliente();
            bllCliente= new BLLCliente();
            InitializeComponent();
        }

      

        private void buttonAlta_Click(object sender, EventArgs e)
        {
            try
            {
                if(String.IsNullOrEmpty(textBoxNombre.Text))
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
            catch (Exception)
            {

                throw;
            }
       }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; Close();

        }

       


    }
}
