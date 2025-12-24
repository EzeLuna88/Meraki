namespace Meraki
{
    partial class AvisoPocoStock
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
            this.iconButtonModificar = new FontAwesome.Sharp.IconButton();
            this.iconButtonCancelar = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxAviso = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.iconButtonModificar.Location = new System.Drawing.Point(35, 65);
            this.iconButtonModificar.Name = "iconButtonModificar";
            this.iconButtonModificar.Size = new System.Drawing.Size(110, 40);
            this.iconButtonModificar.TabIndex = 75;
            this.iconButtonModificar.Text = "Aceptar";
            this.iconButtonModificar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonModificar.UseVisualStyleBackColor = false;
            this.iconButtonModificar.Click += new System.EventHandler(this.iconButtonModificar_Click);
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
            this.iconButtonCancelar.Location = new System.Drawing.Point(151, 65);
            this.iconButtonCancelar.Name = "iconButtonCancelar";
            this.iconButtonCancelar.Size = new System.Drawing.Size(110, 40);
            this.iconButtonCancelar.TabIndex = 74;
            this.iconButtonCancelar.Text = "Cancelar";
            this.iconButtonCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonCancelar.UseVisualStyleBackColor = false;
            this.iconButtonCancelar.Click += new System.EventHandler(this.iconButtonCancelar_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(91)))), ((int)(((byte)(122)))));
            this.panel1.Controls.Add(this.textBoxAviso);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(270, 47);
            this.panel1.TabIndex = 73;
            // 
            // textBoxAviso
            // 
            this.textBoxAviso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxAviso.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxAviso.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAviso.Location = new System.Drawing.Point(199, 10);
            this.textBoxAviso.Name = "textBoxAviso";
            this.textBoxAviso.Size = new System.Drawing.Size(53, 22);
            this.textBoxAviso.TabIndex = 70;
            this.textBoxAviso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxAviso_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(16, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(177, 21);
            this.label7.TabIndex = 69;
            this.label7.Text = "Cantidad para dar aviso:";
            // 
            // AvisoPocoStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.ClientSize = new System.Drawing.Size(296, 117);
            this.Controls.Add(this.iconButtonModificar);
            this.Controls.Add(this.iconButtonCancelar);
            this.Controls.Add(this.panel1);
            this.Name = "AvisoPocoStock";
            this.Text = "Aviso de poco Stock";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FontAwesome.Sharp.IconButton iconButtonModificar;
        private FontAwesome.Sharp.IconButton iconButtonCancelar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxAviso;
    }
}