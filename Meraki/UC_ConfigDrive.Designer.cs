namespace Meraki
{
    partial class UC_ConfigDrive
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.labelClienteActual = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.iconButtonVincularDrive = new FontAwesome.Sharp.IconButton();
            this.labelEstadoDrive = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDriveID = new System.Windows.Forms.TextBox();
            this.iconButtonGuardar = new FontAwesome.Sharp.IconButton();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.labelEstadoDrive);
            this.panel4.Controls.Add(this.labelClienteActual);
            this.panel4.Controls.Add(this.iconButtonVincularDrive);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(15, 15);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.panel4.Size = new System.Drawing.Size(716, 133);
            this.panel4.TabIndex = 92;
            // 
            // labelClienteActual
            // 
            this.labelClienteActual.AutoSize = true;
            this.labelClienteActual.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClienteActual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(15)))), ((int)(((byte)(28)))));
            this.labelClienteActual.Location = new System.Drawing.Point(3, 3);
            this.labelClienteActual.Name = "labelClienteActual";
            this.labelClienteActual.Size = new System.Drawing.Size(313, 37);
            this.labelClienteActual.TabIndex = 2;
            this.labelClienteActual.Text = "Autorización de cuenta";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(7, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(475, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Vinculá el sistema con la cuenta de Google Drive donde se alojará el catálogo ofi" +
    "cial.\"";
            // 
            // iconButtonVincularDrive
            // 
            this.iconButtonVincularDrive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonVincularDrive.FlatAppearance.BorderSize = 0;
            this.iconButtonVincularDrive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonVincularDrive.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonVincularDrive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonVincularDrive.IconChar = FontAwesome.Sharp.IconChar.Google;
            this.iconButtonVincularDrive.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonVincularDrive.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonVincularDrive.IconSize = 30;
            this.iconButtonVincularDrive.Location = new System.Drawing.Point(10, 65);
            this.iconButtonVincularDrive.Name = "iconButtonVincularDrive";
            this.iconButtonVincularDrive.Size = new System.Drawing.Size(190, 41);
            this.iconButtonVincularDrive.TabIndex = 55;
            this.iconButtonVincularDrive.Text = "Vincular cuenta de Google";
            this.iconButtonVincularDrive.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonVincularDrive.UseVisualStyleBackColor = false;
            this.iconButtonVincularDrive.Click += new System.EventHandler(this.iconButtonVincularDrive_Click);
            // 
            // labelEstadoDrive
            // 
            this.labelEstadoDrive.AutoSize = true;
            this.labelEstadoDrive.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEstadoDrive.Location = new System.Drawing.Point(206, 73);
            this.labelEstadoDrive.Name = "labelEstadoDrive";
            this.labelEstadoDrive.Size = new System.Drawing.Size(160, 21);
            this.labelEstadoDrive.TabIndex = 93;
            this.labelEstadoDrive.Text = "Estado: Desconectado";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.iconButtonGuardar);
            this.panel1.Controls.Add(this.textBoxDriveID);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(15, 160);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.panel1.Size = new System.Drawing.Size(716, 133);
            this.panel1.TabIndex = 94;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(7, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(670, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Pegá acá el ID del archivo PDF que ya está subido a Drive. El sistema pisará este" +
    " archivo para no romper el enlace público";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(15)))), ((int)(((byte)(28)))));
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(334, 37);
            this.label3.TabIndex = 2;
            this.label3.Text = "Archivo oficial (Linktree)";
            // 
            // textBoxDriveID
            // 
            this.textBoxDriveID.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDriveID.Location = new System.Drawing.Point(10, 67);
            this.textBoxDriveID.Name = "textBoxDriveID";
            this.textBoxDriveID.Size = new System.Drawing.Size(306, 29);
            this.textBoxDriveID.TabIndex = 4;
            // 
            // iconButtonGuardar
            // 
            this.iconButtonGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButtonGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonGuardar.FlatAppearance.BorderSize = 0;
            this.iconButtonGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonGuardar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonGuardar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonGuardar.IconChar = FontAwesome.Sharp.IconChar.Upload;
            this.iconButtonGuardar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonGuardar.IconSize = 30;
            this.iconButtonGuardar.Location = new System.Drawing.Point(334, 61);
            this.iconButtonGuardar.Name = "iconButtonGuardar";
            this.iconButtonGuardar.Size = new System.Drawing.Size(110, 40);
            this.iconButtonGuardar.TabIndex = 75;
            this.iconButtonGuardar.Text = "Guardar";
            this.iconButtonGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonGuardar.UseVisualStyleBackColor = false;
            this.iconButtonGuardar.Click += new System.EventHandler(this.iconButtonGuardar_Click);
            // 
            // UC_ConfigDrive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Name = "UC_ConfigDrive";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.Size = new System.Drawing.Size(746, 387);
            this.Load += new System.EventHandler(this.UC_ConfigDrive_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label labelClienteActual;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton iconButtonVincularDrive;
        private System.Windows.Forms.Label labelEstadoDrive;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDriveID;
        private FontAwesome.Sharp.IconButton iconButtonGuardar;
    }
}
