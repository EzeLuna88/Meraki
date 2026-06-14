namespace Meraki
{
    partial class ConfiguracionListaPrecios
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.dataGridViewTipos = new System.Windows.Forms.DataGridView();
            this.iconButtonTipoQuitar = new FontAwesome.Sharp.IconButton();
            this.iconButtonTipoAgregar = new FontAwesome.Sharp.IconButton();
            this.dataGridViewProductosSinAsignar = new System.Windows.Forms.DataGridView();
            this.textBoxFiltrarAsignados = new System.Windows.Forms.TextBox();
            this.dataGridViewProductosAsignados = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBoxFiltrarSinAsignar = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.iconButtonQuitarAsignado = new FontAwesome.Sharp.IconButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTipos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductosSinAsignar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductosAsignados)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.panel4.Controls.Add(this.label8);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(987, 30);
            this.panel4.TabIndex = 78;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(2, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(269, 25);
            this.label8.TabIndex = 0;
            this.label8.Text = "Configuracion Lista de precios";
            // 
            // dataGridViewTipos
            // 
            this.dataGridViewTipos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTipos.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewTipos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewTipos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(8)))), ((int)(((byte)(21)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTipos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTipos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTipos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.dataGridViewTipos.Location = new System.Drawing.Point(3, 37);
            this.dataGridViewTipos.MultiSelect = false;
            this.dataGridViewTipos.Name = "dataGridViewTipos";
            this.dataGridViewTipos.ReadOnly = true;
            this.dataGridViewTipos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTipos.Size = new System.Drawing.Size(177, 417);
            this.dataGridViewTipos.TabIndex = 44;
            this.dataGridViewTipos.SelectionChanged += new System.EventHandler(this.dataGridViewTipos_SelectionChanged);
            // 
            // iconButtonTipoQuitar
            // 
            this.iconButtonTipoQuitar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButtonTipoQuitar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonTipoQuitar.FlatAppearance.BorderSize = 0;
            this.iconButtonTipoQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonTipoQuitar.IconChar = FontAwesome.Sharp.IconChar.Minus;
            this.iconButtonTipoQuitar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.iconButtonTipoQuitar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonTipoQuitar.IconSize = 30;
            this.iconButtonTipoQuitar.Location = new System.Drawing.Point(39, 463);
            this.iconButtonTipoQuitar.Name = "iconButtonTipoQuitar";
            this.iconButtonTipoQuitar.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.iconButtonTipoQuitar.Size = new System.Drawing.Size(30, 30);
            this.iconButtonTipoQuitar.TabIndex = 80;
            this.iconButtonTipoQuitar.UseVisualStyleBackColor = false;
            this.iconButtonTipoQuitar.Click += new System.EventHandler(this.iconButtonTipoQuitar_Click);
            // 
            // iconButtonTipoAgregar
            // 
            this.iconButtonTipoAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButtonTipoAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonTipoAgregar.FlatAppearance.BorderSize = 0;
            this.iconButtonTipoAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonTipoAgregar.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.iconButtonTipoAgregar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.iconButtonTipoAgregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonTipoAgregar.IconSize = 30;
            this.iconButtonTipoAgregar.Location = new System.Drawing.Point(3, 463);
            this.iconButtonTipoAgregar.Name = "iconButtonTipoAgregar";
            this.iconButtonTipoAgregar.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.iconButtonTipoAgregar.Size = new System.Drawing.Size(30, 30);
            this.iconButtonTipoAgregar.TabIndex = 79;
            this.iconButtonTipoAgregar.UseVisualStyleBackColor = false;
            this.iconButtonTipoAgregar.Click += new System.EventHandler(this.iconButtonTipoAgregar_Click);
            // 
            // dataGridViewProductosSinAsignar
            // 
            this.dataGridViewProductosSinAsignar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewProductosSinAsignar.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewProductosSinAsignar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewProductosSinAsignar.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(8)))), ((int)(((byte)(21)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProductosSinAsignar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewProductosSinAsignar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProductosSinAsignar.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.dataGridViewProductosSinAsignar.Location = new System.Drawing.Point(3, 37);
            this.dataGridViewProductosSinAsignar.MultiSelect = false;
            this.dataGridViewProductosSinAsignar.Name = "dataGridViewProductosSinAsignar";
            this.dataGridViewProductosSinAsignar.ReadOnly = true;
            this.dataGridViewProductosSinAsignar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewProductosSinAsignar.Size = new System.Drawing.Size(374, 418);
            this.dataGridViewProductosSinAsignar.TabIndex = 81;
            this.dataGridViewProductosSinAsignar.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewProductosSinAsignar_CellDoubleClick);
            // 
            // textBoxFiltrarAsignados
            // 
            this.textBoxFiltrarAsignados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFiltrarAsignados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxFiltrarAsignados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxFiltrarAsignados.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFiltrarAsignados.Location = new System.Drawing.Point(3, 469);
            this.textBoxFiltrarAsignados.Name = "textBoxFiltrarAsignados";
            this.textBoxFiltrarAsignados.Size = new System.Drawing.Size(374, 18);
            this.textBoxFiltrarAsignados.TabIndex = 82;
            this.textBoxFiltrarAsignados.TextChanged += new System.EventHandler(this.textBoxFiltrarAsignados_TextChanged);
            // 
            // dataGridViewProductosAsignados
            // 
            this.dataGridViewProductosAsignados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewProductosAsignados.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewProductosAsignados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewProductosAsignados.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(8)))), ((int)(((byte)(21)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProductosAsignados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewProductosAsignados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProductosAsignados.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.dataGridViewProductosAsignados.Location = new System.Drawing.Point(3, 37);
            this.dataGridViewProductosAsignados.MultiSelect = false;
            this.dataGridViewProductosAsignados.Name = "dataGridViewProductosAsignados";
            this.dataGridViewProductosAsignados.ReadOnly = true;
            this.dataGridViewProductosAsignados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewProductosAsignados.Size = new System.Drawing.Size(374, 390);
            this.dataGridViewProductosAsignados.TabIndex = 83;
            this.dataGridViewProductosAsignados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewProductosAsignados_CellDoubleClick);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.textBoxFiltrarSinAsignar);
            this.panel3.Controls.Add(this.dataGridViewProductosSinAsignar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(6, 6);
            this.panel3.Margin = new System.Windows.Forms.Padding(6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(382, 498);
            this.panel3.TabIndex = 87;
            // 
            // textBoxFiltrarSinAsignar
            // 
            this.textBoxFiltrarSinAsignar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFiltrarSinAsignar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxFiltrarSinAsignar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxFiltrarSinAsignar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFiltrarSinAsignar.Location = new System.Drawing.Point(3, 469);
            this.textBoxFiltrarSinAsignar.Name = "textBoxFiltrarSinAsignar";
            this.textBoxFiltrarSinAsignar.Size = new System.Drawing.Size(374, 18);
            this.textBoxFiltrarSinAsignar.TabIndex = 87;
            this.textBoxFiltrarSinAsignar.TextChanged += new System.EventHandler(this.textBoxFiltrarSinAsignar_TextChanged);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.dataGridViewTipos);
            this.panel5.Controls.Add(this.iconButtonTipoAgregar);
            this.panel5.Controls.Add(this.iconButtonTipoQuitar);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(400, 6);
            this.panel5.Margin = new System.Windows.Forms.Padding(6);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(185, 498);
            this.panel5.TabIndex = 88;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.iconButtonQuitarAsignado);
            this.panel6.Controls.Add(this.textBoxFiltrarAsignados);
            this.panel6.Controls.Add(this.dataGridViewProductosAsignados);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(597, 6);
            this.panel6.Margin = new System.Windows.Forms.Padding(6);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(384, 498);
            this.panel6.TabIndex = 89;
            // 
            // iconButtonQuitarAsignado
            // 
            this.iconButtonQuitarAsignado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iconButtonQuitarAsignado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonQuitarAsignado.FlatAppearance.BorderSize = 0;
            this.iconButtonQuitarAsignado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonQuitarAsignado.IconChar = FontAwesome.Sharp.IconChar.Minus;
            this.iconButtonQuitarAsignado.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.iconButtonQuitarAsignado.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonQuitarAsignado.IconSize = 30;
            this.iconButtonQuitarAsignado.Location = new System.Drawing.Point(3, 433);
            this.iconButtonQuitarAsignado.Name = "iconButtonQuitarAsignado";
            this.iconButtonQuitarAsignado.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.iconButtonQuitarAsignado.Size = new System.Drawing.Size(30, 30);
            this.iconButtonQuitarAsignado.TabIndex = 87;
            this.iconButtonQuitarAsignado.UseVisualStyleBackColor = false;
            this.iconButtonQuitarAsignado.Click += new System.EventHandler(this.iconButtonQuitarAsignado_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.panel6, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 30);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(987, 510);
            this.tableLayoutPanel1.TabIndex = 90;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.label2.Location = new System.Drawing.Point(1, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(241, 25);
            this.label2.TabIndex = 64;
            this.label2.Text = "PRODUCTOS ASIGNADOS";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.label3.Location = new System.Drawing.Point(0, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(251, 25);
            this.label3.TabIndex = 64;
            this.label3.Text = "PRODUCTOS SIN ASIGNAR";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.label1.Location = new System.Drawing.Point(56, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 25);
            this.label1.TabIndex = 64;
            this.label1.Text = "TIPOS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // ConfiguracionListaPrecios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.ClientSize = new System.Drawing.Size(987, 540);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ConfiguracionListaPrecios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ConfiguracionListaPrecios";
            this.Load += new System.EventHandler(this.ConfiguracionListaPrecios_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTipos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductosSinAsignar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductosAsignados)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dataGridViewTipos;
        private FontAwesome.Sharp.IconButton iconButtonTipoQuitar;
        private FontAwesome.Sharp.IconButton iconButtonTipoAgregar;
        private System.Windows.Forms.DataGridView dataGridViewProductosSinAsignar;
        private System.Windows.Forms.TextBox textBoxFiltrarAsignados;
        private System.Windows.Forms.DataGridView dataGridViewProductosAsignados;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxFiltrarSinAsignar;
        private FontAwesome.Sharp.IconButton iconButtonQuitarAsignado;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}