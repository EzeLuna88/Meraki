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
        private Panel leftBorderButton;
        private Form currentChildForm;
        BLLStock bllStock;
        public PrincipalNuevo()
        {
            bllStock = new BLLStock();
            InitializeComponent();
            leftBorderButton = new Panel();
            leftBorderButton.Size = new Size(188, 7);
            panelMenu.Controls.Add(leftBorderButton);

            //Form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
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
                //boton

                currentButton = (IconButton)senderButton;

                currentButton.BackColor = Color.FromArgb(146, 26, 64);
                currentButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                currentButton.ForeColor = Color.White;
                currentButton.IconColor = Color.White;

                //leftBorderButton
                /*leftBorderButton.BackColor = color;
                leftBorderButton.Location = new Point(currentButton.Location.X, 0);
                leftBorderButton.Visible = true;
                leftBorderButton.BringToFront();*/
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
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            // Espacio para el logo
            int panelLogoWidth = panelLogo.Width;

            // Ancho disponible para los botones
            int availableWidth = this.ClientSize.Width - panelLogoWidth;

            // Número de botones
            int buttonCount = 6; // Cantidad de botones

            // Ancho de cada botón
            int buttonWidth = availableWidth / buttonCount;

            // Asignar el ancho calculado a cada botón
            iconButtonCompras.Width = buttonWidth;
            iconButtonProductos.Width = buttonWidth;
            iconButtonStock.Width = buttonWidth;
            iconButtonClientes.Width = buttonWidth;
            iconButtonComprobantes.Width = buttonWidth;
            iconButtonDashboard.Width = buttonWidth;

            // Posicionar los botones en consecuencia
            iconButtonDashboard.Left = panelLogoWidth;
            iconButtonCompras.Left = panelLogoWidth + buttonWidth;
            iconButtonProductos.Left = panelLogoWidth + 2 * buttonWidth;
            iconButtonStock.Left = panelLogoWidth + 3 * buttonWidth;
            iconButtonClientes.Left = panelLogoWidth + 4 * buttonWidth;
            iconButtonComprobantes.Left = panelLogoWidth + 5 * buttonWidth;
        }


        //Drag form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void iconButtonCompras_Click_1(object sender, EventArgs e)
        {
            ActivarBoton(sender, RGBColores.color1);
            OpenChildForm(new CompraMayorista());

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

        private void iconButtonProductos_Click_1(object sender, EventArgs e)
        {
            ActivarBoton(sender, RGBColores.color2);
            OpenChildForm(new Productos());
        }

        private void iconButtonStock_Click_1(object sender, EventArgs e)
        {
            ActivarBoton(sender, RGBColores.color2);
            OpenChildForm(new Stock());
        }

        private void iconButtonClientes_Click_1(object sender, EventArgs e)
        {
            ActivarBoton(sender, RGBColores.color2);
            OpenChildForm(new Clientes());
        }

        private void iconButtonComprobantes_Click_1(object sender, EventArgs e)
        {
            ActivarBoton(sender, RGBColores.color2);
            OpenChildForm(new Comprobantes());
        }

        private void panelBarra_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            ActivarBoton(sender, RGBColores.color2);
            OpenChildForm(new Dashboard());
        }

        private void PrincipalNuevo_Load(object sender, EventArgs e)
        {
            
        }
    }
}
