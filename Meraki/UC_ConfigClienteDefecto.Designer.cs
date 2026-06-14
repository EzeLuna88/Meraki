namespace Meraki
{
    partial class UC_ConfigClienteDefecto
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelClienteActual = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxFiltrarClientes = new System.Windows.Forms.TextBox();
            this.iconButtonEstablecer = new FontAwesome.Sharp.IconButton();
            this.dataGridViewClientes = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClientes)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.labelClienteActual);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(15, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(721, 68);
            this.panel1.TabIndex = 0;
            // 
            // labelClienteActual
            // 
            this.labelClienteActual.AutoSize = true;
            this.labelClienteActual.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClienteActual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(15)))), ((int)(((byte)(28)))));
            this.labelClienteActual.Location = new System.Drawing.Point(6, 25);
            this.labelClienteActual.Name = "labelClienteActual";
            this.labelClienteActual.Size = new System.Drawing.Size(96, 37);
            this.labelClienteActual.TabIndex = 1;
            this.labelClienteActual.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "CLIENTE PREDETERMINADO ACTUAL:";
            // 
            // textBoxFiltrarClientes
            // 
            this.textBoxFiltrarClientes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFiltrarClientes.Location = new System.Drawing.Point(13, 15);
            this.textBoxFiltrarClientes.Name = "textBoxFiltrarClientes";
            this.textBoxFiltrarClientes.Size = new System.Drawing.Size(251, 29);
            this.textBoxFiltrarClientes.TabIndex = 1;
            this.textBoxFiltrarClientes.TextChanged += new System.EventHandler(this.textBoxFiltrarClientes_TextChanged);
            // 
            // iconButtonEstablecer
            // 
            this.iconButtonEstablecer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.iconButtonEstablecer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonEstablecer.FlatAppearance.BorderSize = 0;
            this.iconButtonEstablecer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonEstablecer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonEstablecer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonEstablecer.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.iconButtonEstablecer.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonEstablecer.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonEstablecer.IconSize = 30;
            this.iconButtonEstablecer.Location = new System.Drawing.Point(278, 10);
            this.iconButtonEstablecer.Name = "iconButtonEstablecer";
            this.iconButtonEstablecer.Size = new System.Drawing.Size(228, 41);
            this.iconButtonEstablecer.TabIndex = 54;
            this.iconButtonEstablecer.Text = "Establecer como predeterminado";
            this.iconButtonEstablecer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonEstablecer.UseVisualStyleBackColor = false;
            this.iconButtonEstablecer.Click += new System.EventHandler(this.iconButtonEstablecer_Click);
            // 
            // dataGridViewClientes
            // 
            this.dataGridViewClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewClientes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.dataGridViewClientes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewClientes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(8)))), ((int)(((byte)(21)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewClientes.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.dataGridViewClientes.Location = new System.Drawing.Point(18, 161);
            this.dataGridViewClientes.MultiSelect = false;
            this.dataGridViewClientes.Name = "dataGridViewClientes";
            this.dataGridViewClientes.ReadOnly = true;
            this.dataGridViewClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewClientes.Size = new System.Drawing.Size(715, 219);
            this.dataGridViewClientes.TabIndex = 55;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBoxFiltrarClientes);
            this.panel2.Controls.Add(this.iconButtonEstablecer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(15, 83);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(721, 60);
            this.panel2.TabIndex = 56;
            // 
            // UC_ConfigClienteDefecto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dataGridViewClientes);
            this.Controls.Add(this.panel1);
            this.Name = "UC_ConfigClienteDefecto";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.Size = new System.Drawing.Size(751, 398);
            this.Load += new System.EventHandler(this.UC_ConfigClienteDefecto_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClientes)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelClienteActual;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFiltrarClientes;
        private FontAwesome.Sharp.IconButton iconButtonEstablecer;
        private System.Windows.Forms.DataGridView dataGridViewClientes;
        private System.Windows.Forms.Panel panel2;
    }
}
