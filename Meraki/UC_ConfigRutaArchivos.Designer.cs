namespace Meraki
{
    partial class UC_ConfigRutaArchivos
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
            this.labelClienteActual = new System.Windows.Forms.Label();
            this.textBoxRutaBase = new System.Windows.Forms.TextBox();
            this.iconButtonExaminar = new FontAwesome.Sharp.IconButton();
            this.panelGrupoRuta = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelGrupoRuta.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelClienteActual
            // 
            this.labelClienteActual.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelClienteActual.AutoSize = true;
            this.labelClienteActual.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClienteActual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(15)))), ((int)(((byte)(28)))));
            this.labelClienteActual.Location = new System.Drawing.Point(11, 3);
            this.labelClienteActual.Name = "labelClienteActual";
            this.labelClienteActual.Size = new System.Drawing.Size(309, 37);
            this.labelClienteActual.TabIndex = 2;
            this.labelClienteActual.Text = "Ruta de comprobantes";
            // 
            // textBoxRutaBase
            // 
            this.textBoxRutaBase.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRutaBase.Location = new System.Drawing.Point(18, 27);
            this.textBoxRutaBase.Name = "textBoxRutaBase";
            this.textBoxRutaBase.ReadOnly = true;
            this.textBoxRutaBase.Size = new System.Drawing.Size(382, 29);
            this.textBoxRutaBase.TabIndex = 3;
            // 
            // iconButtonExaminar
            // 
            this.iconButtonExaminar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.iconButtonExaminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonExaminar.FlatAppearance.BorderSize = 0;
            this.iconButtonExaminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonExaminar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonExaminar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonExaminar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.iconButtonExaminar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonExaminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonExaminar.IconSize = 30;
            this.iconButtonExaminar.Location = new System.Drawing.Point(406, 23);
            this.iconButtonExaminar.Name = "iconButtonExaminar";
            this.iconButtonExaminar.Size = new System.Drawing.Size(106, 39);
            this.iconButtonExaminar.TabIndex = 55;
            this.iconButtonExaminar.Text = "Examinar";
            this.iconButtonExaminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonExaminar.UseVisualStyleBackColor = false;
            this.iconButtonExaminar.Click += new System.EventHandler(this.iconButtonExaminar_Click);
            // 
            // panelGrupoRuta
            // 
            this.panelGrupoRuta.Controls.Add(this.textBoxRutaBase);
            this.panelGrupoRuta.Controls.Add(this.iconButtonExaminar);
            this.panelGrupoRuta.Location = new System.Drawing.Point(0, 55);
            this.panelGrupoRuta.Name = "panelGrupoRuta";
            this.panelGrupoRuta.Size = new System.Drawing.Size(530, 80);
            this.panelGrupoRuta.TabIndex = 56;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelClienteActual);
            this.panel1.Location = new System.Drawing.Point(0, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(530, 48);
            this.panel1.TabIndex = 57;
            // 
            // UC_ConfigRutaArchivos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelGrupoRuta);
            this.Name = "UC_ConfigRutaArchivos";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.Size = new System.Drawing.Size(530, 412);
            this.Load += new System.EventHandler(this.UC_ConfigRutaArchivos_Load);
            this.panelGrupoRuta.ResumeLayout(false);
            this.panelGrupoRuta.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelClienteActual;
        private System.Windows.Forms.TextBox textBoxRutaBase;
        private FontAwesome.Sharp.IconButton iconButtonExaminar;
        private System.Windows.Forms.Panel panelGrupoRuta;
        private System.Windows.Forms.Panel panel1;
    }
}
