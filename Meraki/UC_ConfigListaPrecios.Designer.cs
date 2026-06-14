namespace Meraki
{
    partial class UC_ConfigListaPrecios
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.iconButtonQuitarAsignado = new FontAwesome.Sharp.IconButton();
            this.textBoxFiltrarAsignados = new System.Windows.Forms.TextBox();
            this.dataGridViewProductosAsignados = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxFiltrarSinAsignar = new System.Windows.Forms.TextBox();
            this.dataGridViewProductosSinAsignar = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewTipos = new System.Windows.Forms.DataGridView();
            this.iconButtonTipoAgregar = new FontAwesome.Sharp.IconButton();
            this.iconButtonTipoQuitar = new FontAwesome.Sharp.IconButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.iconButtonGenerarPDF = new FontAwesome.Sharp.IconButton();
            this.iconButtonImprimir = new FontAwesome.Sharp.IconButton();
            this.labelClienteActual = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductosAsignados)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductosSinAsignar)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTipos)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 62);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(881, 481);
            this.tableLayoutPanel1.TabIndex = 92;
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
            this.panel6.Location = new System.Drawing.Point(534, 6);
            this.panel6.Margin = new System.Windows.Forms.Padding(6);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(341, 469);
            this.panel6.TabIndex = 89;
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
            this.iconButtonQuitarAsignado.Location = new System.Drawing.Point(3, 404);
            this.iconButtonQuitarAsignado.Name = "iconButtonQuitarAsignado";
            this.iconButtonQuitarAsignado.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.iconButtonQuitarAsignado.Size = new System.Drawing.Size(30, 30);
            this.iconButtonQuitarAsignado.TabIndex = 87;
            this.iconButtonQuitarAsignado.UseVisualStyleBackColor = false;
            this.iconButtonQuitarAsignado.Click += new System.EventHandler(this.iconButtonQuitarAsignado_Click_1);
            // 
            // textBoxFiltrarAsignados
            // 
            this.textBoxFiltrarAsignados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFiltrarAsignados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxFiltrarAsignados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxFiltrarAsignados.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFiltrarAsignados.Location = new System.Drawing.Point(3, 440);
            this.textBoxFiltrarAsignados.Name = "textBoxFiltrarAsignados";
            this.textBoxFiltrarAsignados.Size = new System.Drawing.Size(331, 22);
            this.textBoxFiltrarAsignados.TabIndex = 82;
            this.textBoxFiltrarAsignados.TextChanged += new System.EventHandler(this.textBoxFiltrarAsignados_TextChanged_1);
            // 
            // dataGridViewProductosAsignados
            // 
            this.dataGridViewProductosAsignados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewProductosAsignados.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewProductosAsignados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewProductosAsignados.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(8)))), ((int)(((byte)(21)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProductosAsignados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewProductosAsignados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProductosAsignados.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.dataGridViewProductosAsignados.Location = new System.Drawing.Point(3, 37);
            this.dataGridViewProductosAsignados.MultiSelect = false;
            this.dataGridViewProductosAsignados.Name = "dataGridViewProductosAsignados";
            this.dataGridViewProductosAsignados.ReadOnly = true;
            this.dataGridViewProductosAsignados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewProductosAsignados.Size = new System.Drawing.Size(331, 361);
            this.dataGridViewProductosAsignados.TabIndex = 83;
            this.dataGridViewProductosAsignados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewProductosAsignados_CellDoubleClick_1);
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
            this.panel3.Size = new System.Drawing.Size(340, 469);
            this.panel3.TabIndex = 87;
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
            // 
            // textBoxFiltrarSinAsignar
            // 
            this.textBoxFiltrarSinAsignar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFiltrarSinAsignar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxFiltrarSinAsignar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxFiltrarSinAsignar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFiltrarSinAsignar.Location = new System.Drawing.Point(3, 440);
            this.textBoxFiltrarSinAsignar.Name = "textBoxFiltrarSinAsignar";
            this.textBoxFiltrarSinAsignar.Size = new System.Drawing.Size(332, 22);
            this.textBoxFiltrarSinAsignar.TabIndex = 87;
            this.textBoxFiltrarSinAsignar.TextChanged += new System.EventHandler(this.textBoxFiltrarSinAsignar_TextChanged_1);
            // 
            // dataGridViewProductosSinAsignar
            // 
            this.dataGridViewProductosSinAsignar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewProductosSinAsignar.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewProductosSinAsignar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewProductosSinAsignar.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(8)))), ((int)(((byte)(21)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProductosSinAsignar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewProductosSinAsignar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProductosSinAsignar.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.dataGridViewProductosSinAsignar.Location = new System.Drawing.Point(3, 37);
            this.dataGridViewProductosSinAsignar.MultiSelect = false;
            this.dataGridViewProductosSinAsignar.Name = "dataGridViewProductosSinAsignar";
            this.dataGridViewProductosSinAsignar.ReadOnly = true;
            this.dataGridViewProductosSinAsignar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewProductosSinAsignar.Size = new System.Drawing.Size(332, 389);
            this.dataGridViewProductosSinAsignar.TabIndex = 81;
            this.dataGridViewProductosSinAsignar.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewProductosSinAsignar_CellDoubleClick_1);
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
            this.panel5.Location = new System.Drawing.Point(358, 6);
            this.panel5.Margin = new System.Windows.Forms.Padding(6);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(164, 469);
            this.panel5.TabIndex = 88;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 34);
            this.label1.TabIndex = 64;
            this.label1.Text = "TIPOS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridViewTipos
            // 
            this.dataGridViewTipos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTipos.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewTipos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewTipos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(8)))), ((int)(((byte)(21)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTipos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTipos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTipos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.dataGridViewTipos.Location = new System.Drawing.Point(3, 37);
            this.dataGridViewTipos.MultiSelect = false;
            this.dataGridViewTipos.Name = "dataGridViewTipos";
            this.dataGridViewTipos.ReadOnly = true;
            this.dataGridViewTipos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTipos.Size = new System.Drawing.Size(156, 388);
            this.dataGridViewTipos.TabIndex = 44;
            this.dataGridViewTipos.SelectionChanged += new System.EventHandler(this.dataGridViewTipos_SelectionChanged_1);
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
            this.iconButtonTipoAgregar.Location = new System.Drawing.Point(3, 434);
            this.iconButtonTipoAgregar.Name = "iconButtonTipoAgregar";
            this.iconButtonTipoAgregar.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.iconButtonTipoAgregar.Size = new System.Drawing.Size(30, 30);
            this.iconButtonTipoAgregar.TabIndex = 79;
            this.iconButtonTipoAgregar.UseVisualStyleBackColor = false;
            this.iconButtonTipoAgregar.Click += new System.EventHandler(this.iconButtonTipoAgregar_Click_1);
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
            this.iconButtonTipoQuitar.Location = new System.Drawing.Point(39, 434);
            this.iconButtonTipoQuitar.Name = "iconButtonTipoQuitar";
            this.iconButtonTipoQuitar.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.iconButtonTipoQuitar.Size = new System.Drawing.Size(30, 30);
            this.iconButtonTipoQuitar.TabIndex = 80;
            this.iconButtonTipoQuitar.UseVisualStyleBackColor = false;
            this.iconButtonTipoQuitar.Click += new System.EventHandler(this.iconButtonTipoQuitar_Click_1);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.iconButtonGenerarPDF);
            this.panel4.Controls.Add(this.iconButtonImprimir);
            this.panel4.Controls.Add(this.labelClienteActual);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(15, 15);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.panel4.Size = new System.Drawing.Size(881, 47);
            this.panel4.TabIndex = 91;
            // 
            // iconButtonGenerarPDF
            // 
            this.iconButtonGenerarPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconButtonGenerarPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonGenerarPDF.FlatAppearance.BorderSize = 0;
            this.iconButtonGenerarPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonGenerarPDF.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonGenerarPDF.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonGenerarPDF.IconChar = FontAwesome.Sharp.IconChar.Upload;
            this.iconButtonGenerarPDF.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonGenerarPDF.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonGenerarPDF.IconSize = 30;
            this.iconButtonGenerarPDF.Location = new System.Drawing.Point(607, 0);
            this.iconButtonGenerarPDF.Margin = new System.Windows.Forms.Padding(5);
            this.iconButtonGenerarPDF.Name = "iconButtonGenerarPDF";
            this.iconButtonGenerarPDF.Size = new System.Drawing.Size(120, 47);
            this.iconButtonGenerarPDF.TabIndex = 74;
            this.iconButtonGenerarPDF.Text = "Actualizar lista";
            this.iconButtonGenerarPDF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonGenerarPDF.UseVisualStyleBackColor = false;
            this.iconButtonGenerarPDF.Click += new System.EventHandler(this.iconButtonGenerarPDF_Click);
            // 
            // iconButtonImprimir
            // 
            this.iconButtonImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconButtonImprimir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(26)))), ((int)(((byte)(64)))));
            this.iconButtonImprimir.FlatAppearance.BorderSize = 0;
            this.iconButtonImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButtonImprimir.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButtonImprimir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonImprimir.IconChar = FontAwesome.Sharp.IconChar.Print;
            this.iconButtonImprimir.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(217)))), ((int)(((byte)(208)))));
            this.iconButtonImprimir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButtonImprimir.IconSize = 30;
            this.iconButtonImprimir.Location = new System.Drawing.Point(741, 0);
            this.iconButtonImprimir.Margin = new System.Windows.Forms.Padding(5);
            this.iconButtonImprimir.Name = "iconButtonImprimir";
            this.iconButtonImprimir.Size = new System.Drawing.Size(125, 47);
            this.iconButtonImprimir.TabIndex = 73;
            this.iconButtonImprimir.Text = "Imprimir";
            this.iconButtonImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButtonImprimir.UseVisualStyleBackColor = false;
            this.iconButtonImprimir.Click += new System.EventHandler(this.iconButtonImprimir_Click);
            // 
            // labelClienteActual
            // 
            this.labelClienteActual.AutoSize = true;
            this.labelClienteActual.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClienteActual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(15)))), ((int)(((byte)(28)))));
            this.labelClienteActual.Location = new System.Drawing.Point(3, 3);
            this.labelClienteActual.Name = "labelClienteActual";
            this.labelClienteActual.Size = new System.Drawing.Size(404, 37);
            this.labelClienteActual.TabIndex = 2;
            this.labelClienteActual.Text = "Configuración Lista de precios";
            // 
            // UC_ConfigListaPrecios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel4);
            this.Name = "UC_ConfigListaPrecios";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.Size = new System.Drawing.Size(911, 558);
            this.Load += new System.EventHandler(this.UC_ConfigListaPrecios_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductosAsignados)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductosSinAsignar)).EndInit();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTipos)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private FontAwesome.Sharp.IconButton iconButtonQuitarAsignado;
        private System.Windows.Forms.TextBox textBoxFiltrarAsignados;
        private System.Windows.Forms.DataGridView dataGridViewProductosAsignados;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxFiltrarSinAsignar;
        private System.Windows.Forms.DataGridView dataGridViewProductosSinAsignar;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewTipos;
        private FontAwesome.Sharp.IconButton iconButtonTipoAgregar;
        private FontAwesome.Sharp.IconButton iconButtonTipoQuitar;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label labelClienteActual;
        private FontAwesome.Sharp.IconButton iconButtonImprimir;
        private FontAwesome.Sharp.IconButton iconButtonGenerarPDF;
    }
}
