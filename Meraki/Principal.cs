using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Meraki
{
    public partial class Principal : Form
    {
        private Form ventanaActiva = null; // Variable para almacenar la ventana activa actualmente
        int screenWidth;
        int screenHeight;

        public Principal()
        {

            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();

        }



        private void mayoristaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarVentanaActiva();



            CompraMayorista compraMayorista = new CompraMayorista();
            AbrirVentana(compraMayorista);

        }

        private void clientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CerrarVentanaActiva();


            Clientes clientes = new Clientes();
            AbrirVentana(clientes);

        }

        private void productosToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            Productos productos = new Productos();
            AbrirVentana(productos);


        }

        private void controlToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Stock stock = new Stock();
            AbrirVentana(stock);




        }

        private void minoristaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /* CerrarVentanaActiva();

             CompraMinorista compraMinorista = new CompraMinorista();
             AbrirVentana(compraMinorista);
            */
        }

        private void CerrarVentanaActiva()
        {
            if (ventanaActiva != null && !ventanaActiva.IsDisposed)
            {
                ventanaActiva.Close();
            }
        }

        // Método para abrir una nueva ventana y establecerla como ventana activa
        private void AbrirVentana(Form ventana)
        {
            ventanaActiva = ventana;
            ventana.MdiParent = this;
            ventana.WindowState = FormWindowState.Maximized;
            ventana.Show();
        }

        private void hacerBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void Principal_Load(object sender, EventArgs e)
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        }

        private void comprobantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarVentanaActiva();
            Comprobantes comprobantes = new Comprobantes();
            AbrirVentana(comprobantes);
        }
    }
}
