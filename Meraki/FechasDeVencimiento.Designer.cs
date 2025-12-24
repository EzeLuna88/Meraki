namespace Meraki
{
    partial class FechasDeVencimiento
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewFechasDeVencimiento = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDiasDeAviso = new System.Windows.Forms.TextBox();
            this.iconButtonGuardar = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFechasDeVencimiento)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewFechasDeVencimiento
            // 
            this.dataGridViewFechasDeVencimiento.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewFechasDeVencimiento.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.dataGridViewFechasDeVencimiento.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewFechasDeVencimiento.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(8)))), ((int)(((byte)(21)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFechasDeVencimiento.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewFechasDeVencimiento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFechasDeVencimiento.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.dataGridViewFechasDeVencimiento.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewFechasDeVencimiento.MultiSelect = false;
            this.dataGridViewFechasDeVencimiento.Name = "dataGridViewFechasDeVencimiento";
            this.dataGridViewFechasDeVencimiento.ReadOnly = true;
            this.dataGridViewFechasDeVencimiento.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFechasDeVencimiento.Size = new System.Drawing.Size(613, 354);
            this.dataGridViewFechasDeVencimiento.TabIndex = 45;
            this.dataGridViewFechasDeVencimiento.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewFechasDeVencimiento_CellFormatting);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(91)))), ((int)(((byte)(122)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBoxDiasDeAviso);
            this.panel1.Location = new System.Drawing.Point(12, 379);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(266, 40);
            this.panel1.TabIndex = 53;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 21);
            this.label1.TabIndex = 31;
            this.label1.Text = "Dias de anticipo para avisar:";
            // 
            // textBoxDiasDeAviso
            // 
            this.textBoxDiasDeAviso.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDiasDeAviso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxDiasDeAviso.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxDiasDeAviso.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDiasDeAviso.Location = new System.Drawing.Point(221, 9);
            this.textBoxDiasDeAviso.Name = "textBoxDiasDeAviso";
            this.textBoxDiasDeAviso.Size = new System.Drawing.Size(31, 22);
            this.textBoxDiasDeAviso.TabIndex = 2;
            // 
            // iconButtonGuardar
            // 
            this.iconButtonGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButtonGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonGuardar.FlatAppearance.BorderSize = 0;
            this.iconButtonGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonGuardar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonGuardar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonGuardar.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.iconButtonGuardar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonGuardar.IconSize = 30;
            this.iconButtonGuardar.Location = new System.Drawing.Point(284, 379);
            this.iconButtonGuardar.Name = "iconButtonGuardar";
            this.iconButtonGuardar.Size = new System.Drawing.Size(115, 40);
            this.iconButtonGuardar.TabIndex = 54;
            this.iconButtonGuardar.Text = "Guardar";
            this.iconButtonGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonGuardar.UseVisualStyleBackColor = false;
            this.iconButtonGuardar.Click += new System.EventHandler(this.iconButtonGuardar_Click);
            // 
            // FechasDeVencimiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.ClientSize = new System.Drawing.Size(637, 428);
            this.Controls.Add(this.iconButtonGuardar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridViewFechasDeVencimiento);
            this.Name = "FechasDeVencimiento";
            this.Text = "Fechas De Vencimiento";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFechasDeVencimiento)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewFechasDeVencimiento;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDiasDeAviso;
        private FontAwesome.Sharp.IconButton iconButtonGuardar;
    }
}