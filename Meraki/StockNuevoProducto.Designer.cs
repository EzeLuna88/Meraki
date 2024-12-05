namespace Meraki
{
    partial class StockNuevoProducto
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
            this.comboBoxTipoMedida = new System.Windows.Forms.ComboBox();
            this.textBoxMedida = new System.Windows.Forms.TextBox();
            this.textBoxNombre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCantidad = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.iconButtonCargar = new FontAwesome.Sharp.IconButton();
            this.iconButtonCancelar = new FontAwesome.Sharp.IconButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxTipoMedida
            // 
            this.comboBoxTipoMedida.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.comboBoxTipoMedida.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTipoMedida.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxTipoMedida.FormattingEnabled = true;
            this.comboBoxTipoMedida.Location = new System.Drawing.Point(367, 46);
            this.comboBoxTipoMedida.Name = "comboBoxTipoMedida";
            this.comboBoxTipoMedida.Size = new System.Drawing.Size(82, 23);
            this.comboBoxTipoMedida.TabIndex = 3;
            // 
            // textBoxMedida
            // 
            this.textBoxMedida.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxMedida.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxMedida.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMedida.Location = new System.Drawing.Point(169, 47);
            this.textBoxMedida.Name = "textBoxMedida";
            this.textBoxMedida.Size = new System.Drawing.Size(192, 22);
            this.textBoxMedida.TabIndex = 2;
            this.textBoxMedida.TextChanged += new System.EventHandler(this.textBoxMedida_TextChanged);
            this.textBoxMedida.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxMedida_KeyPress);
            // 
            // textBoxNombre
            // 
            this.textBoxNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxNombre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxNombre.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNombre.Location = new System.Drawing.Point(169, 19);
            this.textBoxNombre.Name = "textBoxNombre";
            this.textBoxNombre.Size = new System.Drawing.Size(280, 22);
            this.textBoxNombre.TabIndex = 1;
            this.textBoxNombre.TextChanged += new System.EventHandler(this.textBoxNombre_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 21);
            this.label3.TabIndex = 33;
            this.label3.Text = "Medida:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 21);
            this.label1.TabIndex = 32;
            this.label1.Text = "Nombre:";
            // 
            // textBoxCantidad
            // 
            this.textBoxCantidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxCantidad.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCantidad.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCantidad.Location = new System.Drawing.Point(169, 75);
            this.textBoxCantidad.Name = "textBoxCantidad";
            this.textBoxCantidad.Size = new System.Drawing.Size(280, 22);
            this.textBoxCantidad.TabIndex = 4;
            this.textBoxCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCantidad_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 21);
            this.label2.TabIndex = 35;
            this.label2.Text = "Cantidad a agregar:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(91)))), ((int)(((byte)(122)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBoxCantidad);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBoxTipoMedida);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBoxNombre);
            this.panel1.Controls.Add(this.textBoxMedida);
            this.panel1.Location = new System.Drawing.Point(12, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(470, 123);
            this.panel1.TabIndex = 71;
            // 
            // iconButtonCargar
            // 
            this.iconButtonCargar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButtonCargar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonCargar.FlatAppearance.BorderSize = 0;
            this.iconButtonCargar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonCargar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonCargar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonCargar.IconChar = FontAwesome.Sharp.IconChar.Upload;
            this.iconButtonCargar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonCargar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonCargar.IconSize = 30;
            this.iconButtonCargar.Location = new System.Drawing.Point(133, 151);
            this.iconButtonCargar.Name = "iconButtonCargar";
            this.iconButtonCargar.Size = new System.Drawing.Size(110, 40);
            this.iconButtonCargar.TabIndex = 74;
            this.iconButtonCargar.Text = "Cargar";
            this.iconButtonCargar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonCargar.UseVisualStyleBackColor = false;
            this.iconButtonCargar.Click += new System.EventHandler(this.iconButtonCargar_Click);
            // 
            // iconButtonCancelar
            // 
            this.iconButtonCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButtonCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonCancelar.FlatAppearance.BorderSize = 0;
            this.iconButtonCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonCancelar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonCancelar.IconChar = FontAwesome.Sharp.IconChar.Ban;
            this.iconButtonCancelar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonCancelar.IconSize = 30;
            this.iconButtonCancelar.Location = new System.Drawing.Point(249, 151);
            this.iconButtonCancelar.Name = "iconButtonCancelar";
            this.iconButtonCancelar.Size = new System.Drawing.Size(110, 40);
            this.iconButtonCancelar.TabIndex = 73;
            this.iconButtonCancelar.Text = "Cancelar";
            this.iconButtonCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonCancelar.UseVisualStyleBackColor = false;
            this.iconButtonCancelar.Click += new System.EventHandler(this.iconButtonCancelar_Click);
            // 
            // StockNuevoProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.ClientSize = new System.Drawing.Size(494, 203);
            this.Controls.Add(this.iconButtonCargar);
            this.Controls.Add(this.iconButtonCancelar);
            this.Controls.Add(this.panel1);
            this.Name = "StockNuevoProducto";
            this.Text = "Nuevo Producto a Stock";
            this.Load += new System.EventHandler(this.StockNuevoProducto_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StockNuevoProducto_MouseDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxTipoMedida;
        private System.Windows.Forms.TextBox textBoxMedida;
        public System.Windows.Forms.TextBox textBoxNombre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCantidad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton iconButtonCargar;
        private FontAwesome.Sharp.IconButton iconButtonCancelar;
    }
}