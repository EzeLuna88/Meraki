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
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.buttonAgregarStock = new System.Windows.Forms.Button();
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
            this.labelProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.labelProducto.Location = new System.Drawing.Point(12, 14);
            this.labelProducto.Name = "labelProducto";
            this.labelProducto.Size = new System.Drawing.Size(57, 20);
            this.labelProducto.TabIndex = 1;
            this.labelProducto.Text = "label1";
            // 
            // labelPacksCajas
            // 
            this.labelPacksCajas.AutoSize = true;
            this.labelPacksCajas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.labelPacksCajas.Location = new System.Drawing.Point(11, 38);
            this.labelPacksCajas.Name = "labelPacksCajas";
            this.labelPacksCajas.Size = new System.Drawing.Size(199, 20);
            this.labelPacksCajas.TabIndex = 2;
            this.labelPacksCajas.Text = "Cantidad Packs / Cajas:";
            // 
            // labelUnidadesXPackCaja
            // 
            this.labelUnidadesXPackCaja.AutoSize = true;
            this.labelUnidadesXPackCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.labelUnidadesXPackCaja.Location = new System.Drawing.Point(11, 64);
            this.labelUnidadesXPackCaja.Name = "labelUnidadesXPackCaja";
            this.labelUnidadesXPackCaja.Size = new System.Drawing.Size(198, 20);
            this.labelUnidadesXPackCaja.TabIndex = 3;
            this.labelUnidadesXPackCaja.Text = "Unidades x Pack / Caja:";
            // 
            // textBoxUnidad
            // 
            this.textBoxUnidad.Location = new System.Drawing.Point(215, 66);
            this.textBoxUnidad.Name = "textBoxUnidad";
            this.textBoxUnidad.Size = new System.Drawing.Size(40, 20);
            this.textBoxUnidad.TabIndex = 2;
            this.textBoxUnidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxUnidad_KeyPress);
            // 
            // textBoxPacks
            // 
            this.textBoxPacks.Location = new System.Drawing.Point(215, 38);
            this.textBoxPacks.Name = "textBoxPacks";
            this.textBoxPacks.Size = new System.Drawing.Size(40, 20);
            this.textBoxPacks.TabIndex = 1;
            this.textBoxPacks.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPacks_KeyPress);
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancelar.ForeColor = System.Drawing.Color.Red;
            this.buttonCancelar.Location = new System.Drawing.Point(152, 102);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(103, 61);
            this.buttonCancelar.TabIndex = 4;
            this.buttonCancelar.Text = "Cancelar";
            this.buttonCancelar.UseVisualStyleBackColor = true;
            this.buttonCancelar.Click += new System.EventHandler(this.buttonCancelar_Click);
            // 
            // buttonAgregarStock
            // 
            this.buttonAgregarStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAgregarStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.buttonAgregarStock.Location = new System.Drawing.Point(15, 102);
            this.buttonAgregarStock.Name = "buttonAgregarStock";
            this.buttonAgregarStock.Size = new System.Drawing.Size(103, 61);
            this.buttonAgregarStock.TabIndex = 3;
            this.buttonAgregarStock.Text = "Agregar";
            this.buttonAgregarStock.UseVisualStyleBackColor = true;
            this.buttonAgregarStock.Click += new System.EventHandler(this.buttonAgregarStock_Click);
            // 
            // StockAgregar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 179);
            this.Controls.Add(this.buttonCancelar);
            this.Controls.Add(this.buttonAgregarStock);
            this.Controls.Add(this.textBoxPacks);
            this.Controls.Add(this.textBoxUnidad);
            this.Controls.Add(this.labelUnidadesXPackCaja);
            this.Controls.Add(this.labelPacksCajas);
            this.Controls.Add(this.labelProducto);
            this.Controls.Add(this.labelCodigoEscondido);
            this.Name = "StockAgregar";
            this.Text = "StockAgregar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelPacksCajas;
        private System.Windows.Forms.Label labelUnidadesXPackCaja;
        private System.Windows.Forms.TextBox textBoxUnidad;
        private System.Windows.Forms.TextBox textBoxPacks;
        public System.Windows.Forms.Label labelProducto;
        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.Button buttonAgregarStock;
        public System.Windows.Forms.Label labelCodigoEscondido;
    }
}