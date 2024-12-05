namespace Meraki
{
    partial class Stock
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewStock = new System.Windows.Forms.DataGridView();
            this.textBoxFiltrar = new System.Windows.Forms.TextBox();
            this.iconButtonAgregarStock = new FontAwesome.Sharp.IconButton();
            this.iconButtonModificar = new FontAwesome.Sharp.IconButton();
            this.iconButtonBorrar = new FontAwesome.Sharp.IconButton();
            this.iconButtonNuevo = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStock)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewStock
            // 
            this.dataGridViewStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewStock.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.dataGridViewStock.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewStock.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(8)))), ((int)(((byte)(21)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewStock.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStock.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.dataGridViewStock.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewStock.MultiSelect = false;
            this.dataGridViewStock.Name = "dataGridViewStock";
            this.dataGridViewStock.ReadOnly = true;
            this.dataGridViewStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewStock.Size = new System.Drawing.Size(490, 351);
            this.dataGridViewStock.TabIndex = 44;
            this.dataGridViewStock.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewStock_CellFormatting_1);
            // 
            // textBoxFiltrar
            // 
            this.textBoxFiltrar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFiltrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxFiltrar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxFiltrar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFiltrar.Location = new System.Drawing.Point(12, 369);
            this.textBoxFiltrar.Name = "textBoxFiltrar";
            this.textBoxFiltrar.Size = new System.Drawing.Size(490, 18);
            this.textBoxFiltrar.TabIndex = 47;
            this.textBoxFiltrar.TextChanged += new System.EventHandler(this.textBoxFiltrar_TextChanged_1);
            this.textBoxFiltrar.Enter += new System.EventHandler(this.textBoxFiltrar_Enter);
            this.textBoxFiltrar.Leave += new System.EventHandler(this.textBoxFiltrar_Leave);
            // 
            // iconButtonAgregarStock
            // 
            this.iconButtonAgregarStock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButtonAgregarStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonAgregarStock.FlatAppearance.BorderSize = 0;
            this.iconButtonAgregarStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonAgregarStock.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonAgregarStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonAgregarStock.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.iconButtonAgregarStock.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonAgregarStock.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonAgregarStock.IconSize = 30;
            this.iconButtonAgregarStock.Location = new System.Drawing.Point(12, 393);
            this.iconButtonAgregarStock.Name = "iconButtonAgregarStock";
            this.iconButtonAgregarStock.Size = new System.Drawing.Size(118, 40);
            this.iconButtonAgregarStock.TabIndex = 54;
            this.iconButtonAgregarStock.Text = "Agregar Stock";
            this.iconButtonAgregarStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonAgregarStock.UseVisualStyleBackColor = false;
            this.iconButtonAgregarStock.Click += new System.EventHandler(this.iconButtonAgregarStock_Click);
            // 
            // iconButtonModificar
            // 
            this.iconButtonModificar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButtonModificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonModificar.FlatAppearance.BorderSize = 0;
            this.iconButtonModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonModificar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonModificar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonModificar.IconChar = FontAwesome.Sharp.IconChar.Pen;
            this.iconButtonModificar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonModificar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonModificar.IconSize = 30;
            this.iconButtonModificar.Location = new System.Drawing.Point(384, 393);
            this.iconButtonModificar.Name = "iconButtonModificar";
            this.iconButtonModificar.Size = new System.Drawing.Size(118, 40);
            this.iconButtonModificar.TabIndex = 53;
            this.iconButtonModificar.Text = "Modificar";
            this.iconButtonModificar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonModificar.UseVisualStyleBackColor = false;
            this.iconButtonModificar.Click += new System.EventHandler(this.iconButtonModificar_Click);
            // 
            // iconButtonBorrar
            // 
            this.iconButtonBorrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButtonBorrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonBorrar.FlatAppearance.BorderSize = 0;
            this.iconButtonBorrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonBorrar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonBorrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonBorrar.IconChar = FontAwesome.Sharp.IconChar.Eraser;
            this.iconButtonBorrar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonBorrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonBorrar.IconSize = 30;
            this.iconButtonBorrar.Location = new System.Drawing.Point(260, 393);
            this.iconButtonBorrar.Name = "iconButtonBorrar";
            this.iconButtonBorrar.Size = new System.Drawing.Size(118, 40);
            this.iconButtonBorrar.TabIndex = 52;
            this.iconButtonBorrar.Text = "Borrar";
            this.iconButtonBorrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonBorrar.UseVisualStyleBackColor = false;
            this.iconButtonBorrar.Click += new System.EventHandler(this.iconButtonBorrar_Click);
            // 
            // iconButtonNuevo
            // 
            this.iconButtonNuevo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButtonNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonNuevo.FlatAppearance.BorderSize = 0;
            this.iconButtonNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonNuevo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonNuevo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonNuevo.IconChar = FontAwesome.Sharp.IconChar.FileCirclePlus;
            this.iconButtonNuevo.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonNuevo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonNuevo.IconSize = 30;
            this.iconButtonNuevo.Location = new System.Drawing.Point(136, 393);
            this.iconButtonNuevo.Name = "iconButtonNuevo";
            this.iconButtonNuevo.Size = new System.Drawing.Size(118, 40);
            this.iconButtonNuevo.TabIndex = 51;
            this.iconButtonNuevo.Text = "Nuevo";
            this.iconButtonNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonNuevo.UseVisualStyleBackColor = false;
            this.iconButtonNuevo.Click += new System.EventHandler(this.iconButtonNuevo_Click);
            // 
            // Stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.ClientSize = new System.Drawing.Size(514, 445);
            this.Controls.Add(this.iconButtonAgregarStock);
            this.Controls.Add(this.iconButtonModificar);
            this.Controls.Add(this.iconButtonBorrar);
            this.Controls.Add(this.iconButtonNuevo);
            this.Controls.Add(this.textBoxFiltrar);
            this.Controls.Add(this.dataGridViewStock);
            this.Name = "Stock";
            this.Text = "Stock";
            this.Load += new System.EventHandler(this.Stock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewStock;
        private System.Windows.Forms.TextBox textBoxFiltrar;
        private FontAwesome.Sharp.IconButton iconButtonAgregarStock;
        private FontAwesome.Sharp.IconButton iconButtonModificar;
        private FontAwesome.Sharp.IconButton iconButtonBorrar;
        private FontAwesome.Sharp.IconButton iconButtonNuevo;
    }
}