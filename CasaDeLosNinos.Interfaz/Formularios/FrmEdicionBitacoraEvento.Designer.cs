namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FrmEdicionBitacoraEvento
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
            pnlCabecera = new Panel();
            btnClose = new FontAwesome.Sharp.IconButton();
            lblTituloForm = new Label();
            lblFecha = new Label();
            dtpFecha = new DateTimePicker();
            lblEtiquetaAsunto = new Label();
            txtTitulo = new TextBox();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            grpFotos = new GroupBox();
            flpFotos = new FlowLayoutPanel();
            btnSubirFoto = new FontAwesome.Sharp.IconButton();
            btnTomarFoto = new FontAwesome.Sharp.IconButton();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            pnlCabecera.SuspendLayout();
            grpFotos.SuspendLayout();
            SuspendLayout();
            // 
            // pnlCabecera
            // 
            pnlCabecera.Controls.Add(btnClose);
            pnlCabecera.Controls.Add(lblTituloForm);
            pnlCabecera.Dock = DockStyle.Top;
            pnlCabecera.Location = new Point(1, 1);
            pnlCabecera.MaximumSize = new Size(798, 60);
            pnlCabecera.MinimumSize = new Size(798, 60);
            pnlCabecera.Name = "pnlCabecera";
            pnlCabecera.Size = new Size(798, 60);
            pnlCabecera.TabIndex = 0;
            pnlCabecera.MouseDown += pnlCabecera_MouseDown;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.IconChar = FontAwesome.Sharp.IconChar.Close;
            btnClose.IconColor = Color.Black;
            btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnClose.IconSize = 20;
            btnClose.Location = new Point(758, 5);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(35, 35);
            btnClose.TabIndex = 0;
            btnClose.Click += AlHacerClickEnCancelar;
            // 
            // lblTituloForm
            // 
            lblTituloForm.AutoSize = true;
            lblTituloForm.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold);
            lblTituloForm.Location = new Point(17, 5);
            lblTituloForm.Name = "lblTituloForm";
            lblTituloForm.Size = new Size(93, 29);
            lblTituloForm.TabIndex = 1;
            lblTituloForm.Text = "Evento";
            lblTituloForm.MouseDown += pnlCabecera_MouseDown;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(30, 73);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(41, 15);
            lblFecha.TabIndex = 5;
            lblFecha.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(77, 67);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(150, 23);
            dtpFecha.TabIndex = 4;
            // 
            // lblEtiquetaAsunto
            // 
            lblEtiquetaAsunto.AutoSize = true;
            lblEtiquetaAsunto.Location = new Point(30, 100);
            lblEtiquetaAsunto.Name = "lblEtiquetaAsunto";
            lblEtiquetaAsunto.Size = new Size(41, 15);
            lblEtiquetaAsunto.TabIndex = 9;
            lblEtiquetaAsunto.Text = "Título:";
            // 
            // txtTitulo
            // 
            txtTitulo.Location = new Point(77, 96);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.Size = new Size(308, 23);
            txtTitulo.TabIndex = 8;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(30, 122);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(72, 15);
            lblDescripcion.TabIndex = 11;
            lblDescripcion.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(30, 140);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.ScrollBars = ScrollBars.Vertical;
            txtDescripcion.Size = new Size(374, 287);
            txtDescripcion.TabIndex = 10;
            // 
            // grpFotos
            // 
            grpFotos.Controls.Add(flpFotos);
            grpFotos.Controls.Add(btnSubirFoto);
            grpFotos.Controls.Add(btnTomarFoto);
            grpFotos.Location = new Point(410, 67);
            grpFotos.Name = "grpFotos";
            grpFotos.Size = new Size(360, 360);
            grpFotos.TabIndex = 1;
            grpFotos.TabStop = false;
            grpFotos.Text = "Fotografías del Evento";
            // 
            // flpFotos
            // 
            flpFotos.AutoScroll = true;
            flpFotos.BorderStyle = BorderStyle.FixedSingle;
            flpFotos.Location = new Point(15, 30);
            flpFotos.Name = "flpFotos";
            flpFotos.Size = new Size(330, 260);
            flpFotos.TabIndex = 3;
            // 
            // btnSubirFoto
            // 
            btnSubirFoto.IconChar = FontAwesome.Sharp.IconChar.Upload;
            btnSubirFoto.IconColor = Color.Black;
            btnSubirFoto.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSubirFoto.IconSize = 20;
            btnSubirFoto.Location = new Point(225, 305);
            btnSubirFoto.Name = "btnSubirFoto";
            btnSubirFoto.Size = new Size(120, 40);
            btnSubirFoto.TabIndex = 1;
            btnSubirFoto.Text = "Subir Foto";
            btnSubirFoto.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSubirFoto.UseVisualStyleBackColor = true;
            btnSubirFoto.Click += AlHacerClickEnSubirFoto;
            // 
            // btnTomarFoto
            // 
            btnTomarFoto.IconChar = FontAwesome.Sharp.IconChar.CameraAlt;
            btnTomarFoto.IconColor = Color.Black;
            btnTomarFoto.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnTomarFoto.IconSize = 20;
            btnTomarFoto.Location = new Point(99, 305);
            btnTomarFoto.Name = "btnTomarFoto";
            btnTomarFoto.Size = new Size(120, 40);
            btnTomarFoto.TabIndex = 2;
            btnTomarFoto.Text = "Cámara";
            btnTomarFoto.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnTomarFoto.UseVisualStyleBackColor = true;
            btnTomarFoto.Click += AlHacerClickEnTomarFoto;
            // 
            // btnGuardar
            // 
            btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnGuardar.IconColor = Color.Black;
            btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardar.IconSize = 24;
            btnGuardar.Location = new Point(469, 433);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(150, 45);
            btnGuardar.TabIndex = 3;
            btnGuardar.Text = "Guardar Evento";
            btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += AlHacerClickEnGuardar;
            // 
            // btnCancelar
            // 
            btnCancelar.IconChar = FontAwesome.Sharp.IconChar.XmarkCircle;
            btnCancelar.IconColor = Color.Black;
            btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelar.IconSize = 24;
            btnCancelar.Location = new Point(625, 433);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(130, 45);
            btnCancelar.TabIndex = 2;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += AlHacerClickEnCancelar;
            // 
            // FrmEdicionBitacoraEvento
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 491);
            Controls.Add(grpFotos);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(txtTitulo);
            Controls.Add(lblEtiquetaAsunto);
            Controls.Add(txtDescripcion);
            Controls.Add(lblDescripcion);
            Controls.Add(dtpFecha);
            Controls.Add(lblFecha);
            Controls.Add(pnlCabecera);
            Name = "FrmEdicionBitacoraEvento";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edición de Evento";
            Load += FrmEdicionBitacoraEvento_Load;
            pnlCabecera.ResumeLayout(false);
            pnlCabecera.PerformLayout();
            grpFotos.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Panel pnlCabecera;
        private System.Windows.Forms.Label lblTituloForm;
        private FontAwesome.Sharp.IconButton btnClose;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblEtiquetaAsunto;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.GroupBox grpFotos;
        private System.Windows.Forms.FlowLayoutPanel flpFotos;
        private FontAwesome.Sharp.IconButton btnSubirFoto;
        private FontAwesome.Sharp.IconButton btnTomarFoto;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private FontAwesome.Sharp.IconButton btnCancelar;
    }
}
