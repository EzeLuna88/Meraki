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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Meraki
{
    public partial class Productos : Form
    {
        BEProductoIndividual beProductoIndividual;
        BEProductoCombo beProductoCombo;
        BLLProducto bllProducto;
        BLLProductoCombo bllProductoCombo;
        public Productos()
        {
            beProductoIndividual = new BEProductoIndividual();
            beProductoCombo = new BEProductoCombo();
            bllProducto = new BLLProducto();
            bllProductoCombo = new BLLProductoCombo();
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
            dataGridViewProductos.Columns["precioMayorista"].HeaderText = "Precio Mayorista";
            dataGridViewProductos.Columns["precioMinorista"].HeaderText = "Precio Minorista";
            dataGridViewProductos.Columns["precioMayorista"].DefaultCellStyle.Format = "c2";
            dataGridViewProductos.Columns["precioMinorista"].DefaultCellStyle.Format = "c2";
            if (dataGridViewProductos.Columns["nombre"] == null)
            {
                dataGridViewProductos.Columns.Add("nombre", "nombre");
            }
                dataGridViewProductos.Columns["nombre"].DisplayIndex = 0;
            dataGridViewProductos.CellFormatting += dataGridViewProductos_CellFormatting;

        }

        private void buttonAlta_Click(object sender, EventArgs e)
        {
            ProductosAlta productosAlta = new ProductosAlta();
            productosAlta.ShowDialog();
            CargarDataGrid();
        }
        
        private void dataGridViewProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                if (rowIndex >= 0)
                {
                    DataGridViewRow selectedRow = dataGridViewProductos.Rows[rowIndex];
                    if (selectedRow.Cells["Tipo"].Value.ToString() == "individual")
                    {
                        beProductoIndividual = (BEProductoIndividual)dataGridViewProductos.CurrentRow.DataBoundItem;
                        labelNombre.Text = beProductoIndividual.Stock.Nombre;
                        labelMedida.Text = beProductoIndividual.Stock.Medida.ToString() + " " + beProductoIndividual.Stock.TipoMedida;
                        labelUnidades.Text = beProductoIndividual.Unidad.ToString();
                        labelPrecioMayorista.Text = beProductoIndividual.PrecioMayorista.ToString();
                        labelPrecioMinorista.Text = beProductoIndividual.PrecioMinorista.ToString();
                    }
                    else
                    {
                        beProductoCombo = (BEProductoCombo)dataGridViewProductos.CurrentRow.DataBoundItem;
                        labelNombre.Text = beProductoCombo.Nombre;
                        labelMedida.Text = string.Empty;
                        labelUnidades.Text = beProductoCombo.Unidad.ToString();
                        labelPrecioMayorista.Text = beProductoCombo.PrecioMayorista.ToString();
                        labelPrecioMinorista.Text = beProductoCombo.PrecioMinorista.ToString();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            
           
        }
        
        public void Limpiar()
        {
            try
            {
                labelNombre.Text = string.Empty;
                labelUnidades.Text = string.Empty;
                labelMedida.Text = string.Empty;
                labelPrecioMayorista.Text = string.Empty;
                labelPrecioMinorista.Text = string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        private void Productos_Load(object sender, EventArgs e)
        {
            if (dataGridViewProductos.Rows.Count > 0)
            {
                dataGridViewProductos.Rows[0].Selected = true;
            }
        }
        
        private void textBoxFiltrar_TextChanged(object sender, EventArgs e)
        {
            string textoABuscar = textBoxFiltrar.Text.ToLower();
          //  var tablaFiltrada = bllProducto.listaProductos().Where(row => row.Stock.Nombre.ToLower().Contains(textoABuscar));
          //  dataGridViewProductos.DataSource = tablaFiltrada.ToList();
        }

        private void buttonBaja_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewProductos.SelectedRows.Count > 0)
                {
                    var producto = dataGridViewProductos.CurrentRow.DataBoundItem;
                    if (producto is BEProductoIndividual)
                    {
                        beProductoIndividual = (BEProductoIndividual)dataGridViewProductos.CurrentRow.DataBoundItem;
                        DialogResult confirmacion;
                        confirmacion = MessageBox.Show("Confirmar baja de producto", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirmacion == DialogResult.Yes)
                        { bllProducto.BorrarProducto(beProductoIndividual); }
                        CargarDataGrid();
                        Limpiar();
                        dataGridViewProductos.Rows[0].Selected = true;
                    }
                    else if (producto is BEProductoCombo)
                    {
                        beProductoCombo = (BEProductoCombo)dataGridViewProductos.CurrentRow.DataBoundItem;
                        DialogResult confirmacion;
                        confirmacion = MessageBox.Show("Confirmar baja de producto", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirmacion == DialogResult.Yes)
                        { bllProductoCombo.BorrarProducto(beProductoCombo); }
                        CargarDataGrid();
                        Limpiar();
                        dataGridViewProductos.Rows[0].Selected = true;
                    }
                }
                else
                { MessageBox.Show("Debe seleccionar un cliente"); }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void buttonModificar_Click_1(object sender, EventArgs e)
        {
            var producto = dataGridViewProductos.CurrentRow.DataBoundItem;
            if (producto != null)
            {
                if (producto is BEProductoIndividual)
                {
                    beProductoIndividual = (BEProductoIndividual)producto;
                    ProductosModificar modificar = new ProductosModificar();
                    modificar.textBoxCodigo.Text = beProductoIndividual.Codigo;
                    modificar.textBoxNombre.Text = beProductoIndividual.Stock.ToString();
                    modificar.textBoxUnidades.Text = beProductoIndividual.Unidad.ToString();

                    modificar.textBoxPrecioMayorista.Text = beProductoIndividual.PrecioMayorista.ToString();
                    modificar.textBoxPrecioMinorista.Text = beProductoIndividual.PrecioMinorista.ToString();

                    modificar.dataGridViewCombo.Visible = false;
                    modificar.ShowDialog();
                    CargarDataGrid();
                    dataGridViewProductos.Rows[0].Selected = true;
                }
                else if (producto is BEProductoCombo)
                {
                    beProductoCombo = (BEProductoCombo)producto;
                    ProductosModificar modificar = new ProductosModificar();
                    modificar.textBoxCodigo.Text = beProductoCombo.Codigo;
                    modificar.textBoxNombre.Text = beProductoCombo.Nombre;
                    modificar.textBoxUnidades.Text = beProductoCombo.Unidad.ToString();
                    modificar.textBoxNombre.Enabled = true;
                    modificar.textBoxPrecioMayorista.Text = beProductoCombo.PrecioMayorista.ToString();
                    modificar.textBoxPrecioMinorista.Text = beProductoCombo.PrecioMinorista.ToString();
                    modificar.dataGridViewCombo.DataSource = beProductoCombo.ListaProductos;
                    modificar.dataGridViewCombo.Columns["Codigo"].Visible = false;
                    modificar.dataGridViewCombo.Columns["CantidadActual"].Visible = false;
                    modificar.dataGridViewCombo.Columns["CantidadIngresada"].Visible = false;
                    modificar.dataGridViewCombo.Columns["FechaIngreso"].Visible = false;


                    modificar.ShowDialog();
                    CargarDataGrid();
                    dataGridViewProductos.Rows[0].Selected = true;
                }
            }
            else
            { MessageBox.Show("debe seleccionar un producto"); }
        }

        private void buttonCrearCombo_Click(object sender, EventArgs e)
        {
            ProductosCrearCombo productosCrearCombo = new ProductosCrearCombo();
            productosCrearCombo.ShowDialog();
            CargarDataGrid();
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
