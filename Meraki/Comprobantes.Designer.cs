namespace Meraki
{
    partial class Comprobantes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewComprobantes = new System.Windows.Forms.DataGridView();
            this.textBoxFiltroNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxFiltroFechaDesde = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxFiltroFechaHasta = new System.Windows.Forms.TextBox();
            this.iconButtonComprobanteMostrar = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComprobantes)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewComprobantes
            // 
            this.dataGridViewComprobantes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewComprobantes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.dataGridViewComprobantes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewComprobantes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(8)))), ((int)(((byte)(21)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComprobantes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewComprobantes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewComprobantes.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.dataGridViewComprobantes.Location = new System.Drawing.Point(19, 22);
            this.dataGridViewComprobantes.MultiSelect = false;
            this.dataGridViewComprobantes.Name = "dataGridViewComprobantes";
            this.dataGridViewComprobantes.ReadOnly = true;
            this.dataGridViewComprobantes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewComprobantes.Size = new System.Drawing.Size(729, 388);
            this.dataGridViewComprobantes.TabIndex = 0;
            this.dataGridViewComprobantes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewComprobantes_CellDoubleClick);
            // 
            // textBoxFiltroNombre
            // 
            this.textBoxFiltroNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFiltroNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxFiltroNombre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxFiltroNombre.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFiltroNombre.Location = new System.Drawing.Point(21, 426);
            this.textBoxFiltroNombre.Name = "textBoxFiltroNombre";
            this.textBoxFiltroNombre.Size = new System.Drawing.Size(444, 18);
            this.textBoxFiltroNombre.TabIndex = 2;
            this.textBoxFiltroNombre.TextChanged += new System.EventHandler(this.textBoxFiltroNombre_TextChanged);
            this.textBoxFiltroNombre.Enter += new System.EventHandler(this.textBoxFiltroNombre_Enter);
            this.textBoxFiltroNombre.Leave += new System.EventHandler(this.textBoxFiltroNombre_Leave);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(471, 425);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Fecha:";
            // 
            // textBoxFiltroFechaDesde
            // 
            this.textBoxFiltroFechaDesde.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFiltroFechaDesde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxFiltroFechaDesde.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxFiltroFechaDesde.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFiltroFechaDesde.Location = new System.Drawing.Point(521, 425);
            this.textBoxFiltroFechaDesde.Name = "textBoxFiltroFechaDesde";
            this.textBoxFiltroFechaDesde.Size = new System.Drawing.Size(100, 18);
            this.textBoxFiltroFechaDesde.TabIndex = 5;
            this.textBoxFiltroFechaDesde.TextChanged += new System.EventHandler(this.textBoxFiltroFechaDesde_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(627, 426);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "a";
            // 
            // textBoxFiltroFechaHasta
            // 
            this.textBoxFiltroFechaHasta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFiltroFechaHasta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxFiltroFechaHasta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxFiltroFechaHasta.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFiltroFechaHasta.Location = new System.Drawing.Point(648, 426);
            this.textBoxFiltroFechaHasta.Name = "textBoxFiltroFechaHasta";
            this.textBoxFiltroFechaHasta.Size = new System.Drawing.Size(100, 18);
            this.textBoxFiltroFechaHasta.TabIndex = 7;
            this.textBoxFiltroFechaHasta.TextChanged += new System.EventHandler(this.textBoxFiltroFechaHasta_TextChanged);
            // 
            // iconButtonComprobanteMostrar
            // 
            this.iconButtonComprobanteMostrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButtonComprobanteMostrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonComprobanteMostrar.FlatAppearance.BorderSize = 0;
            this.iconButtonComprobanteMostrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonComprobanteMostrar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonComprobanteMostrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonComprobanteMostrar.IconChar = FontAwesome.Sharp.IconChar.RectangleList;
            this.iconButtonComprobanteMostrar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonComprobanteMostrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonComprobanteMostrar.IconSize = 30;
            this.iconButtonComprobanteMostrar.Location = new System.Drawing.Point(21, 456);
            this.iconButtonComprobanteMostrar.Name = "iconButtonComprobanteMostrar";
            this.iconButtonComprobanteMostrar.Size = new System.Drawing.Size(177, 40);
            this.iconButtonComprobanteMostrar.TabIndex = 8;
            this.iconButtonComprobanteMostrar.Text = "Mostrar comprobante";
            this.iconButtonComprobanteMostrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonComprobanteMostrar.UseVisualStyleBackColor = false;
            this.iconButtonComprobanteMostrar.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // Comprobantes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.ClientSize = new System.Drawing.Size(771, 505);
            this.Controls.Add(this.iconButtonComprobanteMostrar);
            this.Controls.Add(this.textBoxFiltroFechaHasta);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxFiltroFechaDesde);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxFiltroNombre);
            this.Controls.Add(this.dataGridViewComprobantes);
            this.Name = "Comprobantes";
            this.Text = "Comprobantes";
            this.Load += new System.EventHandler(this.Comprobantes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComprobantes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewComprobantes;
        private System.Windows.Forms.TextBox textBoxFiltroNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxFiltroFechaDesde;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxFiltroFechaHasta;
        private FontAwesome.Sharp.IconButton iconButtonComprobanteMostrar;
    }
}