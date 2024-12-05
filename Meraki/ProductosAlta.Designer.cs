namespace Meraki
{
    partial class ProductosAlta
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
            this.textBoxPrecioMayorista = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPrecioMinorista = new System.Windows.Forms.TextBox();
            this.textBoxUnidades = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewStock = new System.Windows.Forms.DataGridView();
            this.textBoxFiltrar = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.iconButtonCancelar = new FontAwesome.Sharp.IconButton();
            this.iconButtonAlta = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStock)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxPrecioMayorista
            // 
            this.textBoxPrecioMayorista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPrecioMayorista.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxPrecioMayorista.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPrecioMayorista.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPrecioMayorista.Location = new System.Drawing.Point(140, 40);
            this.textBoxPrecioMayorista.Name = "textBoxPrecioMayorista";
            this.textBoxPrecioMayorista.Size = new System.Drawing.Size(302, 22);
            this.textBoxPrecioMayorista.TabIndex = 3;
            this.textBoxPrecioMayorista.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPrecioMayorista_KeyPress);
            this.textBoxPrecioMayorista.Leave += new System.EventHandler(this.textBoxPrecioMayorista_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 21);
            this.label5.TabIndex = 28;
            this.label5.Text = "Precio Minorista:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 21);
            this.label4.TabIndex = 27;
            this.label4.Text = "Precio Mayorista:";
            // 
            // textBoxPrecioMinorista
            // 
            this.textBoxPrecioMinorista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPrecioMinorista.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxPrecioMinorista.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPrecioMinorista.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPrecioMinorista.Location = new System.Drawing.Point(140, 68);
            this.textBoxPrecioMinorista.Name = "textBoxPrecioMinorista";
            this.textBoxPrecioMinorista.Size = new System.Drawing.Size(302, 22);
            this.textBoxPrecioMinorista.TabIndex = 4;
            this.textBoxPrecioMinorista.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPrecioMinorista_KeyPress);
            this.textBoxPrecioMinorista.Leave += new System.EventHandler(this.textBoxPrecioMinorista_Leave);
            // 
            // textBoxUnidades
            // 
            this.textBoxUnidades.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUnidades.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxUnidades.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxUnidades.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUnidades.Location = new System.Drawing.Point(140, 12);
            this.textBoxUnidades.Name = "textBoxUnidades";
            this.textBoxUnidades.Size = new System.Drawing.Size(302, 22);
            this.textBoxUnidades.TabIndex = 2;
            this.textBoxUnidades.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxUnidades_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(56, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 21);
            this.label1.TabIndex = 31;
            this.label1.Text = "Unidades:";
            // 
            // dataGridViewStock
            // 
            this.dataGridViewStock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewStock.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.dataGridViewStock.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStock.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.dataGridViewStock.Location = new System.Drawing.Point(12, 11);
            this.dataGridViewStock.MultiSelect = false;
            this.dataGridViewStock.Name = "dataGridViewStock";
            this.dataGridViewStock.ReadOnly = true;
            this.dataGridViewStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewStock.Size = new System.Drawing.Size(456, 302);
            this.dataGridViewStock.TabIndex = 44;
            this.dataGridViewStock.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewStock_CellFormatting_1);
            // 
            // textBoxFiltrar
            // 
            this.textBoxFiltrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFiltrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxFiltrar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxFiltrar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFiltrar.Location = new System.Drawing.Point(59, 319);
            this.textBoxFiltrar.Name = "textBoxFiltrar";
            this.textBoxFiltrar.Size = new System.Drawing.Size(409, 18);
            this.textBoxFiltrar.TabIndex = 47;
            this.textBoxFiltrar.TextChanged += new System.EventHandler(this.textBoxFiltrar_TextChanged_1);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(9, 319);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 17);
            this.label7.TabIndex = 46;
            this.label7.Text = "Filtrar:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(91)))), ((int)(((byte)(122)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBoxPrecioMayorista);
            this.panel1.Controls.Add(this.textBoxUnidades);
            this.panel1.Controls.Add(this.textBoxPrecioMinorista);
            this.panel1.Location = new System.Drawing.Point(12, 343);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(456, 106);
            this.panel1.TabIndex = 52;
            // 
            // iconButtonCancelar
            // 
            this.iconButtonCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButtonCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonCancelar.FlatAppearance.BorderSize = 0;
            this.iconButtonCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonCancelar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonCancelar.IconChar = FontAwesome.Sharp.IconChar.CircleXmark;
            this.iconButtonCancelar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonCancelar.IconSize = 30;
            this.iconButtonCancelar.Location = new System.Drawing.Point(133, 455);
            this.iconButtonCancelar.Name = "iconButtonCancelar";
            this.iconButtonCancelar.Size = new System.Drawing.Size(115, 40);
            this.iconButtonCancelar.TabIndex = 54;
            this.iconButtonCancelar.Text = "Cancelar";
            this.iconButtonCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonCancelar.UseVisualStyleBackColor = false;
            this.iconButtonCancelar.Click += new System.EventHandler(this.iconButtonCancelar_Click);
            // 
            // iconButtonAlta
            // 
            this.iconButtonAlta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButtonAlta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonAlta.FlatAppearance.BorderSize = 0;
            this.iconButtonAlta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonAlta.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonAlta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonAlta.IconChar = FontAwesome.Sharp.IconChar.CircleChevronUp;
            this.iconButtonAlta.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonAlta.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonAlta.IconSize = 30;
            this.iconButtonAlta.Location = new System.Drawing.Point(12, 455);
            this.iconButtonAlta.Name = "iconButtonAlta";
            this.iconButtonAlta.Size = new System.Drawing.Size(115, 40);
            this.iconButtonAlta.TabIndex = 53;
            this.iconButtonAlta.Text = "Dar de alta";
            this.iconButtonAlta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonAlta.UseVisualStyleBackColor = false;
            this.iconButtonAlta.Click += new System.EventHandler(this.iconButtonAlta_Click);
            // 
            // ProductosAlta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.ClientSize = new System.Drawing.Size(480, 501);
            this.Controls.Add(this.iconButtonCancelar);
            this.Controls.Add(this.iconButtonAlta);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBoxFiltrar);
            this.Controls.Add(this.dataGridViewStock);
            this.Controls.Add(this.label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ProductosAlta";
            this.Text = "ProductosAlta";
            this.Load += new System.EventHandler(this.ProductosAlta_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ProductosAlta_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStock)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxPrecioMayorista;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPrecioMinorista;
        private System.Windows.Forms.TextBox textBoxUnidades;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewStock;
        private System.Windows.Forms.TextBox textBoxFiltrar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton iconButtonCancelar;
        private FontAwesome.Sharp.IconButton iconButtonAlta;
    }
}