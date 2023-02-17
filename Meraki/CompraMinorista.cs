using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Meraki
{
    public partial class CompraMinorista : Form
    {
        BEProductoIndividual beProductoIndividual;
        BEProductoCombo beProductoCombo;
        BLLProducto bllProducto;
        BECompraMinorista beCompraMinorista;
        BECarrito beCarrito;
        BLLStock bllStock;
        List<BEStock> listaStock;
        BLLCompraMinorista bllCompraMinorista;
        public CompraMinorista()
        {
            bllStock = new BLLStock();
            listaStock= new List<BEStock>();
            listaStock = bllStock.CargarStock();
            beCompraMinorista = new BECompraMinorista();
            beProductoIndividual = new BEProductoIndividual();
            beProductoCombo = new BEProductoCombo();
            bllProducto= new BLLProducto();
            bllCompraMinorista = new BLLCompraMinorista();
            InitializeComponent();
            CargarDataGrid();
        }

        public void CargarDataGrid()
        {
            dataGridViewProductos.DataSource = null;
            dataGridViewProductos.DataSource = bllProducto.listaProductos();
            dataGridViewProductos.Columns["Codigo"].Visible = false;
            dataGridViewProductos.Columns["Tipo"].Visible = false;
            dataGridViewProductos.Columns["Unidad"].HeaderText = "Unidades";
            if (dataGridViewProductos.Columns["nombre"] == null)
            {
                dataGridViewProductos.Columns.Add("nombre", "nombre");
            }
            dataGridViewProductos.Columns["nombre"].DisplayIndex = 0;
            dataGridViewProductos.CellFormatting += dataGridViewProductos_CellFormatting; 
            dataGridViewProductos.Columns["precioMayorista"].Visible = false;
            dataGridViewProductos.Columns["precioMinorista"].HeaderText = "Precio";
            dataGridViewProductos.Columns["precioMinorista"].DefaultCellStyle.Format = "c2";
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                bool hayStock = true;
                if (dataGridViewProductos.CurrentRow.DataBoundItem is BEProductoIndividual)
                {
                    beProductoIndividual = (BEProductoIndividual)dataGridViewProductos.CurrentRow.DataBoundItem;
                    var productoEnCarrito = beCompraMinorista.ListaCarrito.Find(p => p.Producto.Codigo == beProductoIndividual.Codigo);
                    if (productoEnCarrito != null)
                    {
                        var productoEnStock = listaStock.Find(p => p.Codigo == beProductoIndividual.Stock.Codigo);
                        if (productoEnStock.CantidadActual >= beProductoIndividual.Unidad)
                        {
                            productoEnCarrito.Cantidad++;
                            productoEnCarrito.Total = beProductoIndividual.PrecioMinorista * beCarrito.Cantidad;
                            productoEnStock.CantidadActual -= beProductoIndividual.Unidad;
                            CargarDataGridCarrito();
                            CalcularTotal();

                        }
                        else
                        {
                            MessageBox.Show("No hay suficiente stock");
                        }
                    }
                    else
                    {
                        beCarrito = new BECarrito();
                        beCarrito.Producto = beProductoIndividual;


                        var productoEnStock = listaStock.Find(p => p.Codigo == beProductoIndividual.Stock.Codigo);
                        if (productoEnStock.CantidadActual >= beProductoIndividual.Unidad)
                        {
                            beCarrito.Cantidad++;
                            beCarrito.Total = beProductoIndividual.PrecioMinorista * beCarrito.Cantidad;
                            productoEnStock.CantidadActual -= beProductoIndividual.Unidad;
                            beCompraMinorista.ListaCarrito.Add(beCarrito);
                            CargarDataGridCarrito();
                            CalcularTotal();

                        }
                        else
                        {
                            MessageBox.Show("No hay suficiente stock");
                        }
                    }
                }
                else
                {
                    beProductoCombo = (BEProductoCombo)dataGridViewProductos.CurrentRow.DataBoundItem;
                    var productoEnCarrito = beCompraMinorista.ListaCarrito.Find(p => p.Producto.Codigo == beProductoCombo.Codigo);
                    if (productoEnCarrito != null)
                    {
                        foreach (BEStock item in beProductoCombo.ListaProductos)
                        {
                            var itemEnStock = listaStock.Find(p => p.Codigo == item.Codigo);
                            if (itemEnStock.CantidadActual >= 1)
                            {
                                itemEnStock.CantidadActual--;
                                hayStock = true;
                            }
                            else
                            {
                                hayStock = false;
                                MessageBox.Show("No hay suficiente stock");
                                break;
                            }
                        }
                        if (hayStock == true)

                        {
                            beCarrito.Cantidad++;
                            beCarrito.Total = beProductoCombo.PrecioMinorista * beCarrito.Cantidad;
                            CargarDataGridCarrito();
                            CalcularTotal();
                        }
                    }
                    else
                    {
                        beCarrito = new BECarrito();
                        beCarrito.Producto = beProductoCombo;
                        foreach (BEStock item in beProductoCombo.ListaProductos)
                        {
                            var itemEnStock = listaStock.Find(p => p.Codigo == item.Codigo);
                            if (itemEnStock.CantidadActual >= 1)
                            {
                                itemEnStock.CantidadActual--;
                                hayStock = true;
                            }
                            else
                            {
                                hayStock = false;
                                MessageBox.Show("No hay suficiente stock");
                            }
                        }

                        if(hayStock == true)

                        {
                            beCarrito.Cantidad++;
                            beCarrito.Total = beProductoCombo.PrecioMinorista * beCarrito.Cantidad;
                            beCompraMinorista.ListaCarrito.Add(beCarrito);
                            CargarDataGridCarrito();
                            CalcularTotal();
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void CargarDataGridCarrito()
        {
            dataGridViewCarrito.DataSource = null;
            dataGridViewCarrito.DataSource = beCompraMinorista.ListaCarrito;
            dataGridViewCarrito.Columns["Total"].DefaultCellStyle.Format = "c2";
                }

        private void CalcularTotal()
        {
            decimal total = 0;
            foreach (var producto in beCompraMinorista.ListaCarrito)
            {
                total += producto.Total;
                beCompraMinorista.Total = total;

            }
            labelTotal.Text = "TOTAL: $ " + beCompraMinorista.Total.ToString();
        }

        private void textBoxFiltrar_TextChanged(object sender, EventArgs e)
        {
            string filtro = textBoxFiltrar.Text.ToLower();

            dataGridViewProductos.DataSource = null;
            dataGridViewProductos.Rows.Clear();

            List<BEProducto> productosFiltrados = new List<BEProducto>();

            foreach (BEProducto producto in bllProducto.listaProductos())
            {
                if (producto is BEProductoIndividual)
                {
                    if (producto.ToString().ToLower().Contains(filtro))
                    {
                        productosFiltrados.Add(producto);
                    }
                }
                else if (producto is BEProductoCombo)
                {
                    if (producto.ToString().ToLower().Contains(filtro))
                    {
                        productosFiltrados.Add(producto);
                    }
                }
            }
            dataGridViewProductos.DataSource = productosFiltrados;
            dataGridViewProductos.Columns["Codigo"].Visible = false;
            dataGridViewProductos.Columns["Tipo"].Visible = false;
            dataGridViewProductos.Columns["Unidad"].HeaderText = "Unidades";
            if (dataGridViewProductos.Columns["nombre"] == null)
            {
                dataGridViewProductos.Columns.Add("nombre", "nombre");
            }
            dataGridViewProductos.Columns["nombre"].DisplayIndex = 0;
            dataGridViewProductos.CellFormatting += dataGridViewProductos_CellFormatting;
            dataGridViewProductos.Columns["precioMinorista"].Visible = false;
            dataGridViewProductos.Columns["precioMayorista"].HeaderText = "Precio";
            dataGridViewProductos.Columns["precioMayorista"].DefaultCellStyle.Format = "c2";
        }

        private void CompraMinorista_Load(object sender, EventArgs e)
        {
            if (dataGridViewProductos.Rows.Count > 0)
            {
                dataGridViewProductos.Rows[0].Selected = true;
            }
        }

        private void buttonSacar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewCarrito.Rows.Count > 0)
                {
                    beCarrito = (BECarrito)dataGridViewCarrito.CurrentRow.DataBoundItem;
                    if (beCarrito.Producto is BEProductoIndividual)
                    {
                        if (beCarrito.Cantidad > 1)
                        {
                            beCarrito.Cantidad--;
                            beCarrito.Total -= beCarrito.Producto.PrecioMinorista;
                            dataGridViewCarrito.DataSource = null;
                            dataGridViewCarrito.DataSource = beCompraMinorista.ListaCarrito;
                            dataGridViewCarrito.Columns["Total"].DefaultCellStyle.Format = "c2";
                            CalcularTotal();
                            var productoEnStock = listaStock.Find(p => p.Codigo == beProductoIndividual.Stock.Codigo);
                            productoEnStock.CantidadActual += beProductoIndividual.Unidad;
                        }
                        else
                        {
                            var productoEnCarrito = beCompraMinorista.ListaCarrito.Find(p => p.Producto.Codigo == beCarrito.Producto.Codigo);
                            beCompraMinorista.ListaCarrito.Remove(productoEnCarrito);
                            dataGridViewCarrito.DataSource = null;
                            dataGridViewCarrito.DataSource = beCompraMinorista.ListaCarrito;
                            dataGridViewCarrito.Columns["Total"].DefaultCellStyle.Format = "c2";
                            decimal total = 0;
                            if (beCompraMinorista.ListaCarrito.Count > 0)
                            {
                                foreach (var producto in beCompraMinorista.ListaCarrito)
                                {
                                    total += producto.Total;
                                    beCompraMinorista.Total = total;
                                }
                            }
                            else
                            {
                                beCompraMinorista.Total = 0;
                            }
                            labelTotal.Text = "TOTAL: $ " + beCompraMinorista.Total.ToString();
                            var productoEnStock = listaStock.Find(p => p.Codigo == beProductoIndividual.Stock.Codigo);
                            productoEnStock.CantidadActual += beProductoIndividual.Unidad;
                        }
                    }
                    else if (beCarrito.Producto is BEProductoCombo)
                    {
                        var beProductoCombo = (BEProductoCombo)beCarrito.Producto;
                        if (beCarrito.Cantidad > 1)
                        {
                            beCarrito.Cantidad--;
                            beCarrito.Total -= beCarrito.Producto.PrecioMinorista;
                            dataGridViewCarrito.DataSource = null;
                            dataGridViewCarrito.DataSource = beCompraMinorista.ListaCarrito;
                            dataGridViewCarrito.Columns["Total"].DefaultCellStyle.Format = "c2";
                            CalcularTotal();
                            foreach (BEStock item in beProductoCombo.ListaProductos)
                            {
                                var itemEnStock = listaStock.Find(p => p.Codigo == item.Codigo);
                                itemEnStock.CantidadActual ++ ;
                            }
                        }
                        else
                        {
                            var productoEnCarrito = beCompraMinorista.ListaCarrito.Find(p => p.Producto.Codigo == beCarrito.Producto.Codigo);
                            beCompraMinorista.ListaCarrito.Remove(productoEnCarrito);
                            dataGridViewCarrito.DataSource = null;
                            dataGridViewCarrito.DataSource = beCompraMinorista.ListaCarrito;
                            dataGridViewCarrito.Columns["Total"].DefaultCellStyle.Format = "c2";
                            decimal total = 0;
                            if (beCompraMinorista.ListaCarrito.Count > 0)
                            {
                                foreach (var producto in beCompraMinorista.ListaCarrito)
                                {
                                    total += producto.Total;
                                    beCompraMinorista.Total = total;
                                }
                            }
                            else
                            {
                                beCompraMinorista.Total = 0;
                            }
                            labelTotal.Text = "TOTAL: $ " + beCompraMinorista.Total.ToString();
                            foreach (BEStock item in beProductoCombo.ListaProductos)
                            {
                                var itemEnStock = listaStock.Find(p => p.Codigo == item.Codigo);
                                itemEnStock.CantidadActual++;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("El carrito esta vacio");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void buttonConfirmarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewCarrito.Rows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Desea confirmar la compra? El total es de $ " + beCompraMinorista.Total.ToString(), "Confirmar compra", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        beCompraMinorista.Fecha = DateTime.Now;
                        beCompraMinorista.Codigo = Guid.NewGuid().ToString();
                        bllCompraMinorista.GuardarCompraMinorista(beCompraMinorista);
                        bllStock.ActualizarStock(listaStock);
                        listaStock = bllStock.CargarStock();
                        PrintDialog printDialog = new PrintDialog();
                        if (printDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Obtener la impresora seleccionada por el usuario:
                            string printerName = printDialog.PrinterSettings.PrinterName;

                            // Usar la impresora seleccionada para imprimir la información de la compra:
                            ImprimirCompra(printerName);
                        }
                        limpiarCompraMinorista();
                    }
                }
                else
                {
                    MessageBox.Show("El carrito se encuentra vacio");
                }
            }
            catch (Exception)
            {
                throw;
            }
            
            

        }

        public void limpiarCompraMinorista()
        {
            beCompraMinorista.ListaCarrito.Clear();
            beCompraMinorista.Total = 0;
            dataGridViewCarrito.DataSource = null;
            dataGridViewCarrito.DataSource = beCompraMinorista.ListaCarrito;
            dataGridViewCarrito.Columns["Total"].DefaultCellStyle.Format = "c2";
            labelTotal.Text = "TOTAL: $ " + beCompraMinorista.Total.ToString();

        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Consolas", 12);

            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            float z = e.MarginBounds.Right;

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Far;

            string text = "MERAKI BEBIDAS";
            e.Graphics.DrawString(text, font, Brushes.Black, x, y);
            y += e.Graphics.MeasureString(text, font).Height;

            text = " ";
            e.Graphics.DrawString(text, font, Brushes.Black, x, y);
            y += e.Graphics.MeasureString(text, font).Height;


            text = "Código de compra: " + beCompraMinorista.Codigo;
            e.Graphics.DrawString(text, font, Brushes.Black, x, y);
            y += e.Graphics.MeasureString(text, font).Height;

            text = "Fecha: " + beCompraMinorista.Fecha.ToString("dd/MM/yyyy");
            e.Graphics.DrawString(text, font, Brushes.Black, x, y);
            y += e.Graphics.MeasureString(text, font).Height;

            text = " ";
            e.Graphics.DrawString(text, font, Brushes.Black, x, y);
            y += e.Graphics.MeasureString(text, font).Height;

            text = "-- ITEMS --";
            e.Graphics.DrawString(text, font, Brushes.Black, x, y);
            y += e.Graphics.MeasureString(text, font).Height;



            foreach (BECarrito compra in beCompraMinorista.ListaCarrito)
            {
                if (compra.Producto is BEProductoIndividual)
                {
                    text = compra.Producto.ToString() + " - " + compra.Cantidad + " Un.";
                    e.Graphics.DrawString(text, font, Brushes.Black, x, y);
                }
                else if (compra.Producto is BEProductoCombo)
                {
                    BEProductoCombo combo = (BEProductoCombo)compra.Producto;
                    text = combo.Nombre + " - " + compra.Cantidad + " Un.";
                    e.Graphics.DrawString(text, font, Brushes.Black, x, y);
                }
                text = "$" + (compra.Producto.PrecioMinorista * compra.Cantidad).ToString();
                e.Graphics.DrawString(text, font, Brushes.Black, z, y, stringFormat);

                y += e.Graphics.MeasureString(text, font).Height;

            }

            text = " ";
            e.Graphics.DrawString(text, font, Brushes.Black, x, y);
            y += e.Graphics.MeasureString(text, font).Height;


            text = "Total: $" + beCompraMinorista.Total.ToString();
            e.Graphics.DrawString(text, font, Brushes.Black, z, y, stringFormat);
            y += e.Graphics.MeasureString(text, font).Height;



        }

        private void ImprimirCompra(string printerName)
        {
            // Aquí es donde deberías implementar la lógica para imprimir la información de la compra.
            // Puedes usar la clase PrintDocument y asignarle el nombre de la impresora.
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrinterSettings.PrinterName = printerName;
            printDocument.PrintPage += PrintDocument_PrintPage;
            printDocument.Print();
        }

        private void dataGridViewProductos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewProductos.Columns["nombre"].Index)
            {
                var producto = dataGridViewProductos.Rows[e.RowIndex].DataBoundItem;
                if (producto != null)
                {
                    // Determina qué propiedad de la clase mostrar
                    if (producto is BEProductoIndividual)
                    {
                        BEProductoIndividual c1 = (BEProductoIndividual)producto;
                        e.Value = c1.ToString();
                    }
                    else if (producto is BEProductoCombo)
                    {
                        BEProductoCombo c2 = (BEProductoCombo)producto;
                        e.Value = c2.Nombre;
                    }
                }
            }
        }

        

        
    }
}
