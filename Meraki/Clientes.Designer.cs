namespace Meraki
{
    partial class Clientes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label9 = new System.Windows.Forms.Label();
            this.richTextBoxComentarios = new System.Windows.Forms.RichTextBox();
            this.dataGridViewClientes = new System.Windows.Forms.DataGridView();
            this.textBoxFiltrar = new System.Windows.Forms.TextBox();
            this.iconButtonAlta2 = new FontAwesome.Sharp.IconButton();
            this.iconButtonBaja2 = new FontAwesome.Sharp.IconButton();
            this.iconButtonModificar2 = new FontAwesome.Sharp.IconButton();
            this.iconButtonComentariosGuardar = new FontAwesome.Sharp.IconButton();
            this.iconButtonComentariosBorrar = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClientes)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(9, 211);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 17);
            this.label9.TabIndex = 37;
            this.label9.Text = "Comentarios:";
            // 
            // richTextBoxComentarios
            // 
            this.richTextBoxComentarios.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxComentarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(91)))), ((int)(((byte)(122)))));
            this.richTextBoxComentarios.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxComentarios.Enabled = false;
            this.richTextBoxComentarios.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxComentarios.Location = new System.Drawing.Point(12, 231);
            this.richTextBoxComentarios.Name = "richTextBoxComentarios";
            this.richTextBoxComentarios.Size = new System.Drawing.Size(472, 127);
            this.richTextBoxComentarios.TabIndex = 38;
            this.richTextBoxComentarios.Text = "";
            this.richTextBoxComentarios.TextChanged += new System.EventHandler(this.richTextBoxComentarios_TextChanged);
            // 
            // dataGridViewClientes
            // 
            this.dataGridViewClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewClientes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.dataGridViewClientes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewClientes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(8)))), ((int)(((byte)(21)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewClientes.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.dataGridViewClientes.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewClientes.MultiSelect = false;
            this.dataGridViewClientes.Name = "dataGridViewClientes";
            this.dataGridViewClientes.ReadOnly = true;
            this.dataGridViewClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewClientes.Size = new System.Drawing.Size(587, 196);
            this.dataGridViewClientes.TabIndex = 42;
            this.dataGridViewClientes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewClientes_CellClick_1);
            this.dataGridViewClientes.SelectionChanged += new System.EventHandler(this.dataGridViewClientes_SelectionChanged_1);
            // 
            // textBoxFiltrar
            // 
            this.textBoxFiltrar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFiltrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxFiltrar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxFiltrar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFiltrar.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBoxFiltrar.Location = new System.Drawing.Point(12, 364);
            this.textBoxFiltrar.Name = "textBoxFiltrar";
            this.textBoxFiltrar.Size = new System.Drawing.Size(472, 18);
            this.textBoxFiltrar.TabIndex = 43;
            this.textBoxFiltrar.TextChanged += new System.EventHandler(this.textBoxFiltrar_TextChanged_1);
            this.textBoxFiltrar.Enter += new System.EventHandler(this.textBoxFiltrar_Enter);
            this.textBoxFiltrar.Leave += new System.EventHandler(this.textBoxFiltrar_Leave);
            // 
            // iconButtonAlta2
            // 
            this.iconButtonAlta2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButtonAlta2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonAlta2.FlatAppearance.BorderSize = 0;
            this.iconButtonAlta2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonAlta2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonAlta2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonAlta2.IconChar = FontAwesome.Sharp.IconChar.CircleChevronUp;
            this.iconButtonAlta2.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonAlta2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonAlta2.IconSize = 30;
            this.iconButtonAlta2.Location = new System.Drawing.Point(12, 388);
            this.iconButtonAlta2.Name = "iconButtonAlta2";
            this.iconButtonAlta2.Size = new System.Drawing.Size(106, 40);
            this.iconButtonAlta2.TabIndex = 44;
            this.iconButtonAlta2.Text = "Alta";
            this.iconButtonAlta2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonAlta2.UseVisualStyleBackColor = false;
            this.iconButtonAlta2.Click += new System.EventHandler(this.iconButtonAlta2_Click);
            // 
            // iconButtonBaja2
            // 
            this.iconButtonBaja2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButtonBaja2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonBaja2.FlatAppearance.BorderSize = 0;
            this.iconButtonBaja2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonBaja2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonBaja2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonBaja2.IconChar = FontAwesome.Sharp.IconChar.CircleChevronDown;
            this.iconButtonBaja2.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonBaja2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonBaja2.IconSize = 30;
            this.iconButtonBaja2.Location = new System.Drawing.Point(124, 388);
            this.iconButtonBaja2.Name = "iconButtonBaja2";
            this.iconButtonBaja2.Size = new System.Drawing.Size(106, 40);
            this.iconButtonBaja2.TabIndex = 45;
            this.iconButtonBaja2.Text = "Baja";
            this.iconButtonBaja2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonBaja2.UseVisualStyleBackColor = false;
            this.iconButtonBaja2.Click += new System.EventHandler(this.iconButtonBaja2_Click);
            // 
            // iconButtonModificar2
            // 
            this.iconButtonModificar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButtonModificar2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonModificar2.FlatAppearance.BorderSize = 0;
            this.iconButtonModificar2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonModificar2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonModificar2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonModificar2.IconChar = FontAwesome.Sharp.IconChar.Pen;
            this.iconButtonModificar2.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonModificar2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonModificar2.IconSize = 30;
            this.iconButtonModificar2.Location = new System.Drawing.Point(236, 388);
            this.iconButtonModificar2.Name = "iconButtonModificar2";
            this.iconButtonModificar2.Size = new System.Drawing.Size(106, 40);
            this.iconButtonModificar2.TabIndex = 46;
            this.iconButtonModificar2.Text = "Modificar";
            this.iconButtonModificar2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonModificar2.UseVisualStyleBackColor = false;
            this.iconButtonModificar2.Click += new System.EventHandler(this.iconButtonModificar2_Click);
            // 
            // iconButtonComentariosGuardar
            // 
            this.iconButtonComentariosGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.iconButtonComentariosGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonComentariosGuardar.FlatAppearance.BorderSize = 0;
            this.iconButtonComentariosGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonComentariosGuardar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonComentariosGuardar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonComentariosGuardar.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.iconButtonComentariosGuardar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonComentariosGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonComentariosGuardar.IconSize = 30;
            this.iconButtonComentariosGuardar.Location = new System.Drawing.Point(493, 271);
            this.iconButtonComentariosGuardar.Name = "iconButtonComentariosGuardar";
            this.iconButtonComentariosGuardar.Size = new System.Drawing.Size(106, 40);
            this.iconButtonComentariosGuardar.TabIndex = 47;
            this.iconButtonComentariosGuardar.Text = "Guardar";
            this.iconButtonComentariosGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonComentariosGuardar.UseVisualStyleBackColor = false;
            this.iconButtonComentariosGuardar.Click += new System.EventHandler(this.iconButtonComentariosGuardar_Click);
            // 
            // iconButtonComentariosBorrar
            // 
            this.iconButtonComentariosBorrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.iconButtonComentariosBorrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonComentariosBorrar.FlatAppearance.BorderSize = 0;
            this.iconButtonComentariosBorrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonComentariosBorrar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonComentariosBorrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonComentariosBorrar.IconChar = FontAwesome.Sharp.IconChar.Pen;
            this.iconButtonComentariosBorrar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonComentariosBorrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonComentariosBorrar.IconSize = 30;
            this.iconButtonComentariosBorrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButtonComentariosBorrar.Location = new System.Drawing.Point(493, 271);
            this.iconButtonComentariosBorrar.Name = "iconButtonComentariosBorrar";
            this.iconButtonComentariosBorrar.Size = new System.Drawing.Size(106, 40);
            this.iconButtonComentariosBorrar.TabIndex = 48;
            this.iconButtonComentariosBorrar.Text = "Modificar";
            this.iconButtonComentariosBorrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonComentariosBorrar.UseVisualStyleBackColor = false;
            this.iconButtonComentariosBorrar.Click += new System.EventHandler(this.iconButtonComentariosBorrar_Click);
            // 
            // Clientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.ClientSize = new System.Drawing.Size(613, 434);
            this.Controls.Add(this.iconButtonComentariosBorrar);
            this.Controls.Add(this.iconButtonComentariosGuardar);
            this.Controls.Add(this.iconButtonModificar2);
            this.Controls.Add(this.iconButtonBaja2);
            this.Controls.Add(this.iconButtonAlta2);
            this.Controls.Add(this.textBoxFiltrar);
            this.Controls.Add(this.dataGridViewClientes);
            this.Controls.Add(this.richTextBoxComentarios);
            this.Controls.Add(this.label9);
            this.Name = "Clientes";
            this.Text = "Clientes";
            this.Load += new System.EventHandler(this.Clientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox richTextBoxComentarios;
        private System.Windows.Forms.DataGridView dataGridViewClientes;
        private System.Windows.Forms.TextBox textBoxFiltrar;
        private FontAwesome.Sharp.IconButton iconButtonAlta2;
        private FontAwesome.Sharp.IconButton iconButtonBaja2;
        private FontAwesome.Sharp.IconButton iconButtonModificar2;
        private FontAwesome.Sharp.IconButton iconButtonComentariosGuardar;
        private FontAwesome.Sharp.IconButton iconButtonComentariosBorrar;
    }
}

