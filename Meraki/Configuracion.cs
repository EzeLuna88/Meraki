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
using System.Runtime.InteropServices;

namespace Meraki
{
    public partial class Configuracion : Form
    {
        BLLCliente bllCliente;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public Configuracion()
        {
            InitializeComponent();
            bllCliente = new BLLCliente();
        }

                    
  

        private void Configuracion_Load(object sender, EventArgs e)
        {
            
        }

        

        private void iconButtonCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconButtonConfiguracionListaDePrecios_Click(object sender, EventArgs e)
        {
            ConfiguracionListaPrecios frmConfig = new ConfiguracionListaPrecios();

            // Opción A: Si querés que aparezca como una ventana emergente centrada (Modal)
            frmConfig.StartPosition = FormStartPosition.CenterParent;
            frmConfig.ShowDialog();
        }

        private void AbrirPantallaEnContenedor(UserControl pantallaHija)
        {
            // Limpiamos lo que haya en el escenario derecho
            panelContenedor.Controls.Clear();

            // Hacemos que la pantalla inyectada ocupe todo el espacio disponible
            pantallaHija.Dock = DockStyle.Fill;

            // La agregamos físicamente al panel
            panelContenedor.Controls.Add(pantallaHija);
        }

        private void buttonListaDePrecios_Click(object sender, EventArgs e)
        {
            // Instanciamos nuestro nuevo módulo de precios y lo mandamos al escenario
            UC_ConfigListaPrecios moduloPrecios = new UC_ConfigListaPrecios();
            AbrirPantallaEnContenedor(moduloPrecios);
        }

        private void buttonClienteDefecto_Click(object sender, EventArgs e)
        {
            UC_ConfigClienteDefecto pantallaCliente = new UC_ConfigClienteDefecto();

            // Limpiamos el panel derecho, le damos DockStyle.Fill a la pantalla y la agregamos
            panelContenedor.Controls.Clear();
            pantallaCliente.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(pantallaCliente);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private void Configuracion_Paint(object sender, PaintEventArgs e)
        {
            // Dibuja un marco de 2 píxeles con tu color bordó oscuro alrededor de toda la ventana
            Color colorBorde = System.Drawing.ColorTranslator.FromHtml("#400F1C");
            using (Pen pen = new Pen(colorBorde, 2))
            {
                // Se resta 1 al ancho y alto para que la línea no se dibuje fuera de la pantalla
                e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }

        private void iconButtonCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UC_ConfigRutaArchivos pantallaRutas = new UC_ConfigRutaArchivos();
            panelContenedor.Controls.Clear();
            pantallaRutas.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(pantallaRutas);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UC_ConfigDrive configuracionDrive = new UC_ConfigDrive();
            panelContenedor.Controls.Clear();
            configuracionDrive.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(configuracionDrive);
        }

        private void panelBarra_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }
    }
}
