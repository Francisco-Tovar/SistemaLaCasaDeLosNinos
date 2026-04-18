namespace CasaDeLosNinos.Interfaz.Formularios
{
    partial class FrmObservaciones
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
            panelEncabezado = new Panel();
            btnClose = new FontAwesome.Sharp.IconButton();
            lblTitulo = new Label();
            panelCaptura = new Panel();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            lblContador = new Label();
            txtNuevaObservacion = new TextBox();
            lblAutorInfo = new Label();
            lblNueva = new Label();
            lblHistorial = new Label();
            panelHistorial = new Panel();
            panelEncabezado.SuspendLayout();
            panelCaptura.SuspendLayout();
            SuspendLayout();
            // 
            // panelEncabezado
            // 
            panelEncabezado.Controls.Add(btnClose);
            panelEncabezado.Controls.Add(lblTitulo);
            panelEncabezado.Dock = DockStyle.Top;
            panelEncabezado.Location = new Point(1, 1);
            panelEncabezado.Name = "panelEncabezado";
            panelEncabezado.Padding = new Padding(12, 8, 12, 8);
            panelEncabezado.Size = new Size(702, 54);
            panelEncabezado.TabIndex = 0;
            panelEncabezado.MouseDown += panelEncabezado_MouseDown;
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
            btnClose.Location = new Point(672, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(30, 30);
            btnClose.TabIndex = 1;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += AlHacerClickEnCerrar;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitulo.Location = new Point(12, 14);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(267, 21);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "📋  Bitácora de Observaciones — ";
            // 
            // panelCaptura
            // 
            panelCaptura.Controls.Add(btnGuardar);
            panelCaptura.Controls.Add(lblContador);
            panelCaptura.Controls.Add(txtNuevaObservacion);
            panelCaptura.Controls.Add(lblAutorInfo);
            panelCaptura.Controls.Add(lblNueva);
            panelCaptura.Dock = DockStyle.Bottom;
            panelCaptura.Location = new Point(1, 390);
            panelCaptura.Name = "panelCaptura";
            panelCaptura.Padding = new Padding(12, 8, 12, 10);
            panelCaptura.Size = new Size(702, 190);
            panelCaptura.TabIndex = 1;
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnGuardar.IconColor = Color.Black;
            btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardar.IconSize = 24;
            btnGuardar.Location = new Point(518, 140);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(170, 40);
            btnGuardar.TabIndex = 4;
            btnGuardar.Text = "Guardar";
            btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += AlGuardarObservacion;
            // 
            // lblContador
            // 
            lblContador.AutoSize = true;
            lblContador.Font = new Font("Segoe UI", 8.5F);
            lblContador.Location = new Point(12, 140);
            lblContador.Name = "lblContador";
            lblContador.Size = new Size(48, 15);
            lblContador.TabIndex = 3;
            lblContador.Text = "0 / 2000";
            // 
            // txtNuevaObservacion
            // 
            txtNuevaObservacion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtNuevaObservacion.BorderStyle = BorderStyle.None;
            txtNuevaObservacion.Font = new Font("Segoe UI", 10F);
            txtNuevaObservacion.Location = new Point(12, 65);
            txtNuevaObservacion.MaxLength = 2000;
            txtNuevaObservacion.Multiline = true;
            txtNuevaObservacion.Name = "txtNuevaObservacion";
            txtNuevaObservacion.ScrollBars = ScrollBars.Vertical;
            txtNuevaObservacion.Size = new Size(678, 60);
            txtNuevaObservacion.TabIndex = 2;
            txtNuevaObservacion.TextChanged += AlCambiarTexto;
            // 
            // lblAutorInfo
            // 
            lblAutorInfo.AutoSize = true;
            lblAutorInfo.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblAutorInfo.Location = new Point(12, 40);
            lblAutorInfo.Name = "lblAutorInfo";
            lblAutorInfo.Size = new Size(0, 15);
            lblAutorInfo.TabIndex = 1;
            // 
            // lblNueva
            // 
            lblNueva.AutoSize = true;
            lblNueva.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNueva.Location = new Point(12, 10);
            lblNueva.Name = "lblNueva";
            lblNueva.Size = new Size(145, 19);
            lblNueva.TabIndex = 0;
            lblNueva.Text = "Nueva Observación:";
            // 
            // lblHistorial
            // 
            lblHistorial.Dock = DockStyle.Top;
            lblHistorial.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHistorial.Location = new Point(1, 55);
            lblHistorial.Name = "lblHistorial";
            lblHistorial.Padding = new Padding(12, 10, 0, 0);
            lblHistorial.Size = new Size(702, 40);
            lblHistorial.TabIndex = 2;
            lblHistorial.Text = "Historial:";
            // 
            // panelHistorial
            // 
            panelHistorial.AutoScroll = true;
            panelHistorial.Dock = DockStyle.Fill;
            panelHistorial.Location = new Point(1, 95);
            panelHistorial.Name = "panelHistorial";
            panelHistorial.Padding = new Padding(15, 10, 15, 10);
            panelHistorial.Size = new Size(702, 295);
            panelHistorial.TabIndex = 3;
            // 
            // FrmObservaciones
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(704, 581);
            Controls.Add(panelHistorial);
            Controls.Add(lblHistorial);
            Controls.Add(panelCaptura);
            Controls.Add(panelEncabezado);
            Font = new Font("Segoe UI", 9.5F);
            Name = "FrmObservaciones";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Bitácora";
            Load += FrmObservaciones_Load;
            panelEncabezado.ResumeLayout(false);
            panelEncabezado.PerformLayout();
            panelCaptura.ResumeLayout(false);
            panelCaptura.PerformLayout();
            ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelCaptura;
        private System.Windows.Forms.Label lblNueva;
        private System.Windows.Forms.Label lblAutorInfo;
        private System.Windows.Forms.TextBox txtNuevaObservacion;
        private System.Windows.Forms.Label lblContador;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private System.Windows.Forms.Label lblHistorial;
        private System.Windows.Forms.Panel panelHistorial;
        private FontAwesome.Sharp.IconButton btnClose;
    }
}
