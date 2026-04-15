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
using BLL;
using FontAwesome.Sharp;


namespace Meraki
{
    public partial class PrincipalNuevo : Form
    {
        private IconButton currentButton;
        private Form currentChildForm;

        private Dashboard formDashboard;
        private CompraMayorista formCompraMayorista;
        private Productos formProductos;
        private Stock formStock;
        private Clientes formClientes;
        private Comprobantes formComprobantes;

        public PrincipalNuevo()
        {
            InitializeComponent();


            //Form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.WindowState = FormWindowState.Maximized;
        }

        private struct RGBColores
        {
            public static Color color1 = Color.FromArgb(0, 103, 105);
            public static Color color2 = Color.FromArgb(16, 67, 159);
            public static Color color3 = Color.FromArgb(7, 15, 43);
            public static Color color4 = Color.FromArgb(80, 60, 60);

        }

        private void ActivarBoton(object senderButton, Color color)
        {
            if (senderButton != null)
            {
                DesactivarBoton();

                currentButton = (IconButton)senderButton;
                currentButton.BackColor = Color.FromArgb(146, 26, 64);
                currentButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                currentButton.ForeColor = Color.White;
                currentButton.IconColor = Color.White;

            }
        }

        private void DesactivarBoton()
        {
            if (currentButton != null)
            {
                currentButton.BackColor = Color.FromArgb(199, 91, 122);
                currentButton.ForeColor = Color.Black;
                currentButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                currentButton.IconColor = Color.Black;

            }
        }

        private void OpenChildForm(Form childForm)
        {
            // Ya NO hacemos Dispose() de la pantalla anterior, simplemente la escondemos.
            if (currentChildForm != null)
            {
                currentChildForm.Hide();
            }

            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Solo lo agregamos al panel si es la primera vez que se crea
            if (!panelDesktop.Controls.Contains(childForm))
            {
                panelDesktop.Controls.Add(childForm);
            }

            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        // --- EVENTOS DE LOS BOTONES DEL MENÚ ---
        private void iconButton1_Click(object sender, EventArgs e)
        {
            ActivarBoton(sender, RGBColores.color2);
            if (formDashboard == null || formDashboard.IsDisposed) { formDashboard = new Dashboard(); }
            OpenChildForm(formDashboard);
        }

        private void iconButtonCompras_Click_1(object sender, EventArgs e)
        {
            ActivarBoton(sender, RGBColores.color1);
            if (formCompraMayorista == null || formCompraMayorista.IsDisposed) { formCompraMayorista = new CompraMayorista(); }
            OpenChildForm(formCompraMayorista);
        }

        private void iconButtonProductos_Click_1(object sender, EventArgs e)
        {
            ActivarBoton(sender, RGBColores.color2);
            if (formProductos == null || formProductos.IsDisposed) { formProductos = new Productos(); }
            OpenChildForm(formProductos);
        }

        private void iconButtonStock_Click_1(object sender, EventArgs e)
        {
            ActivarBoton(sender, RGBColores.color2);
            if (formStock == null || formStock.IsDisposed) { formStock = new Stock(); }
            OpenChildForm(formStock);
        }

        private void iconButtonClientes_Click_1(object sender, EventArgs e)
        {
            ActivarBoton(sender, RGBColores.color2);
            if (formClientes == null || formClientes.IsDisposed) { formClientes = new Clientes(); }
            OpenChildForm(formClientes);
        }

        private void iconButtonComprobantes_Click_1(object sender, EventArgs e)
        {
            ActivarBoton(sender, RGBColores.color2);
            if (formComprobantes == null || formComprobantes.IsDisposed) { formComprobantes = new Comprobantes(); }
            OpenChildForm(formComprobantes);
        }

        // --- ARRASTRE DE VENTANA Y BOTONES DE CONTROL ---
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelBarra_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

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

       

        private void PrincipalNuevo_Load(object sender, EventArgs e)
        {

        }

        private void iconButtonConfiguracion_Click_1(object sender, EventArgs e)
        {
            Configuracion configuracion = new Configuracion();
            configuracion.ShowDialog();
        }

        private void iconButtonCompras_VisibleChanged(object sender, EventArgs e)
        {

        }
    }
}
