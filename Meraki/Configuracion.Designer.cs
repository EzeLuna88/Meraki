namespace Meraki
{
    partial class Configuracion
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
            this.panelMenuLateral = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonClienteDefecto = new System.Windows.Forms.Button();
            this.buttonListaDePrecios = new System.Windows.Forms.Button();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.panelBarra = new System.Windows.Forms.Panel();
            this.iconButtonCerrar = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panelMenuLateral.SuspendLayout();
            this.panelBarra.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenuLateral
            // 
            this.panelMenuLateral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(15)))), ((int)(((byte)(28)))));
            this.panelMenuLateral.Controls.Add(this.button2);
            this.panelMenuLateral.Controls.Add(this.button1);
            this.panelMenuLateral.Controls.Add(this.buttonClienteDefecto);
            this.panelMenuLateral.Controls.Add(this.buttonListaDePrecios);
            this.panelMenuLateral.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenuLateral.Location = new System.Drawing.Point(0, 26);
            this.panelMenuLateral.Name = "panelMenuLateral";
            this.panelMenuLateral.Size = new System.Drawing.Size(250, 520);
            this.panelMenuLateral.TabIndex = 56;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Top;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.button2.Location = new System.Drawing.Point(0, 150);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(250, 50);
            this.button2.TabIndex = 58;
            this.button2.Text = "Conexión a Google Drive";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.button1.Location = new System.Drawing.Point(0, 100);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(250, 50);
            this.button1.TabIndex = 57;
            this.button1.Text = "Ruta de comprobantes";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonClienteDefecto
            // 
            this.buttonClienteDefecto.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonClienteDefecto.FlatAppearance.BorderSize = 0;
            this.buttonClienteDefecto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClienteDefecto.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClienteDefecto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.buttonClienteDefecto.Location = new System.Drawing.Point(0, 50);
            this.buttonClienteDefecto.Name = "buttonClienteDefecto";
            this.buttonClienteDefecto.Size = new System.Drawing.Size(250, 50);
            this.buttonClienteDefecto.TabIndex = 56;
            this.buttonClienteDefecto.Text = "Cliente por defecto";
            this.buttonClienteDefecto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonClienteDefecto.UseVisualStyleBackColor = true;
            this.buttonClienteDefecto.Click += new System.EventHandler(this.buttonClienteDefecto_Click);
            // 
            // buttonListaDePrecios
            // 
            this.buttonListaDePrecios.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonListaDePrecios.FlatAppearance.BorderSize = 0;
            this.buttonListaDePrecios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonListaDePrecios.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonListaDePrecios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.buttonListaDePrecios.Location = new System.Drawing.Point(0, 0);
            this.buttonListaDePrecios.Name = "buttonListaDePrecios";
            this.buttonListaDePrecios.Size = new System.Drawing.Size(250, 50);
            this.buttonListaDePrecios.TabIndex = 0;
            this.buttonListaDePrecios.Text = "Lista de precios";
            this.buttonListaDePrecios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonListaDePrecios.UseVisualStyleBackColor = true;
            this.buttonListaDePrecios.Click += new System.EventHandler(this.buttonListaDePrecios_Click);
            // 
            // panelContenedor
            // 
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(250, 26);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(904, 520);
            this.panelContenedor.TabIndex = 57;
            // 
            // panelBarra
            // 
            this.panelBarra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(8)))), ((int)(((byte)(21)))));
            this.panelBarra.Controls.Add(this.iconButtonCerrar);
            this.panelBarra.Controls.Add(this.label1);
            this.panelBarra.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBarra.Location = new System.Drawing.Point(0, 0);
            this.panelBarra.Name = "panelBarra";
            this.panelBarra.Size = new System.Drawing.Size(1154, 26);
            this.panelBarra.TabIndex = 8;
            this.panelBarra.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelBarra_MouseDown);
            // 
            // iconButtonCerrar
            // 
            this.iconButtonCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(8)))), ((int)(((byte)(21)))));
            this.iconButtonCerrar.Dock = System.Windows.Forms.DockStyle.Right;
            this.iconButtonCerrar.FlatAppearance.BorderSize = 0;
            this.iconButtonCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonCerrar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonCerrar.IconChar = FontAwesome.Sharp.IconChar.X;
            this.iconButtonCerrar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonCerrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonCerrar.IconSize = 18;
            this.iconButtonCerrar.Location = new System.Drawing.Point(1124, 0);
            this.iconButtonCerrar.Name = "iconButtonCerrar";
            this.iconButtonCerrar.Size = new System.Drawing.Size(30, 26);
            this.iconButtonCerrar.TabIndex = 8;
            this.iconButtonCerrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconButtonCerrar.UseVisualStyleBackColor = false;
            this.iconButtonCerrar.Click += new System.EventHandler(this.iconButtonCerrar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.label1.Location = new System.Drawing.Point(4, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Configuración";
            // 
            // Configuracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.ClientSize = new System.Drawing.Size(1154, 546);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.panelMenuLateral);
            this.Controls.Add(this.panelBarra);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Configuracion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuracion";
            this.Load += new System.EventHandler(this.Configuracion_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Configuracion_Paint);
            this.panelMenuLateral.ResumeLayout(false);
            this.panelBarra.ResumeLayout(false);
            this.panelBarra.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelMenuLateral;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.Button buttonClienteDefecto;
        private System.Windows.Forms.Button buttonListaDePrecios;
        private System.Windows.Forms.Panel panelBarra;
        private FontAwesome.Sharp.IconButton iconButtonCerrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}