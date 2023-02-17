using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Meraki
{
    public partial class Principal : Form
    {
        public Principal()
        {
            this.WindowState= FormWindowState.Maximized;
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clientes clientes = new Clientes();
            clientes.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Productos productos= new Productos();
            productos.Show();
        }

        private void buttonStock_Click(object sender, EventArgs e)
        {
            Stock stock = new Stock();
            stock.Show();
        }

        private void buttonNuevaCompra_Click(object sender, EventArgs e)
        {
            Compra compra= new Compra();
            compra.ShowDialog();
        }

        private void mayoristaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompraMayorista compraMayorista = new CompraMayorista();
            compraMayorista.MdiParent= this;
            compraMayorista.WindowState = FormWindowState.Maximized;
            compraMayorista.Show();
        }

        private void clientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clientes clientes = new Clientes();
            clientes.MdiParent = this;
            clientes.WindowState = FormWindowState.Maximized;
            clientes.Show();


        }

        private void productosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Productos productos = new Productos();
            productos.MdiParent = this;
            productos.WindowState = FormWindowState.Maximized;
            productos.Show();
        }

        private void controlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stock stock = new Stock();
            stock.MdiParent = this;
            stock.WindowState= FormWindowState.Maximized;
            stock.Show();
        }

        private void minoristaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompraMinorista compraMinorista = new CompraMinorista();
            compraMinorista.MdiParent = this;
            compraMinorista.WindowState = FormWindowState.Maximized;
            compraMinorista.Show();
        }
    }
}
