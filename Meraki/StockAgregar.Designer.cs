namespace Meraki
{
    partial class StockAgregar
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
            this.labelCodigoEscondido = new System.Windows.Forms.Label();
            this.labelProducto = new System.Windows.Forms.Label();
            this.labelPacksCajas = new System.Windows.Forms.Label();
            this.labelUnidadesXPackCaja = new System.Windows.Forms.Label();
            this.textBoxUnidad = new System.Windows.Forms.TextBox();
            this.textBoxPacks = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.iconButtonAgregarStock = new FontAwesome.Sharp.IconButton();
            this.iconButtonCancelar = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.maskedTextBoxFechaDeVencimiento = new System.Windows.Forms.MaskedTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelCodigoEscondido
            // 
            this.labelCodigoEscondido.AutoSize = true;
            this.labelCodigoEscondido.Location = new System.Drawing.Point(12, 9);
            this.labelCodigoEscondido.Name = "labelCodigoEscondido";
            this.labelCodigoEscondido.Size = new System.Drawing.Size(0, 13);
            this.labelCodigoEscondido.TabIndex = 0;
            this.labelCodigoEscondido.Visible = false;
            // 
            // labelProducto
            // 
            this.labelProducto.AutoSize = true;
            this.labelProducto.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProducto.Location = new System.Drawing.Point(13, 15);
            this.labelProducto.Name = "labelProducto";
            this.labelProducto.Size = new System.Drawing.Size(52, 21);
            this.labelProducto.TabIndex = 1;
            this.labelProducto.Text = "label1";
            // 
            // labelPacksCajas
            // 
            this.labelPacksCajas.AutoSize = true;
            this.labelPacksCajas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPacksCajas.Location = new System.Drawing.Point(12, 41);
            this.labelPacksCajas.Name = "labelPacksCajas";
            this.labelPacksCajas.Size = new System.Drawing.Size(168, 21);
            this.labelPacksCajas.TabIndex = 2;
            this.labelPacksCajas.Text = "Cantidad Packs / Cajas:";
            // 
            // labelUnidadesXPackCaja
            // 
            this.labelUnidadesXPackCaja.AutoSize = true;
            this.labelUnidadesXPackCaja.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUnidadesXPackCaja.Location = new System.Drawing.Point(12, 69);
            this.labelUnidadesXPackCaja.Name = "labelUnidadesXPackCaja";
            this.labelUnidadesXPackCaja.Size = new System.Drawing.Size(168, 21);
            this.labelUnidadesXPackCaja.TabIndex = 3;
            this.labelUnidadesXPackCaja.Text = "Unidades x Pack / Caja:";
            // 
            // textBoxUnidad
            // 
            this.textBoxUnidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxUnidad.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxUnidad.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUnidad.Location = new System.Drawing.Point(184, 69);
            this.textBoxUnidad.Name = "textBoxUnidad";
            this.textBoxUnidad.Size = new System.Drawing.Size(110, 22);
            this.textBoxUnidad.TabIndex = 2;
            this.textBoxUnidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxUnidad_KeyPress);
            // 
            // textBoxPacks
            // 
            this.textBoxPacks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxPacks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPacks.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPacks.Location = new System.Drawing.Point(184, 41);
            this.textBoxPacks.Name = "textBoxPacks";
            this.textBoxPacks.Size = new System.Drawing.Size(110, 22);
            this.textBoxPacks.TabIndex = 1;
            this.textBoxPacks.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPacks_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(91)))), ((int)(((byte)(122)))));
            this.panel1.Controls.Add(this.maskedTextBoxFechaDeVencimiento);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labelProducto);
            this.panel1.Controls.Add(this.labelPacksCajas);
            this.panel1.Controls.Add(this.labelUnidadesXPackCaja);
            this.panel1.Controls.Add(this.textBoxPacks);
            this.panel1.Controls.Add(this.textBoxUnidad);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(373, 134);
            this.panel1.TabIndex = 5;
            // 
            // iconButtonAgregarStock
            // 
            this.iconButtonAgregarStock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButtonAgregarStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonAgregarStock.FlatAppearance.BorderSize = 0;
            this.iconButtonAgregarStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonAgregarStock.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonAgregarStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonAgregarStock.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.iconButtonAgregarStock.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonAgregarStock.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonAgregarStock.IconSize = 30;
            this.iconButtonAgregarStock.Location = new System.Drawing.Point(80, 164);
            this.iconButtonAgregarStock.Name = "iconButtonAgregarStock";
            this.iconButtonAgregarStock.Size = new System.Drawing.Size(110, 40);
            this.iconButtonAgregarStock.TabIndex = 56;
            this.iconButtonAgregarStock.Text = "Agregar";
            this.iconButtonAgregarStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonAgregarStock.UseVisualStyleBackColor = false;
            this.iconButtonAgregarStock.Click += new System.EventHandler(this.iconButtonAgregarStock_Click);
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
            this.iconButtonCancelar.Location = new System.Drawing.Point(196, 164);
            this.iconButtonCancelar.Name = "iconButtonCancelar";
            this.iconButtonCancelar.Size = new System.Drawing.Size(110, 40);
            this.iconButtonCancelar.TabIndex = 55;
            this.iconButtonCancelar.Text = "Cancelar";
            this.iconButtonCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonCancelar.UseVisualStyleBackColor = false;
            this.iconButtonCancelar.Click += new System.EventHandler(this.iconButtonCancelar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "Fecha de vencimiento:";
            // 
            // maskedTextBoxFechaDeVencimiento
            // 
            this.maskedTextBoxFechaDeVencimiento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.maskedTextBoxFechaDeVencimiento.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskedTextBoxFechaDeVencimiento.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.maskedTextBoxFechaDeVencimiento.Location = new System.Drawing.Point(184, 96);
            this.maskedTextBoxFechaDeVencimiento.Name = "maskedTextBoxFechaDeVencimiento";
            this.maskedTextBoxFechaDeVencimiento.Size = new System.Drawing.Size(110, 22);
            this.maskedTextBoxFechaDeVencimiento.TabIndex = 6;
            // 
            // StockAgregar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.ClientSize = new System.Drawing.Size(397, 219);
            this.Controls.Add(this.iconButtonAgregarStock);
            this.Controls.Add(this.iconButtonCancelar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelCodigoEscondido);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "StockAgregar";
            this.Text = "StockAgregar";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StockAgregar_MouseDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelPacksCajas;
        private System.Windows.Forms.Label labelUnidadesXPackCaja;
        private System.Windows.Forms.TextBox textBoxUnidad;
        private System.Windows.Forms.TextBox textBoxPacks;
        public System.Windows.Forms.Label labelProducto;
        public System.Windows.Forms.Label labelCodigoEscondido;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton iconButtonAgregarStock;
        private FontAwesome.Sharp.IconButton iconButtonCancelar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxFechaDeVencimiento;
    }
}