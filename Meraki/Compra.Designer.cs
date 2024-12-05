namespace Meraki
{
    partial class Compra
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
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.buttonCompraMinorista = new System.Windows.Forms.Button();
            this.buttonCompraMayorista = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.buttonCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.buttonCancelar.Location = new System.Drawing.Point(231, 11);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(103, 61);
            this.buttonCancelar.TabIndex = 3;
            this.buttonCancelar.Text = "Cancelar";
            this.buttonCancelar.UseVisualStyleBackColor = true;
            this.buttonCancelar.Click += new System.EventHandler(this.buttonModificar_Click);
            // 
            // buttonCompraMinorista
            // 
            this.buttonCompraMinorista.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.buttonCompraMinorista.ForeColor = System.Drawing.Color.Red;
            this.buttonCompraMinorista.Location = new System.Drawing.Point(122, 12);
            this.buttonCompraMinorista.Name = "buttonCompraMinorista";
            this.buttonCompraMinorista.Size = new System.Drawing.Size(103, 61);
            this.buttonCompraMinorista.TabIndex = 2;
            this.buttonCompraMinorista.Text = "Compra Minorista";
            this.buttonCompraMinorista.UseVisualStyleBackColor = true;
            this.buttonCompraMinorista.Click += new System.EventHandler(this.buttonCompraMinorista_Click);
            // 
            // buttonCompraMayorista
            // 
            this.buttonCompraMayorista.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.buttonCompraMayorista.ForeColor = System.Drawing.Color.Blue;
            this.buttonCompraMayorista.Location = new System.Drawing.Point(13, 11);
            this.buttonCompraMayorista.Name = "buttonCompraMayorista";
            this.buttonCompraMayorista.Size = new System.Drawing.Size(103, 61);
            this.buttonCompraMayorista.TabIndex = 1;
            this.buttonCompraMayorista.Text = "Compra Mayorista";
            this.buttonCompraMayorista.UseVisualStyleBackColor = true;
            this.buttonCompraMayorista.Click += new System.EventHandler(this.buttonCompraMayorista_Click);
            // 
            // Compra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 86);
            this.Controls.Add(this.buttonCancelar);
            this.Controls.Add(this.buttonCompraMinorista);
            this.Controls.Add(this.buttonCompraMayorista);
            this.Name = "Compra";
            this.Text = "Compra";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.Button buttonCompraMinorista;
        private System.Windows.Forms.Button buttonCompraMayorista;
    }
}