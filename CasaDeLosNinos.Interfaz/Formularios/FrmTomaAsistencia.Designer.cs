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
            this.panelCabecera = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelFecha = new System.Windows.Forms.Panel();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lblFecha = new System.Windows.Forms.Label();
            this.btnCargar = new FontAwesome.Sharp.IconButton();
            this.btnMarcarTodos = new FontAwesome.Sharp.IconButton();
            this.btnDesmarcarTodos = new FontAwesome.Sharp.IconButton();
            this.panelInferior = new System.Windows.Forms.Panel();
            this.lblResumen = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.btnGuardar = new FontAwesome.Sharp.IconButton();
            this.grdAsistencia = new System.Windows.Forms.DataGridView();
            this.panelCabecera.SuspendLayout();
            this.panelFecha.SuspendLayout();
            this.panelInferior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAsistencia)).BeginInit();
            this.SuspendLayout();
            // 
            // panelCabecera
            // 
            this.panelCabecera.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(62)))));
            this.panelCabecera.Controls.Add(this.lblTitulo);
            this.panelCabecera.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCabecera.Location = new System.Drawing.Point(0, 0);
            this.panelCabecera.Name = "panelCabecera";
            this.panelCabecera.Size = new System.Drawing.Size(650, 50);
            this.panelCabecera.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(12, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(222, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "📋  Toma de Asistencia";
            // 
            // panelFecha
            // 
            this.panelFecha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(74)))));
            this.panelFecha.Controls.Add(this.dtpFecha);
            this.panelFecha.Controls.Add(this.lblFecha);
            this.panelFecha.Controls.Add(this.btnCargar);
            this.panelFecha.Controls.Add(this.btnMarcarTodos);
            this.panelFecha.Controls.Add(this.btnDesmarcarTodos);
            this.panelFecha.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFecha.Location = new System.Drawing.Point(0, 50);
            this.panelFecha.Name = "panelFecha";
            this.panelFecha.Padding = new System.Windows.Forms.Padding(12, 9, 12, 5);
            this.panelFecha.Size = new System.Drawing.Size(650, 48);
            this.panelFecha.TabIndex = 1;
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(65, 12);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(100, 24);
            this.dtpFecha.TabIndex = 1;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblFecha.Location = new System.Drawing.Point(15, 15);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(44, 17);
            this.lblFecha.TabIndex = 0;
            this.lblFecha.Text = "Fecha:";
            // 
            // btnCargar
            // 
            this.btnCargar.FlatAppearance.BorderSize = 0;
            this.btnCargar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCargar.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnCargar.IconChar = FontAwesome.Sharp.IconChar.Rotate;
            this.btnCargar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnCargar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCargar.IconSize = 24;
            this.btnCargar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCargar.Location = new System.Drawing.Point(180, 6);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(100, 36);
            this.btnCargar.TabIndex = 2;
            this.btnCargar.Text = "Cargar";
            this.btnCargar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.AlHacerClickEnCargar);
            // 
            // btnMarcarTodos
            // 
            this.btnMarcarTodos.FlatAppearance.BorderSize = 0;
            this.btnMarcarTodos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarcarTodos.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnMarcarTodos.IconChar = FontAwesome.Sharp.IconChar.CheckDouble;
            this.btnMarcarTodos.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnMarcarTodos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMarcarTodos.IconSize = 24;
            this.btnMarcarTodos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMarcarTodos.Location = new System.Drawing.Point(280, 6);
            this.btnMarcarTodos.Name = "btnMarcarTodos";
            this.btnMarcarTodos.Size = new System.Drawing.Size(110, 36);
            this.btnMarcarTodos.TabIndex = 3;
            this.btnMarcarTodos.Text = "Todos";
            this.btnMarcarTodos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMarcarTodos.UseVisualStyleBackColor = true;
            this.btnMarcarTodos.Click += new System.EventHandler(this.AlHacerClickEnMarcarTodos);
            // 
            // btnDesmarcarTodos
            // 
            this.btnDesmarcarTodos.FlatAppearance.BorderSize = 0;
            this.btnDesmarcarTodos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDesmarcarTodos.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnDesmarcarTodos.IconChar = FontAwesome.Sharp.IconChar.Square;
            this.btnDesmarcarTodos.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDesmarcarTodos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDesmarcarTodos.IconSize = 24;
            this.btnDesmarcarTodos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDesmarcarTodos.Location = new System.Drawing.Point(390, 6);
            this.btnDesmarcarTodos.Name = "btnDesmarcarTodos";
            this.btnDesmarcarTodos.Size = new System.Drawing.Size(120, 36);
            this.btnDesmarcarTodos.TabIndex = 4;
            this.btnDesmarcarTodos.Text = "Ninguno";
            this.btnDesmarcarTodos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDesmarcarTodos.UseVisualStyleBackColor = true;
            this.btnDesmarcarTodos.Click += new System.EventHandler(this.AlHacerClickEnDesmarcarTodos);
            // 
            // panelInferior
            // 
            this.panelInferior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(62)))));
            this.panelInferior.Controls.Add(this.lblResumen);
            this.panelInferior.Controls.Add(this.lblEstado);
            this.panelInferior.Controls.Add(this.btnGuardar);
            this.panelInferior.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelInferior.Location = new System.Drawing.Point(0, 527);
            this.panelInferior.Name = "panelInferior";
            this.panelInferior.Padding = new System.Windows.Forms.Padding(12, 8, 12, 6);
            this.panelInferior.Size = new System.Drawing.Size(650, 54);
            this.panelInferior.TabIndex = 2;
            // 
            // lblResumen
            // 
            this.lblResumen.AutoSize = true;
            this.lblResumen.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblResumen.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblResumen.Location = new System.Drawing.Point(12, 10);
            this.lblResumen.Name = "lblResumen";
            this.lblResumen.Size = new System.Drawing.Size(0, 17);
            this.lblResumen.TabIndex = 0;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblEstado.ForeColor = System.Drawing.Color.Silver;
            this.lblEstado.Location = new System.Drawing.Point(12, 30);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(0, 15);
            this.lblEstado.TabIndex = 1;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnGuardar.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
            this.btnGuardar.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(126)))), ((int)(((byte)(241)))));
            this.btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnGuardar.IconSize = 24;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(470, 8);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(160, 36);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.AlHacerClickEnGuardar);
            // 
            // grdAsistencia
            // 
            this.grdAsistencia.AllowUserToAddRows = false;
            this.grdAsistencia.AllowUserToDeleteRows = false;
            this.grdAsistencia.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(74)))));
            this.grdAsistencia.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdAsistencia.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.grdAsistencia.ColumnHeadersHeight = 35;
            this.grdAsistencia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAsistencia.EnableHeadersVisualStyles = false;
            this.grdAsistencia.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(81)))));
            this.grdAsistencia.Location = new System.Drawing.Point(0, 98);
            this.grdAsistencia.MultiSelect = false;
            this.grdAsistencia.Name = "grdAsistencia";
            this.grdAsistencia.RowHeadersVisible = false;
            this.grdAsistencia.RowTemplate.Height = 35;
            this.grdAsistencia.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdAsistencia.Size = new System.Drawing.Size(650, 429);
            this.grdAsistencia.TabIndex = 3;
            // 
            // FrmTomaAsistencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(74)))));
            this.ClientSize = new System.Drawing.Size(650, 581);
            this.Controls.Add(this.grdAsistencia);
            this.Controls.Add(this.panelInferior);
            this.Controls.Add(this.panelFecha);
            this.Controls.Add(this.panelCabecera);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "FrmTomaAsistencia";
            this.Text = "Toma de Asistencia";
            this.Load += new System.EventHandler(this.FrmTomaAsistencia_Load);
            this.panelCabecera.ResumeLayout(false);
            this.panelCabecera.PerformLayout();
            this.panelFecha.ResumeLayout(false);
            this.panelFecha.PerformLayout();
            this.panelInferior.ResumeLayout(false);
            this.panelInferior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAsistencia)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelCabecera;
        private System.Windows.Forms.Label lblTitulo;
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
