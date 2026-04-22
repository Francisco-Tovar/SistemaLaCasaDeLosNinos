namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FrmBitacoraSistema
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            panelCabecera = new Panel();
            btnLimpiar = new FontAwesome.Sharp.IconButton();
            lblDesde = new Label();
            txtBuscar = new TextBox();
            dtpDesde = new DateTimePicker();
            lblBuscar = new Label();
            lblHasta = new Label();
            cboModulo = new ComboBox();
            dtpHasta = new DateTimePicker();
            lblModulo = new Label();
            btnFiltrar = new FontAwesome.Sharp.IconButton();
            panelFiltros = new Panel();
            dgvBitacora = new DataGridView();
            panelFooter = new Panel();
            lblResultados = new Label();
            lblCargando = new Label();
            panelCabecera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).BeginInit();
            panelFooter.SuspendLayout();
            SuspendLayout();
            // 
            // panelCabecera
            // 
            panelCabecera.Controls.Add(btnLimpiar);
            panelCabecera.Controls.Add(lblDesde);
            panelCabecera.Controls.Add(txtBuscar);
            panelCabecera.Controls.Add(dtpDesde);
            panelCabecera.Controls.Add(lblBuscar);
            panelCabecera.Controls.Add(lblHasta);
            panelCabecera.Controls.Add(cboModulo);
            panelCabecera.Controls.Add(dtpHasta);
            panelCabecera.Controls.Add(lblModulo);
            panelCabecera.Controls.Add(btnFiltrar);
            panelCabecera.Dock = DockStyle.Top;
            panelCabecera.Location = new Point(1, 1);
            panelCabecera.Name = "panelCabecera";
            panelCabecera.Size = new Size(1078, 65);
            panelCabecera.TabIndex = 0;
            panelCabecera.MouseDown += panelCabecera_MouseDown;
            // 
            // btnLimpiar
            // 
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLimpiar.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            btnLimpiar.IconColor = Color.Black;
            btnLimpiar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnLimpiar.IconSize = 24;
            btnLimpiar.Location = new Point(770, 20);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(121, 32);
            btnLimpiar.TabIndex = 9;
            btnLimpiar.Text = "Limpiar 90d";
            btnLimpiar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // lblDesde
            // 
            lblDesde.AutoSize = true;
            lblDesde.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDesde.Location = new Point(13, 3);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(42, 15);
            lblDesde.TabIndex = 0;
            lblDesde.Text = "Desde";
            // 
            // txtBuscar
            // 
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.Location = new Point(458, 32);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "🔍  Buscar...";
            txtBuscar.Size = new Size(200, 25);
            txtBuscar.TabIndex = 8;
            txtBuscar.TextChanged += AlCambiarBusqueda;
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(10, 32);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(150, 25);
            dtpDesde.TabIndex = 1;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBuscar.Location = new Point(461, 3);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(126, 15);
            lblBuscar.TabIndex = 7;
            lblBuscar.Text = "Buscar (User/Detalle)";
            // 
            // lblHasta
            // 
            lblHasta.AutoSize = true;
            lblHasta.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHasta.Location = new Point(169, 3);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(38, 15);
            lblHasta.TabIndex = 2;
            lblHasta.Text = "Hasta";
            // 
            // cboModulo
            // 
            cboModulo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboModulo.FlatStyle = FlatStyle.Flat;
            cboModulo.FormattingEnabled = true;
            cboModulo.Location = new Point(322, 32);
            cboModulo.Name = "cboModulo";
            cboModulo.Size = new Size(130, 25);
            cboModulo.TabIndex = 6;
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(166, 32);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(150, 25);
            dtpHasta.TabIndex = 3;
            // 
            // lblModulo
            // 
            lblModulo.AutoSize = true;
            lblModulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblModulo.Location = new Point(325, 3);
            lblModulo.Name = "lblModulo";
            lblModulo.Size = new Size(49, 15);
            lblModulo.TabIndex = 5;
            lblModulo.Text = "Módulo";
            // 
            // btnFiltrar
            // 
            btnFiltrar.FlatAppearance.BorderSize = 0;
            btnFiltrar.IconChar = FontAwesome.Sharp.IconChar.Filter;
            btnFiltrar.IconColor = Color.Black;
            btnFiltrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnFiltrar.IconSize = 20;
            btnFiltrar.Location = new Point(664, 20);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(100, 32);
            btnFiltrar.TabIndex = 4;
            btnFiltrar.Text = "Consultar";
            btnFiltrar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnFiltrar.UseVisualStyleBackColor = true;
            btnFiltrar.Click += btnFiltrar_Click;
            // 
            // panelFiltros
            // 
            panelFiltros.Dock = DockStyle.Top;
            panelFiltros.Location = new Point(1, 66);
            panelFiltros.Name = "panelFiltros";
            panelFiltros.Size = new Size(1078, 1);
            panelFiltros.TabIndex = 10;
            // 
            // dgvBitacora
            // 
            dgvBitacora.AllowUserToAddRows = false;
            dgvBitacora.AllowUserToDeleteRows = false;
            dgvBitacora.AllowUserToResizeRows = false;
            dgvBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBitacora.BackgroundColor = Color.White;
            dgvBitacora.BorderStyle = BorderStyle.None;
            dgvBitacora.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvBitacora.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvBitacora.ColumnHeadersHeight = 30;
            dgvBitacora.Dock = DockStyle.Fill;
            dgvBitacora.Location = new Point(1, 67);
            dgvBitacora.MultiSelect = false;
            dgvBitacora.Name = "dgvBitacora";
            dgvBitacora.ReadOnly = true;
            dgvBitacora.RowHeadersVisible = false;
            dgvBitacora.RowTemplate.Height = 35;
            dgvBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBitacora.Size = new Size(1078, 502);
            dgvBitacora.TabIndex = 11;
            // 
            // panelFooter
            // 
            panelFooter.Controls.Add(lblResultados);
            panelFooter.Controls.Add(lblCargando);
            panelFooter.Dock = DockStyle.Bottom;
            panelFooter.Location = new Point(1, 569);
            panelFooter.Name = "panelFooter";
            panelFooter.Padding = new Padding(10, 5, 10, 0);
            panelFooter.Size = new Size(1078, 30);
            panelFooter.TabIndex = 12;
            // 
            // lblResultados
            // 
            lblResultados.AutoSize = true;
            lblResultados.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblResultados.Location = new Point(10, 5);
            lblResultados.Name = "lblResultados";
            lblResultados.Size = new Size(135, 15);
            lblResultados.TabIndex = 0;
            lblResultados.Text = "Registros encontrados: 0";
            // 
            // lblCargando
            // 
            lblCargando.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCargando.AutoSize = true;
            lblCargando.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblCargando.Location = new Point(980, 5);
            lblCargando.Name = "lblCargando";
            lblCargando.Size = new Size(60, 15);
            lblCargando.TabIndex = 1;
            lblCargando.Text = "Cargando...";
            lblCargando.Visible = false;
            // 
            // FrmBitacoraSistema
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1080, 600);
            Controls.Add(dgvBitacora);
            Controls.Add(panelFooter);
            Controls.Add(panelFiltros);
            Controls.Add(panelCabecera);
            Font = new Font("Segoe UI", 9.5F);
            Name = "FrmBitacoraSistema";
            Text = "Bitácora del Sistema";
            Load += FrmBitacoraSistema_Load;
            panelCabecera.ResumeLayout(false);
            panelCabecera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).EndInit();
            panelFooter.ResumeLayout(false);
            panelFooter.PerformLayout();
            ResumeLayout(false);
        }

        private Panel panelCabecera;
        private Panel panelFiltros;
        private DateTimePicker dtpDesde;
        private Label lblDesde;
        private DateTimePicker dtpHasta;
        private Label lblHasta;
        private FontAwesome.Sharp.IconButton btnFiltrar;
        private ComboBox cboModulo;
        private Label lblModulo;
        private TextBox txtBuscar;
        private Label lblBuscar;
        private DataGridView dgvBitacora;
        private Panel panelFooter;
        private Label lblResultados;
        private Label lblCargando;
        private FontAwesome.Sharp.IconButton btnLimpiar;
    }
}
