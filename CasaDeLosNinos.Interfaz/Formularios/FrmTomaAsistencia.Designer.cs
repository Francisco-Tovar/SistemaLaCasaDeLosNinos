namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FrmTomaAsistencia
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
            panelFecha = new Panel();
            txtBuscar = new TextBox();
            dtpFecha = new DateTimePicker();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            lblFecha = new Label();
            btnCargar = new FontAwesome.Sharp.IconButton();
            btnMarcarTodos = new FontAwesome.Sharp.IconButton();
            btnDesmarcarTodos = new FontAwesome.Sharp.IconButton();
            panelInferior = new Panel();
            lblResumen = new Label();
            lblEstado = new Label();
            grdAsistencia = new DataGridView();
            panelFecha.SuspendLayout();
            panelInferior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grdAsistencia).BeginInit();
            SuspendLayout();
            // 
            // panelFecha
            // 
            panelFecha.Controls.Add(txtBuscar);
            panelFecha.Controls.Add(dtpFecha);
            panelFecha.Controls.Add(btnGuardar);
            panelFecha.Controls.Add(lblFecha);
            panelFecha.Controls.Add(btnCargar);
            panelFecha.Controls.Add(btnMarcarTodos);
            panelFecha.Controls.Add(btnDesmarcarTodos);
            panelFecha.Dock = DockStyle.Top;
            panelFecha.Location = new Point(1, 1);
            panelFecha.Name = "panelFecha";
            panelFecha.Padding = new Padding(12, 9, 12, 5);
            panelFecha.Size = new Size(648, 48);
            panelFecha.TabIndex = 1;
            // 
            // txtBuscar
            // 
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscar.Location = new Point(175, 12);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "🔍  Buscar...";
            txtBuscar.Size = new Size(160, 25);
            txtBuscar.TabIndex = 2;
            txtBuscar.TextChanged += AlCambiarBusqueda;
            // 
            // dtpFecha
            // 
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(65, 12);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(100, 24);
            dtpFecha.TabIndex = 1;
            dtpFecha.ValueChanged += dtpFecha_ValueChanged;
            // 
            // btnGuardar
            // 
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnGuardar.IconColor = Color.Black;
            btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardar.IconSize = 24;
            btnGuardar.Location = new Point(539, 7);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(106, 32);
            btnGuardar.TabIndex = 5;
            btnGuardar.Text = "Guardar";
            btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += AlHacerClickEnGuardar;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(15, 15);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(44, 17);
            lblFecha.TabIndex = 0;
            lblFecha.Text = "Fecha:";
            // 
            // btnCargar
            // 
            btnCargar.FlatAppearance.BorderSize = 0;
            btnCargar.FlatStyle = FlatStyle.Flat;
            btnCargar.IconChar = FontAwesome.Sharp.IconChar.RotateRight;
            btnCargar.IconColor = Color.Black;
            btnCargar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCargar.IconSize = 24;
            btnCargar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCargar.Location = new Point(180, 6);
            btnCargar.Name = "btnCargar";
            btnCargar.Size = new Size(100, 36);
            btnCargar.TabIndex = 2;
            btnCargar.Text = "Cargar";
            btnCargar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCargar.UseVisualStyleBackColor = true;
            btnCargar.Visible = false;
            btnCargar.Click += AlHacerClickEnCargar;
            // 
            // btnMarcarTodos
            // 
            btnMarcarTodos.FlatAppearance.BorderSize = 0;
            btnMarcarTodos.IconChar = FontAwesome.Sharp.IconChar.CheckDouble;
            btnMarcarTodos.IconColor = Color.Black;
            btnMarcarTodos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnMarcarTodos.IconSize = 24;
            btnMarcarTodos.Location = new Point(341, 7);
            btnMarcarTodos.Name = "btnMarcarTodos";
            btnMarcarTodos.Size = new Size(89, 32);
            btnMarcarTodos.TabIndex = 3;
            btnMarcarTodos.Text = "Todos";
            btnMarcarTodos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnMarcarTodos.UseVisualStyleBackColor = true;
            btnMarcarTodos.Click += AlHacerClickEnMarcarTodos;
            // 
            // btnDesmarcarTodos
            // 
            btnDesmarcarTodos.FlatAppearance.BorderSize = 0;
            btnDesmarcarTodos.IconChar = FontAwesome.Sharp.IconChar.Eraser;
            btnDesmarcarTodos.IconColor = Color.Black;
            btnDesmarcarTodos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDesmarcarTodos.IconSize = 24;
            btnDesmarcarTodos.Location = new Point(435, 7);
            btnDesmarcarTodos.Name = "btnDesmarcarTodos";
            btnDesmarcarTodos.Size = new Size(98, 32);
            btnDesmarcarTodos.TabIndex = 4;
            btnDesmarcarTodos.Text = "Ninguno";
            btnDesmarcarTodos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDesmarcarTodos.UseVisualStyleBackColor = true;
            btnDesmarcarTodos.Click += AlHacerClickEnDesmarcarTodos;
            // 
            // panelInferior
            // 
            panelInferior.Controls.Add(lblResumen);
            panelInferior.Controls.Add(lblEstado);
            panelInferior.Dock = DockStyle.Bottom;
            panelInferior.Location = new Point(1, 550);
            panelInferior.Name = "panelInferior";
            panelInferior.Padding = new Padding(12, 8, 12, 6);
            panelInferior.Size = new Size(648, 30);
            panelInferior.TabIndex = 2;
            // 
            // lblResumen
            // 
            lblResumen.AutoSize = true;
            lblResumen.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblResumen.Location = new Point(12, 10);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(0, 17);
            lblResumen.TabIndex = 0;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblEstado.Location = new Point(12, 30);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(0, 15);
            lblEstado.TabIndex = 1;
            // 
            // grdAsistencia
            // 
            grdAsistencia.AllowUserToAddRows = false;
            grdAsistencia.AllowUserToDeleteRows = false;
            grdAsistencia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdAsistencia.BorderStyle = BorderStyle.None;
            grdAsistencia.ColumnHeadersHeight = 35;
            grdAsistencia.Dock = DockStyle.Fill;
            grdAsistencia.Location = new Point(1, 49);
            grdAsistencia.MultiSelect = false;
            grdAsistencia.Name = "grdAsistencia";
            grdAsistencia.RowHeadersVisible = false;
            grdAsistencia.RowTemplate.Height = 35;
            grdAsistencia.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdAsistencia.Size = new Size(648, 501);
            grdAsistencia.TabIndex = 3;
            // 
            // FrmTomaAsistencia
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(650, 581);
            Controls.Add(grdAsistencia);
            Controls.Add(panelInferior);
            Controls.Add(panelFecha);
            Font = new Font("Segoe UI", 9.5F);
            Name = "FrmTomaAsistencia";
            Text = "Toma de Asistencia";
            Load += FrmTomaAsistencia_Load;
            panelFecha.ResumeLayout(false);
            panelFecha.PerformLayout();
            panelInferior.ResumeLayout(false);
            panelInferior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)grdAsistencia).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelFecha;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private FontAwesome.Sharp.IconButton btnCargar;
        private FontAwesome.Sharp.IconButton btnMarcarTodos;
        private FontAwesome.Sharp.IconButton btnDesmarcarTodos;
        private System.Windows.Forms.Panel panelInferior;
        private System.Windows.Forms.Label lblResumen;
        private System.Windows.Forms.Label lblEstado;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView grdAsistencia;
    }
}
