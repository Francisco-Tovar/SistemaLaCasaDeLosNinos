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
            dtpFecha = new DateTimePicker();
            lblFecha = new Label();
            btnCargar = new FontAwesome.Sharp.IconButton();
            btnMarcarTodos = new FontAwesome.Sharp.IconButton();
            btnDesmarcarTodos = new FontAwesome.Sharp.IconButton();
            panelInferior = new Panel();
            lblResumen = new Label();
            lblEstado = new Label();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            grdAsistencia = new DataGridView();
            panelFecha.SuspendLayout();
            panelInferior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grdAsistencia).BeginInit();
            SuspendLayout();
            // 
            // panelFecha
            // 
            panelFecha.BackColor = Color.FromArgb(34, 33, 74);
            panelFecha.Controls.Add(dtpFecha);
            panelFecha.Controls.Add(lblFecha);
            panelFecha.Controls.Add(btnCargar);
            panelFecha.Controls.Add(btnMarcarTodos);
            panelFecha.Controls.Add(btnDesmarcarTodos);
            panelFecha.Dock = DockStyle.Top;
            panelFecha.Location = new Point(0, 0);
            panelFecha.Name = "panelFecha";
            panelFecha.Padding = new Padding(12, 9, 12, 5);
            panelFecha.Size = new Size(650, 48);
            panelFecha.TabIndex = 1;
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
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.ForeColor = Color.Gainsboro;
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
            btnCargar.ForeColor = Color.Gainsboro;
            btnCargar.IconChar = FontAwesome.Sharp.IconChar.SyncAlt;
            btnCargar.IconColor = Color.FromArgb(52, 152, 219);
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
            btnCargar.Click += AlHacerClickEnCargar;
            // 
            // btnMarcarTodos
            // 
            btnMarcarTodos.FlatAppearance.BorderSize = 0;
            btnMarcarTodos.FlatStyle = FlatStyle.Flat;
            btnMarcarTodos.ForeColor = Color.Gainsboro;
            btnMarcarTodos.IconChar = FontAwesome.Sharp.IconChar.CheckDouble;
            btnMarcarTodos.IconColor = Color.FromArgb(46, 204, 113);
            btnMarcarTodos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnMarcarTodos.IconSize = 24;
            btnMarcarTodos.ImageAlign = ContentAlignment.MiddleLeft;
            btnMarcarTodos.Location = new Point(280, 6);
            btnMarcarTodos.Name = "btnMarcarTodos";
            btnMarcarTodos.Size = new Size(110, 36);
            btnMarcarTodos.TabIndex = 3;
            btnMarcarTodos.Text = "Todos";
            btnMarcarTodos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnMarcarTodos.UseVisualStyleBackColor = true;
            btnMarcarTodos.Click += AlHacerClickEnMarcarTodos;
            // 
            // btnDesmarcarTodos
            // 
            btnDesmarcarTodos.FlatAppearance.BorderSize = 0;
            btnDesmarcarTodos.FlatStyle = FlatStyle.Flat;
            btnDesmarcarTodos.ForeColor = Color.Gainsboro;
            btnDesmarcarTodos.IconChar = FontAwesome.Sharp.IconChar.Square;
            btnDesmarcarTodos.IconColor = Color.FromArgb(231, 76, 60);
            btnDesmarcarTodos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDesmarcarTodos.IconSize = 24;
            btnDesmarcarTodos.ImageAlign = ContentAlignment.MiddleLeft;
            btnDesmarcarTodos.Location = new Point(390, 6);
            btnDesmarcarTodos.Name = "btnDesmarcarTodos";
            btnDesmarcarTodos.Size = new Size(120, 36);
            btnDesmarcarTodos.TabIndex = 4;
            btnDesmarcarTodos.Text = "Ninguno";
            btnDesmarcarTodos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDesmarcarTodos.UseVisualStyleBackColor = true;
            btnDesmarcarTodos.Click += AlHacerClickEnDesmarcarTodos;
            // 
            // panelInferior
            // 
            panelInferior.BackColor = Color.FromArgb(26, 25, 62);
            panelInferior.Controls.Add(lblResumen);
            panelInferior.Controls.Add(lblEstado);
            panelInferior.Controls.Add(btnGuardar);
            panelInferior.Dock = DockStyle.Bottom;
            panelInferior.Location = new Point(0, 527);
            panelInferior.Name = "panelInferior";
            panelInferior.Padding = new Padding(12, 8, 12, 6);
            panelInferior.Size = new Size(650, 54);
            panelInferior.TabIndex = 2;
            // 
            // lblResumen
            // 
            lblResumen.AutoSize = true;
            lblResumen.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblResumen.ForeColor = Color.Gainsboro;
            lblResumen.Location = new Point(12, 10);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(0, 17);
            lblResumen.TabIndex = 0;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblEstado.ForeColor = Color.Silver;
            lblEstado.Location = new Point(12, 30);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(0, 15);
            lblEstado.TabIndex = 1;
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.Gainsboro;
            btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnGuardar.IconColor = Color.FromArgb(172, 126, 241);
            btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardar.IconSize = 24;
            btnGuardar.ImageAlign = ContentAlignment.MiddleLeft;
            btnGuardar.Location = new Point(470, 8);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(160, 36);
            btnGuardar.TabIndex = 2;
            btnGuardar.Text = "Guardar";
            btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += AlHacerClickEnGuardar;
            // 
            // grdAsistencia
            // 
            grdAsistencia.AllowUserToAddRows = false;
            grdAsistencia.AllowUserToDeleteRows = false;
            grdAsistencia.BackgroundColor = Color.FromArgb(34, 33, 74);
            grdAsistencia.BorderStyle = BorderStyle.None;
            grdAsistencia.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grdAsistencia.ColumnHeadersHeight = 35;
            grdAsistencia.Dock = DockStyle.Fill;
            grdAsistencia.EnableHeadersVisualStyles = false;
            grdAsistencia.GridColor = Color.FromArgb(45, 45, 81);
            grdAsistencia.Location = new Point(0, 48);
            grdAsistencia.MultiSelect = false;
            grdAsistencia.Name = "grdAsistencia";
            grdAsistencia.RowHeadersVisible = false;
            grdAsistencia.RowTemplate.Height = 35;
            grdAsistencia.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdAsistencia.Size = new Size(650, 479);
            grdAsistencia.TabIndex = 3;
            // 
            // FrmTomaAsistencia
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 33, 74);
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
        private System.Windows.Forms.DataGridView grdAsistencia;
    }
}
