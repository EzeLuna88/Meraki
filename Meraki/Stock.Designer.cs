namespace Meraki
{
    partial class Stock
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewStock = new System.Windows.Forms.DataGridView();
            this.buttonAgregarStock = new System.Windows.Forms.Button();
            this.buttonNuevoProducto = new System.Windows.Forms.Button();
            this.textBoxFiltrar = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonBorrarProducto = new System.Windows.Forms.Button();
            this.buttonModificar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStock)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewStock
            // 
            this.dataGridViewStock.AllowUserToResizeColumns = false;
            this.dataGridViewStock.AllowUserToResizeRows = false;
            this.dataGridViewStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStock.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewStock.MultiSelect = false;
            this.dataGridViewStock.Name = "dataGridViewStock";
            this.dataGridViewStock.ReadOnly = true;
            this.dataGridViewStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewStock.Size = new System.Drawing.Size(558, 598);
            this.dataGridViewStock.TabIndex = 17;
            this.dataGridViewStock.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewStock_CellFormatting);
            // 
            // buttonAgregarStock
            // 
            this.buttonAgregarStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.buttonAgregarStock.ForeColor = System.Drawing.Color.Blue;
            this.buttonAgregarStock.Location = new System.Drawing.Point(576, 12);
            this.buttonAgregarStock.Name = "buttonAgregarStock";
            this.buttonAgregarStock.Size = new System.Drawing.Size(103, 61);
            this.buttonAgregarStock.TabIndex = 30;
            this.buttonAgregarStock.Text = "Agregar stock";
            this.buttonAgregarStock.UseVisualStyleBackColor = true;
            this.buttonAgregarStock.Click += new System.EventHandler(this.buttonAgregarStock_Click);
            // 
            // buttonNuevoProducto
            // 
            this.buttonNuevoProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.buttonNuevoProducto.ForeColor = System.Drawing.Color.Blue;
            this.buttonNuevoProducto.Location = new System.Drawing.Point(576, 79);
            this.buttonNuevoProducto.Name = "buttonNuevoProducto";
            this.buttonNuevoProducto.Size = new System.Drawing.Size(103, 61);
            this.buttonNuevoProducto.TabIndex = 31;
            this.buttonNuevoProducto.Text = "Nuevo Producto";
            this.buttonNuevoProducto.UseVisualStyleBackColor = true;
            this.buttonNuevoProducto.Click += new System.EventHandler(this.buttonNuevoProducto_Click);
            // 
            // textBoxFiltrar
            // 
            this.textBoxFiltrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFiltrar.Location = new System.Drawing.Point(79, 622);
            this.textBoxFiltrar.Name = "textBoxFiltrar";
            this.textBoxFiltrar.Size = new System.Drawing.Size(482, 26);
            this.textBoxFiltrar.TabIndex = 35;
            this.textBoxFiltrar.TextChanged += new System.EventHandler(this.textBoxFiltrar_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 625);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 20);
            this.label6.TabIndex = 34;
            this.label6.Text = "Filtrar:";
            // 
            // buttonBorrarProducto
            // 
            this.buttonBorrarProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.buttonBorrarProducto.ForeColor = System.Drawing.Color.Red;
            this.buttonBorrarProducto.Location = new System.Drawing.Point(685, 79);
            this.buttonBorrarProducto.Name = "buttonBorrarProducto";
            this.buttonBorrarProducto.Size = new System.Drawing.Size(103, 61);
            this.buttonBorrarProducto.TabIndex = 36;
            this.buttonBorrarProducto.Text = "Borrar Producto";
            this.buttonBorrarProducto.UseVisualStyleBackColor = true;
            this.buttonBorrarProducto.Click += new System.EventHandler(this.buttonBorrarProducto_Click);
            // 
            // buttonModificar
            // 
            this.buttonModificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.buttonModificar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonModificar.Location = new System.Drawing.Point(794, 79);
            this.buttonModificar.Name = "buttonModificar";
            this.buttonModificar.Size = new System.Drawing.Size(103, 61);
            this.buttonModificar.TabIndex = 37;
            this.buttonModificar.Text = "Modificar Producto";
            this.buttonModificar.UseVisualStyleBackColor = true;
            this.buttonModificar.Click += new System.EventHandler(this.buttonModificar_Click);
            // 
            // Stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 661);
            this.Controls.Add(this.buttonModificar);
            this.Controls.Add(this.buttonBorrarProducto);
            this.Controls.Add(this.textBoxFiltrar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonNuevoProducto);
            this.Controls.Add(this.buttonAgregarStock);
            this.Controls.Add(this.dataGridViewStock);
            this.Name = "Stock";
            this.Text = "Stock";
            this.Load += new System.EventHandler(this.Stock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewStock;
        private System.Windows.Forms.Button buttonAgregarStock;
        private System.Windows.Forms.Button buttonNuevoProducto;
        private System.Windows.Forms.TextBox textBoxFiltrar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonBorrarProducto;
        private System.Windows.Forms.Button buttonModificar;
    }
}