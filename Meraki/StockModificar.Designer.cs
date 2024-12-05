namespace Meraki
{
    partial class StockModificar
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
            this.textBoxCodigo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxMedida = new System.Windows.Forms.TextBox();
            this.textBoxNombre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.iconButtonCancelar = new FontAwesome.Sharp.IconButton();
            this.iconButtonModificar = new FontAwesome.Sharp.IconButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxTipoMedida
            // 
            this.comboBoxTipoMedida.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.comboBoxTipoMedida.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTipoMedida.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxTipoMedida.FormattingEnabled = true;
            this.comboBoxTipoMedida.IntegralHeight = false;
            this.comboBoxTipoMedida.Location = new System.Drawing.Point(285, 67);
            this.comboBoxTipoMedida.Name = "comboBoxTipoMedida";
            this.comboBoxTipoMedida.Size = new System.Drawing.Size(82, 23);
            this.comboBoxTipoMedida.TabIndex = 4;
            // 
            // textBoxCodigo
            // 
            this.textBoxCodigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxCodigo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCodigo.Enabled = false;
            this.textBoxCodigo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCodigo.Location = new System.Drawing.Point(87, 11);
            this.textBoxCodigo.Name = "textBoxCodigo";
            this.textBoxCodigo.Size = new System.Drawing.Size(280, 22);
            this.textBoxCodigo.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(16, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 21);
            this.label7.TabIndex = 69;
            this.label7.Text = "Codigo:";
            // 
            // textBoxMedida
            // 
            this.textBoxMedida.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxMedida.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxMedida.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMedida.Location = new System.Drawing.Point(87, 67);
            this.textBoxMedida.Name = "textBoxMedida";
            this.textBoxMedida.Size = new System.Drawing.Size(192, 22);
            this.textBoxMedida.TabIndex = 3;
            // 
            // textBoxNombre
            // 
            this.textBoxNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxNombre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxNombre.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNombre.Location = new System.Drawing.Point(87, 39);
            this.textBoxNombre.Name = "textBoxNombre";
            this.textBoxNombre.Size = new System.Drawing.Size(280, 22);
            this.textBoxNombre.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 21);
            this.label3.TabIndex = 60;
            this.label3.Text = "Medida:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 21);
            this.label1.TabIndex = 58;
            this.label1.Text = "Nombre:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(91)))), ((int)(((byte)(122)))));
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.comboBoxTipoMedida);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBoxCodigo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBoxNombre);
            this.panel1.Controls.Add(this.textBoxMedida);
            this.panel1.Location = new System.Drawing.Point(12, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 107);
            this.panel1.TabIndex = 70;
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
            this.iconButtonCancelar.Location = new System.Drawing.Point(209, 134);
            this.iconButtonCancelar.Name = "iconButtonCancelar";
            this.iconButtonCancelar.Size = new System.Drawing.Size(110, 40);
            this.iconButtonCancelar.TabIndex = 71;
            this.iconButtonCancelar.Text = "Cancelar";
            this.iconButtonCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonCancelar.UseVisualStyleBackColor = false;
            this.iconButtonCancelar.Click += new System.EventHandler(this.iconButtonCancelar_Click);
            // 
            // iconButtonModificar
            // 
            this.iconButtonModificar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButtonModificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonModificar.FlatAppearance.BorderSize = 0;
            this.iconButtonModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonModificar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonModificar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonModificar.IconChar = FontAwesome.Sharp.IconChar.Pen;
            this.iconButtonModificar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonModificar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonModificar.IconSize = 30;
            this.iconButtonModificar.Location = new System.Drawing.Point(93, 134);
            this.iconButtonModificar.Name = "iconButtonModificar";
            this.iconButtonModificar.Size = new System.Drawing.Size(110, 40);
            this.iconButtonModificar.TabIndex = 72;
            this.iconButtonModificar.Text = "Modificar";
            this.iconButtonModificar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonModificar.UseVisualStyleBackColor = false;
            this.iconButtonModificar.Click += new System.EventHandler(this.iconButtonModificar_Click);
            // 
            // StockModificar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.ClientSize = new System.Drawing.Size(418, 186);
            this.Controls.Add(this.iconButtonModificar);
            this.Controls.Add(this.iconButtonCancelar);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "StockModificar";
            this.Text = "StockModificar";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StockModificar_MouseDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox comboBoxTipoMedida;
        public System.Windows.Forms.TextBox textBoxCodigo;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox textBoxMedida;
        public System.Windows.Forms.TextBox textBoxNombre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton iconButtonCancelar;
        private FontAwesome.Sharp.IconButton iconButtonModificar;
    }
}